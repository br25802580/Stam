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
    public class PaymentsConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(PaymentsConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is Project)
                    return PaymentsUtils.GetPaymentsAmount(value as Project);
                else if (value is Flat)
                    return PaymentsUtils.GetPaymentsAmount(value as Flat);
                else if (value is Customer)
                    return PaymentsUtils.GetPaymentsAmount(((Customer)value).CustomerInProjects.SelectMany(cInP => cInP.Payments));
                else if (value is Supplier)
                    return PaymentsUtils.GetPaymentsAmount(((Supplier)value).SupplierInProjects.SelectMany(sInP => sInP.Payments));
                else if (value is CustomerInProject)
                    return PaymentsUtils.GetPaymentsAmount(value as CustomerInProject);
            }
            catch (Exception ex)
            {
                log.Error("Convertion Error", ex);
            }
            return null;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class RevenuesConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(RevenuesConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is Project)
                    return PaymentsUtils.GetPaymentsRevenues(value as Project);
                else if (value is Flat)
                    return PaymentsUtils.GetPaymentsRevenues(value as Flat);

            }
            catch (Exception ex)
            {
                log.Error("Convertion Error", ex);
            }

            return null;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class ExpensesConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(ExpensesConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is Project)
                    return PaymentsUtils.GetPaymentsExpenses(value as Project);
                else if (value is Flat)
                    return PaymentsUtils.GetPaymentsExpenses(value as Flat);
            }
            catch (Exception ex)
            {
                log.Error("Convertion Error", ex);
            }

            return null;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class DebtsConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(DebtsConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is Project)
                    return PaymentsUtils.GetDebtsAmount(value as Project);
                else if (value is Flat)
                    return PaymentsUtils.GetDebtsAmount(value as Flat);
                else if (value is Customer)
                    return PaymentsUtils.GetDebtsAmount(((Customer)value).CustomerInProjects.SelectMany(cInP => cInP.Debts));
                else if (value is Supplier)
                    return PaymentsUtils.GetDebtsAmount(((Supplier)value).SupplierInProjects.SelectMany(sInP => sInP.Debts));
                else if (value is CustomerInProject)
                    return PaymentsUtils.GetDebtsAmount(value as CustomerInProject);
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }

            return null;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
