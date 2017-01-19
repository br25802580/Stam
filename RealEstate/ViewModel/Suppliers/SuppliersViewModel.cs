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
    public class SuppliersViewModel
        : PageViewModel
    {
        #region Ctor

        public SuppliersViewModel() : base()
        {
            PageType = PageType.Suppliers;
            PageUriString = "/Views/Suppliers/SuppliersPage.xaml";
        }

        #endregion

        public override void InitLinks()
        {
            Links = new LinkCollection();

            RealEstateRepository.Instance.AddEditor(EditorType.AllSuppliers);
            RealEstateRepository.Instance.SelectEditor(EditorType.AllSuppliers);

        }
    }
}
