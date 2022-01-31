using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using System.Collections.Specialized;
using MVVMTest.Pages;
using MVVMTest.Classes;

namespace MVVMTest
{
    class ShowRequestsViewModel : BaseViewModel
    {
        #region properties
        private bool isVisible = true;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        private bool isUp;
        public bool IsUp
        {
            get { return isUp; }
            set
            {
                isUp = value;
                OnPropertyChanged("IsUp");
            }
        }
        private TABLE_REQUEST selectedRow;
        public TABLE_REQUEST SelectedRow
        {
            get { return selectedRow; }
            set
            {
                selectedRow = value;
                OnPropertyChanged("SelectedRow");
            }
        }
        private dbContext context { get; set; }

        private ObservableCollection<TABLE_REQUEST> requests_collection;
        public ObservableCollection<TABLE_REQUEST> Requests_collection
        {
            get { return requests_collection; }
            set
            {
                requests_collection = value;
                OnPropertyChanged("Requests_collection");
            }
        }

        private string toolTipForIconUpDown;
        public string ToolTipForIconUpDown
        {
            get { return toolTipForIconUpDown; }
            set
            {
                toolTipForIconUpDown = value;
                OnPropertyChanged("ToolTipForIconUpDown");
            }
        }
        #endregion

        #region комманды
        // Кнопка "обновить таблицу"
        private RelayCommand updateDBContent;
        public RelayCommand UpdateDBContent
        {
            get
            {
                return updateDBContent ??
                    (updateDBContent = new RelayCommand(o => { LoadData(); }));
            }
        }
        
        // Кнопка "взять заявку"
        private RelayCommand takeRequest;
        public RelayCommand TakeRequest
        {
            get
            {
                return takeRequest ??
                    (takeRequest = new RelayCommand(o => { TakeRequestFunc(); }));
            }
        }

        //Кнопка "информация по заявке"
        private RelayCommand infoRequest;
        public RelayCommand InfoRequest
        {
            get
            {
                return infoRequest ??
                    (infoRequest = new RelayCommand(o => { InfoRequestFunc(); }));
            }
        }

        //Кнопка "следующая стадия"
        private RelayCommand nextStage;
        public RelayCommand NextStage
        {
            get
            {
                return nextStage ??
                    (nextStage = new RelayCommand(o => { NextStageFunc(); }));
            }
        }

        //кнопка "история заявки"
        private RelayCommand stageHistory;
        public RelayCommand StageHistory
        {
            get
            {
                return stageHistory ??
                    (stageHistory = new RelayCommand(o => { StageHistoryFunc(); }));
            }
        }

        //кнопка "Сменить пользователя"
        private RelayCommand changeUser;
        public RelayCommand ChangeUser
        {
            get
            {
                return changeUser ??
                    (changeUser = new RelayCommand(o => { ChangeUserFunc(); }));
            }
        }

        //кнопка "Свернуть или раскрыть панель"
        private RelayCommand upDown;
        public RelayCommand UpDown
        {
            get
            {
                return upDown ??
                    (upDown = new RelayCommand(o => { UpDownFunc(); }));
            }
        }
        #endregion

