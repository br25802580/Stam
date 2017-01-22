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
    public class PaymentRelationTypeConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(PaymentRelationTypeConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedValue = string.Empty;
            try
            {
                PaymentType paymentType = value as PaymentType;

                if (paymentType?.PaymentRelation != null)
                    returnedValue = paymentType.PaymentRelation.FromSenderTypeId == 4 ? "הוצאה" : "הכנסה";
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

    public class PaymentRelationSenderConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(PaymentRelationSenderConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedValue = string.Empty;
            try
            {
                PaymentType paymentType = value as PaymentType;

                if (paymentType.PaymentRelation != null)
                    returnedValue = paymentType.PaymentRelation.FromSenderTypeId == 4 ? paymentType.PaymentRelation.ToSenderType.Name : paymentType.PaymentRelation.FromSenderType.Name;
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
