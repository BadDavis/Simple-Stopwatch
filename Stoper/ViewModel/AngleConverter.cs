using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Stoper.ViewModel
{
    class AngleConverter : IValueConverter
    {
        double parsedValue;
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((value != null) && double.TryParse(value.ToString(), out parsedValue) && parameter != null)
            {
                switch (parameter.ToString())
                {
                    case "Hours":
                        return parsedValue * 30;
                    case "Minutes":
                    case "Seconds":
                        return parsedValue * 6;
                }
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
