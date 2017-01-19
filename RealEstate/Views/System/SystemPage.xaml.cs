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

namespace RealEstate
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SystemPage : ModernUserControl
    {
        public SystemPage()
        {
            InitializeComponent();

            IsEditor = false;

            SystemViewModel viewModel = RealEstateRepository.Instance.Pages[PageType.System] as SystemViewModel;
            this.DataContext = viewModel;
            RealEstateRepository.Instance.CurrentPage = viewModel;
        }
    }
}
