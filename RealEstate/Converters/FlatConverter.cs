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
    public class PaymentFlatConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(PaymentFlatConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Flat flat = null;
            try
            {
                flat = GetFlat(value as Payment);
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
            return flat?.FlatNumber;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private Flat GetFlat(Payment payment)
        {
            return payment.CustomerInProject != null ?
                payment.CustomerInProject.Flat : payment.SupplierInProject != null ?
                payment.SupplierInProject.Flat : payment.Flat != null ?
                  payment.Flat : null;
        }
    }

    public class DebtFlatConverter : IValueConverter
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetFlat(value as Debt)?.FlatNumber; ;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        private Flat GetFlat(Debt debt)
        {
            return debt.CustomerInProject != null ?
                 debt.CustomerInProject.Flat : debt.SupplierInProject != null ?
                 debt.SupplierInProject.Flat : null;
        }
    }
}
