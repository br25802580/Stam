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
    public class PaymentsViewModel
        : PageViewModel
    {
        #region Ctor

        public PaymentsViewModel() : base()
        {
            PageType = PageType.Payments;
            PageUriString = "/Views/Payments/PaymentsPage.xaml";
        }

        #endregion

        public override void InitLinks()
        {
            Links = new LinkCollection();

            RealEstateRepository.Instance.AddEditor(EditorType.AllPayments);
            RealEstateRepository.Instance.AddEditor(EditorType.AllRevenues);
            RealEstateRepository.Instance.AddEditor(EditorType.AllExpenses);
            RealEstateRepository.Instance.AddEditor(EditorType.AllDebts);
            RealEstateRepository.Instance.SelectEditor(EditorType.AllPayments);

        }
    }
}
