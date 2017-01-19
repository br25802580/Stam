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
    public class PaymentTypesConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(PaymentTypesConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedValue = null;
            try
            {
                Payment payment = value as Payment;

                if (payment?.PaymentItems != null)
                {
                    IList<string> paymentTypes = payment.PaymentItems.Where(paymentItem => paymentItem?.PaymentType != null).Select(paymentItem => paymentItem.PaymentType.Name).Distinct().ToList();
                    returnedValue = string.Join(",", paymentTypes);
                }
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
