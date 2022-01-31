using System.Windows.Controls;

namespace MVVMTest
{
    public partial class TestingPage : Page
    {
        public TestingPage()
        {
            InitializeComponent();
            DataContext = new TestingPageViewModel();
        }
        
    }
}
