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
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class LeasesTable : ModernUserControl
    {
        public LeasesTable()
        {
            InitializeComponent();

            //EditorViewModel viewModel = RealEstateRepository.Instance.CurrentPage.CurrentEditor;

            //this.DataContext = viewModel;
            // create and assign the appearance view model
        }
    }
}
