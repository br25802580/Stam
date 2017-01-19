using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RealEstate
{
    public class BooleanConverter<T> : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(BooleanConverter<T>));
        public BooleanConverter(T trueValue, T falseValue, T nullValue)
        {
            True = trueValue;
            False = falseValue;
            NullValue = nullValue;
        }

        public T True { get; set; }
        public T False { get; set; }
        public T NullValue { get; set; }

        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is bool)
                    return ((bool)value) ? True : False;

                if (value is bool?)
                    if (value == null)
                        return NullValue;
                    else
                        return ((bool)value) ? True : False;
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }

            return NullValue;
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
        }
    }

    public sealed class YesNoConverter : BooleanConverter<string>
    {
        public YesNoConverter() :
            base("כן", "לא", "לא")
        { }
    }
}
