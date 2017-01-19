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
    public class CustomerViewModel : EditorViewModel
    {
        ILog log = LogManager.GetLogger(typeof(CustomerViewModel));

        #region Ctor

        public CustomerViewModel()
        {
            InitList(typeof(City));
            InitList(typeof(Country));
            InitList(typeof(Gender));
        }

        #endregion Ctor

        #region Properties

        public Customer Customer
        {
            get { return Entity as Customer; }
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

        #endregion Properties

        #region Methods



        public override void Init()
        {
            base.Init();
            InitLinks();

            if (!IsEditEditor)
            {
                Customer.Country = Countries.FirstOrDefault(country => country.Name == "ישראל");
                Customer.Gender = Genders.FirstOrDefault(gender => gender.Name == "זכר");
            }
        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (string.IsNullOrEmpty(Customer.Name))
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר שם פרטי";
                return beforeSaveResult;
            }

            if (string.IsNullOrEmpty(Customer.Family))
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר שם משפחה";
                return beforeSaveResult;
            }

            RefreshMapLink(Customer.Country, Customer.City, Customer.Street, Convert.ToString(Customer.HouseNumber));

            return beforeSaveResult;
        }

        private void InitLinks()
        {
            Links = new LinkCollection();

            string UriString = "/Views/Customers/CustomerDetails/CustomerMainDetails.xaml";
            UriString = $"{UriString}#{EditorMetaData.PageType}${EditorKey}$Details";
            OpenedEditors.Add("Details", this);

            Link link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "פרטים", ViewModel = this };
            Links.Add(link);

            selectedSource = new ModernUri(UriString, UriKind.Relative);

            AddDebtsLink();
            AddPaymentsLink();
            AddContractsLink();
            AddProjectsLink();
            AddFlatsLink();

            if (IsEditEditor)
                AddMapLink(Customer.Country, Customer.City, Customer.Street, Convert.ToString(Customer.HouseNumber));

            link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "קבצים", ViewModel = new TableViewModel() };
            Links.Add(link);

            //link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "מפה" };
            //Links.Add(link);
        }

        public override void RefreshEntityTitle()
        {
            EntityTitle = $"{Customer.Name} {Customer.Family}";
            base.RefreshEntityTitle();
        }

        private ICommand GetAddProjectPaymentCommand(EditorType editorType)
        {
            ICommand addPaymentCommand = new RelayCommand(
              (parameter) =>
              {
                  try
                  {
                      Project project = (OpenedEditors["Projects"] as TableViewModel).SelectedItems[0] as Project;
                      int fromSenderTypeId = project.ProjectTypeId == 1 ? 2 : 3;

                      if (editorType == EditorType.PaymentNew)
                          PaymentsUtils.OpenAddPayment(project, null, Customer, null, fromSenderTypeId, 4);
                      else if (editorType == EditorType.DebtNew)
                          PaymentsUtils.OpenAddDebt(project, null, Customer, null, fromSenderTypeId, 4);
                  }
                  catch (Exception ex)
                  {
                      log.HandleError(ex);
                  }
              }, (parameter) =>
              {
                  return (OpenedEditors["Projects"] as TableViewModel).SelectedItems.Count == 1;
              }
               );

            return addPaymentCommand;
        }

        private ICommand GetAddFlatPaymentCommand(EditorType editorType)
        {
            ICommand addPaymentCommand = new RelayCommand(
              (parameter) =>
              {
                  try
                  {
                      Flat flat = (OpenedEditors["Flats"] as TableViewModel).SelectedItems[0] as Flat;
                      int fromSenderTypeId = flat.Project?.ProjectTypeId == 1 ? 2 : 3;

                      if (editorType == EditorType.PaymentNew)
                          PaymentsUtils.OpenAddPayment(flat.Project, flat, Customer, null, fromSenderTypeId, 4);
                      else if (editorType == EditorType.DebtNew)
                          PaymentsUtils.OpenAddDebt(flat.Project, flat, Customer, null, fromSenderTypeId, 4);
                  }
                  catch (Exception ex)
                  {
                      log.HandleError(ex);
                  }
              }, (parameter) =>
              {
                  return (OpenedEditors["Flats"] as TableViewModel).SelectedItems.Count == 1;
              }
               );

            return addPaymentCommand;
        }

        private void AddProjectsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllProjects, this, "Projects", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.AfterAddEntity = (entity) =>
            {
                Project project = entity as Project;
                if (!project.CustomerInProjects.Any(cInP => cInP.Customer == Customer))
                {
                    EntityCreatorUtils.NewCustomerInProject(Customer, project, null);
                    project.CustomerInProjects.Add(EntityCreatorUtils.NewCustomerInProject(Customer, project, null));
                }
            };

            tableViewModel.DetachEntity = (entity) =>
            {
                Project project = entity as Project;

                CustomerInProject customerInProject = project.CustomerInProjects.FirstOrDefault(cInP => cInP.Customer == Customer);

                if (customerInProject != null)
                    new GeneralBL().DeleteEntity(customerInProject);
            };

            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת תשלום", GetAddProjectPaymentCommand(EditorType.PaymentNew), PackIconKind.Database));
            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת חוב", GetAddProjectPaymentCommand(EditorType.DebtNew), PackIconKind.CodeNotEqual));

            tableViewModel.InitialFilter = (obj) => { return ((Project)obj).CustomerInProjects.Any(cInP => cInP.Customer == Customer); };
            tableViewModel.Init();
        }

        private void AddFlatsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllFlats, this, "Flats", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.AfterAddEntity = (entity) =>
            {
                Flat flat = entity as Flat;


                if (!flat.CustomerInProjects.Any(cInP => cInP.Customer == Customer))
                {
                    CustomerInProject customerInProject = flat.CustomerInProjects.FirstOrDefault(cInP => cInP.Customer == Customer);

                    if (customerInProject != null)
                    {
                        customerInProject.Flat = flat;
                        flat.CustomerInProjects.Add(customerInProject);
                    }
                    else
                    {
                        flat.CustomerInProjects.Add(EntityCreatorUtils.NewCustomerInProject(Customer, flat.Project, flat));
                    }
                }
            };

            tableViewModel.DetachEntity = (entity) =>
            {
                Flat flat = entity as Flat;

                CustomerInProject customerInProject = flat.CustomerInProjects.FirstOrDefault(cInP => cInP.Customer == Customer);

                if (customerInProject != null)
                    flat.CustomerInProjects.Remove(customerInProject);
            };

            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת תשלום", GetAddFlatPaymentCommand(EditorType.PaymentNew), PackIconKind.Database));
            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת חוב", GetAddFlatPaymentCommand(EditorType.DebtNew), PackIconKind.CodeNotEqual));

            tableViewModel.InitialFilter = (obj) => { return ((Flat)obj).CustomerInProjects.Any(cInP => cInP.Customer == Customer); };
            tableViewModel.Init();
        }

        private void AddPaymentsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllPayments, this, "Payments", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;
            //tableViewModel.AfterAddEntity = (entity) =>
            //{
            //    Payment payment = entity as Payment;
            //    if (payment.CustomerInProject != null && payment.CustomerInProject.Customer == null)
            //    {
            //        payment.CustomerInProject.Customer = Customer;
            //    }
            //};

            //tableViewModel.DetachEntity = (entity) =>
            //{
            //    Payment payment = entity as Payment;

            //    if (payment.CustomerInProject != null)
            //    {
            //        payment.CustomerInProject = null;
            //    }
            //};

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                PaymentViewModel paymentViewModel = editorViewModel as PaymentViewModel;

                paymentViewModel.PaymentType = DebtType.Revenue;
                paymentViewModel.SenderFilter = SenderFilter.BySender;
                paymentViewModel.Customer = Customer;
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Payment payment = obj as Payment;

                return payment.CustomerInProject != null && payment.CustomerInProject.Customer == Customer;
            };
            tableViewModel.Init();
        }

        private void AddContractsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllContracts, this, "Contracts", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                ContractViewModel contractViewModel = editorViewModel as ContractViewModel;

                contractViewModel.Contract.Customer = Customer;
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                CustomerInProject customerInProject = obj as CustomerInProject;

                return customerInProject != null && customerInProject.Customer == Customer;
            };

            tableViewModel.Init();
        }

        private void AddDebtsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllDebts, this, "Debts", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;
            //tableViewModel.AfterAddEntity = (entity) =>
            //{
            //    Debt debt = entity as Debt;
            //    if (debt.CustomerInProject != null && debt.CustomerInProject.Customer == null)
            //    {
            //        debt.CustomerInProject.Customer = Customer;
            //    }
            //};

            //tableViewModel.DetachEntity = (entity) =>
            //{
            //    Debt debt = entity as Debt;

            //    if (debt.CustomerInProject != null)
            //    {
            //        debt.CustomerInProject = null;
            //    }
            //};

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                DebtViewModel debtViewModel = editorViewModel as DebtViewModel;

                debtViewModel.DebtType = DebtType.Revenue;
                debtViewModel.SenderFilter = SenderFilter.BySender;
                debtViewModel.Customer = Customer;
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Debt debt = obj as Debt;

                return debt.CustomerInProject != null && debt.CustomerInProject.Customer == Customer;
            };
            tableViewModel.Init();
        }

        public override void RefreshData()
        {
            base.RefreshData();
        }

        #endregion Methods
    }

}
