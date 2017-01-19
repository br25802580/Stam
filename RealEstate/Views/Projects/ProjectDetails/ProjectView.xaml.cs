using Microsoft.Practices.Prism.Regions;
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
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class ProjectView : ModernUserControl
    {
        public ProjectView()
        {
            InitializeComponent();

            //LeasesViewModel leasesViewModel = RealEstateRepository.Instance.LeasesViewModel;
            //ModernUri uri = leasesViewModel.SelectedSource as ModernUri;

            //if (uri != null)
            //{
            //    string parameter = Convert.ToString(uri.EditorKey);
            //    EditorViewModel viewModel = RealEstateRepository.Instance.CurrentPage. CurrentEditor;

            //    this.DataContext = viewModel;
            //}


        }
    }
}
