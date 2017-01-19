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
    public class CustomerOrSupplierByEntityConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(CustomerOrSupplierByEntityConverter));
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Payment payment = value as Payment;
            string returnedValue = string.Empty;

            try
            {
                if (payment?.PaymentRelation != null)
                {
                    if (payment.PaymentRelation.FromSenderTypeId != 4)
                    {
                        if (payment.PaymentRelation.FromSenderTypeId == 1)
                        {
                            if (payment.SupplierInProject?.Supplier != null)
                                returnedValue = $"{payment.SupplierInProject.Supplier.Name} {payment.SupplierInProject.Supplier.Family} (ספק)";
                        }
                        else
                        {
                            if (payment.CustomerInProject?.Customer != null)
                                returnedValue = $"{payment.CustomerInProject.Customer.Name} {payment.CustomerInProject.Customer.Family} (לקוח)";
                        }
                    }
                    else if (payment.PaymentRelation.ToSenderTypeId != 4)
                    {
                        if (payment.PaymentRelation.ToSenderTypeId == 1)
                        {
                            if (payment.SupplierInProject?.Supplier != null)
                                returnedValue = $"{payment.SupplierInProject.Supplier.Name} {payment.SupplierInProject.Supplier.Family} (ספק)";
                        }
                        else
                        {
                            if (payment.CustomerInProject?.Customer != null)
                                returnedValue = $"{payment.CustomerInProject.Customer.Name} {payment.CustomerInProject.Customer.Family} (לקוח)";
                        }
                    }
                }
                else
                {
                    Debt debt = value as Debt;

                    if (debt?.PaymentRelation != null)
                    {
                        if (debt.PaymentRelation.ToSenderTypeId == 1)
                        {
                            if (debt.SupplierInProject?.Supplier != null)
                                returnedValue = $"{debt.SupplierInProject.Supplier.Name} {debt.SupplierInProject.Supplier.Family} (ספק)";
                        }
                        else
                        {
                            if (debt.CustomerInProject?.Customer != null)
                                returnedValue = $"{debt.CustomerInProject.Customer.Name} {debt.CustomerInProject.Customer.Family} (לקוח)";
                        }
                    }
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

    public class CustomerOrSupplierConverter : IMultiValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(CustomerOrSupplierConverter));
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedValue = string.Empty;

            try
            {
                if (values[0] is bool)
                {
                    bool isCustomerSender = (bool)values[0];
                    Customer customer = values[1] as Customer;
                    Supplier supplier = values[2] as Supplier;

                    if (isCustomerSender)
                    {
                        if (customer != null)
                            returnedValue = $"{customer.Name} {customer.Family}";
                    }
                    else
                    {
                        if (supplier != null)
                            returnedValue = $"{supplier.Name} {supplier.Family}";
                    }
                }
            }
            catch   (Exception ex)
            {
                log.HandleError(ex);
            }

            return returnedValue;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
