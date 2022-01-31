using System;
using System.Globalization;
using System.Windows.Data;

namespace MVVMTest.Classes
{
    public class LoadImageVisibilityConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
                return "Collapsed";
            else if ((bool)value == false)
                return "Visible";
            return "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
