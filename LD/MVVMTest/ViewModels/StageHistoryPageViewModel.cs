using MVVMTest.Classes;
using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.ViewModels
{
    class StageHistoryPageViewModel:BaseViewModel
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
        private RelayCommand backToRequests;
        public RelayCommand BackToRequests
        {
            get
            {
                return backToRequests ??
                    (backToRequests = new RelayCommand(o => { backfunc(); }));
            }
        }
        #endregion
        public StageHistoryPageViewModel()
        {
            Request = ShareData.curRequest;
        }

        private void backfunc()
        {
            Base.CurrentPage = new ShowRequests();
        }
    }
}
