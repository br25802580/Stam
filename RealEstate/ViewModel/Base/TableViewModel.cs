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
using System.Collections;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Data;
using System.Windows;
using log4net;

namespace RealEstate
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class TableViewModel
        : EditorViewModel
    {
        ILog log = LogManager.GetLogger(typeof(TableViewModel));

        #region Ctor

        public TableViewModel() : base()
        {
            AddCommand = new RelayCommand(AddExecute);
            EditCommand = new RelayCommand(EditExecute, CanEdit);
            DeleteCommand = new RelayCommand(DeleteExecute, CanEdit);
            AttachCommand = new RelayCommand(AttachExecute);
            DetachCommand = new RelayCommand(DetachExecute, CanEdit);
        }

        #endregion

        #region Fields


        #endregion

        #region Properties  

        public Action<IList> BeforeDelete { get; set; }

        private string itemsCount;
        public string ItemsCount
        {
            get { return itemsCount; }
            set
            {
                itemsCount = value;
                OnPropertyChanged("ItemsCount");
            }
        }

        private PackIconKind? parentIcon;
        public PackIconKind? ParentIcon
        {
            get { return parentIcon; }
            set
            {
                parentIcon = value;
                OnPropertyChanged("ParentIcon");
            }
        }

        public IList<ColumnMetadata> Fields { get; set; }

        public IList ReadOnlyItems { get; set; }
        public Func<IList, IList> GetReadOnlyItems { get; set; }

        public IList<ButtonMetadata> Buttons { get; set; }

        private bool displaySummary = false;
        public bool DisplaySummary
        {
            get { return displaySummary; }
            set
            {
                displaySummary = value;
                OnPropertyChanged("DisplaySummary");
            }
        }

        private bool displayExtendedSummary = false;
        public bool DisplayExtendedSummary
        {
            get { return displayExtendedSummary; }
            set
            {
                displayExtendedSummary = value;
                OnPropertyChanged("DisplayExtendedSummary");
            }
        }


        private IList list;
        public IList List
        {
            get { return list; }
            set
            {
                if (list != value)
                {
                    list = value;
                    OnPropertyChanged("List");
                    ListCollectionView = CollectionViewSource.GetDefaultView(List);
                    //   ListCollectionView.fil
                }
            }
        }

        private ICollectionView listCollectionView;
        public ICollectionView ListCollectionView
        {
            get { return listCollectionView; }
            set
            {
                listCollectionView = value;
                OnPropertyChanged("ListCollectionView");
            }
        }

        public Func<IList> InitListSource { get; set; }

        public Func<object, bool> InitialFilter { get; set; }

        public Action<EditorViewModel> AfterCreateNewEditor { get; set; }

        private IList selectedItems;
        public IList SelectedItems
        {
            get { return selectedItems; }
            set
            {
                if (selectedItems != value)
                {
                    selectedItems = value;
                    OnPropertyChanged("SelectedProjects");
                }
            }
        }

        private bool displayCommands = true;
        public bool DisplayCommands
        {
            get { return displayCommands; }
            set
            {
                displayCommands = value;
                OnPropertyChanged("DisplayCommands");
            }
        }

        private bool enableGroup = true;
        public bool EnableGrouping
        {
            get { return enableGroup; }
            set
            {
                enableGroup = value;
            }
        }

        #endregion Properties

        public EditorType ChildEditorType { get; set; }
        public EditorType NewEditorType { get; set; }
        public TableEditorMetadata TableEditorMetadata
        {
            get
            {
                return EditorMetaData as TableEditorMetadata;
            }
        }

        #region Commands

        public ICommand AddCommand { get; set; }
        private void AddExecute(object parameter)
        {
            try
            {
                AddEntity(NewEditorType);
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        public ICommand EditCommand { get; set; }
        public void EditExecute(object parameter)
        {
            try
            {
                if (AreSelectedItemsReadOnly())
                {
                    DialogUtils.DisplayMessage("לא ניתן לערוך פריט מוגן", "העריכה נכשלה");
                    return;
                }

                EditEntities(SelectedItems, ChildEditorType);
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }
        public bool CanEdit(object parameter)
        {
            return SelectedItems?.Count > 0;
        }

        private bool AreSelectedItemsReadOnly()
        {
            return SelectedItems?.OfType<object>().Any(item => ReadOnlyItems?.Contains(item) == true) == true;
        }

        public ICommand DeleteCommand { get; set; }
        public void DeleteExecute(object parameter)
        {
            try
            {
                if (AreSelectedItemsReadOnly())
                {
                    DialogUtils.DisplayMessage("לא ניתן למחוק פריט מוגן", "המחיקה נכשלה");
                    return;
                }

                DeleteEntities(SelectedItems);
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        public ICommand AttachCommand { get; set; }
        public void AttachExecute(object parameter)
        {
            try
            {
                //  PageType pageType = (PageType)Enum.Parse(typeof(PageType), navigationParams[0], true);
                IList attachedItems = DialogUtils.DisplayAttachItemsDialog(EditorMetaData.EditorType, List);

                if (attachedItems != null)
                {
                    foreach (var item in attachedItems)
                    {
                        AfterAddEntity?.Invoke(item);
                    }

                    new GeneralBL().Save();
                    RealEstateRepository.Instance.RefreshAllEditors();
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        public ICommand DetachCommand { get; set; }
        public void DetachExecute(object parameter)
        {
            try
            {
                if (DialogUtils.DisplayBeforeDeleteMessage() == MessageBoxResult.Yes)
                {
                    foreach (var item in SelectedItems)
                    {
                        DetachEntity?.Invoke(item);
                    }
                    new GeneralBL().Save();
                    RealEstateRepository.Instance.RefreshAllEditors();
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        #endregion

        #region Methods

        public void RemoveGroupDescription(string propertyName)
        {
            GroupDescription GroupDescription = ListCollectionView.GroupDescriptions.OfType<PropertyGroupDescription>()
                .Where(group => group.PropertyName == propertyName) as GroupDescription;

            if (GroupDescription != null)
                ListCollectionView.GroupDescriptions.Remove(GroupDescription);
        }

        public void AddGroupDescription(ColumnMetadata groupDetails)
        {
            ListCollectionView.GroupDescriptions.Add(new PropertyGroupDescription(groupDetails.Property, groupDetails.Converter));

            //foreach (object groupItem in ListCollectionView.Groups)
            //{
            //    var group = groupItem as CollectionViewGroup;
            //    foreach (IEntityWrapper entityWrapper in group.Items)
            //    {
            //        entityWrapper.IsExpanded = true;
            //    }
            //}
        }

        public override void Init()
        {
            if (InitListSource != null)
            {
                IList list = InitListSource();
                if (InitialFilter != null)
                {
                    list = list.OfType<object>().Where(InitialFilter).ToList();
                }

                if (TableEditorMetadata?.EntityWrapperType != null)
                {
                    IList entityWrapperList = new List<IEntityWrapper>();
                    foreach (var item in list)
                    {
                        IEntityWrapper entityWrapper = Activator.CreateInstance(TableEditorMetadata.EntityWrapperType) as IEntityWrapper;
                        entityWrapper.Entity = item;
                        entityWrapperList.Add(entityWrapper);
                    }
                    list = entityWrapperList;
                }

                List = list;

                if (EnableGrouping)
                {
                    foreach (var groupDetails in TableEditorMetadata.Groups)
                    {
                        AddGroupDescription(groupDetails);
                    }
                }

                if (Link != null)
                {
                    //    string linkDescription = List.Count == 1 ? EditorMetaData.SingleCaption : EditorMetaData.PluralCaption;
                    //      Link.DisplayName = $"{List.Count} {linkDescription}";
                    ItemsCount = List.Count > 0 ? List.Count.ToString() : string.Empty;
                    //ItemsCount = List.Count.ToString();
                    //Modernl
                }
            }

            switch (EditorMetaData.EditorType)
            {
                case EditorType.AllPayments:
                    DisplaySummary = true;
                    DisplayExtendedSummary = true;
                    break;
                case EditorType.AllRevenues:
                case EditorType.AllExpenses:
                case EditorType.AllDebts:
                    DisplaySummary = true;
                    break;
                default:
                    break;
            }

            ReadOnlyItems = GetReadOnlyItems?.Invoke(List);
        }

        public override void RefreshData()
        {
            base.RefreshData();
            Init();
            //OnPropertyChanged("Customers");
            //OnPropertyChanged("SelectedCustomers");
        }

        #endregion Methods

        #region Event Handlers

        #endregion

        public Action<object> AfterAddEntity { get; set; }
        public Action<object> DetachEntity { get; set; }

        public void EditEntityInPopup(object entity, EditorType editorType)
        {
            string title = $"עריכת {EditorMetaData.SingleCaption}";
            // object newEntity = entity.CloneObject();

            DialogResult dialogResult = RealEstateRepository.Instance.OpenNewEditor(editorType, title, AfterCreateNewEditor, entity, EditorMetaData.SingleCaption);

            if (dialogResult.MessageBoxResult == MessageBoxResult.OK)
            {
                //   entity = newEntity.CloneObject();
                //if (AfterAddEntity != null)
                //    AfterAddEntity(entity);

                //new GeneralBL().AddEntity(entity);
                new GeneralBL().SaveEntity(entity);
                //new GeneralBL().Save();
                RealEstateRepository.Instance.RefreshAllEditors();
                //  SelectedItems.Clear();
                //   SelectedItems.Add(entity);
            }
        }

        public void AddEntity(EditorType editorType)
        {
            string title = string.Format("{0} - הוספת {1}", EditorMetaData.PluralCaption, EditorMetaData.SingleCaption);
            DialogResult dialogResult = RealEstateRepository.Instance.OpenNewEditor(editorType, title, AfterCreateNewEditor, null, EditorMetaData.SingleCaption);

            if (dialogResult.Result != null)
            {
                AfterAddEntity?.Invoke(dialogResult.Result);

                new GeneralBL().AddEntity(dialogResult.Result);
                new GeneralBL().Save();
                RealEstateRepository.Instance.RefreshAllEditors();
                //  SelectedItems.Clear();
                //   SelectedItems.Add(entity);
            }
        }

        public void DeleteEntities(IList entities)
        {
            if (entities.Count > 0)
            {
                if (DialogUtils.DisplayBeforeDeleteMessage() == MessageBoxResult.Yes)
                {
                    if (entities[0] is IEntityWrapper)
                        entities = entities.OfType<IEntityWrapper>().Select(e => e.Entity).ToList();

                    foreach (var entity in entities)
                    {
                        string editorKey = EditorUtils.GetEditorKey(entity, ChildEditorType);
                        if (PageViewModel.OpenedEditors.ContainsKey(editorKey))
                            PageViewModel.RemoveEditor(PageViewModel.OpenedEditors[editorKey], false);
                    }
                    RealEstateRepository.Instance.MainViewModel.ArrangeHistory();

                    BeforeDelete?.Invoke(entities);
                    new GeneralBL().DeleteEntities(entities);

                    RealEstateRepository.Instance.RefreshAllEditors();
                    // RealEstateRepository.Instance.MainViewModel.RemoveFromHistory();
                }
            }
        }

        public void EditEntities(IList entities, EditorType editorType)
        {
            if (editorType == EditorType.Undefined && TableEditorMetadata.GetEditorTypeByEntity == null)
                return;
            Func<object, EditorType> GetEditorTypeByEntity = TableEditorMetadata.GetEditorTypeByEntity;

            foreach (object entity in new List<object>(entities.Cast<object>()))
            {
                if (GetEditorTypeByEntity != null)
                    editorType = GetEditorTypeByEntity(entity);
                EditEntity(entity, editorType);
            }
        }

        public void EditEntity(object entity, EditorType editorType)
        {
            if (TableEditorMetadata.EditInPopup)
                EditEntityInPopup(entity, editorType);
            else
                RealEstateRepository.Instance.OpenEditor(entity, editorType);
        }
    }
}
