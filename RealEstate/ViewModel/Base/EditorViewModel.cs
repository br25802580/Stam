using FirstFloor.ModernUI.Presentation;
using log4net;
using MaterialDesignThemes.Wpf;
using RealEstate.BL;
using RealEstate.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealEstate
{
    public class EditorViewModel : NotifyPropertyChanged, INavigationViewModel
    {
        ILog log = LogManager.GetLogger(typeof(EditorViewModel));

        public EditorViewModel()
        {
            ListsInitializer = new Dictionary<Type, Action>();
            ListsInitializer.Add(typeof(Country), InitCountries);
            ListsInitializer.Add(typeof(City), InitCities);
            ListsInitializer.Add(typeof(Supplier), InitSuppliers);
            ListsInitializer.Add(typeof(Customer), InitCustomers);
            ListsInitializer.Add(typeof(Project), InitProjects);
            ListsInitializer.Add(typeof(SenderType), InitSenderTypes);
            ListsInitializer.Add(typeof(PaymentType), InitPaymentTypes);
            ListsInitializer.Add(typeof(PaymentMethod), InitPaymentMethods);
            ListsInitializer.Add(typeof(PaymentRelation), InitPaymentRelations);
            ListsInitializer.Add(typeof(ProjectType), InitProjectTypes);
            ListsInitializer.Add(typeof(ServiceType), InitServiceTypes);
            ListsInitializer.Add(typeof(Gender), InitGenders);
            ListsInitializer.Add(typeof(Bank), InitBanks);

            SaveCommand = new RelayCommand(SaveExecute);
        }

        #region Properties

        private string entityTitle;
        public string EntityTitle
        {
            get { return entityTitle; }
            set
            {
                entityTitle = value;
                OnPropertyChanged("EntityTitle");
            }
        }
        public string MainTitle { get; set; }
        public PackIconKind IconKind { get; set; }
        public PageViewModel PageViewModel { get; set; }

        private Dictionary<string, EditorViewModel> openedEditors = new Dictionary<string, EditorViewModel>();
        public Dictionary<string, EditorViewModel> OpenedEditors
        {
            get { return openedEditors; }
            set
            {
                openedEditors = value;
                OnPropertyChanged("OpenedEditors");
            }
        }

        public int EditorOrder { get; set; }

        private object entity;
        public object Entity
        {
            get { return entity; }
            set
            {
                entity = value;
                //if (entity != null && (entityWrapper == null || entity != entityWrapper.Entity))
                //{
                //    Entity = entityWrapper.Entity;
                //}
                OnPropertyChanged("Entity");
            }
        }

        private IEntityWrapper entityWrapper;
        public IEntityWrapper EntityWrapper
        {
            get { return entityWrapper; }
            set
            {
                entityWrapper = value;
                if (entityWrapper != null && entity != entityWrapper.Entity)
                {
                    Entity = entityWrapper.Entity;
                }
                OnPropertyChanged("EntityWrapper");
            }
        }

        private bool isSubEditor = false;
        public bool IsSubEditor
        {
            get { return isSubEditor; }
            set { isSubEditor = value; }
        }

        private bool isEditEditor = false;
        public bool IsEditEditor
        {
            get { return isEditEditor; }
            set
            {
                isEditEditor = value;
                OnPropertyChanged("IsEditEditor");
            }
        }

        private LinkCollection links;
        public LinkCollection Links
        {
            get { return links; }
            set
            {
                links = value;
                OnPropertyChanged("Links");
            }
        }

        public string EditorKey { get; set; }

        private EditorMetaData editorMetaData;
        public EditorMetaData EditorMetaData
        {
            get { return editorMetaData; }
            set
            {
                editorMetaData = value;

                if (editorMetaData?.PageType != PageType.Undefined)
                    PageViewModel = RealEstateRepository.Instance.Pages[editorMetaData.PageType];
                else
                {
                    PageViewModel = null;
                }

            }
        }
        public ModernLink Link { get; set; }
        public Dictionary<Type, Action> ListsInitializer { get; set; }

        #endregion Properties

        public virtual void RefreshEntityTitle()
        {
            foreach (EditorViewModel editorViewModel in OpenedEditors.Values)
            {
                if (editorViewModel != this)
                    editorViewModel.EntityTitle = EntityTitle;
            }
        }

        IList<Type> ListsInitializers = new List<Type>();

        public virtual void InitList(Type listType)
        {
            if (ListsInitializer.ContainsKey(listType))
            {
                ListsInitializer[listType]();
            }
            if (!ListsInitializers.Contains(listType))
            {
                ListsInitializers.Add(listType);
            }
        }

        private void InitCountries()
        {
            Countries = new GeneralBL().GetCountries();
        }

        private void InitCities()
        {
            Cities = new GeneralBL().GetCities();
        }

        private void InitSuppliers()
        {
            Suppliers = new SuppliersBL().GetSuppliers();
        }

        private void InitCustomers()
        {
            Customers = new CustomersBL().GetCustomers();
        }

        private void InitProjects()
        {
            Projects = new ProjectsBL().GetProjects();
        }

        private void InitProjectTypes()
        {
            ProjectTypes = new GeneralBL().GetProjectTypes();
        }

        private void InitSenderTypes()
        {
            SenderTypes = new GeneralBL().GetSenderTypes().ToList();
        }

        private void InitServiceTypes()
        {
            ServiceTypes = new GeneralBL().GetServiceTypes().ToList();
        }

        private void InitGenders()
        {
            Genders = new GeneralBL().GetGenders().ToList();
        }

        private void InitBanks()
        {
            Banks = new GeneralBL().GetBanks().ToList();
        }

        private void InitPaymentTypes()
        {
            PaymentTypes = new PaymentsBL().GetPaymentTypes().ToList();
        }

        private void InitPaymentMethods()
        {
            PaymentMethods = new PaymentsBL().GetPaymentMethods().ToList();
        }

        private void InitPaymentRelations()
        {
            paymentRelations = new PaymentsBL().GetPaymentRelations().ToList();
        }

        public virtual void Init()
        {
            IsEditEditor = !(EditorMetaData is NewEditorMetadata);
            this.IconKind = EditorMetaData.IconKind;
            RefreshEntityTitle();
        }

        public virtual void RefreshData()
        {
            foreach (var editor in OpenedEditors.Values)
            {
                if (editor != this)
                {
                    RefreshEntityTitle();
                    editor.RefreshData();
                    TableViewModel tableViewModel = editor as TableViewModel;
                    if (tableViewModel != null)
                    {

                    }
                }
            }

            foreach (var listType in ListsInitializers)
            {
                if (ListsInitializer.ContainsKey(listType))
                {
                    ListsInitializer[listType]();
                }
            }
        }

        public virtual BeforeSaveResult BeforeSave()
        {
            return null;
        }

        public virtual void OnCancel()
        {
        }

        private IList<Country> countries;
        public IList<Country> Countries
        {
            get { return countries; }
            set
            {
                countries = value;
                OnPropertyChanged("Countries");
            }
        }

        private IList<City> cities;
        public IList<City> Cities
        {
            get { return cities; }
            set
            {
                cities = value;
                OnPropertyChanged("Cities");
            }
        }

        private IList<Supplier> suppliers;
        public IList<Supplier> Suppliers
        {
            get { return suppliers; }
            set
            {
                suppliers = value;
                OnPropertyChanged("Suppliers");
            }
        }

        private IList<Customer> customers;
        public IList<Customer> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                OnPropertyChanged("Customers");
            }
        }

        private IList<PaymentType> paymentTypes;
        public IList<PaymentType> PaymentTypes
        {
            get { return paymentTypes; }
            set
            {
                paymentTypes = value;
                OnPropertyChanged("PaymentTypes");
            }
        }

        private IList<PaymentMethod> paymentMethods;
        public IList<PaymentMethod> PaymentMethods
        {
            get { return paymentMethods; }
            set
            {
                paymentMethods = value;
                OnPropertyChanged("PaymentMethods");
            }
        }

        private IList<PaymentRelation> paymentRelations;
        public IList<PaymentRelation> PaymentRelations
        {
            get { return paymentRelations; }
            set
            {
                paymentRelations = value;
                OnPropertyChanged("PaymentRelations");
            }
        }

        private IList<Project> projects;
        public IList<Project> Projects
        {
            get { return projects; }
            set
            {
                projects = value;
                OnPropertyChanged("Projects");
            }
        }

        private IList<ProjectType> projectTypes;
        public IList<ProjectType> ProjectTypes
        {
            get { return projectTypes; }
            set
            {
                projectTypes = value;
                OnPropertyChanged("ProjectTypes");
            }
        }

        private IList<Flat> flats;
        public IList<Flat> Flats
        {
            get { return flats; }
            set
            {
                flats = value;
                OnPropertyChanged("Flats");
            }
        }

        private IList<SenderType> senderTypes;
        public IList<SenderType> SenderTypes
        {
            get { return senderTypes; }
            set
            {
                senderTypes = value;
                OnPropertyChanged("SenderTypes");
            }
        }

        private IList<ServiceType> serviceTypes;
        public IList<ServiceType> ServiceTypes
        {
            get { return serviceTypes; }
            set
            {
                serviceTypes = value;
                OnPropertyChanged("ServiceTypes");
            }
        }

        private IList<Gender> genders;
        public IList<Gender> Genders
        {
            get { return genders; }
            set
            {
                genders = value;
                OnPropertyChanged("Genders");
            }
        }

        private IList<Bank> banks;
        public IList<Bank> Banks
        {
            get { return banks; }
            set
            {
                banks = value;
                OnPropertyChanged("Banks");
            }
        }

        private bool isDisposed = false;

        public bool IsDisposed
        {
            get { return isDisposed; }
            set { isDisposed = value; }
        }


        public ICommand SaveCommand { get; set; }
        public void SaveExecute(object parameter)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        public BeforeSaveResult Save()
        {
            BeforeSaveResult beforeSaveResult = BeforeSave();

            if (beforeSaveResult != null && !beforeSaveResult.IsValidData)
            {
                DialogUtils.DisplaySaveErrorMessage(beforeSaveResult.ErrorMessage);
            }
            else
            {
                new GeneralBL().SaveEntity(Entity);
                RealEstateRepository.Instance.RefreshAllEditors();
            }

            return beforeSaveResult;
        }

        protected void RefreshMapLink(Country country, City city, string street, string houseNumber)
        {
            if (OpenedEditors.ContainsKey("Map"))
            {
                MapViewModel mapViewModel = OpenedEditors["Map"] as MapViewModel;
                if (mapViewModel != null)
                {
                    string _country = country != null ? country.Name + "," : "";
                    string _city = city != null ? city.Name + "," : "";
                    mapViewModel.Address = $"{_country} {_city} {street} {houseNumber}";

                    mapViewModel.Zoom = 16;
                    if (string.IsNullOrEmpty(street))
                    {
                        mapViewModel.Zoom = 11;
                    }
                    if (string.IsNullOrEmpty(_city))
                    {
                        mapViewModel.Zoom = 6;
                    }
                }
            }
        }

        protected void AddMapLink(Country country, City city, string street, string houseNumber)
        {
            MapViewModel mapViewModel = RealEstateRepository.Instance.AddEditor(EditorType.Map, this, "Map", false) as MapViewModel;
            mapViewModel.EntityTitle = EntityTitle;

            string _country = country != null ? country.Name + "," : "";
            string _city = city != null ? city.Name + "," : "";
            mapViewModel.Address = $"{_country} {_city} {street} {houseNumber}";

            mapViewModel.Zoom = 16;
            if (string.IsNullOrEmpty(street))
            {
                mapViewModel.Zoom = 11;
            }
            if (string.IsNullOrEmpty(_city))
            {
                mapViewModel.Zoom = 6;
            }

            mapViewModel.Init();
        }
        public event EventHandler Disposed;

        public void Dispose()
        {
            IsDisposed = true;
            foreach (var editor in OpenedEditors.Values)
            {
                if (editor != this)
                    editor.Dispose();
            }

            this.Disposed?.Invoke(this, EventArgs.Empty);
        }
    }
}
