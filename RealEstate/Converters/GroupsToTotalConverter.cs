using log4net;
using RealEstate.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Data;

namespace RealEstate
{
    public class PaymentsGroupsToTotalConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(PaymentsGroupsToTotalConverter));
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value is ReadOnlyObservableCollection<Object>)
                {
                    var items = (ReadOnlyObservableCollection<Object>)value;
                    Decimal total = 0;
                    foreach (Payment payment in items)
                    {
                        int amount = payment.Amount.HasValue ? payment.Amount.Value : 0;

                        if (payment.PaymentRelation.FromSenderTypeId == 4)
                            total -= amount;
                        else
                            total += amount;
                    }
                    return total.ToString();
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class GroupsToTotalConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(GroupsToTotalConverter));
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value is ReadOnlyObservableCollection<Object>)
                {
                    var items = (ReadOnlyObservableCollection<Object>)value;

                    int total = (int)ConvertItems(items);
                    return total;

                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }

            return "";
        }

        private int ConvertItems(IList items)
        {
            if (items != null && items.Count > 0)
            {
                int total = 0;

                if (items[0] is Debt)
                    foreach (Debt item in items)
                    {
                        int amount = item.DelinquentAmount.HasValue ? item.DelinquentAmount.Value : 0;

                        //    int amount = payment.Amount.HasValue ? payment.Amount.Value : 0;

                        if (item.PaymentRelation.FromSenderTypeId == 4)
                            total -= amount;
                        else
                            total += amount;
                        //    total += amount;
                    }
                else if (items[0] is Payment)
                    foreach (Payment item in items)
                    {
                        int amount = item.Amount.HasValue ? item.Amount.Value : 0;
                        if (item.PaymentRelation.FromSenderTypeId == 4)
                            total -= amount;
                        else
                            total += amount;
                        //total += amount;
                    }
                else if (items[0] is CollectionViewGroup)
                {
                    //CollectionViewGroup a;a.Items[0]
                    foreach (var item in items)
                    {
                        int amount = (int)ConvertItems(((CollectionViewGroup)item).Items);
                        //  int amount = item.Amount.HasValue ? item.Amount.Value : 0;
                        total += amount;
                    }
                }
                else
                    return 0;

                return total;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class GroupsToRevenuesConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(GroupsToRevenuesConverter));
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value is ReadOnlyObservableCollection<Object>)
                {
                    var items = (ReadOnlyObservableCollection<Object>)value;

                    int total = (int)ConvertItems(items);
                    return total;
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
            return "";
        }

        private int ConvertItems(IList items)
        {
            if (items != null && items.Count > 0)
            {
                int total = 0;

                if (items[0] is Debt)
                    foreach (Debt item in items)
                    {
                        int amount = item.Amount.HasValue ? item.Amount.Value : 0;
                        if (item.PaymentRelation.ToSenderTypeId == 4)
                            //    total -= amount;
                            // else
                            total += amount;
                        //    total += amount;
                    }
                else if (items[0] is Payment)
                    foreach (Payment item in items)
                    {
                        int amount = item.Amount.HasValue ? item.Amount.Value : 0;
                        if (item.PaymentRelation.ToSenderTypeId == 4)
                            // total -= amount;
                            //  else
                            total += amount;
                        //  if (amount > 0)
                        //    total += amount;
                    }
                else if (items[0] is CollectionViewGroup)
                {
                    //CollectionViewGroup a;a.Items[0]
                    foreach (var item in items)
                    {
                        int amount = (int)ConvertItems(((CollectionViewGroup)item).Items);
                        //  int amount = item.Amount.HasValue ? item.Amount.Value : 0;
                        if (amount > 0)
                            total += amount;
                    }
                }
                else
                    return 0;

                return total;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class GroupsToExpensesConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(GroupsToExpensesConverter));
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                if (value is ReadOnlyObservableCollection<Object>)
                {
                    var items = (ReadOnlyObservableCollection<Object>)value;

                    int total = (int)ConvertItems(items);
                    return total;
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }

            return "";
        }

        private int ConvertItems(IList items)
        {
            if (items != null && items.Count > 0)
            {
                int total = 0;

                if (items[0] is Debt)
                    foreach (Debt item in items)
                    {
                        int amount = item.Amount.HasValue ? item.Amount.Value : 0;
                        if (item.PaymentRelation.FromSenderTypeId == 4)
                            // total -= amount;
                            //  else
                            total += amount;
                        //total += amount;
                    }
                else if (items[0] is Payment)
                    foreach (Payment item in items)
                    {
                        int amount = item.Amount.HasValue ? item.Amount.Value : 0;
                        if (item.PaymentRelation.FromSenderTypeId == 4)
                            //  total -= amount;
                            //  else
                            //      total += amount;
                            //if (amount < 0)
                            total += amount;
                    }
                else if (items[0] is CollectionViewGroup)
                {
                    //CollectionViewGroup a;a.Items[0]
                    foreach (var item in items)
                    {
                        int amount = (int)ConvertItems(((CollectionViewGroup)item).Items);
                        //  int amount = item.Amount.HasValue ? item.Amount.Value : 0;
                        //   if (amount < 0)
                        total += amount;
                    }
                }
                else
                    return 0;

                return total;
            }
            return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

    public class GroupTotalHeaderConverter : IValueConverter
    {
        ILog log = LogManager.GetLogger(typeof(GroupTotalHeaderConverter));
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedValue = string.Empty;
            try
            {
                IList items = null;

                if (value is ReadOnlyObservableCollection<Object>)
                {
                    items = value as IList;
                }

                if (items?.Count > 0)
                {
                    var item = items[0];

                    if (item is CollectionViewGroup)
                    {
                        items = ((CollectionViewGroup)item).Items;
                        if (items?.Count > 0)
                            item = items[0];
                    }
                    if (item is Debt)
                    {
                        returnedValue = "יתרת חוב: ";
                    }
                    else if (item is Payment)
                    {
                        returnedValue = "תשלומים: ";
                    }
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }

            return returnedValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
