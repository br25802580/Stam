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
    public class CustomerConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(CustomerConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedValue = string.Empty;
            try
            {
                Customer customer = value as Customer;
                returnedValue = customer != null ? $"{customer.Name} {customer.Family}" : string.Empty;
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
