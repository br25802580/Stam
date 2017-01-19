using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RealEstate
{
    public sealed class OppositveBoolToVisibilityConverters  : BooleanConverter<Visibility>
    {
        public OppositveBoolToVisibilityConverters() :
            base(Visibility.Collapsed, Visibility.Visible, Visibility.Collapsed)
        { }
    }
}
