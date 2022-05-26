using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LandsiteMobile.Converters
{
    public class DatetimeConverter : IValueConverter
    {
        public string DatetimeToString(DateTime? dt)
        {
            return dt == null ? "N/A" : ((DateTime)dt).ToString("dd/MM/yyyy");
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime myDatetime = (DateTime)value;
            return DatetimeToString(myDatetime);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            DateTime resultDateTime;
            return DateTime.TryParse(strValue, out resultDateTime) ? resultDateTime : value;
        }
    }
}
