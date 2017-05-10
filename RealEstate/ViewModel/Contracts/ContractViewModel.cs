using FirstFloor.ModernUI.Presentation;
using MaterialDesignThemes.Wpf;
using RealEstate.BL;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RealEstate
{
    public class ContractViewModel : EditorViewModel
    {
        #region Ctor

        public ContractViewModel()
        {
            InitList(typeof(Customer));
            //   InitList(typeof(Project));
            //InitList(typeof(Gender));
        }

        #endregion Ctor

        #region Properties

        public CustomerInProject Contract
        {
            get { return Entity as CustomerInProject; }
            set { Entity = value; }
        }      

        private bool? isLeaseProject = null;
        public bool? IsLeaseProject
        {
            get { return isLeaseProject; }
            set
            {
                if (isLeaseProject != value)
                {
                    isLeaseProject = value;
                    OnPropertyChanged("IsLeaseProject");
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
                    Contract.Flat = flat;
                    if (flat != null)
                    {
                        if (flat.Price.HasValue)
                        {
                            LeaseContract.Amount = flat.Price.Value;
                            OnPropertyChanged(null);
                        }
                    }
                }
            }
        }

        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                    Contract.EndDate = endDate;
                    if (endDate != null)
                    {
                        if (endDate.HasValue && SaleContract != null)
                        {
                            SaleContract.GettingKeyDate = endDate.Value;
                            OnPropertyChanged(null);
                        }
                    }
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
                    Contract.Project = project;
                    if (project != null)
                    {
                        Flats = project.Flats.ToList();
                        IsLeaseProject = project.ProjectTypeId == 2;

                        //if (project.ProjectTypeId == 2)
                        //{
                        //    IsLeaseProject = true;
                        //    //if ((!LeaseContract.Amount.HasValue || LeaseContract.Amount.Value == 0))
                        //    //{
                        //    //    //LeaseContract.Amount=
                        //    //}
                        //    //if (Contract.LeaseContract == null)
                        //    //{
                        //    //    int price = Contract.Flat?.Price!=null ? Contract.Flat.Price.Value : 0;
                        //    //    Contract.LeaseContract = new LeaseContract() { Amount = price };
                        //    //    //if(IsEditEditor)
                        //    //    //{
                        //    //    //    new GeneralBL().AddEntity(Contract.LeaseContract);
                        //    //    //}
                        //    //    Contract.SaleContract = null;
                        //    //}
                        //}
                        //else
                        //{
                        //    IsLeaseProject = false;
                        //    //if (Contract.LeaseContract == null)
                        //    //{
                        //    //    Contract.SaleContract = new SaleContract();
                        //    //    //if (IsEditEditor)
                        //    //    //{
                        //    //    //    new GeneralBL().AddEntity(Contract.SaleContract);
                        //    //    //}
                        //    //    Contract.LeaseContract = null;
                        //    //}

                        //}
                    }
                    //FillListsByProject();
                }
            }
        }

        private SaleContract saleContract;
        public SaleContract SaleContract
        {
            get { return saleContract; }
            set
            {
                saleContract = value;
                OnPropertyChanged("SaleContract");
            }
        }

        private LeaseContract leaseContract;
        public LeaseContract LeaseContract
        {
            get { return leaseContract; }
            set
            {
                leaseContract = value;
                OnPropertyChanged("LeaseContract");
            }
        }


        #endregion Properties

        #region Methods

        public override void Init()
        {
            base.Init();
            InitLinks();
            InitList(typeof(Customer));
            InitList(typeof(Project));

            if (IsEditEditor)
            {
                Project = Contract.Project;
                EndDate = Contract.EndDate;
                SaleContract = Contract.SaleContract != null ? Contract.SaleContract : new SaleContract() { GettingKeyDate = EndDate };
                LeaseContract = Contract.LeaseContract != null ? Contract.LeaseContract : new LeaseContract() { Amount = 0, MonthlyPaymentDay = 10 };
                Flat = Contract.Flat;
                DateTime? getKeyDate = SaleContract.GettingKeyDate;
                SaleContract.GettingKeyDate = getKeyDate;
            }
            else
            {
                Contract.CreatedDate = DateTime.Now;
                Contract.StartDate = DateTime.Now;
                Contract.IsActive = true;
                SaleContract = new SaleContract() { GettingKeyDate = DateTime.Now.AddYears(1) };
                LeaseContract = new LeaseContract() { Amount = 0, MonthlyPaymentDay = 10 };
                EndDate = DateTime.Now.AddYears(1);
            }
            //  Projects = new ProjectsBL().GetProjects().Where(project => project.ProjectTypeId == 2).ToList();
            //if (!IsEditEditor)
            //{
            //    Customer.Country = Countries.FirstOrDefault(country => country.Name == "ישראל");
            //    Customer.Gender = Genders.FirstOrDefault(gender => gender.Name == "זכר");
            //}
        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            //if (string.IsNullOrWhiteSpace(Customer.Name))
            //{
            //    beforeSaveResult.IsValidData = false;
            //    beforeSaveResult.ErrorMessage = "נא הגדר שם פרטי";
            //    return beforeSaveResult;
            //}

            //if (string.IsNullOrWhiteSpace(Customer.Family))
            //{
            //    beforeSaveResult.IsValidData = false;
            //    beforeSaveResult.ErrorMessage = "נא הגדר שם משפחה";
            //    return beforeSaveResult;
            //}

            if (Contract.Customer == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר לקוח";
                return beforeSaveResult;
            }

            if (Contract.Project == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר פרויקט";
                return beforeSaveResult;
            }

            if (Contract.StartDate == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר תאריך תחילת חוזה";
                return beforeSaveResult;
            }

            if (Contract.EndDate == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר תאריך סיום חוזה";
                return beforeSaveResult;
            }

            if (Contract.StartDate > Contract.EndDate)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "על תאריך תחילת חוזה להיות קטן מתאריך סיום חוזה";
                return beforeSaveResult;
            }

            IList<CustomerInProject> customerInProjects = Contract.Project.CustomerInProjects.Where(cInP => cInP != Contract && cInP.IsActive && Contract.IsActive
               && cInP.Customer == Contract.Customer && cInP.Flat == Contract.Flat).ToList();

            if (customerInProjects.Count > 0)
            {
                MessageBoxResult result = DialogUtils.DisplayYesNoMessage("נמצאו חוזים פעילים קיימים התואמים לנתוני לקוח + פרויקט + דירה, האם ברצונך להפוך חוזים אלו ללא פעילים?", "הוספת חוזה");
                if (result == MessageBoxResult.Yes)
                {
                    foreach (var cInP in customerInProjects)
                    {
                        cInP.IsActive = false;
                    }
                }
                else
                {
                    beforeSaveResult.IsValidData = false;
                    return beforeSaveResult;
                }
            }

            if (Project.ProjectTypeId == 2)
            {
                Contract.LeaseContract = LeaseContract;
                Contract.SaleContract = null;
            }
            else
            {
                Contract.LeaseContract = null;
                Contract.SaleContract = SaleContract;
            }

            return beforeSaveResult;
        }

        private void InitLinks()
        {
            Links = new LinkCollection();

            string UriString = "/Views/Contracts/ContractDetails/ContractMainDetails.xaml";
            Link link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "פרטים", ViewModel = this };
            Links.Add(link);

            SelectedSource = new ModernUri(UriString, UriKind.Relative);

            AddDebtsLink();
            AddPaymentsLink();
            //AddProjectsLink();
            //AddFlatsLink();
            //AddMapLink();

            link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "קבצים", ViewModel = new TableViewModel() };
            Links.Add(link);

            //link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "מפה" };
            //Links.Add(link);
        }

        private void AddPaymentsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllPayments, this, "Payments", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                PaymentViewModel paymentViewModel = editorViewModel as PaymentViewModel;

                paymentViewModel.DuringAutoChanges = true;
                paymentViewModel.PaymentType = DebtType.Revenue;
                paymentViewModel.SenderFilter = SenderFilter.BySender;
                paymentViewModel.Customer = Contract.Customer;
                paymentViewModel.Project = Contract.Project;
                paymentViewModel.Flat = Contract.Flat;

                int fromSenderTypeId = Project?.ProjectTypeId == 1 ? 2 : 3;
                paymentViewModel.FromSenderType = new GeneralBL().GetSenderTypes().FirstOrDefault(senderType => senderType.Id == fromSenderTypeId);
                paymentViewModel.DuringAutoChanges = false;

                if (Contract.LeaseContract != null && Contract.LeaseContract.Amount.HasValue)
                {
                    paymentViewModel.Amount = Contract.LeaseContract.Amount.Value;
                }
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Payment payment = obj as Payment;

                return payment.CustomerInProject != null && payment.CustomerInProject == Contract;
            };
            tableViewModel.Init();
        }

        private void AddDebtsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllDebts, this, "Debts", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.IsSubEditor = false;

            tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
            {
                DebtViewModel debtViewModel = editorViewModel as DebtViewModel;

                debtViewModel.DuringAutoChanges = true;
                debtViewModel.DebtType = DebtType.Revenue;
                debtViewModel.SenderFilter = SenderFilter.BySender;
                debtViewModel.Customer = Contract.Customer;
                debtViewModel.Project = Contract.Project;
                debtViewModel.Flat = Contract.Flat;
                int fromSenderTypeId = Project?.ProjectTypeId == 1 ? 2 : 3;
                debtViewModel.FromSenderType = new GeneralBL().GetSenderTypes().FirstOrDefault(senderType => senderType.Id == fromSenderTypeId);
                debtViewModel.DuringAutoChanges = false;

                if (Contract.LeaseContract != null && Contract.LeaseContract.Amount.HasValue)
                {
                    debtViewModel.Amount = Contract.LeaseContract.Amount.Value;
                }
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Debt debt = obj as Debt;

                return debt.CustomerInProject != null && debt.CustomerInProject == Contract;
            };
            tableViewModel.Init();
        }

        public override void RefreshEntityTitle()
        {
            string projectDescription = Contract.Project != null ? Contract.Project.Name : "";
            projectDescription += Contract.Flat != null ? $", דירה {Contract.Flat.FlatNumber}" : "";

            //  EntityTitle = $"{projectDescription}, {Contract.Customer.Family}";
            base.RefreshEntityTitle();
        }

        public override void RefreshData()
        {
            base.RefreshData();
        }

        #endregion Methods
    }

}
