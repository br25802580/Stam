using FirstFloor.ModernUI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealEstate.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SalesPage : ModernUserControl
    {
        public SalesPage()
        {
            InitializeComponent();

            IsEditor = false;

            SalesViewModel salesViewModel = RealEstateRepository.Instance.Pages[PageType.Sale] as SalesViewModel;
            this.DataContext = salesViewModel;
            RealEstateRepository.Instance.CurrentPage = salesViewModel;
        }
    }
}
