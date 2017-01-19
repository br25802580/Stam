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
using System.Windows;
using System.Windows.Input;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using GalaSoft.MvvmLight.Messaging;
using log4net;

namespace RealEstate
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class MainViewModel
        : NotifyPropertyChanged
    {
        ILog log = LogManager.GetLogger(typeof(CustomerConverter));
        private const string FontSmall = "small";
        private const string FontLarge = "large";

        public MainViewModel()
        {
            RealEstateRepository.Instance.InitPages();

            InitLinks();
            BackCommand = new RelayCommand(BackCommandExecute, BackCommandCanExecute);
            ForwardCommand = new RelayCommand(ForwardCommandExecute, ForwardCommandCanExecute);

            ContentSource = new Uri("/Views/Home/Home.xaml", UriKind.Relative);
        }

        private void InitLinks()
        {
            Links = new LinkGroupCollection();

            Links.Add(GetMainLinkGroup());
            Links.Add(GetPaymentsLinkGroup());
            Links.Add(GetLeasesLinkGroup());
            Links.Add(GetSalesLinkGroup());
            Links.Add(GetCustomersLinkGroup());
            Links.Add(GetSuppliersLinkGroup());
            Links.Add(GetContractsLinkGroup());
            Links.Add(GetSystemLinkGroup());
        }

        private LinkGroup GetMainLinkGroup()
        {
            LinkGroup linkGroup = new LinkGroup();
            linkGroup.DisplayName = "ראשי";

            Link link = new Link() { DisplayName = "ראשי", Source = new Uri("/Views/Home/Home.xaml", UriKind.Relative) };
            linkGroup.Links.Add(link);

            return linkGroup;
        }

        private LinkGroup GetPaymentsLinkGroup()
        {
            LinkGroup linkGroup = new LinkGroup();
            linkGroup.DisplayName = "תשלומים";

            Link link = new Link() { DisplayName = "תשלומים", Source = new Uri("/Views/Payments/PaymentsPage.xaml", UriKind.Relative) };
            linkGroup.Links.Add(link);

            return linkGroup;
        }

        private LinkGroup GetLeasesLinkGroup()
        {
            LinkGroup linkGroup = new LinkGroup();
            linkGroup.DisplayName = "השכרה";

            Link link = new Link() { DisplayName = "השכרה", Source = new Uri("/Views/Lease/LeasesPage.xaml", UriKind.Relative) };
            linkGroup.Links.Add(link);

            return linkGroup;
        }

        private LinkGroup GetSalesLinkGroup()
        {
            LinkGroup linkGroup = new LinkGroup();
            linkGroup.DisplayName = "מכירה";

            Link link = new Link() { DisplayName = "מכירה", Source = new Uri("/Views/Lease/SalesPage.xaml", UriKind.Relative) };
            linkGroup.Links.Add(link);

            return linkGroup;
        }

        private LinkGroup GetCustomersLinkGroup()
        {
            LinkGroup linkGroup = new LinkGroup();
            linkGroup.DisplayName = "לקוחות";

            Link link = new Link() { DisplayName = "לקוחות", Source = new Uri("/Views/Customers/CustomersPage.xaml", UriKind.Relative) };
            linkGroup.Links.Add(link);

            return linkGroup;
        }

        private LinkGroup GetContractsLinkGroup()
        {
            LinkGroup linkGroup = new LinkGroup();
            linkGroup.DisplayName = "חוזים";

            Link link = new Link() { DisplayName = "חוזים", Source = new Uri("/Views/Contracts/ContractsPage.xaml", UriKind.Relative) };
            linkGroup.Links.Add(link);

            return linkGroup;
        }

        private LinkGroup GetSystemLinkGroup()
        {
            LinkGroup linkGroup = new LinkGroup();
            linkGroup.DisplayName = "כללי";

            Link link = new Link() { DisplayName = "כללי", Source = new Uri("/Views/System/SystemPage.xaml", UriKind.Relative) };
            linkGroup.Links.Add(link);

            return linkGroup;
        }

        private LinkGroup GetSuppliersLinkGroup()
        {
            LinkGroup linkGroup = new LinkGroup();
            linkGroup.DisplayName = "ספקים";

            Link link = new Link() { DisplayName = "ספקים", Source = new Uri("/Views/Suppliers/SuppliersPage.xaml", UriKind.Relative) };
            linkGroup.Links.Add(link);

            return linkGroup;
        }

        private LinkGroup GetSettingsLinkGroup()
        {
            LinkGroup linkGroup = new LinkGroup();
            linkGroup.DisplayName = "settings";

            Link link = new Link() { DisplayName = "software", Source = new Uri("/Views/Settings/SettingsPage.xaml", UriKind.Relative) };
            linkGroup.Links.Add(link);

            return linkGroup;
        }

        private LinkGroupCollection links;
        public LinkGroupCollection Links
        {
            get { return links; }
            set
            {
                links = value;
                OnPropertyChanged("Links");
            }
        }

        private Uri contentSource;
        public Uri ContentSource
        {
            get { return contentSource; }
            set
            {
                contentSource = value;
                CurrentPage = RealEstateRepository.Instance.PagesByUri[contentSource.OriginalString];
                OnPropertyChanged("ContentSource");
            }
        }

        private PageViewModel currentPage;
        public PageViewModel CurrentPage
        {
            get { return currentPage; }
            set
            {
                currentPage = value;
                RealEstateRepository.Instance.CurrentPage = currentPage;
                //      OnPropertyChanged("CurrentPage");
            }
        }

        public IList<HistoryItem> History = new List<HistoryItem>();

        private bool duringBack;
        public bool DuringBack
        {
            get { return duringBack; }
            set { duringBack = value; }
        }

        private int historyIndex = -1;
        public int HistoryIndex
        {
            get { return historyIndex; }
            set { historyIndex = value; }
        }

        public ICommand BackCommand { get; set; }
        private void BackCommandExecute(object parameter)
        {
            try
            {
                OnBrowseBack();
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        private bool BackCommandCanExecute(object parameter)
        {
            return HistoryIndex > 0;
        }

        public ICommand ForwardCommand { get; set; }
        private void ForwardCommandExecute(object parameter)
        {
            try
            {
                OnBrowseForward();
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        private bool ForwardCommandCanExecute(object parameter)
        {
            return HistoryIndex < History.Count - 1;
        }

        public void RemoveFromHistory(PageViewModel pageViewModel)
        {
            RemoveFromHistory(pageViewModel.PageType, pageViewModel.CurrentEditor?.EditorKey);
        }

        public void ArrangeHistory()
        {
            for (int i = History.Count - 1; i > 0; i--)
            {
                HistoryItem historyItem = History[i];
                HistoryItem prevHistoryItem = History[i - 1];

                if (historyItem.PageType == prevHistoryItem.PageType && historyItem.EditorKey == prevHistoryItem.EditorKey)
                {
                    History.Remove(historyItem);
                    if (HistoryIndex >= i)
                        HistoryIndex--;
                }
            }
        }

        //public void RemoveFromHistory(PageType pageType, IList<string> editorKeys)
        //{
        //    foreach (var editorKey in editorKeys)
        //    {
        //        RemoveFromHistory(pageType, editorKey, false);
        //    }

        //    ArrangeHistory();
        //}

        public void RemoveFromHistory(PageType pageType, string editorKey, bool arrangeHistory = true)
        {
            DuringBack = true;
            for (int i = History.Count - 1; i >= 0; i--)
            {
                HistoryItem historyItem = History[i];
                if (historyItem.EditorKey == editorKey && historyItem.PageType == pageType)
                {
                    if (HistoryIndex >= i)
                        HistoryIndex--;
                    History.RemoveAt(i);
                }
            }

            if (arrangeHistory)
                ArrangeHistory();
            DuringBack = false;
        }

        public void AddToHistory(PageViewModel pageViewModel)
        {
            AddToHistory(pageViewModel.PageType, pageViewModel.CurrentEditor?.EditorKey);
        }

        private void AddToHistory(PageType pageType, string editorKey)
        {
            if (!DuringBack)
            {
                HistoryItem historyItem = null;

                if (History != null && History.Count > 0)
                {
                    historyItem = History.ElementAt(historyIndex);
                }
                // string pageUri = pageViewModel.PageUriString;

                if (historyItem == null || pageType != historyItem.PageType
                    || historyItem.EditorKey != null && editorKey != historyItem.EditorKey)
                {
                    historyIndex++;
                    for (int i = History.Count - 1; i >= historyIndex; i--)
                    {
                        History.RemoveAt(i);
                    }
                    History.Add(new HistoryItem()
                    {
                        PageType = pageType,
                        EditorKey = editorKey
                    });
                }
            }
        }

        public void AddToHistory(EditorViewModel editorViewModel)
        {
            if (editorViewModel != null)
                AddToHistory(editorViewModel.PageViewModel.PageType, editorViewModel.EditorKey);
        }

        private void browseTo(HistoryItem historyItem)
        {
            PageViewModel pageViewModel = RealEstateRepository.Instance.Pages[historyItem.PageType];

            if (historyItem.EditorKey != null)
            {
                EditorViewModel editorViewModel = pageViewModel.OpenedEditors[historyItem.EditorKey];
                pageViewModel.SelectedSource = editorViewModel.Link.Source as ModernUri;
            }

            Messenger.Default.Send(new NavigationMessage()
            {
                Page = pageViewModel.PageUriString
            });
        }

        private void OnBrowseForward()
        {
            DuringBack = true;
            if (History.Count > 0 && historyIndex < History.Count - 1)
            {
                historyIndex++;
                HistoryItem historyItem = History.ElementAt(historyIndex);     // do not remove just yet, navigation may be cancelled
                browseTo(historyItem);
            }
            DuringBack = false;
        }

        private void OnBrowseBack()
        {
            DuringBack = true;
            if (History.Count > 0 && historyIndex > 0)
            {
                //var oldValue = this.Source;
                //  Uri oldValue = null;
                historyIndex--;
                HistoryItem historyItem = History.ElementAt(historyIndex);     // do not remove just yet, navigation may be cancelled
                browseTo(historyItem);
                //if (CanNavigate(oldValue, newValue))
                //{

                //ContentSource = newValue;
                //this.isNavigatingHistory = true;
                //SetCurrentValue(SourceProperty, this.history.Pop());
                //this.isNavigatingHistory = false;
                //}
            }
            DuringBack = false;
            //      ContentSource = Links[1].Links[0].Source;
            //BBCodeBlock bs = new BBCodeBlock();
            //bs.LinkNavigator.Navigate(new Uri("/Views/AddGame.xaml", UriKind.Relative), this);

        }

        private bool CanNavigate(Uri oldValue, Uri newValue)
        {
            //var cancelArgs = new NavigatingCancelEventArgs
            //{
            //    Frame = this,
            //    Source = newValue,
            //    IsParentFrameNavigating = true,
            //    NavigationType = navigationType,
            //    Cancel = false,
            //};
            //OnNavigating(this.Content as IContent, cancelArgs);

            //// check if navigation cancelled
            //if (cancelArgs.Cancel)
            //{
            //    Debug.WriteLine("Cancelled navigation from '{0}' to '{1}'", oldValue, newValue);

            //    if (this.Source != oldValue)
            //    {
            //        // enqueue the operation to reset the source back to the old value
            //        Dispatcher.BeginInvoke((Action)(() => {
            //            this.isResetSource = true;
            //            SetCurrentValue(SourceProperty, oldValue);
            //            this.isResetSource = false;
            //        }));
            //    }
            //    return false;
            //}

            return true;
        }
    }
}
