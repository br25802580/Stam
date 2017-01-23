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
    public class SupplierViewModel : EditorViewModel
    {
        ILog log = LogManager.GetLogger(typeof(SupplierViewModel));
        #region Ctor

        public SupplierViewModel()
        {
            //InitList(typeof(City));
            InitList(typeof(ServiceType));
            InitList(typeof(Country));
            InitList(typeof(Gender));
        }

        #endregion Ctor

        #region Properties

        public Supplier Supplier
        {
            get { return Entity as Supplier; }
            set
            {
                Entity = value;
                OnPropertyChanged("Supplier");
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

        #region Methods

        public override void Init()
        {
            base.Init();
            EntityTitle = $"{Supplier.Name} {Supplier.Family}";
            InitLinks();
            if (!IsEditEditor)
            {
                Supplier.Country = Countries.FirstOrDefault(country => country.Name == "ישראל");
                Supplier.Gender = Genders.FirstOrDefault(gender => gender.Name == "זכר");
            }

            Supplier.PropertyChanged += Supplier_PropertyChanged;
            SetCitiesByCountry();
        }

        private void Supplier_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Country")
            {
                SetCitiesByCountry();
            }
        }

        private void SetCitiesByCountry()
        {
            Cities = Supplier.Country.Cities.OrderBy(city => city.Name).ToList();
        }

        public override void RefreshEntityTitle()
        {
            EntityTitle = $"{Supplier.Name} {Supplier.Family}";
            base.RefreshEntityTitle();
        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (string.IsNullOrEmpty(Supplier.Name))
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר שם פרטי";
                return beforeSaveResult;
            }

            if (string.IsNullOrEmpty(Supplier.Family))
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר שם משפחה";
                return beforeSaveResult;
            }

            RefreshMapLink(Supplier.Country, Supplier.City, Supplier.Street, Convert.ToString(Supplier.HouseNumber));

            return beforeSaveResult;
        }

        private void InitLinks()
        {
            Links = new LinkCollection();

            string UriString = "/Views/Suppliers/SupplierDetails/SupplierMainDetails.xaml";
            UriString = $"{UriString}#{EditorMetaData.PageType}${EditorKey}$Details";
            OpenedEditors.Add("Details", this);
            Link link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "פרטים", ViewModel = this };
            Links.Add(link);

            selectedSource = new ModernUri(UriString, UriKind.Relative);

            AddDebtsLink();
            AddPaymentsLink();
            AddProjectsLink();
            AddFlatsLink();

            if (IsEditEditor)
                AddMapLink(Supplier.Country, Supplier.City, Supplier.Street, Convert.ToString(Supplier.HouseNumber));

            link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "קבצים", ViewModel = new TableViewModel() };
            Links.Add(link);
        }

        private ICommand GetAddProjectPaymentCommand(EditorType editorType)
        {
            ICommand addPaymentCommand = new RelayCommand(
              (parameter) =>
              {
                  try
                  {
                      Project project = (OpenedEditors["Projects"] as TableViewModel).SelectedItems[0] as Project;

                      if (editorType == EditorType.PaymentNew)
                          PaymentsUtils.OpenAddPayment(project, null, null, Supplier, 4, 1);
                      else if (editorType == EditorType.DebtNew)
                          PaymentsUtils.OpenAddDebt(project, null, null, Supplier, 4, 1);
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

                      if (editorType == EditorType.PaymentNew)
                          PaymentsUtils.OpenAddPayment(flat.Project, flat, null, Supplier, 4, 1);
                      else if (editorType == EditorType.DebtNew)
                          PaymentsUtils.OpenAddDebt(flat.Project, flat, null, Supplier, 4, 1);
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
                if (!project.SupplierInProjects.Any(sInP => sInP.Supplier == Supplier))
                {
                    project.SupplierInProjects.Add(new SupplierInProject() { Supplier = Supplier, Project = project });
                }
            };

            tableViewModel.DetachEntity = (entity) =>
            {
                Project project = entity as Project;

                SupplierInProject supplierInProject = project.SupplierInProjects.FirstOrDefault(sInP => sInP.Supplier == Supplier);

                if (supplierInProject != null)
                    new GeneralBL().DeleteEntity(supplierInProject);
            };

            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת תשלום", GetAddProjectPaymentCommand(EditorType.PaymentNew), PackIconKind.Database));
            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת חוב", GetAddProjectPaymentCommand(EditorType.DebtNew), PackIconKind.CodeNotEqual));
            tableViewModel.Fields.Add(new ColumnMetadata("Project.Name", "פרויקט"));

            tableViewModel.InitialFilter = (obj) => { return ((Project)obj).SupplierInProjects.Any(sInP => sInP.Supplier == Supplier); };
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
            //    if (payment.SupplierInProject != null && payment.SupplierInProject.Supplier == null)
            //    {
            //        payment.SupplierInProject.Supplier = Supplier;
            //    }
            //};

            //tableViewModel.DetachEntity = (entity) =>
            //{
            //    Payment payment = entity as Payment;

            //    if (payment.SupplierInProject != null)
            //    {
            //        payment.SupplierInProject = null;
            //    }
            //};

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                PaymentViewModel paymentViewModel = editorViewModel as PaymentViewModel;

                paymentViewModel.PaymentType = DebtType.Expense;
                paymentViewModel.SenderFilter = SenderFilter.BySender;
                paymentViewModel.Supplier = Supplier;
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Payment payment = obj as Payment;

                return payment.SupplierInProject != null && payment.SupplierInProject.Supplier == Supplier;
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
            //    if (debt.SupplierInProject != null && debt.SupplierInProject.Supplier == null)
            //    {
            //        debt.SupplierInProject.Supplier = Supplier;
            //    }
            //};

            //tableViewModel.DetachEntity = (entity) =>
            //{
            //    Debt debt = entity as Debt;

            //    if (debt.SupplierInProject != null)
            //    {
            //        debt.SupplierInProject = null;
            //    }
            //};

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                DebtViewModel debtViewModel = editorViewModel as DebtViewModel;

                debtViewModel.DebtType = DebtType.Expense;
                debtViewModel.SenderFilter = SenderFilter.BySender;
                debtViewModel.Supplier = Supplier;
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Debt debt = obj as Debt;

                return debt.SupplierInProject != null && debt.SupplierInProject.Supplier == Supplier;
            };
            tableViewModel.Init();
        }

        private void AddFlatsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllFlats, this, "Flats", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.AfterAddEntity = (entity) =>
            {
                Flat flat = entity as Flat;
                if (!flat.SupplierInProjects.Any(sInP => sInP.Supplier == Supplier))
                {
                    SupplierInProject supplierInProject = flat.SupplierInProjects.FirstOrDefault(sInP => sInP.Supplier == Supplier);

                    if (supplierInProject != null)
                    {
                        supplierInProject.Flat = flat;
                        flat.SupplierInProjects.Add(supplierInProject);
                    }
                    else
                    {
                        flat.SupplierInProjects.Add(new SupplierInProject() { Supplier = Supplier, Flat = flat });
                    }
                }
            };

            tableViewModel.DetachEntity = (entity) =>
            {
                Flat flat = entity as Flat;

                SupplierInProject supplierInProject = flat.SupplierInProjects.FirstOrDefault(sInP => sInP.Supplier == Supplier);

                if (supplierInProject != null)
                    flat.SupplierInProjects.Remove(supplierInProject);
            };

            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת תשלום", GetAddFlatPaymentCommand(EditorType.PaymentNew), PackIconKind.Database));
            tableViewModel.Buttons.Add(new ButtonMetadata("הוספת חוב", GetAddFlatPaymentCommand(EditorType.DebtNew), PackIconKind.CodeNotEqual));

            tableViewModel.InitialFilter = (obj) => { return ((Flat)obj).SupplierInProjects.Any(sInP => sInP.Supplier == Supplier); };
            tableViewModel.Init();
        }

        #endregion Methods
    }
}
