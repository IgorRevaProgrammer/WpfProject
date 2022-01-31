using MVVMTest.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MVVMTest
{
    public partial class Test2 : Window
    {
        public ObservableCollection<TABLE_REQUEST> Items { get; set; }

        public Test2()
        {
            InitializeComponent();
            DataContext = this;

            Items = new ObservableCollection<TABLE_REQUEST>();

            LoadData();
        }

        private void LoadData()
        {
            using (dbContext context = new dbContext(Settings.connectSqlString))
            {
                var items = context.TABLE_REQUEST.ToList();

                foreach(var item in items)
                {
                    Items.Add(item);
                }
                
            }
                
        }
    }
}
