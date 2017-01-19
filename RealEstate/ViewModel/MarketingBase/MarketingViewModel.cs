using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using RealEstate.Data;
using RealEstate.BL;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RealEstate
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class MarketingViewModel
        : PageViewModel
    {
        #region Ctor

        public MarketingViewModel() : base()
        {
        }

        #endregion
    }

    public class ModernUri : Uri
    {
        public ModernUri(string uriString, UriKind uriKind) : base(uriString, uriKind)
        {

        }

        public string EditorKey { get; set; }
    }
}
