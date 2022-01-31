using System;
using System.Globalization;
using System.Windows.Data;

namespace MVVMTest.Classes
{
    public class TableVisibilityConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
                return "Visible";
            else if ((bool)value == false)
                return "Collapsed";
            return "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
