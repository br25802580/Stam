﻿using FirstFloor.ModernUI.Windows;
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
    public partial class MapPage : ModernUserControl
    {
        public MapPage()
        {
            InitializeComponent();

            IsEditor = false;

            MapPageViewModel viewModel = RealEstateRepository.Instance.Pages[PageType.Map] as MapPageViewModel;
            this.DataContext = viewModel;
            RealEstateRepository.Instance.CurrentPage = viewModel;


        }
    }
}
