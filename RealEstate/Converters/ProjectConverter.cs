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
    public class PaymentProjectConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(PaymentProjectConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedValue = string.Empty;

            try
            {
                returnedValue = GetProject(value as Payment)?.Name;
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

        private Project GetProject(Payment payment)
        {
            return payment.CustomerInProject != null ?
                 payment.CustomerInProject.Project : payment.SupplierInProject != null ?
                 payment.SupplierInProject.Project : payment.Project != null ?
                 payment.Project : null;
        }
    }

    public class DebtProjectConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(DebtProjectConverter));

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedValue = string.Empty;
            try
            {
                returnedValue = GetProject(value as Debt)?.Name;
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

        private Project GetProject(Debt debt)
        {
            return debt.CustomerInProject != null ?
                 debt.CustomerInProject.Project : debt.SupplierInProject != null ?
                 debt.SupplierInProject.Project : debt.Project != null ?
                 debt.Project : null;
        }
    }
}
