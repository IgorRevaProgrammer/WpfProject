using MVVMTest.Model;

namespace MVVMTest
{
    class RequestNomenclViewModel : BaseViewModel
    {

        #region Свойства
        private TABLE_REQUEST _request;
        public TABLE_REQUEST Request
        {
            get { return _request; }
            set
            {
                _request = value;
                OnPropertyChanged("Request");
            }
        }
        #endregion

        #region Команды(кнопки)
        private RelayCommand _backButton;
        public RelayCommand BackButton
        {
            get
            {
                return _backButton ??
                    (_backButton = new RelayCommand(o => { Settings.OpenShowRequestPage(); }));
            }
        }
        #endregion

        #region Конструктор
        public RequestNomenclViewModel()
        {
            Request = ShareData.curRequest;
        }
        #endregion

    }
}
