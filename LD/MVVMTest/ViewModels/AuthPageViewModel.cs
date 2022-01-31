using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVVMTest
{
    public class AuthPageViewModel : BaseViewModel
    {
        private bool authorized,valid;

        #region Свойства
        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
                ErrMsg = "";
                authorized = false;
                valid = false;
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
                ErrMsg = "";
                authorized = false;
                valid = false;
            }
        }
        private string _errMsg = "";
        public string ErrMsg
        {
            get { return _errMsg; }
            set
            {
                _errMsg = value;
                OnPropertyChanged("ErrMsg");
            }
        }
        private bool _isVisible = true;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }
        private RelayCommand _authorization;
        public RelayCommand Authorization
        {
            get
            {
                return _authorization ??
                    (_authorization = new RelayCommand(o => { UserAuthorizationAsync(); }));
            }
        }
        private dbContext DBContext { get; set; }
        #endregion

        #region Конструктор
        public AuthPageViewModel()  {  }
        #endregion
        

        /// <summary>
        /// ассинхронная функция авторизации
        /// </summary>
        public void UserAuthorizationAsync()
        {
            IsVisible = false; // Скрыть таблицу и показать анимацию загрузки

            // Проверяем введен ли логин с паролем
            valid = CheckValidInput(Login, Password);

            IsVisible = true; // Показать таблицу и скрыть анимацию загрузки

            // Асинхронно читает данные о пользователе из БД, вкл/выкл анимацию загрузки, 
            // меняет страницу с авторизации на отображение заявок
            if (valid) AuthenticationAsync(Settings.connectSqlString, Login, Password);
        }


        /// <summary>
        /// проверка на правильность введённых данных
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public bool CheckValidInput(string login, string pass)
        {
            string ret = "Введите ";
            if (pass == "" || pass == null || pass.Contains(" "))
            {
                if (login == "" || login == null || login.Contains(" "))
                {
                    ret += "логин и пароль";
                    ErrMsg = ret;
                    OnPropertyChanged("ErrMsg");
                    return false;
                }
                ret += "пароль";
                ErrMsg = ret;
                OnPropertyChanged("ErrMsg");
                return false;
            }
            else if (login == "" || login == null || login.Contains(" "))
            {
                ret += "логин";
                ErrMsg = ret;
                OnPropertyChanged("ErrMsg");
                return false;
            }
            else {
                ErrMsg = "";
                OnPropertyChanged("ErrMsg");
                return true; 
            }
        }


        /// <summary>
        /// Подключение к БД и считывание данных о пользователе
        /// Далее идут проверки заблокирован ли пользователь и
        /// какой тип авторизации установлен для него
        /// Если тип авторизации ползователя LDAP, то начать авторизацию по LDAP
        /// иначе начать авторизацию по БД
        /// </summary>
        private int getUser(string connectionString, string login, string pass)
        {
            try
            {
                using (DBContext = new dbContext(connectionString))
                {
                    // Поиск данных пользователя в БД
                    var User = DBContext.DICT_USERS.Where(u => u.user_login == login).FirstOrDefault();
                    
                    if (User != null)
                    {
                        Settings.id_User = User.id_user;
                        Settings.FullName = User.user_full_name;
                        Settings.UserLogin = User.user_login;
                        Settings.TypeAuthLDAP = User.type_auth;
                        Settings.UserLock = User.user_lock;

                        // Поиск пользователя в списке исполнителей в БД,
                        // а после проверка является ли пользователь исполнителем
                        var Exec = DBContext.DICT_EXECUTORS.Where(w => w.id_user == Settings.id_User).FirstOrDefault();
                        if (Exec != null) Settings.id_executor = Exec.id_exec;

                        // Заблокирован ли пользователь
                        if (Settings.UserLock)
                        {
                            ErrMsg = "Пользователь заблокирован";
                            authorized = false;
                            return 0;
                        }
                        // Если тип авторизации ползователя LDAP, то начать авторизацию по LDAP
                        // иначе начать авторизацию по БД
                        if (Settings.TypeAuthLDAP) { AuthorizationLDAP(Settings.stringDomain, login, pass); return 0; }
                        else
                        {
                            if (User.user_password == pass)
                            { 
                                ErrMsg = "Авторизация успешна БД";
                                authorized = true;
                                return 0;  /*пароль правильный*/
                            }
                            else
                            { 
                                ErrMsg = "Неверный логин или пароль";
                                authorized = false;
                                return 0; /*пароль НЕправильный*/
                            }
                        }
                    }
                    else
                    { 
                        ErrMsg = "Неверный логин или пароль";
                        authorized = false;
                        return 0; /*Пользователь не найден в базе или другая ошибка*/
                    }
                }
            }
            catch
            {
                IsVisible = true; // Показать таблицу и скрыть анимацию загрузки
                ErrMsg = "Не удалось подключиться к базе данных";
                authorized = false;
                return 0;
            }
        }

        private async void AuthenticationAsync(string connectionString, string login, string pass)
        {
            IsVisible = false; // Скрыть таблицу и показать анимацию загрузки

            // Асинхронная авторизация пользователя
            await Task.Run(() => getUser(connectionString, login, pass));

            IsVisible = true; // Показать таблицу и скрыть анимацию загрузки

            // Если пользователь авторизован, то происходит отображение страницы с таблицей заявок
            if (authorized) Base.CurrentPage = new ShowRequests();
        }


        /// <summary>
        /// метод авторизации через ldap
        /// </summary>
        /// <param name="LDAPdomain"></param>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        private void AuthorizationLDAP(string LDAPdomain, string login, string pass)
        {
            try
            {
                using (DirectoryEntry de = new DirectoryEntry(LDAPdomain, login, pass))
                {
                    DirectorySearcher ds = new DirectorySearcher(de);
                    ds.Filter = "(sAMAccountName=" + login + ")";
                    SearchResult userinfo = ds.FindOne();
                    DirectoryEntry ent = userinfo.GetDirectoryEntry();

                    if (userinfo != null)
                    {
                        // Пользователь зарегистрирован в системе
                        ErrMsg = "Авторизация успешна LDAP";
                        authorized = true;
                        return;
                    }
                    else
                    {
                        // Пользователь НЕ зарегистрирован в системе
                        ErrMsg = "Неверный логин или пароль";
                        authorized = false;
                        return;
                    }

                }
            }
            catch
            {
                // Не удалось подключится к LDAP
                ErrMsg = "Не удалось подключиться к базе данных";
                authorized = false;
                return;
            }
        }
    }
}
