using log4net;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RealEstate
{
    public class FlatPriceForMeterConverter : IMultiValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(FlatPriceForMeterConverter));

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 0 || !(values[0] is int?) || !(values[1] is int?))
                return null;

            int? price = (int?)values[0];
            int? squareMeter = (int?)values[1];

            if (price.HasValue && squareMeter.HasValue)
                return (float)price.Value / (float)squareMeter.Value;

            return null;
        }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int returnedValue = 0;
            try
            {
                Flat flat = value as Flat;
                returnedValue = flat?.Price != null && flat.SquareMeter.HasValue ? flat.Price.Value / flat.SquareMeter.Value : 0;
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
            return returnedValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
