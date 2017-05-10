using FirstFloor.ModernUI.Presentation;
using log4net;
using MaterialDesignThemes.Wpf;
using RealEstate.BL;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealEstate
{
    public class FlatViewModel : EditorViewModel
    {
        ILog log = LogManager.GetLogger(typeof(CustomerConverter));
        #region Properties

        protected virtual int ProjectTypeId { get; }

        public Flat Flat
        {
            get { return Entity as Flat; }
            set { Entity = value; }
        }

        public virtual string CustomersTitle
        {
            get;
        }

        public string CustomersMainTitle
        {
            get
            {
                return "רשימת " + CustomersTitle;
            }
        }

        private ProjectType projectType;
        public ProjectType ProjectType
        {
            get { return projectType; }
            set
            {
                if (projectType != value)
                {
                    projectType = value;
                    OnPropertyChanged("ProjectType");
                    Projects = new ProjectsBL().GetProjects().Where(project => project.ProjectType == projectType).ToList();
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
                    Flat.Project = project;
                    if (project?.ProjectType != null)
                    {
                        ProjectType = project.ProjectType;
                    }
                    else
                    {
                        ProjectType = null;
                    }
                }
            }
        }


        #endregion

        #region Ctor

        public FlatViewModel()
        {
            InitList(typeof(City));
            InitList(typeof(ProjectType));
            InitList(typeof(Status));
        }

        public override void RefreshData()
        {
            base.RefreshData();
            ProjectType = Flat.Project?.ProjectType != null ? Flat.Project.ProjectType : ProjectTypes[0];
            Project = Flat.Project;
            OnPropertyChanged(null);

        }

        public override void Init()
        {
            base.Init();

            if (!IsEditEditor)
            {
                ProjectType = ProjectTypes.FirstOrDefault(projectType => projectType.Id == ProjectTypeId);
                Flat.Status = new GeneralBL().GetStatuses().FirstOrDefault(status => status.Id == 1);
            }
            else
            {
                RefreshData();
            }

            InitLinks();
        }

        public override void RefreshEntityTitle()
        {
            string projectDescription = Flat.Project != null ? Flat.Project.Name : "ללא פרויקט";
            EntityTitle = $"{projectDescription}, דירה {Flat.FlatNumber}";
            base.RefreshEntityTitle();
        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (Flat.Project == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר פרויקט";
                return beforeSaveResult;
            }

            if (Flat.FlatNumber == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר מס' דירה";
                return beforeSaveResult;
            }

            if (Flat.Price == null)
            {
                beforeSaveResult.IsValidData = false;
                if (ProjectTypeId == 1)
                {
                    beforeSaveResult.ErrorMessage = "נא הגדר מחיר דירה";
                }
                else
                {
                    beforeSaveResult.ErrorMessage = "נא הגדר דמי שכירות";
                }
                return beforeSaveResult;
            }

            RefreshMapLink(Project.Country, Project.City, Project.Street, Convert.ToString(Project.HouseNumber));

            return beforeSaveResult;
        }

        #endregion

        #region Methods

        private void InitLinks()
        {
            Links = new LinkCollection();

            string UriString = "/Views/Flats/FlatDetails/FlatMainDetails.xaml";
            UriString = $"{UriString}#{EditorMetaData.PageType}${EditorKey}$Details";
            OpenedEditors.Add("Details", this);
            Link link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "פרטים", ViewModel = this };
            Links.Add(link);

            SelectedSource = new ModernUri(UriString, UriKind.Relative);

            AddDebtsLink();
            AddPaymentsLink();
            AddContractsLink();
            AddCustomersLink();
            AddSuppliersLink();

            if (IsEditEditor)
                AddMapLink(Project.Country, Project.City, Project.Street, Project.HouseNumber);

            link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "קבצים", ViewModel = new TableViewModel() };
            Links.Add(link);
        }

        private ICommand GetAddCustomerPaymentCommand(EditorType editorType)
        {
            ICommand addPaymentCommand = new RelayCommand(
              (parameter) =>
              {
                  try
                  {
                      Customer customer = (OpenedEditors["Customers"] as TableViewModel).SelectedItems[0] as Customer;
                      int fromSenderTypeId = Project.ProjectTypeId == 1 ? 2 : 3;

                      if (editorType == EditorType.PaymentNew)
                          PaymentsUtils.OpenAddPayment(Project, Flat, customer, null, fromSenderTypeId, 4);
                      else if (editorType == EditorType.DebtNew)
                          PaymentsUtils.OpenAddDebt(Project, Flat, customer, null, fromSenderTypeId, 4);
                  }
                  catch (Exception ex)
                  {
                      log.HandleError(ex);
                  }
              }, (parameter) =>
              {
                  return (OpenedEditors["Customers"] as TableViewModel).SelectedItems.Count == 1;
              }
               );

            return addPaymentCommand;
        }

        private ICommand GetAddSupplierPaymentCommand(EditorType editorType)
        {
            ICommand addPaymentCommand = new RelayCommand(
              (parameter) =>
              {
                  try
                  {
                      Supplier supplier = (OpenedEditors["Suppliers"] as TableViewModel).SelectedItems[0] as Supplier;

                      if (editorType == EditorType.PaymentNew)
                          PaymentsUtils.OpenAddPayment(Project, Flat, null, supplier, 4, 1);
                      else if (editorType == EditorType.DebtNew)
                          PaymentsUtils.OpenAddDebt(Project, Flat, null, supplier, 4, 1);
                  }
                  catch (Exception ex)
                  {
                      log.HandleError(ex);
                  }
              }, (parameter) =>
              {
                  return (OpenedEditors["Suppliers"] as TableViewModel).SelectedItems.Count == 1;
              }
               );

            return addPaymentCommand;
        }

        private void AddContractsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllContracts, this, "Contracts", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                ContractViewModel contractViewModel = editorViewModel as ContractViewModel;

                contractViewModel.Project = Project;
                contractViewModel.Contract.Flat = Flat;
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                CustomerInProject customerInProject = obj as CustomerInProject;

                return customerInProject != null && customerInProject.Flat == Flat;
            };

            tableViewModel.Init();
        }

        private void AddCustomersLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllCustomers, this, "Customers", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.AfterAddEntity = (entity) =>
            {
                Customer customer = entity as Customer;
                if (!customer.CustomerInProjects.Any(cInP => cInP.Flat == Flat))
                {
                    customer.CustomerInProjects.Add(EntityCreatorUtils.NewCustomerInProject(customer, Flat.Project, Flat));
                }
            };

            tableViewModel.DetachEntity = (entity) =>
            {
                Customer customer = entity as Customer;

                CustomerInProject customerInProject = customer.CustomerInProjects.FirstOrDefault(cInP => cInP.Flat == Flat);

                if (customerInProject != null)
                    customer.CustomerInProjects.Remove(customerInProject);
            };

            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת תשלום", GetAddCustomerPaymentCommand(EditorType.PaymentNew), PackIconKind.Database));
            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת חוב", GetAddCustomerPaymentCommand(EditorType.DebtNew), PackIconKind.CodeNotEqual));

            tableViewModel.InitialFilter = (obj) => { return ((Customer)obj).CustomerInProjects.Any(cInP => cInP.Flat == Flat); };
            tableViewModel.Init();
        }

        private void AddSuppliersLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllSuppliers, this, "Suppliers", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.AfterAddEntity = (entity) =>
            {
                Supplier supplier = entity as Supplier;
                if (!supplier.SupplierInProjects.Any(cInP => cInP.Flat == Flat))
                {
                    supplier.SupplierInProjects.Add(new SupplierInProject() { Supplier = supplier, Project = Flat.Project, Flat = Flat });
                }
            };

            tableViewModel.DetachEntity = (entity) =>
            {
                Supplier supplier = entity as Supplier;

                SupplierInProject supplierInProject = supplier.SupplierInProjects.FirstOrDefault(cInP => cInP.Flat == Flat);

                if (supplierInProject != null)
                    supplier.SupplierInProjects.Remove(supplierInProject);
            };

            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת תשלום", GetAddSupplierPaymentCommand(EditorType.PaymentNew), PackIconKind.Database));
            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת חוב", GetAddSupplierPaymentCommand(EditorType.DebtNew), PackIconKind.CodeNotEqual));

            tableViewModel.InitialFilter = (obj) => { return ((Supplier)obj).SupplierInProjects.Any(cInP => cInP.Flat == Flat); };
            tableViewModel.Init();
        }

        private void AddPaymentsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllPayments, this, "Payments", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                PaymentViewModel paymentViewModel = editorViewModel as PaymentViewModel;

                paymentViewModel.SenderFilter = SenderFilter.ByProject;
                paymentViewModel.Project = Project;
                paymentViewModel.Flat = Flat;

                int fromSenderTypeId = Project?.ProjectTypeId == 1 ? 2 : 3;
                paymentViewModel.FromSenderType = new GeneralBL().GetSenderTypes().FirstOrDefault(senderType => senderType.Id == fromSenderTypeId);
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Payment payment = obj as Payment;

                return payment.CustomerInProject != null && payment.CustomerInProject.Project == Flat.Project && payment.CustomerInProject.Flat == Flat
                || payment.SupplierInProject != null && payment.SupplierInProject.Project == Flat.Project && payment.SupplierInProject.Flat == Flat;
            };
            tableViewModel.Init();
            tableViewModel.RemoveGroupDescription(".");
        }

        private void AddDebtsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllDebts, this, "Debts", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                DebtViewModel debtViewModel = editorViewModel as DebtViewModel;

                debtViewModel.SenderFilter = SenderFilter.ByProject;
                debtViewModel.Project = Project;
                debtViewModel.Flat = Flat;
                int fromSenderTypeId = Project?.ProjectTypeId == 1 ? 2 : 3;
                debtViewModel.FromSenderType = new GeneralBL().GetSenderTypes().FirstOrDefault(senderType => senderType.Id == fromSenderTypeId);
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Debt debt = obj as Debt;

                return debt.CustomerInProject != null && debt.CustomerInProject.Project == Flat.Project && debt.CustomerInProject.Flat == Flat
                || debt.SupplierInProject != null && debt.SupplierInProject.Project == Flat.Project && debt.SupplierInProject.Flat == Flat;
            };
            tableViewModel.Init();
            tableViewModel.RemoveGroupDescription(".");
        }

        #endregion Methods
    }
}
