using FirstFloor.ModernUI.Presentation;
using log4net;
using RealEstate.BL;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealEstate
{
    public class DebtViewModel : EditorViewModel
    {
        ILog log = LogManager.GetLogger(typeof(DebtViewModel));
        #region Ctor

        public DebtViewModel()
        {
            OpenCustomerCommand = new RelayCommand(OpenCustomerExecute, OpenCustomerCanExecute);
            OpenSupplierCommand = new RelayCommand(OpenSupplierExecute, OpenSupplierCanExecute);
            OpenProjectCommand = new RelayCommand(OpenProjectExecute, OpenProjectCanExecute);
            OpenFlatCommand = new RelayCommand(OpenFlatExecute, OpenFlatCanExecute);
        }

        #endregion Ctor

        #region Properties

        public Debt Debt
        {
            get { return Entity as Debt; }
            set { Entity = value; }
        }

        private DebtType debtType = DebtType.None;
        public DebtType DebtType
        {
            get { return debtType; }
            set
            {
                if (debtType != value)
                {
                    debtType = value;
                    OnPropertyChanged("DebtType");

                    FromSenderTypes = new ObservableCollection<SenderType>(SenderTypes);
                    ToSenderTypes = new ObservableCollection<SenderType>(SenderTypes);

                    if (debtType == DebtType.Revenue)
                    {
                        if (PaymentRelation == null || (PaymentRelation.FromSenderTypeId != 2 && PaymentRelation.FromSenderTypeId != 3
                            || PaymentRelation.ToSenderTypeId != 4))
                        {
                            PaymentRelation = new PaymentsBL().GetPaymentRelation(3, 4);
                        }
                        FromSenderTypes.Remove(FromSenderTypes.First(senderType => senderType.Id == 4));
                    }
                    else
                    {
                        if (PaymentRelation == null || (PaymentRelation.FromSenderTypeId != 4
                          || PaymentRelation.ToSenderTypeId != 1))
                        {
                            PaymentRelation = new PaymentsBL().GetPaymentRelation(4, 1);

                            ToSenderTypes.Remove(ToSenderTypes.First(senderType => senderType.Id == 4));
                        }
                    }
                    Debt.CalculateDebtAmount();
                }
            }
        }

        private SenderType GetSenderType(int id)
        {
            return SenderTypes.FirstOrDefault(senderType => senderType.Id == id);
        }


        private PaymentRelation paymentRelation;
        public PaymentRelation PaymentRelation
        {
            get { return paymentRelation; }
            set
            {
                if (value != paymentRelation)
                {
                    OnPropertyChanged("PaymentRelation");
                    paymentRelation = value;
                    Debt.PaymentRelation = value;

                    FromSenderType = value.FromSenderType;
                    ToSenderType = value.ToSenderType;

                    //PaymentTypes = new PaymentsBL().GetPaymentTypes()
                    //    .Where(pt => pt.PaymentRelation == value).ToList();

                    RefreshPaymentTypes();
                    FillBySenderFilter();

                    // FillPaymentTypeBySupplier();
                }
            }
        }

        private int? projectTypeId;
        public int? ProjectTypeId
        {
            get { return projectTypeId; }
            set
            {
                if (value != projectTypeId)
                {
                    projectTypeId = value;
                    OnPropertyChanged("ProjectTypeId");
                }
            }
        }

        private SenderType fromSenderType;
        public SenderType FromSenderType
        {
            get { return fromSenderType; }
            set
            {
                if (value != fromSenderType)
                {
                    fromSenderType = value;
                    OnPropertyChanged("FromSenderType");

                    if (value != null)
                    {
                        SetPaymentSender(value.Id);
                    }

                    if (paymentRelation.FromSenderType != value)
                    {
                        PaymentRelation = new PaymentsBL().GetPaymentRelation(fromSenderType.Id, ToSenderType.Id);
                    }

                }
            }
        }

        private SenderType toSenderType;
        public SenderType ToSenderType
        {
            get { return toSenderType; }
            set
            {
                if (value != toSenderType)
                {
                    toSenderType = value;
                    OnPropertyChanged("ToSenderType");

                    if (value != null)
                        SetPaymentSender(value.Id);

                    if (paymentRelation.ToSenderType != value)
                    {
                        PaymentRelation = new PaymentsBL().GetPaymentRelation(FromSenderType.Id, toSenderType.Id);
                    }
                }
            }
        }

        private ObservableCollection<SenderType> fromSenderTypes;
        public ObservableCollection<SenderType> FromSenderTypes
        {
            get { return fromSenderTypes; }
            set
            {
                fromSenderTypes = value;
                OnPropertyChanged("FromSenderTypes");
            }
        }

        private ObservableCollection<SenderType> toSenderTypes;
        public ObservableCollection<SenderType> ToSenderTypes
        {
            get { return toSenderTypes; }
            set
            {
                toSenderTypes = value;
                OnPropertyChanged("ToSenderTypes");
            }
        }

        private SenderFilter senderFilter = SenderFilter.BySender;
        public SenderFilter SenderFilter
        {
            get { return senderFilter; }
            set
            {
                senderFilter = value;
                OnPropertyChanged("SenderFilter");
                FillBySenderFilter();
            }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged("Amount");
                    Debt.Amount = value;
                    Debt.CalculateDebtAmount();

                    OnPropertyChanged(null);
                }
            }
        }

        private int amountPaid;
        public int AmountPaid
        {
            get { return amountPaid; }
            set
            {
                if (amountPaid != value)
                {
                    amountPaid = value;
                    OnPropertyChanged("AmountPaid");
                    Debt.AmountPaid = value;
                    Debt.CalculateDebtAmount();

                    OnPropertyChanged(null);
                }
            }
        }

        private Customer customer;
        public Customer Customer
        {
            get { return customer; }
            set
            {
                if (customer != value)
                {
                    customer = value;
                    OnPropertyChanged("Customer");

                    FillProjectsBySender();

                    if (SenderFilter == SenderFilter.ByProject)
                    {
                        if (Flats != null && (Flat == null || !Flat.CustomerInProjects.Any(cInp => cInp.Customer == value)))
                        {
                            if (IsCustomerSender)
                                Flat = Flats.FirstOrDefault(f => f.CustomerInProjects.Any(cInp => cInp.Customer == value));
                        }
                    }

                    if (PaymentType == null && PaymentTypes?.Count > 0)
                        PaymentType = PaymentTypes[0];


                    //if (supplier != null && supplier.ServiceType != null && Payment.PaymentType == null)
                    //{
                    //    if (PaymentType == null || !PaymentType.PaymentTypeForServices.Any(p => p.ServiceType == supplier.ServiceType))
                    //    {
                    //        PaymentTypeForService paymentTypeForService = supplier.ServiceType
                    //        .PaymentTypeForServices.FirstOrDefault();
                    //        if (paymentTypeForService != null)
                    //            PaymentType = paymentTypeForService.PaymentType;
                    //        // Debt.PaymentType = paymentTypeForService.PaymentType;
                    //    }
                    //}
                    //     FillLists();
                }
            }
        }

        private PaymentType paymentType;
        public PaymentType PaymentType
        {
            get { return paymentType; }
            set
            {
                if (paymentType != value)
                {
                    paymentType = value;
                    OnPropertyChanged("PaymentType");
                    Debt.PaymentType = PaymentType;
                }
            }
        }

        private Supplier supplier;
        public Supplier Supplier
        {
            get { return supplier; }
            set
            {
                if (supplier != value)
                {
                    supplier = value;
                    OnPropertyChanged("Supplier");
                    // FillLists();
                    FillProjectsBySender();
                    if (SenderFilter == SenderFilter.ByProject)
                    {
                        if (Flats != null && (Flat == null || !Flat.SupplierInProjects.Any(sInp => sInp.Supplier == supplier)))
                        {
                            Flat = Flats.FirstOrDefault(f => f.SupplierInProjects.Any(fInp => fInp.Supplier == Supplier));
                        }
                    }

                    // FillPaymentTypeBySupplier();
                    RefreshPaymentTypes();
                }
            }
        }

        private Project project;
        public Project Project
        {
            get { return project; }
            set
            {
                if (project != value)
                {
                    project = value;
                    OnPropertyChanged("Project");
                    FillListsByProject();
                }
            }
        }

        private Flat flat;
        public Flat Flat
        {
            get { return flat; }
            set
            {
                if (flat != value)
                {
                    flat = value;
                    OnPropertyChanged("Flat");

                    if (SenderFilter == SenderFilter.ByProject)
                    {
                        if (IsSupplierSender)
                        {
                            if (Suppliers != null && (Supplier == null || !Supplier.SupplierInProjects.Any(s => s.Flat == Flat)))
                            {
                                Supplier = Suppliers.FirstOrDefault(s => s.SupplierInProjects.Any(sInp => sInp.Flat == Flat));
                            }
                        }
                        else if (IsCustomerSender)
                        {
                            if (Customers != null && (Customer == null || !Customer.CustomerInProjects.Any(c => c.Flat == Flat)))
                            {
                                Customer = Customers.FirstOrDefault(c => c.CustomerInProjects.Any(cInp => cInp.Flat == Flat));
                            }
                        }
                    }
                    else
                    {
                        if (!DuringAutoChanges)
                            if (Projects != null && (Project == null || !Project.Flats.Contains(flat)))
                            {
                                Project = Projects.FirstOrDefault(p => p.Flats.Contains(flat));
                            }
                    }

                    if (!DuringAutoChanges && flat?.Price != null && Customer != null && flat.Project.ProjectTypeId == 2)
                    {
                        int amount = flat.Price.Value;
                        Amount = amount;
                    }
                }
            }
        }

        private bool duringAutoChanges = false;
        public bool DuringAutoChanges
        {
            get { return duringAutoChanges; }
            set
            {
                duringAutoChanges = value;
            }
        }

        private bool isCustomerSender = false;
        public bool IsCustomerSender
        {
            get { return isCustomerSender; }
            set
            {
                if (isCustomerSender != value)
                {
                    isCustomerSender = value;

                    OnPropertyChanged("IsCustomerSender");

                    if (isCustomerSender)
                    {
                        DisplaySenderFilter = true;
                    }
                }
            }
        }

        private bool isBankSender = false;
        public bool IsBankSender
        {
            get { return isBankSender; }
            set
            {
                if (isBankSender != value)
                {
                    isBankSender = value;

                    OnPropertyChanged("IsBankSender");

                    if (isBankSender)
                    {
                        DisplaySenderFilter = false;
                    }

                    //FillProjectsBySender();
                }
            }
        }

        private bool displaySenderFilter = false;
        public bool DisplaySenderFilter
        {
            get { return displaySenderFilter; }
            set
            {
                if (displaySenderFilter != value)
                {
                    displaySenderFilter = value;

                    OnPropertyChanged("DisplaySenderFilter");

                    if (!displaySenderFilter)
                        SenderFilter = SenderFilter.ByProject;

                }
            }
        }

        private bool isNoneSender = false;
        public bool IsNoneSender
        {
            get { return isNoneSender; }
            set
            {
                if (isNoneSender != value)
                {
                    isNoneSender = value;

                    OnPropertyChanged("IsNoneSender");

                    if (isNoneSender)
                    {
                        DisplaySenderFilter = false;
                    }
                }
            }
        }

        private bool isSupplierSender = false;
        public bool IsSupplierSender
        {
            get { return isSupplierSender; }
            set
            {
                if (isSupplierSender != value)
                {
                    isSupplierSender = value;

                    OnPropertyChanged("isSupplierSender");

                    if (isSupplierSender)
                    {
                        DisplaySenderFilter = true;
                    }
                }
            }
        }

        private ModernUri selectedSource;
        public ModernUri SelectedSource
        {
            get { return selectedSource; }
            set
            {
                selectedSource = value;
                OnPropertyChanged("SelectedSource");
            }
        }

        #endregion Properties

        #region Commands

        public ICommand OpenCustomerCommand { get; set; }
        private void OpenCustomerExecute(object parameter)
        {
            try
            {
                if (Customer != null)
                    RealEstateRepository.Instance.OpenEditor(Customer, EditorType.Customer);
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }
        private bool OpenCustomerCanExecute(object parameter)
        {
            return IsCustomerSender && Customer != null;
        }

        public ICommand OpenSupplierCommand { get; set; }
        private void OpenSupplierExecute(object parameter)
        {
            try
            {
                if (Supplier != null)
                    RealEstateRepository.Instance.OpenEditor(Supplier, EditorType.Supplier);
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }
        private bool OpenSupplierCanExecute(object parameter)
        {
            return IsSupplierSender && Supplier != null;
        }

        public ICommand OpenProjectCommand { get; set; }
        private void OpenProjectExecute(object parameter)
        {
            try
            {
                if (Project != null)
                {
                    EditorType editorType = Project.ProjectTypeId == 1 ? EditorType.SaleProject : EditorType.LeaseProject;
                    RealEstateRepository.Instance.OpenEditor(Project, editorType);
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }
        private bool OpenProjectCanExecute(object parameter)
        {
            return Project != null;
        }

        public ICommand OpenFlatCommand { get; set; }
        private void OpenFlatExecute(object parameter)
        {
            try
            {
                if (Flat != null)
                {
                    EditorType editorType = Flat.Project.ProjectTypeId == 1 ? EditorType.SaleFlat : EditorType.LeaseFlat;
                    RealEstateRepository.Instance.OpenEditor(Flat, editorType);
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }
        private bool OpenFlatCanExecute(object parameter)
        {
            return Flat != null;
        }

        #endregion Commands

        #region Methods

        public override void Init()
        {

            base.Init();
            InitLinks();
            //  EntityTitle = Debt.Amount.ToString();

            InitList(typeof(City));
            InitList(typeof(SenderType));
            InitList(typeof(PaymentRelation));
            InitList(typeof(Bank));

            PaymentRelation paymentRelation = Debt.PaymentRelation;

            if (IsEditEditor && Debt.PaymentRelation != null)
            {
                if (Debt.PaymentRelation.FromSenderTypeId == 4)
                {
                    DebtType = DebtType.Expense;
                }
                else
                {
                    DebtType = DebtType.Revenue;
                }
            }
            else
                DebtType = DebtType.Revenue;

            if (paymentRelation != null)
                PaymentRelation = paymentRelation;

            //      FillBySenderFilter();

            //if (PaymentRelation.ToSenderTypeId == 1)
            //{
            //    IsSupplierSender = true;
            //}
            //else if (PaymentRelation.FromSenderTypeId == 2 || PaymentRelation.FromSenderTypeId == 3)
            //{
            //    IsCustomerSender = true;
            //}

            if (Debt.CustomerInProject != null)
            {
                Customer = Debt.CustomerInProject.Customer;
            }
            if (Debt.SupplierInProject != null)
            {
                Supplier = Debt.SupplierInProject.Supplier;
            }
            PaymentType = Debt.PaymentType;
            Project = GetProject();
            Flat = GetFlat();

            if (!IsEditEditor)
            {
                Debt.CreatedDate = DateTime.Now;
                Debt.AmountPaid = 0;
                Debt.DelinquentAmount = 0;
                Debt.DueDate = DateTime.Now.AddMonths(1);
            }

            Amount = Debt.Amount.HasValue ? Debt.Amount.Value : 0;
            AmountPaid = Debt.AmountPaid.HasValue ? Debt.AmountPaid.Value : 0;
            Debt.CalculateDebtAmount();

            OnPropertyChanged(null);

        }

        private void FillPaymentTypeBySupplier()
        {
            if (supplier != null && supplier.ServiceType != null && Debt.PaymentType == null)
            {
                if (PaymentType == null || !PaymentType.PaymentTypeForServices.Any(p => p.ServiceType == supplier.ServiceType))
                {
                    PaymentTypeForService paymentTypeForService = supplier.ServiceType
                    .PaymentTypeForServices.FirstOrDefault();
                    if (paymentTypeForService != null)
                        PaymentType = paymentTypeForService.PaymentType;
                    else if (PaymentTypes?.Count > 0)
                        PaymentType = PaymentTypes[0];
                }
            }
        }


        private void SetPaymentSender(int senderId)
        {
            switch (senderId)
            {
                case 1:
                    SetSupplierSender();
                    break;
                case 2:
                case 3:
                    SetCustomerSender();
                    break;
                case 4:
                    break;
                case 5:
                    SetBankSender();
                    break;
                default:
                    SetNoneSender();
                    break;
            }
        }

        private void SetNoneSender()
        {
            IsSupplierSender = false;
            IsCustomerSender = false;
            IsBankSender = false;
            IsNoneSender = true;
        }

        private void SetSupplierSender()
        {
            IsCustomerSender = false;
            IsBankSender = false;
            IsNoneSender = false;
            IsSupplierSender = true;
        }

        private void SetCustomerSender()
        {
            IsSupplierSender = false;
            IsBankSender = false;
            IsNoneSender = false;
            IsCustomerSender = true;
        }

        private void SetBankSender()
        {
            IsCustomerSender = false;
            IsSupplierSender = false;
            IsNoneSender = false;
            IsBankSender = true;
        }

        public Project GetProject()
        {
            return Debt.Project;
            //return Debt.CustomerInProject != null ?
            //     Debt.CustomerInProject.Project : Debt.SupplierInProject != null ?
            //     Debt.SupplierInProject.Project : null;
        }

        public Flat GetFlat()
        {
            return Debt.Flat;
            //return Debt.CustomerInProject != null ?
            //     Debt.CustomerInProject.Flat : Debt.SupplierInProject != null ?
            //     Debt.SupplierInProject.Flat : null;
        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (Project == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר פרויקט";
                return beforeSaveResult;
            }

            if (IsCustomerSender)
            {
                if (Customer == null)
                {
                    beforeSaveResult.IsValidData = false;
                    beforeSaveResult.ErrorMessage = "נא הגדר לקוח";
                    return beforeSaveResult;
                }

                IList<CustomerInProject> customerInProjects = new CustomersBL().GetCustomerInProjects();
                CustomerInProject customerInProject = customerInProjects.FirstOrDefault(cInP =>
                cInP.Customer == Customer && cInP.Project == Project && cInP.Flat == Flat);

                if (customerInProject != null)
                {
                    Debt.CustomerInProject = customerInProject;
                }
                else
                {
                    Debt.CustomerInProject = new CustomerInProject() { Customer = Customer, Project = Project, Flat = Flat };
                }

                Debt.SupplierInProject = null;
            }
            else if (IsSupplierSender)
            {
                if (Supplier == null)
                {
                    beforeSaveResult.IsValidData = false;
                    beforeSaveResult.ErrorMessage = "נא הגדר ספק";
                    return beforeSaveResult;
                }

                IList<SupplierInProject> supplierInProjects = new SuppliersBL().GetSupplierInProjects();
                SupplierInProject supplierInProject = supplierInProjects.FirstOrDefault(sInP =>
                sInP.Supplier == Supplier && sInP.Project == Project && sInP.Flat == Flat);

                if (supplierInProject != null)
                    Debt.SupplierInProject = supplierInProject;
                else
                    Debt.SupplierInProject = new SupplierInProject() { Supplier = Supplier, Project = Project, Flat = Flat };

                Debt.CustomerInProject = null;
            }

            Debt.Project = Project;
            Debt.Flat = Flat;

            if (Debt.Amount == null || Debt.Amount == 0)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר סכום";
                return beforeSaveResult;
            }

            if (Debt.DueDate == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר תאריך יעד";
                return beforeSaveResult;
            }

            return beforeSaveResult;
        }

        private void FillListsByProject()
        {
            if (Project != null)
            {
                if (SenderFilter == SenderFilter.ByProject)
                {
                    Customers = Project.CustomerInProjects.Select(project => project.Customer).Distinct().ToList();
                    Suppliers = Project.SupplierInProjects.Where(sInP => sInP != null && sInP.Supplier != null).Select(project => project.Supplier).Distinct().ToList();
                    Flats = Project.Flats.ToList();
                    //Flats = Project.Flats.Where(flat => flat.CustomerInProjects?.Count > 0).ToList();
                }
                else
                {
                    if (IsCustomerSender)
                    {
                        Flats = Project.Flats.Where(flat => flat.CustomerInProjects.Any(cInP => cInP.Customer == Customer)).ToList();
                    }
                    else if(IsSupplierSender)
                    {
                        Flats = Project.Flats.Where(flat => flat.SupplierInProjects.Any(cInP => cInP.Supplier == Supplier)).ToList();
                    }
                }


                if (!DuringAutoChanges)
                    if (Flat == null && Flats != null && Flats.Count > 0)
                        Flat = Flats.FirstOrDefault(flat => flat.CustomerInProjects?.Count > 0);
            }
            else
            {
                Flats = null;

                if (SenderFilter == SenderFilter.ByProject)
                {
                    Customers = null;
                    Suppliers = null;
                }
            }
        }

        private void FillBySenderFilter()
        {
            if (FromSenderType.Id == 2 || toSenderType.Id == 2)
                ProjectTypeId = 1;
            else if (FromSenderType.Id == 3 || toSenderType.Id == 3)
                ProjectTypeId = 2;
            else
                ProjectTypeId = 1;

            if (SenderFilter == SenderFilter.BySender)
            {
                if (IsSupplierSender)
                    InitList(typeof(Supplier));
                else
                {
                    Customers = new CustomersBL().GetCustomers().Where(customer => customer.CustomerInProjects
                    .Any(cInP => cInP?.Project?.ProjectTypeId == ProjectTypeId)).ToList();
                }
                FillProjectsBySender();
            }
            else
            {
                if (IsCustomerSender)
                    Projects = new ProjectsBL().GetProjects().Where(project =>
                      project.ProjectTypeId == ProjectTypeId).ToList();
                else
                {
                    InitList(typeof(Project));
                }
                FillListsByProject();
            }
        }

        public override void RefreshData()
        {
            base.RefreshData();
            OnPropertyChanged(null);

        }

        public override void RefreshEntityTitle()
        {
            string senderName = string.Empty;
            if (isCustomerSender)
            {
                if (Customer != null)
                    senderName = $"{Customer.Name} {Customer.Family}";
            }
            else
            {
                if (Supplier != null)
                    senderName = $"{Supplier.Name} {Supplier.Family}";

            }
            string projectDescription = Project != null ? Project.Name : "";

            projectDescription += Flat != null ? $", דירה {Flat.FlatNumber}" : "";
            EntityTitle = $"{Debt.Amount}   {senderName}, {projectDescription}";

            base.RefreshEntityTitle();
        }

        private void FillProjectsBySender()
        {
            if (SenderFilter == SenderFilter.BySender)
            {
                bool isProjectsAssigned = false;
                if (IsCustomerSender)
                {
                    if (Customer != null)
                    {
                        Projects = Customer.CustomerInProjects.Where(cInP => cInP.Project?.ProjectTypeId == ProjectTypeId.Value).Select(cInP => cInP.Project).Distinct().ToList();
                        Flats = Customer.CustomerInProjects.Where(cInP => cInP.Project != null && cInP.Flat?.Project?.ProjectTypeId == ProjectTypeId.Value).Select(customer => customer.Flat).Distinct().ToList();
                        isProjectsAssigned = true;

                        if (!DuringAutoChanges)
                            if (Flat == null && Flats != null && Flats.Count > 0)
                                Flat = Flats.FirstOrDefault(f => f.CustomerInProjects.Any(cInp => cInp.Customer == Customer));
                    }
                    else
                    {
                        Projects = null;
                        Flats = null;
                    }
                }
                else if (IsSupplierSender)
                {
                    if (Supplier != null)
                    {
                        Projects = Supplier.SupplierInProjects.Where(sInP => sInP.Project != null).Select(supplier => supplier.Project).Distinct().ToList();
                        Flats = Supplier.SupplierInProjects.Where(sInP => sInP.Project != null && sInP.Flat != null).Select(supplier => supplier.Flat).Distinct().ToList();
                        isProjectsAssigned = true;

                        if (!DuringAutoChanges)
                            if (Flat == null && Flats != null && Flats.Count > 0)
                                Flat = Flats.FirstOrDefault(f => f.SupplierInProjects.Any(cInp => cInp.Supplier == Supplier));
                    }
                    else
                    {
                        Projects = null;
                        Flats = null;
                    }
                }

                if (!isProjectsAssigned)
                {
                    Projects = null;
                    Flats = null;
                }

                //if (Flat == null && Flats != null && Flats.Count > 0)
                //    Flat = Flats[0];

                if (!DuringAutoChanges)
                    if (Project == null && Projects != null && Projects.Count > 0)
                    {
                        if (Flat != null)
                            Project = Flat.Project;
                        else
                            Project = Projects[0];
                    }
            }
        }

        private void InitLinks()
        {
            Links = new LinkCollection();

            string UriString = "/Views/Debts/DebtDetails/DebtMainDetails.xaml";
            UriString = $"{UriString}#{EditorMetaData.PageType}${EditorKey}$Details";
            OpenedEditors.Add("Details", this);
            Link link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "פרטים", ViewModel = this };
            Links.Add(link);

            selectedSource = new ModernUri(UriString, UriKind.Relative);

            //AddPaymentsLink();
            AddPaymentItemsLink();
            //link = new Link() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "תשלומים" };
            //Links.Add(link);

            //      UriString = "/Views/Flats/FlatDetails/FlatCustomers.xaml";
            //    UriString = "/Views/Customers/CustomersTable.xaml";
            //link = new Link() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "פרויקטים" };
            //Links.Add(link);

            //link = new Link() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "דירות" };
            //Links.Add(link);

            //link = new Link() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "קבצים" };
            //Links.Add(link);
        }

        private void RefreshPaymentTypes()
        {
            PaymentTypes = GetPaymentItems(paymentRelation);
            if (PaymentTypes?.Count > 0)
            {
                PaymentType = PaymentTypes[0];
            }
        }

        private IList<PaymentType> GetPaymentItems(PaymentRelation paymentRelation)
        {
            IList<PaymentType> paymentTypes = null;

            if (paymentRelation != null)
            {
                if (paymentRelation.ToSenderType.Id == 1)
                {
                    if (Supplier?.ServiceType != null)//ספק
                    {
                        paymentTypes = Supplier.ServiceType.PaymentTypeForServices.Select
                            (paymentTypeForService => paymentTypeForService.PaymentType).ToList();
                        PaymentType generalPaymentType = paymentRelation.PaymentTypes.FirstOrDefault
                            (paymentType => paymentType.Name == "כללי");
                        if (generalPaymentType != null)
                        {
                            paymentTypes.Add(generalPaymentType);
                        }
                    }
                }
                else
                {
                    paymentTypes = paymentRelation.PaymentTypes.ToList();
                }
            }

            if (paymentTypes != null)
            {
                paymentTypes = paymentTypes.OrderBy(paymentType => paymentType.Order).ToList();
            }

            return paymentTypes;
        }

        private void AddPaymentItemsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.PaymentItems, this, "PaymentItems", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.DisplayCommands = false;
            //tableViewModel.DisplayCommands = false;

            tableViewModel.InitListSource = () =>
            {
                return Debt.PaymentItems.ToList();
            };

            tableViewModel.Fields = new List<ColumnMetadata>(tableViewModel.TableEditorMetadata.Fields);

            tableViewModel.Fields.RemoveAt(6);
            tableViewModel.Fields.RemoveAt(5);
            tableViewModel.Fields.RemoveAt(4);

            tableViewModel.Init();
        }

        //private void AddPaymentsLink()
        //{
        //    TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllPayments, this, "Payments", false) as TableViewModel;
        //    tableViewModel.EntityTitle = EntityTitle;
        //    tableViewModel.IsSubEditor = false;
        //    //tableViewModel.AfterAddEntity = (entity) =>
        //    //{
        //    //    Payment payment = entity as Payment;
        //    //    if (payment.CustomerInProject != null && payment.CustomerInProject.Project == null)
        //    //    {
        //    //        payment.CustomerInProject.Project = Project;
        //    //    }
        //    //    if (payment.SupplierInProject != null && payment.SupplierInProject.Project == null)
        //    //    {
        //    //        payment.SupplierInProject.Project = Project;
        //    //    }
        //    //};

        //    //tableViewModel.DetachEntity = (entity) =>
        //    //{
        //    //    Payment payment = entity as Payment;

        //    //    if (payment.SupplierInProject != null)
        //    //    {
        //    //        payment.SupplierInProject = null;
        //    //    }
        //    //    if (payment.CustomerInProject != null)
        //    //    {
        //    //        payment.CustomerInProject = null;
        //    //    }
        //    //};

        //    //tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
        //    //{
        //    //    PaymentViewModel paymentViewModel = editorViewModel as PaymentViewModel;

        //    //    paymentViewModel.SenderFilter = SenderFilter.ByProject;
        //    //    paymentViewModel.Project = Project;
        //    //};
        //    tableViewModel.Fields.RemoveAt(6);
        //    tableViewModel.Fields.RemoveAt(6);
        //    tableViewModel.Fields.RemoveAt(6);
        //    tableViewModel.Fields.RemoveAt(6);

        //    tableViewModel.InitialFilter = (obj) =>
        //    {
        //        Payment payment = obj as Payment;

        //        return payment.PaymentItems.Any(paymentItem => paymentItem.Debt == Debt); ;
        //    };
        //    tableViewModel.Init();
        //    //tableViewModel.RemoveGroupDescription(".");
        //    //     tableViewModel.AddGroupDescription("ProjectName");
        //}

        #endregion Methods
    }
}
