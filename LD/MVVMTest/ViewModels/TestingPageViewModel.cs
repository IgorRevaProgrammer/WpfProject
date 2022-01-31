using MVVMTest.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace MVVMTest
{
    class TestingPageViewModel : BaseViewModel
    {

        public ObservableCollection<TABLE_REQUEST> items { get; set; }

        public TestingPageViewModel()
        {
            items = new ObservableCollection<TABLE_REQUEST>();

            LoadData();
        }

        private void LoadData()
        {
            using (dbContext context = new dbContext(Settings.connectSqlString))
            {
                var items = context.TABLE_REQUEST.ToList();

                foreach (var item in items)
                {
                    this.items.Add(item);
                }

            }

        }





    }
}