        #region Конструктор
        public ShowRequestsViewModel()
        {
            try
            { 
                Requests_collection = new ObservableCollection<TABLE_REQUEST>();
                context = new dbContext(Settings.connectSqlString);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            LoadData();
            ToolTipForIconUpDown = "Развернуть панель";
        }
        #endregion

        /// <summary>
        /// загрузка данных
        /// </summary>
        private async void LoadData()
        {
            try
            {
                if (Requests_collection.Count > 0)
                    Requests_collection.Clear();
                
                IsVisible = false; // Флаг регулирующий отображение Таблицы БД и иконки загрузки

                //Получение списка пользователей
                var requests = await GetRequestAsync();
                Settings.statuses = await GetStatuses();

                //Добавление пользователей в коллекцию
                foreach (var r in requests)
                    Requests_collection.Add(r);

                // Искуственная задержка: 1 секунда
                IsVisible = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Ошибка", MessageBoxButton.OK);
            }
        }


        /// <summary>
        /// Получение списка заявок ассинхронно
        /// </summary>
        /// <returns> Возвращает список заявок </returns>
        private async Task<List<TABLE_REQUEST>> GetRequestAsync()
        {
            return await context.TABLE_REQUEST
                .Include(r => r.DICT_DIVISIONS)
                .Include(r => r.DICT_SERVICES)
                .Include(r => r.DICT_STAGES)
                .Include(r => r.DICT_STATUSES)
                .Include(r => r.DICT_USERS)
                .Include(r => r.DICT_EXECUTORS)
                .Include("TABLE_REQUEST_NOMENCL.DICT_NOMENCLATURE")
                .Include("TABLE_REQUEST_NOMENCL.DICT_NOMENCLATURE.GLOBAL_DK")
                .Include(r => r.TABLE_STAGE_HISTORY)
                .OrderBy(r => r.id_request)
                .Where(r => r.id_stage != 4 && r.id_status != 6)
                .ToListAsync();
        }


        /// <summary>
        /// Для выбранной заявки устанавливает исполнителем текущего пользователя
        /// </summary>
        private void TakeRequestFunc()
        {
            string message = "Взять заявку на исполнение?";
            string caption = "Подтверждение действия";
            
            if (SelectedRow != null)
            {
                // Проверить является ли текущий пользователь исполнителем
                if (!Settings.id_executor.HasValue) 
                {
                    MessageBox.Show("Вы не являетесь исполнителем");
                }
                else
                {
                    if (SelectedRow.id_exec.HasValue) 
                    {
                        MessageBox.Show("Данная заявка уже взята на исполнение");
                    }
                    else
                    {
                        var res = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (res == MessageBoxResult.Yes)
                        {
                            SelectedRow.id_exec = Settings.id_executor;
                            foreach(DICT_STATUSES s in Settings.statuses)
                            {
                                if (s.status_name=="В работе")
                                {
                                    SelectedRow.id_status = s.id_status;
                                }
                            }
                            SelectedRow.updated_at = DateTime.Now;
                            TABLE_STAGE_HISTORY stHis=new TABLE_STAGE_HISTORY();
                            stHis.id_request = SelectedRow.id_request;
                            stHis.id_stage = SelectedRow.id_stage;
                            stHis.id_status = SelectedRow.id_status;
                            stHis.updated_at = DateTime.Now;
                            context.TABLE_STAGE_HISTORY.Add(stHis);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
        private void InfoRequestFunc()
        {
            if (SelectedRow != null)
            {
                ShareData.curRequest = SelectedRow;
                Base.CurrentPage = new ShowInfoRequest();
            }
        }
        private void NextStageFunc()
        {
            string message = "Перейти на следующую стадию ?";
            string caption = "Подтверждение действия";
            if (SelectedRow != null)
            {
                if (SelectedRow.id_exec.HasValue)
                {
                    if (SelectedRow.DICT_STAGES.stage == "ОМТС")
                    {
                        var res = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (res == MessageBoxResult.Yes)
                        {
                            SelectedRow.id_stage = 2;
                            SelectedRow.updated_at = DateTime.Now;
                            TABLE_STAGE_HISTORY stHis = new TABLE_STAGE_HISTORY();
                            stHis.id_request = SelectedRow.id_request;
                            stHis.id_stage = SelectedRow.id_stage;
                            stHis.id_status = SelectedRow.id_status;
                            stHis.updated_at = DateTime.Now;
                            context.TABLE_STAGE_HISTORY.Add(stHis);
                        }      
                    }
                    else if (SelectedRow.DICT_STAGES.stage == "Руководство")
                    {
                        var res = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (res == MessageBoxResult.Yes)
                        {
                            SelectedRow.id_stage = 3;
                            if (MessageBox.Show("Согласовать заявку?", "Согласование", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                SelectedRow.id_status = 2;
                            else SelectedRow.id_status = 3;
                            SelectedRow.updated_at = DateTime.Now;
                            TABLE_STAGE_HISTORY stHis = new TABLE_STAGE_HISTORY();
                            stHis.id_request = SelectedRow.id_request;
                            stHis.id_stage = SelectedRow.id_stage;
                            stHis.id_status = SelectedRow.id_status;
                            stHis.updated_at = DateTime.Now;
                            context.TABLE_STAGE_HISTORY.Add(stHis);
                        }
                    }
                    else
                        MessageBox.Show("Действие невозможно");

                    context.SaveChanges();
                }
                else MessageBox.Show("Не назначен исполнитель","Ошибка");
            }
        }
        private void StageHistoryFunc()
        {
            if (SelectedRow != null)
            {
                ShareData.curRequest = SelectedRow;
                Base.CurrentPage = new StageHistoryPage();
            }
        }
        private void ChangeUserFunc()
        {
            var res = MessageBox.Show("Хотите сменить пользователя?", "Подтверждение действия", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.Yes)
            {
                Base.CurrentPage = new AuthPage();
            }
        }
        private void UpDownFunc()
        {
            IsUp = !IsUp;
            if (IsUp)    
                ToolTipForIconUpDown = "Свернуть панель";
            else
                ToolTipForIconUpDown = "Развернуть панель";
        }
        private async Task<List<DICT_STATUSES>> GetStatuses()
        {
            return await context.DICT_STATUSES.ToListAsync();
        }
    }
}
