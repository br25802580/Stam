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
    public class PaymentTypeConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(PaymentTypeConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedVale = string.Empty;
            try
            {
                PaymentRelation paymentRelation = value as PaymentRelation;

                if (paymentRelation != null)
                {
                    if (paymentRelation.FromSenderTypeId == 4)
                    {
                        returnedVale = "הוצאה";
                    }
                    else
                    {
                        returnedVale = "הכנסה";
                    }
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
            return returnedVale;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
