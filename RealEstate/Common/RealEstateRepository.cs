using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using RealEstate.BL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RealEstate
{
    public sealed class RealEstateRepository
    {
        #region Fields

        private MainViewModel mainViewModel;
        private static volatile ViewModelFactory metaDataFactory;
        private static volatile RealEstateRepository instance;
        private static object syncRoot = new Object();

        #endregion Fields

        #region Ctor

        private RealEstateRepository()
        {
            metaDataFactory = new ViewModelFactory();
        }

        #endregion Ctor

        #region Properties

        public MainViewModel MainViewModel
        {
            get
            {
                return mainViewModel;
            }
            set
            {
                mainViewModel = value;
            }
        }

        public ViewModelFactory EntityMetaDataFactory
        {
            get
            {
                return metaDataFactory;
            }
        }

        public static RealEstateRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RealEstateRepository();
                    }
                }

                return instance;
            }
        }

        #endregion Properties

        #region Public Methods

        public void InitPages()
        {
            Pages = new Dictionary<PageType, PageViewModel>();
            PagesByUri = new Dictionary<string, PageViewModel>();

            PageViewModel pageViewModel = new LeasesViewModel();
            Pages.Add(PageType.Leases, pageViewModel);
            pageViewModel.InitLinks();
            PagesByUri.Add("/Views/Lease/LeasesPage.xaml", pageViewModel);

            pageViewModel = new HomeViewModel();
            Pages.Add(PageType.Home, pageViewModel);
            PagesByUri.Add("/Views/Home/Home.xaml", pageViewModel);

            pageViewModel = new SalesViewModel();
            Pages.Add(PageType.Sale, pageViewModel);
            pageViewModel.InitLinks();
            PagesByUri.Add("/Views/Lease/SalesPage.xaml", pageViewModel);

            pageViewModel = new CustomersViewModel();
            Pages.Add(PageType.Customers, pageViewModel);
            pageViewModel.InitLinks();
            PagesByUri.Add("/Views/Customers/CustomersPage.xaml", pageViewModel);

            pageViewModel = new SuppliersViewModel();
            Pages.Add(PageType.Suppliers, pageViewModel);
            pageViewModel.InitLinks();
            PagesByUri.Add("/Views/Suppliers/SuppliersPage.xaml", pageViewModel);

            pageViewModel = new PaymentsViewModel();
            Pages.Add(PageType.Payments, pageViewModel);
            pageViewModel.InitLinks();
            PagesByUri.Add("/Views/Suppliers/PaymentsPage.xaml", pageViewModel);

            pageViewModel = new ContractsViewModel();
            Pages.Add(PageType.Contracts, pageViewModel);
            pageViewModel.InitLinks();
            PagesByUri.Add("/Views/Contracts/ContractsPage.xaml", pageViewModel);

            pageViewModel = new SystemViewModel();
            Pages.Add(PageType.System, pageViewModel);
            pageViewModel.InitLinks();
            PagesByUri.Add("/Views/System/SystemPage.xaml", pageViewModel);
        }

        private string GetEntityName(object entity)
        {
            Type entityType = entity.GetType();
            string entityName = Convert.ToString(entityType.GetProperty("Name").GetValue(entity, null));

            return entityName;
        }

        public void SelectEditor(EditorType editorType)
        {
            EditorMetaData editorMetaData = metaDataFactory.EditorsMetaData[editorType];
            string editorKey = editorType.ToString();
            PageType pageType = editorMetaData.PageType;
            PageViewModel pageViewModel = Pages[pageType];
            EditorViewModel editorViewModel = pageViewModel.OpenedEditors[editorKey];
            pageViewModel.SelectedSource = editorViewModel.Link.Source as ModernUri;
        }

        public TableViewModel GetTableViewModel(EditorType editorType)
        {
            // TableViewModel tableViewModel = null;
            TableEditorMetadata tableEditorMetadata = metaDataFactory.EditorsMetaData[editorType] as TableEditorMetadata;

            Type viewModelType = tableEditorMetadata.ViewModelType;
            TableViewModel tableViewModel = Activator.CreateInstance(viewModelType) as TableViewModel;
            tableViewModel.IsSubEditor = true;
            tableViewModel.EditorMetaData = tableEditorMetadata;

            tableViewModel.EditorKey = editorType.ToString();
            tableViewModel.MainTitle = tableEditorMetadata.PluralCaption;
            tableViewModel.IconKind = tableEditorMetadata.IconKind;
            tableViewModel.Fields = tableEditorMetadata.Fields;
            tableViewModel.Buttons = new List<ButtonMetadata>(tableEditorMetadata.Buttons);

            var listType = typeof(ObservableCollection<>);
            var innerListType = tableEditorMetadata.EntityWrapperType != null ? tableEditorMetadata.EntityWrapperType : tableEditorMetadata.EntityType;
            var constructedListType = listType.MakeGenericType(innerListType);

            var instance = Activator.CreateInstance(constructedListType);
            tableViewModel.SelectedItems = instance as IList;
            tableViewModel.ChildEditorType = tableEditorMetadata.ChildEditorType;
            tableViewModel.NewEditorType = tableEditorMetadata.NewEditorType;
            tableViewModel.InitListSource = tableEditorMetadata.InitList;
            tableViewModel.AfterCreateNewEditor = tableEditorMetadata.AfterCreateNewEditor;
            tableViewModel.GetReadOnlyItems = tableEditorMetadata.GetReadOnlyItems;
            tableViewModel.BeforeDelete = tableEditorMetadata.BeforeDelete;
            tableViewModel.Init();

            return tableViewModel;
        }

        public EditorViewModel AddEditor(EditorType editorType, INavigationViewModel navigationViewModel = null, string extraEditorKey = "", bool needInitViewmodel = true, int? insertAtIndex = null)
        {
            EditorMetaData editorMetaData = metaDataFactory.EditorsMetaData[editorType];
            EditorViewModel viewModel = null;
            PageType pageType;

            if (editorMetaData != null)
            {
                string editorKey = null;
                string uriString = null;

                Type viewModelType = editorMetaData.ViewModelType;
                viewModel = Activator.CreateInstance(viewModelType) as EditorViewModel;

                if (string.IsNullOrEmpty(extraEditorKey))
                {
                    pageType = editorMetaData.PageType;
                    editorKey = editorType.ToString();
                    uriString = $"{editorMetaData.UriString}?v={editorKey}#{pageType}${editorKey}";
                }
                else
                {
                    pageType = ((EditorViewModel)navigationViewModel).EditorMetaData.PageType;
                    editorKey = (navigationViewModel as EditorViewModel).EditorKey;
                    uriString = $"{editorMetaData.UriString}?v={extraEditorKey}#{pageType}${editorKey}${extraEditorKey}";
                    viewModel.IsSubEditor = true;

                    if (viewModel is TableViewModel)
                        ((TableViewModel)viewModel).EnableGrouping = false;
                }

                ModernUri uri = new ModernUri(uriString, UriKind.Relative);
                uri.EditorKey = editorKey;

                viewModel.EditorMetaData = editorMetaData;

                Link link = null;
                if (navigationViewModel == null)
                {
                    navigationViewModel = Pages[pageType];
                    link = new ModernLink() { DisplayName = string.Empty, Source = uri, ViewModel = viewModel };
                    navigationViewModel.OpenedEditors.Add(editorKey, viewModel);
                }
                else
                {
                    link = new ModernLink() { DisplayName = editorMetaData.PluralCaption, Source = uri, ViewModel = viewModel };
                    navigationViewModel.OpenedEditors.Add(extraEditorKey, viewModel);

                }
                viewModel.Link = link as ModernLink;

                navigationViewModel.Links.Add(link);
                viewModel.EditorKey = editorKey;
                viewModel.MainTitle = editorMetaData.PluralCaption;
                viewModel.IconKind = editorMetaData.IconKind;

                TableViewModel tableViewModel = viewModel as TableViewModel;
                if (tableViewModel != null)
                {
                    TableEditorMetadata tableEditorMetadata = editorMetaData as TableEditorMetadata;
                    if (tableEditorMetadata != null)
                    {
                        var listType = typeof(List<>);
                        var innerListType = tableEditorMetadata.EntityWrapperType != null ? tableEditorMetadata.EntityWrapperType : tableEditorMetadata.EntityType;
                        var constructedListType = listType.MakeGenericType(innerListType);

                        var instance = Activator.CreateInstance(constructedListType);
                        tableViewModel.SelectedItems = instance as IList;
                        tableViewModel.ChildEditorType = tableEditorMetadata.ChildEditorType;
                        tableViewModel.NewEditorType = tableEditorMetadata.NewEditorType;
                        tableViewModel.InitListSource = tableEditorMetadata.InitList;
                        tableViewModel.AfterCreateNewEditor = tableEditorMetadata.AfterCreateNewEditor;
                        tableViewModel.BeforeDelete = tableEditorMetadata.BeforeDelete;
                        tableViewModel.GetReadOnlyItems = tableEditorMetadata.GetReadOnlyItems;
                        tableViewModel.Fields = tableEditorMetadata.Fields;
                        tableViewModel.Buttons = new List<ButtonMetadata>(tableEditorMetadata.Buttons);
                    }

                    if (!string.IsNullOrEmpty(extraEditorKey))
                    {
                        EditorViewModel editorNavigationViewModel = navigationViewModel as EditorViewModel;
                        if (editorNavigationViewModel != null)
                        {
                            tableViewModel.ParentIcon = editorNavigationViewModel.IconKind;
                        }
                    }
                }

                if (needInitViewmodel)
                    viewModel.Init();
            }

            return viewModel;
        }
        public DialogResult OpenNewEditor(EditorType editorType, string title, Action<EditorViewModel> afterCreateNewEditor = null, object entity = null, string entityTitle="")
        {
            DialogResult dialogResult = new DialogResult();
            NewEditorMetadata editorMetaData = metaDataFactory.EditorsMetaData[editorType] as NewEditorMetadata;

            if (editorMetaData != null)
            {
                EditorViewModel viewModel = null;
                Type viewModelType = editorMetaData.ViewModelType;
                viewModel = Activator.CreateInstance(viewModelType) as EditorViewModel;
                viewModel.EditorMetaData = editorMetaData;

                if (entity == null)
                {
                    Type entityType = editorMetaData.EntityType;
                    entity = Activator.CreateInstance(entityType);
                }

                viewModel.Entity = entity;

                if (editorMetaData.EntityWrapperType != null)
                {
                    Type entityWrapperType = editorMetaData.EntityWrapperType;
                    IEntityWrapper entityWrapper = Activator.CreateInstance(entityWrapperType) as IEntityWrapper;
                    entityWrapper.Entity = entity;
                    viewModel.EntityWrapper = entityWrapper;
                }

                if (viewModel is TableViewModel)
                    ((TableViewModel)viewModel).EnableGrouping = false;

                viewModel.Init();

                if (!string.IsNullOrEmpty(entityTitle))
                {
                    viewModel.EntityTitle = entityTitle;
                }

                Type viewType = editorMetaData.NewEditorType;
                //  viewModel = Activator.CreateInstance(viewModelType) as EditorViewModel;
                UserControl uc = Activator.CreateInstance(viewType) as UserControl;
                var dialog = new ModernDialog
                {
                    Title = title,
                    Content = uc
                };
                uc.DataContext = viewModel;
                Button okButton = DialogUtils.GetButton(dialog.OkButton, "שמירה", PackIconKind.ContentSave);
                Button cancelButton = DialogUtils.GetButton(dialog.CancelButton, "ביטול", PackIconKind.Close);

                okButton.Click += OkButton_Click;
                cancelButton.Click += CancelButton_Click;

                okButton.SetResourceReference(Button.BorderBrushProperty, "Accent");
                okButton.BorderThickness = new Thickness(0, 0, 0, 3);
                dialog.Buttons = new Button[] { cancelButton, okButton };
                dialog.Style = Application.Current.Resources["ModernDialog"] as Style;
                dialog.Closing += Dialog_Closing;
                currentNewEditor = viewModel;

                afterCreateNewEditor?.Invoke(viewModel);

                dialog.ShowDialog();

                dialogResult.MessageBoxResult = dialog.MessageBoxResult;
                if (dialog.MessageBoxResult == MessageBoxResult.Cancel)
                    entity = null;

                currentNewEditor = null;
                canCloseDialog = true;
                // currentBeforeSaveResult = null;

                //        if (dialog.MessageBoxResult == MessageBoxResult.OK)
                //        {
                ////            viewModel.BeforeSave();
                //            new GeneralBL().AddEntity(entity);
                //        }
            }
            dialogResult.Result = entity;
            return dialogResult;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            canCloseDialog = true;
            currentNewEditor.OnCancel();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            BeforeSaveResult beforeSaveResult = currentNewEditor.BeforeSave();
            if (!beforeSaveResult.IsValidData && !string.IsNullOrEmpty(beforeSaveResult.ErrorMessage))
            {
                DialogUtils.DisplaySaveErrorMessage(beforeSaveResult.ErrorMessage);
                //   e.Cancel = true;
            }

            canCloseDialog = beforeSaveResult.IsValidData;
        }

        private EditorViewModel currentNewEditor;
        private bool canCloseDialog = true;
        // private BeforeSaveResult currentBeforeSaveResult;

        private void Dialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !canCloseDialog;
            //bool isSecondEvent = currentBeforeSaveResult != null;
            //if (currentBeforeSaveResult == null)
            //{
            //    currentBeforeSaveResult = currentNewEditor.BeforeSave();

            //    if (!currentBeforeSaveResult.IsValidData)
            //    {
            //        DialogUtils.DisplaySaveErrorMessage(currentBeforeSaveResult.ErrorMessage);
            //        //   e.Cancel = true;
            //    }
            //    e.Cancel = !currentBeforeSaveResult.IsValidData;
            //}
            //else
            //{
            //    e.Cancel = !currentBeforeSaveResult.IsValidData;
            //    currentBeforeSaveResult = null;
            //}
            //..       DisplaySaveErrorMessage
        }

        public void RefreshAllEditors()
        {
            foreach (var page in Pages.Values)
            {
                foreach (var editor in page.OpenedEditors.Values)
                {
                    editor.RefreshData();
                }
            }
        }

        public void OpenEditor(object entity, EditorType editorType)
        {
            //    Type entityType = entity.GetType().BaseType;
            IEntityWrapper entityWrapper = entity as IEntityWrapper;
            if (entityWrapper != null)
            {
                entity = entityWrapper.Entity;
            }

            string editorKey = EditorUtils.GetEditorKey(entity, editorType);
            EditorMetaData editorMetaData = metaDataFactory.EditorsMetaData[editorType];
            PageType pageType = editorMetaData.PageType;
            PageViewModel pageViewModel = Pages[pageType];
            EditorViewModel viewModel = null;

            if (editorMetaData != null)
            {
                string uriString = editorMetaData.UriString;
                //uriString = string.Format("{0}?v={1}#{1}", uriString, editorKey);
                uriString = $"{uriString}?v={editorKey}#{pageType}${editorKey}";

                if (pageViewModel.OpenedEditors.ContainsKey(editorKey))
                {
                    viewModel = pageViewModel.OpenedEditors[editorKey];
                }
                else
                {
                    Type viewModelType = editorMetaData.ViewModelType;
                    viewModel = Activator.CreateInstance(viewModelType) as EditorViewModel;
                    viewModel.EditorMetaData = editorMetaData;
                    viewModel.Entity = entity;
                    if (entityWrapper != null)
                    {
                        viewModel.EntityWrapper = entityWrapper;
                    }
                    pageViewModel.OpenedEditors.Add(editorKey, viewModel);
                    viewModel.EditorKey = editorKey;
                    viewModel.IconKind = editorMetaData.IconKind;
                    viewModel.Init();

                    ModernUri uri = new ModernUri(uriString, UriKind.Relative);
                    uri.EditorKey = editorKey;
                    // string entityName = GetEntityName(entity);
                    ModernLink link = new ModernLink() { DisplayName = "", Source = uri, ViewModel = viewModel };
                    viewModel.Link = link;
                    pageViewModel.Links.Add(link);
                }

                //   EditorViewModel viewModel = RealEstateRepository.Instance.EntityMetaDataFactory.GetViewModel(entity, EditorType.LeaseProject);

                //if (viewModel != null)
                //{
                //string editorKey = viewModel.EditorKey;
                //UriWithParameters uri = new UriWithParameters(viewPath + editorKey, UriKind.Relative);
                //uri.Parameter = editorKey;
                //Link link = new Link() { DisplayName = project.Name, Source = uri };
                //Links.Add(link);

                //SelectedSource = uri;
                //}

                pageViewModel.SelectedSource = viewModel.Link.Source as ModernUri;

                Messenger.Default.Send(new NavigationMessage()
                {
                    Page = pageViewModel.PageUriString
                });
                //     CurrentEditor = viewModel;
            }
        }


        //private void AddToHistory(PageViewModel pageViewModel)
        //{
        //    if (!DuringBack)
        //    {
        //        HistoryItem historyItem = null;

        //        if (history != null && history.Count > 0)
        //        {
        //            historyItem = history.Peek();
        //        }
        //        string pageUri = pageViewModel.PageUriString;

        //        if (historyItem == null || pageUri != historyItem.PageUri.OriginalString)
        //        {
        //            history.Push(new HistoryItem() { PageUri = new Uri(pageUri, UriKind.Relative) });

        //        }
        //    }
        //}

        //public void 

        #endregion

        public Dictionary<PageType, PageViewModel> Pages { get; set; }
        public Dictionary<string, PageViewModel> PagesByUri { get; set; }
        //public EditorViewModel CurrentEditor { get; set; }

        private PageViewModel currentPage;
        public PageViewModel CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                if (currentPage != value)
                {
                    if (currentPage != null)
                    {
                        // AddToHistory(currentPage);
                    }
                    currentPage = value;
                }
            }
        }
    }

    public class HistoryItem
    {
        public PageType PageType { get; set; }
        public string EditorKey { get; set; }
        //public Uri PageUri { get; set; }
        //public Uri EditorUri { get; set; }
    }

    public class ModernLink : Link
    {
        public object ViewModel { get; set; }
    }

    public class DialogResult
    {
        public object Result { get; set; }
        public MessageBoxResult MessageBoxResult { get; set; }
    }
}
