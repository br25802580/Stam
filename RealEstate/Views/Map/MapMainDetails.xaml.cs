using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public partial class MapMainDetails : ModernUserControl
    {
        public MapMainDetails()
        {
            InitializeComponent();
            this.Loaded += MapView_Loaded;
            this.DataContextChanged += MapMainDetails_DataContextChanged;
        }

        public string Address { get; set; }

        private void MapMainDetails_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MapViewModel mapViewModel = this.DataContext as MapViewModel;

            if (mapViewModel != null)
                loadMap(mapViewModel);
        }
        static void CallWithTimeout(Action action, int timeoutMilliseconds)
        {
            System.Threading.Thread t = new System.Threading.Thread(
         () =>
         {
             System.Threading.Thread.Sleep(timeoutMilliseconds);
             Application.Current.Dispatcher.Invoke(action);
             // action.Invoke();
         }
     );
            t.Start();

        }
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapViewModel mapViewModel = this.DataContext as MapViewModel;
            if (mapViewModel != null)
            {
                mapViewModel.IsMapVisible = false;
                mapViewModel.MapVisibility = System.Windows.Visibility.Hidden;

                if (mapViewModel.Address != Address)
                {
                    loadMap(mapViewModel);
                }

                CallWithTimeout(() =>
                {
                    if (mapViewModel != null)
                    {
                        mapViewModel.IsMapVisible = true;
                        mapViewModel.MapVisibility = System.Windows.Visibility.Visible;
                    }
                }, 700);
            }

        }

        private void setupObjectForScripting(object sender, RoutedEventArgs e)
        {
            ((WebBrowser)sender).ObjectForScripting = new HtmlInteropInternalTestClass();

            HideScriptErrors((WebBrowser)sender, true);
        }

        public void HideScriptErrors(WebBrowser wb, bool hide)
        {
            var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            var objComWebBrowser = fiComWebBrowser.GetValue(wb);
            if (objComWebBrowser == null)
            {
                wb.Loaded += (o, s) => HideScriptErrors(wb, hide); //In case we are to early
                return;
            }
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }

        //private GeoCoordinate GetLocationProperty()
        //{
        //    GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();

        //    // Do not suppress prompt, and wait 1000 milliseconds to start.
        //    watcher.TryStart(false, TimeSpan.FromMilliseconds(4000));

        //    GeoCoordinate coord = watcher.Position.Location;

        //    if (coord.IsUnknown != true)
        //    {
        //        Console.WriteLine("Lat: {0}, Long: {1}",
        //            coord.Latitude,
        //            coord.Longitude);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Unknown latitude and longitude.");
        //    }

        //    return coord;
        //}

        private void loadMap(MapViewModel mapViewModel)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "html\\realMap.html"))
            {

                //GeoCoordinate geoCoordinate = GetLocationProperty();
             //   string folder = new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).Directory.ToString();
              //  StreamReader objReader = new StreamReader(folder + "html\\realMap.html");
                StreamReader objReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "html\\realMap.html");
                string line = "";
                line = objReader.ReadToEnd();
                objReader.Close();
                Address = mapViewModel.Address;
                line = line.Replace("[address]", Address);
                line = line.Replace("[zoom]", Convert.ToString(mapViewModel.Zoom));
                //line = line.Replace("[destination]", "25.520581, -103.50607");
                StreamWriter page = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "html\\map1.html");
                page.Write(line);
                page.Close();
                Uri uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "html\\map1.html");
                webBrowser1.Navigate(uri);
                //datos.Width = 140;
            }
        }
    }

    // Object used for communication from JS -> WPF
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class HtmlInteropInternalTestClass
    {
        public void endDragMarkerCS(Decimal Lat, Decimal Lng)
        {
            //((MainWindow)Application.Current.MainWindow).tbLocation.Text = Math.Round(Lat, 5) + "," + Math.Round(Lng, 5);
        }
    }

}
