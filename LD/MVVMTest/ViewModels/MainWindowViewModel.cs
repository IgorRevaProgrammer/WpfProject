using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.DirectoryServices;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using MVVMTest.Model;
using System.Data.Entity;

namespace MVVMTest
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            BaseViewModel.Base.CurrentPage = new AuthPage();
        }
    }
}
