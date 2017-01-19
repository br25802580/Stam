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
    public abstract class EntityStateConverter<T> : IValueConverter where T : class
    {
        ILog log = LogManager.GetLogger(typeof(EntityStateConverter<T>));
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EntityState returnedValue = EntityState.Valid;
            T entity = value as T;

            try
            {
                if (entity != null)
                {
                    returnedValue = GetValidState(entity);
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }

            //  return customer != null ? $"{customer.Name} {customer.Family}" : string.Empty;
            return returnedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        protected abstract EntityState GetValidState(T entity);
    }

    public class DebtStateConverter : EntityStateConverter<Debt>
    {
        protected override EntityState GetValidState(Debt obj)
        {
            EntityState entityState = EntityState.Valid;
            if (obj?.DelinquentAmount > 0)
            {
                if (obj.DueDate < DateTime.Now)
                    entityState = EntityState.Problem;
                else if (obj.DueDate < DateTime.Now.AddDays(5))
                    entityState = EntityState.Note;
                else
                    entityState = EntityState.Marked;
            }
            return entityState;
        }
    }
}
