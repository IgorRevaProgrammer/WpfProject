using MVVMTest.Classes;
using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.ViewModels
{
    class ShowInfoRequestViewModel:BaseViewModel
    {
        #region свойства
        private TABLE_REQUEST request;
        public TABLE_REQUEST Request
        {
            get
            {
                return request;
            }
            set
            {
                request = value;
                OnPropertyChanged("Request");
            }
        }
        #endregion

        #region комманды
        private RelayCommand comeBackToRequests;
        public RelayCommand ComeBackToRequests
        {
            get
            {
                return comeBackToRequests ??
                    (comeBackToRequests = new RelayCommand(o => { comebackfunc(); }));
            }
        }
        #endregion


        /// <summary>
        /// возврат на прошлую страницу
        /// </summary>
        private void comebackfunc()
        {
            Base.CurrentPage = new ShowRequests();
        }


        /// <summary>
        /// конструктор
        /// </summary>
        public ShowInfoRequestViewModel ()
        {
            Request = ShareData.curRequest;
        }
    }
}
