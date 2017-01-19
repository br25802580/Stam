using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealEstate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        ILog log = LogManager.GetLogger(typeof(MainWindow));
        public MainWindow()
        {
            InitializeComponent();

            log4net.Config.XmlConfigurator.Configure();

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("he-IL");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("he-IL");
                FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
                            XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

                MainViewModel mainViewModel = new RealEstate.MainViewModel();
                DataContext = mainViewModel;
                RealEstateRepository.Instance.MainViewModel = mainViewModel;
                AppearanceManager.Current.AccentColor = Color.FromRgb(0x00, 0xab, 0xa9);

                RegisterNavigation();
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        /// <summary>
        /// Registers the navigation.
        /// </summary>
        private void RegisterNavigation()
        {
            Messenger.Default.Register<NavigationMessage>(this, p =>
            {
                try
                {
                    var frame = GetDescendantFromName(this, "ContentFrame") as ModernFrame;

                    // Set the frame source, which initiates navigation
                    if (frame != null)
                    {
                        frame.Source = new Uri(p.Page, UriKind.Relative);
                    }
                }
                catch (Exception ex)
                {
                    log.HandleError(ex);
                }
            });
        }

        /// <summary>
        /// Gets the name of the descendant from.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="name">The name.</param>
        /// <returns>The FrameworkElement.</returns>
        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                var frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }

            return null;
        }
    }
}
