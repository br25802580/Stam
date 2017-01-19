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
    public class SalesViewModel
        : MarketingViewModel
    {
        #region Ctor

        public SalesViewModel() : base()
        {
            PageType = PageType.Sale;
            PageUriString = "/Views/Lease/SalesPage.xaml";
        }

        #endregion

        #region Fields

     //   private int EditorOrder = 0;

        #endregion

        #region Properties  


        #endregion Properties

        #region Methods

        public override void InitLinks()
        {
            Links = new LinkCollection();

            RealEstateRepository.Instance.AddEditor(EditorType.SaleAllProjects);
            RealEstateRepository.Instance.AddEditor(EditorType.SaleAllFlats);
            RealEstateRepository.Instance.SelectEditor(EditorType.SaleAllProjects);
        }

        #endregion Methods

        #region Event Handlers

        #endregion
    }
}
