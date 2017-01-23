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
    public class SystemViewModel
        : PageViewModel
    {
        #region Ctor

        public SystemViewModel() : base()
        {
            PageType = PageType.System;
            PageUriString = "/Views/System/SystemPage.xaml";
        }

        #endregion

        public override void InitLinks()
        {
            Links = new LinkCollection();

            RealEstateRepository.Instance.AddEditor(EditorType.Countries);
            RealEstateRepository.Instance.AddEditor(EditorType.Cities);
            RealEstateRepository.Instance.AddEditor(EditorType.Senders);
           // RealEstateRepository.Instance.AddEditor(EditorType.PaymentRelations);
            RealEstateRepository.Instance.AddEditor(EditorType.Banks);
            RealEstateRepository.Instance.AddEditor(EditorType.PaymentTypes);
            RealEstateRepository.Instance.AddEditor(EditorType.ServiceTypes);
            RealEstateRepository.Instance.AddEditor(EditorType.PaymentTypeForServices);
            RealEstateRepository.Instance.AddEditor(EditorType.Statuses);

            RealEstateRepository.Instance.SelectEditor(EditorType.Countries);
        }
    }
}
