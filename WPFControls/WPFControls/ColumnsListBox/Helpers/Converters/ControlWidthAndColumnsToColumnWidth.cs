using System;
using System.Globalization;
using System.Windows.Data;

namespace WPFControls.ColumnsListBox.Helpers.Converters
{
    public class ControlWidthAndColumnsToColumnWidth : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var controlWidth = (double)values[0];
            var numberOfColumns = (int)values[1];
            return controlWidth / numberOfColumns;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
