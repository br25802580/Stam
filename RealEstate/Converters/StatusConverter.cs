using log4net;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace RealEstate
{
    public class StatusConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(StatusConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush returnedValue  = Brushes.Transparent;
            try
            {
                if (value is int)
                {
                    switch ((int)value)
                    {
                        //case 1:
                        //    returnedValue = (SolidColorBrush)(new BrushConverter().ConvertFrom("#eeeeee"));
                        //    break;
                        case 2:
                            returnedValue = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDEDEDE"));
                            break;
                        case 3:
                            returnedValue = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFB8D"));
                            break;
                        case 4:
                            returnedValue = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFB1E2C1"));
                            break;
                        default:
                            break;
                    }
                }

                return returnedValue;
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
            return returnedValue;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
