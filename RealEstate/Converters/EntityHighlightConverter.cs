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
    public abstract class EntityHighlightConverter<T> : IValueConverter where T : class
    {
        ILog log = LogManager.GetLogger(typeof(EntityHighlightConverter<T>));
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool returnedValue = false;
            T entity = value as T;
            TableViewModel = parameter as TableViewModel;

            try
            {
                if (entity != null)
                {
                    returnedValue = IsHighlight(entity);
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }

            //  return customer != null ? $"{customer.Name} {customer.Family}" : string.Empty;
            return returnedValue;
        }

        protected TableViewModel TableViewModel { get; set; }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        protected abstract bool IsHighlight(T entity);
    }

    public class DebtHighlightConverter : EntityHighlightConverter<Debt>
    {
        protected override bool IsHighlight(Debt obj)
        {
            return obj.PaymentRelation?.FromSenderTypeId == 4;
        }
    }

    public class PaymentHighlightConverter : EntityHighlightConverter<Payment>
    {
        protected override bool IsHighlight(Payment obj)
        {
            return obj.PaymentRelation?.FromSenderTypeId == 4;
        }
    }

    public class ContractHighlightConverter : EntityHighlightConverter<CustomerInProject>
    {
        protected override bool IsHighlight(CustomerInProject obj)
        {
            return obj.IsActive;
        }
    }

    public class EntityLockedHighlightConverter : EntityHighlightConverter<object>
    {
        protected override bool IsHighlight(object obj)
        {
            if(TableViewModel?.ReadOnlyItems?.Count>0)
            {
                return TableViewModel.ReadOnlyItems.Contains(obj);
            }

            return false;
        }
    }
}
