using FirstFloor.ModernUI.Presentation;
using log4net;
using MaterialDesignThemes.Wpf;
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
    public class ProjectViewModel : EditorViewModel
    {
        ILog log = LogManager.GetLogger(typeof(ProjectViewModel));
        #region Ctor

        public ProjectViewModel()
        {
            InitList(typeof(ProjectType));
            //InitList(typeof(City));
            InitList(typeof(Country));
        }

        #endregion Ctor

        #region Properties

        public Project Project
        {
            get { return Entity as Project; }
            set { Entity = value; }
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

        protected virtual int ProjectTypeId { get; }
        protected virtual EditorType FlatsEditor { get; }

        #endregion Properties

        #region Methods

        public override void Init()
        {
            base.Init();
            InitLinks();

            if (!IsEditEditor)
            {
                Project.ProjectType = ProjectTypes.FirstOrDefault(projectType => projectType.Id == ProjectTypeId);
                Project.Country = Countries.FirstOrDefault(country => country.Name == "ישראל");
            }

            Project.PropertyChanged += Project_PropertyChanged;
            SetCitiesByCountry();
        }

        private void Project_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Country")
            {
                SetCitiesByCountry();
            }
        }

        private void SetCitiesByCountry()
        {
            Cities = Project.Country.Cities.OrderBy(city => city.Name).ToList();
        }

        public override void RefreshEntityTitle()
        {
            EntityTitle = Project.Name;
            base.RefreshEntityTitle();
        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (string.IsNullOrEmpty(Project.Name))
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר שם פרויקט";
                return beforeSaveResult;
            }

            RefreshMapLink(Project.Country, Project.City, Project.Street, Convert.ToString(Project.HouseNumber));

            return beforeSaveResult;
        }

        private void InitLinks()
        {
            Links = new LinkCollection();

            string UriString = "/Views/Projects/ProjectDetails/ProjectMainDetails.xaml";
            UriString = $"{UriString}#{EditorMetaData.PageType}${EditorKey}$Details";
            OpenedEditors.Add("Details", this);
            Link link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "פרטים", ViewModel = this };
            Links.Add(link);

            selectedSource = new ModernUri(UriString, UriKind.Relative);

            AddFlatsLink();
            AddDebtsLink();
            AddPaymentsLink();

            if (Project.ProjectTypeId == 1)
                AddConstantPaymentsLink();

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
                          PaymentsUtils.OpenAddPayment(Project, null, customer, null, fromSenderTypeId, 4);
                      else if (editorType == EditorType.DebtNew)
                          PaymentsUtils.OpenAddDebt(Project, null, customer, null, fromSenderTypeId, 4);
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
                          PaymentsUtils.OpenAddPayment(Project, null, null, supplier, 4, 1);
                      else if (editorType == EditorType.DebtNew)
                          PaymentsUtils.OpenAddDebt(Project, null, null, supplier, 4, 1);
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
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                CustomerInProject customerInProject = obj as CustomerInProject;

                return customerInProject != null && customerInProject.Project == Project;
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
                if (!customer.CustomerInProjects.Any(cInP => cInP.Project == Project))
                {
                    CustomerInProject customerInProject = EntityCreatorUtils.NewCustomerInProject(customer, Project, null);
                    customer.CustomerInProjects.Add(customerInProject);

                    if (Project.ProjectTypeId == 1 && Project.ConstantPayments != null)
                    {
                        foreach (var constantPayment in Project.ConstantPayments)
                        {
                            if (constantPayment != null)
                            {
                                AddDebtByConstantPayment(customerInProject, constantPayment);
                            }
                        }
                    }
                }
            };

            tableViewModel.DetachEntity = (entity) =>
            {
                Customer customer = entity as Customer;

                CustomerInProject customerInProject = customer.CustomerInProjects.FirstOrDefault(cInP => cInP.Project == Project);

                if (customerInProject != null)
                    new GeneralBL().DeleteEntity(customerInProject);
            };

            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת תשלום", GetAddCustomerPaymentCommand(EditorType.PaymentNew), PackIconKind.Database));
            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת חוב", GetAddCustomerPaymentCommand(EditorType.DebtNew), PackIconKind.CodeNotEqual));

            tableViewModel.InitialFilter = (obj) => { return ((Customer)obj).CustomerInProjects.Any(cInP => cInP.Project == Project); };
            tableViewModel.Init();
        }

        private void AddFlatsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(FlatsEditor, this, "Flats", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;
            tableViewModel.EnableGrouping = false;

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                FlatViewModel flatViewModel = editorViewModel as FlatViewModel;

                flatViewModel.Project = Project;
            };
            //tableViewModel.AfterAddEntity = (entity) =>
            //{
            //    Supplier supplier = entity as Supplier;
            //    if (!supplier.SupplierInProjects.Any(cInP => cInP.Project == Project))
            //    {
            //        supplier.SupplierInProjects.Add(new SupplierInProject() { Supplier = supplier, Project = Project });
            //    }
            //};

            //tableViewModel.DetachEntity = (entity) =>
            //{
            //    Supplier supplier = entity as Supplier;

            //    SupplierInProject supplierInProject = supplier.SupplierInProjects.FirstOrDefault(cInP => cInP.Project == Project);

            //    if (supplierInProject != null)
            //        new GeneralBL().DeleteEntity(supplierInProject);
            //};

            tableViewModel.InitialFilter = (obj) => { return ((Flat)obj).Project == Project; };
            tableViewModel.Init();
        }

        private void AddSuppliersLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllSuppliers, this, "Suppliers", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.AfterAddEntity = (entity) =>
            {
                Supplier supplier = entity as Supplier;
                if (!supplier.SupplierInProjects.Any(cInP => cInP.Project == Project))
                {
                    supplier.SupplierInProjects.Add(new SupplierInProject() { Supplier = supplier, Project = Project });
                }
            };

            tableViewModel.DetachEntity = (entity) =>
            {
                Supplier supplier = entity as Supplier;

                SupplierInProject supplierInProject = supplier.SupplierInProjects.FirstOrDefault(cInP => cInP.Project == Project);

                if (supplierInProject != null)
                    new GeneralBL().DeleteEntity(supplierInProject);
            };

            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת תשלום", GetAddSupplierPaymentCommand(EditorType.PaymentNew), PackIconKind.Database));
            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת חוב", GetAddSupplierPaymentCommand(EditorType.DebtNew), PackIconKind.CodeNotEqual));

            tableViewModel.InitialFilter = (obj) => { return ((Supplier)obj).SupplierInProjects.Any(cInP => cInP.Project == Project); };
            tableViewModel.Init();
        }

        private void AddPaymentsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllPayments, this, "Payments", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;

            //tableViewModel.DetachEntity = (entity) =>
            //{
            //    Payment payment = entity as Payment;

            //    if (payment.SupplierInProject != null)
            //    {
            //        payment.SupplierInProject = null;
            //    }
            //    if (payment.CustomerInProject != null)
            //    {
            //        payment.CustomerInProject = null;
            //    }
            //};

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                PaymentViewModel paymentViewModel = editorViewModel as PaymentViewModel;

                paymentViewModel.SenderFilter = SenderFilter.ByProject;
                paymentViewModel.Project = Project;

                int fromSenderTypeId = Project?.ProjectTypeId == 1 ? 2 : 3;
                paymentViewModel.FromSenderType = new GeneralBL().GetSenderTypes().FirstOrDefault(senderType => senderType.Id == fromSenderTypeId);
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Payment payment = obj as Payment;

                return payment.CustomerInProject != null && payment.CustomerInProject.Project == Project
                || payment.SupplierInProject != null && payment.SupplierInProject.Project == Project;
            };
            tableViewModel.Init();
            tableViewModel.RemoveGroupDescription(".");
            //     tableViewModel.AddGroupDescription("ProjectName");
        }

        private void AddDebtsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllDebts, this, "Debts", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;

            //tableViewModel.DetachEntity = (entity) =>
            //{
            //    Debt debt = entity as Debt;

            //    if (debt.SupplierInProject != null)
            //    {
            //        debt.SupplierInProject = null;
            //    }
            //    if (debt.CustomerInProject != null)
            //    {
            //        debt.CustomerInProject = null;
            //    }
            //};

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                DebtViewModel debtViewModel = editorViewModel as DebtViewModel;

                debtViewModel.SenderFilter = SenderFilter.ByProject;
                debtViewModel.Project = Project;
                int fromSenderTypeId = Project?.ProjectTypeId == 1 ? 2 : 3;
                debtViewModel.FromSenderType = new GeneralBL().GetSenderTypes().FirstOrDefault(senderType => senderType.Id == fromSenderTypeId);
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Debt debt = obj as Debt;

                return debt.CustomerInProject != null && debt.CustomerInProject.Project == Project
                || debt.SupplierInProject != null && debt.SupplierInProject.Project == Project;
            };
            tableViewModel.Init();
            tableViewModel.RemoveGroupDescription(".");
            //     tableViewModel.AddGroupDescription("ProjectName");
        }

        private void AddDebtByConstantPayment(CustomerInProject customerInProject, ConstantPayment constantPayment)
        {
            if (customerInProject?.Debts != null)
                customerInProject.Debts.Add(new Debt()
                {
                    Amount = constantPayment.Amount,
                    CreatedDate = DateTime.Now,
                    DueDate = constantPayment.DueDate,
                    PaymentRelationId = 2,
                    PaymentType = constantPayment.PaymentType,
                    AmountPaid = 0,
                    DelinquentAmount = constantPayment.Amount
                }
                );
        }

        private void AddConstantPaymentsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.ConstantPayments, this, "ConstantPayments", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;

            tableViewModel.AfterAddEntity = (entity) =>
            {
                ConstantPayment constantPayment = entity as ConstantPayment;
                constantPayment.Project = Project;

                if (Project.CustomerInProjects != null)
                    foreach (var customerInProject in Project.CustomerInProjects)
                    {
                        AddDebtByConstantPayment(customerInProject, constantPayment);
                    }
            };

            tableViewModel.InitListSource = () =>
            {
                return Project.ConstantPayments.ToList();
            };
            tableViewModel.Init();
        }

        #endregion Methods
    }
}
