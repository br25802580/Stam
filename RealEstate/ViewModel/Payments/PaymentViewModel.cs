using FirstFloor.ModernUI.Presentation;
using log4net;
using RealEstate.BL;
using RealEstate.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealEstate
{
    public class PaymentViewModel : EditorViewModel
    {
        ILog log = LogManager.GetLogger(typeof(PaymentViewModel));
        #region Ctor

        public PaymentViewModel()
        {
            AddPaymentItemCommand = new RelayCommand(AddPaymentItemExecute);
            DeletePaymentItemsCommand = new RelayCommand(DeletePaymentItemsExecute);
            LinkDebtCommand = new RelayCommand(LinkDebtExecute);
            UnLinkDebtCommand = new RelayCommand(UnLinkDebtExecute);
            OpenDebtCommand = new RelayCommand(OpenDebtExecute);
            OpenCustomerCommand = new RelayCommand(OpenCustomerExecute, OpenCustomerCanExecute);
            OpenSupplierCommand = new RelayCommand(OpenSupplierExecute, OpenSupplierCanExecute);
            OpenProjectCommand = new RelayCommand(OpenProjectExecute, OpenProjectCanExecute);
            OpenFlatCommand = new RelayCommand(OpenFlatExecute, OpenFlatCanExecute);
        }

        #endregion Ctor

        #region Properties

        private int? amount;
        public int? Amount
        {
            get { return amount; }
            set
            {
                if (amount != value)
                {
                    amount = value;
                    OnPropertyChanged("Amount");
                    Payment.Amount = value;
                    if (!isDuringDelete && !DuringAutoChanges && value.HasValue)
                    {
                        if (PaymentItems.Count == 0)
                            AddPaymentItem(amount.Value);
                        else if (PaymentItems.Count == 1)
                            PaymentItems[0].Amount = amount.Value;
                    }
                }
            }
        }

        private PaymentMethod paymentMethod;
        public PaymentMethod PaymentMethod
        {
            get { return paymentMethod; }
            set
            {
                paymentMethod = value;
                Payment.PaymentMethod = value;
                DisplayChequeDetails = value != null && value.Id == 3;
                DisplayCreditDetails = value != null && value.Id == 4;
                OnPropertyChanged("PaymentMethod");
            }
        }

        private bool displayChequeDetails = false;
        public bool DisplayChequeDetails
        {
            get { return displayChequeDetails; }
            set
            {
                displayChequeDetails = value;
                OnPropertyChanged("DisplayChequeDetails");
            }
        }

        private bool displayCreditDetails = false;
        public bool DisplayCreditDetails
        {
            get { return displayCreditDetails; }
            set
            {
                displayCreditDetails = value;
                OnPropertyChanged("DisplayCreditDetails");
            }
        }

        private ObservableCollection<PaymentItemWrapper> paymentItems = new ObservableCollection<PaymentItemWrapper>();
        public ObservableCollection<PaymentItemWrapper> PaymentItems
        {
            get { return paymentItems; }
            set
            {
                paymentItems = value;
                OnPropertyChanged("PaymentItems");
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

        private ObservableCollection<PaymentItemWrapper> selectedPaymentItems;
        public ObservableCollection<PaymentItemWrapper> SelectedPaymentItems
        {
            get { return selectedPaymentItems; }
            set
            {
                selectedPaymentItems = value;
                OnPropertyChanged("SelectedPaymentItems");
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

        public Payment Payment
        {
            get { return Entity as Payment; }
            set { Entity = value; }
        }

        private void InitSenderTypes()
        {
            FromSenderTypes = new ObservableCollection<SenderType>(SenderTypes);
            ToSenderTypes = new ObservableCollection<SenderType>(SenderTypes);

            if (PaymentType == DebtType.Revenue)
                FromSenderTypes.Remove(FromSenderTypes.First(senderType => senderType.Id == 4));
            else
                ToSenderTypes.Remove(ToSenderTypes.First(senderType => senderType.Id == 4));
        }

        private DebtType paymentType = DebtType.None;
        public DebtType PaymentType
        {
            get { return paymentType; }
            set
            {
                if (paymentType != value)
                {
                    paymentType = value;
                    OnPropertyChanged("PaymentType");

                    if (paymentType == DebtType.Revenue)
                    {
                        if (PaymentRelation == null || (PaymentRelation.FromSenderTypeId != 2 && PaymentRelation.FromSenderTypeId != 3
                            || PaymentRelation.ToSenderTypeId != 4))
                        {
                            PaymentRelation = new PaymentsBL().GetPaymentRelation(3, 4);
                        }
                    }
                    else
                    {
                        if (PaymentRelation == null || (PaymentRelation.FromSenderTypeId != 4
                          || PaymentRelation.ToSenderTypeId != 1))
                        {
                            PaymentRelation = new PaymentsBL().GetPaymentRelation(4, 1);
                        }
                    }

                    InitSenderTypes();
                }
            }
        }

        private SenderType GetSenderType(int id)
        {
            return SenderTypes.FirstOrDefault(senderType => senderType.Id == id);
        }

        private void RefreshPaymentItems()
        {
            PaymentTypes = GetPaymentItems(paymentRelation);

            PaymentRelation oppositivePaymentRelation = new PaymentsBL().GetPaymentRelation(
                paymentRelation.ToSenderTypeId.Value, paymentRelation.FromSenderTypeId.Value);

            if (oppositivePaymentRelation != null)
            {
                RefundPaymentTypes = GetPaymentItems(oppositivePaymentRelation);
            }

            if (PaymentItems != null)
            {
                foreach (var paymentItem in PaymentItems)
                {
                    SetPaymentType(paymentItem);
                    //IList<PaymentType> paymentTypes = paymentItem.IsRefund ? RefundPaymentTypes : PaymentTypes;
                    //if (!paymentTypes.Contains(paymentItem.PaymentType))
                    //    paymentItem.PaymentType = null;
                }
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
                        IList<PaymentType> generalPaymentTypes = paymentRelation.PaymentTypes
                             .Where(paymentType => paymentType.PaymentTypeForServices.Count == 0).ToList();
                        foreach (var paymentType in generalPaymentTypes)
                        {
                            paymentTypes.Add(paymentType);
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

        private IList<PaymentType> refundPaymentTypes;
        public IList<PaymentType> RefundPaymentTypes
        {
            get { return refundPaymentTypes; }
            set
            {
                refundPaymentTypes = value;
                OnPropertyChanged("RefundPaymentTypes");
            }
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
                    Payment.PaymentRelation = value;

                    if (paymentRelation != null)
                    {
                        FromSenderType = paymentRelation.FromSenderType;
                        ToSenderType = paymentRelation.ToSenderType;
                    }
                    RefreshPaymentItems();

                    FillBySenderFilter();
                    //FillPaymentTypeBySupplier();
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

                    RefreshPaymentItems();

                    //FillPaymentTypeBySupplier();
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
                            if (Suppliers != null && (Supplier == null || !Supplier.SupplierInProjects.Any(s => s.Flat == value)))
                            {
                                Supplier = Suppliers.FirstOrDefault(s => s.SupplierInProjects.Any(sInp => sInp.Flat == value));
                            }
                        }
                        //if (SenderFilter == SenderFilter.ByProject)
                        //{
                        //    if (Flats != null && (Flat == null || !Flat.CustomerInProjects.Any(cInp => cInp.Customer == customer)))
                        //    {
                        //        Flat = Flats.FirstOrDefault(f => f.CustomerInProjects.Any(cInp => cInp.Customer == customer));
                        //    }
                        //}
                        else if (IsCustomerSender)
                        {
                            if (Customers != null && (Customer == null || !Customer.CustomerInProjects.Any(c => c.Flat == value)))
                            {
                                Customer = Customers.FirstOrDefault(c => c.CustomerInProjects.Any(cInp => cInp.Flat == value));
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
                        if (PaymentItems.Count == 0)
                        {
                            Amount = amount;
                        }
                        else
                        {
                            PaymentItemWrapper paymentItem = PaymentItems.FirstOrDefault(_paymentItem =>
                             _paymentItem?.PaymentType?.Id == 5);
                            if (paymentItem != null)
                            {
                                paymentItem.Amount = amount;
                            }
                        }
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
                    //FillProjectsBySender();
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

        private void SetPaymentType(PaymentItemWrapper paymentItemWrapper)
        {
            if (paymentItemWrapper?.PaymentItem != null)
            {
                IList<PaymentType> paymentTypes = paymentItemWrapper.IsRefund ? RefundPaymentTypes : PaymentTypes;
                PaymentType paymentType = null;
                if (paymentTypes != null && paymentTypes.Count > 0)
                {
                    if (paymentTypes.Count == 1)
                    {
                        paymentType = paymentTypes[0];
                    }
                    else
                    {
                        foreach (var _paymentType in paymentTypes)
                        {
                            if (!PaymentItems.Any(_paymentItem => _paymentItem.PaymentType == _paymentType))
                            {

                                paymentType = _paymentType;
                                break;
                            }
                        }
                        if (paymentType == null)
                        {
                            PaymentType generalPaymentType = paymentTypes.FirstOrDefault
                        (_paymentType => _paymentType.Name == "כללי");
                            paymentType = generalPaymentType;
                        }
                    }
                }
                paymentItemWrapper.PaymentType = paymentType;
            }
        }

        private void AddPaymentItem(int amount = 0)
        {
            //PaymentType paymentType = null;
            //if (PaymentTypes != null && PaymentTypes.Count > 0)
            //{
            //    if (PaymentTypes.Count == 1)
            //    {
            //        paymentType = PaymentTypes[0];
            //    }
            //    else
            //    {
            //        foreach (var _paymentType in PaymentTypes)
            //        {
            //            if (!PaymentItems.Any(_paymentItem => _paymentItem.PaymentType == _paymentType))
            //            {

            //                paymentType = _paymentType;
            //                break;
            //            }
            //        }
            //        if (paymentType == null)
            //            paymentType = PaymentTypes[0];
            //    }
            //}

            PaymentItem paymentItem = new PaymentItem() { Amount = amount, IsRefuned = false };
            PaymentItemWrapper paymentItemWrapper = new PaymentItemWrapper(paymentItem, Payment);
            SetPaymentType(paymentItemWrapper);
            PaymentItems.Add(paymentItemWrapper);
            if (IsEditEditor)
            {
                new GeneralBL().Save();
                OpenedEditors["PaymentItems"].RefreshData();
            }
            OnPropertyChanged("PaymentItems");
        }

        public ICommand UnLinkDebtCommand { get; set; }
        private void UnLinkDebtExecute(object parameter)
        {
            try
            {
                PaymentItemWrapper paymentItemWrapper = parameter as PaymentItemWrapper;
                //Debt debt = paymentItemWrapper.Debt;
                //if (debt != null)
                //{
                //    if (debt.PaymentItems.Contains(paymentItemWrapper.PaymentItem))
                //    {
                //        debt.PaymentItems.Remove(paymentItemWrapper.PaymentItem);
                paymentItemWrapper.Debt = null;
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }


        public ICommand OpenDebtCommand { get; set; }
        private void OpenDebtExecute(object parameter)
        {
            try
            {
                PaymentItemWrapper paymentItemWrapper = parameter as PaymentItemWrapper;
                RealEstateRepository.Instance.OpenEditor(paymentItemWrapper.Debt, EditorType.Debt);
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

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

        public ICommand LinkDebtCommand { get; set; }
        private void LinkDebtExecute(object parameter)
        {
            try
            {
                PaymentItemWrapper paymentItemWrapper = parameter as PaymentItemWrapper;

                Func<IList> initList = null;

                if (IsCustomerSender)
                {
                    if (Customer != null && Project != null)
                    {
                        initList = () =>
                      {
                          return Customer.CustomerInProjects.Where(cInP => cInP.Project == Project)
                          .SelectMany(cInP => cInP.Debts).Where(debt => debt.DelinquentAmount > 0 && debt.PaymentRelation == PaymentRelation).ToList();
                      };
                    }
                }
                else
                {
                    if (Supplier != null && Project != null)
                    {
                        initList = () =>
                        {
                            return Supplier.SupplierInProjects.Where(sInP => sInP.Project == Project)
                            .SelectMany(sInP => sInP.Debts).Where(debt => debt.DelinquentAmount > 0 && debt.PaymentRelation == PaymentRelation).ToList();
                        };
                    }
                }

                IList<Debt> debts = DialogUtils.DisplayAttachItemsDialog(EditorType.AllDebts, null, initList) as IList<Debt>;

                if (debts != null && debts.Count > 0)
                {
                    Debt debt = debts[0];
                    paymentItemWrapper.Debt = debt;
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        public ICommand AddPaymentItemCommand { get; set; }
        private void AddPaymentItemExecute(object parameter)
        {
            try
            {
                AddPaymentItem();
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        bool isDuringDelete = false;
        public ICommand DeletePaymentItemsCommand { get; set; }
        private void DeletePaymentItemsExecute(object parameter)
        {
            try
            {
                if (SelectedPaymentItems != null)
                {
                    isDuringDelete = true;
                    foreach (var item in SelectedPaymentItems.ToList())
                    {
                        PaymentItems.Remove(item);

                    }
                    //for (int i = SelectedPaymentItems.Count - 1; i >= 0; i--)
                    //{
                    //}
                    isDuringDelete = false;
                    if (IsEditEditor)
                    {
                        new GeneralBL().Save();
                        OpenedEditors["PaymentItems"].RefreshData();
                        //new GeneralBL().DeleteEntities(SelectedPaymentItems.Select(paymentWrapper => paymentWrapper.PaymentItem).ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                log.HandleError(ex);
            }
        }

        #endregion Commands

        #region Methods

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
            EntityTitle = $"{Payment.Amount}ש\"ח   {senderName}, {projectDescription}";

            base.RefreshEntityTitle();
        }

        public override void OnCancel()
        {
            base.OnCancel();

            foreach (var paymentItem in PaymentItems)
            {
                paymentItem.Debt = null; ;
            }
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
                    Payment.CustomerInProject = customerInProject;
                }
                else
                {
                    Payment.CustomerInProject = new CustomerInProject() { Customer = Customer, Project = Project, Flat = Flat };
                }

                Payment.SupplierInProject = null;
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
                    Payment.SupplierInProject = supplierInProject;
                else
                    Payment.SupplierInProject = new SupplierInProject() { Supplier = Supplier, Project = Project, Flat = Flat };

                Payment.CustomerInProject = null;
            }
            //else if (IsBankSender) { }
            //else if (IsNoneSender)
            //{
            //}

            Payment.Project = Project;
            Payment.Flat = Flat;

            if (Amount == null || Amount == 0)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר סכום";
                return beforeSaveResult;
            }

            int paymentsSum = PaymentItems.Sum(paymentItem => paymentItem.Amount);
            if (paymentsSum != Amount)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "הסכום הכולל לא תואם את סכום פרטי התשלום";
                return beforeSaveResult;
            }

            if (Payment.PaymentDate == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר תאריך תשלום";
                return beforeSaveResult;
            }

            return beforeSaveResult;
        }

        private void InitDebtsDetails()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.GetTableViewModel(EditorType.AllDebts);

            //  TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllDebts, this, "Debts", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.AfterAddEntity = (entity) =>
            {
                Debt debt = entity as Debt;
                if (debt.CustomerInProject != null && debt.CustomerInProject.Project == null)
                {
                    debt.CustomerInProject.Project = Flat.Project;
                    debt.CustomerInProject.Flat = Flat;
                }
                if (debt.SupplierInProject != null && debt.SupplierInProject.Project == null)
                {
                    debt.SupplierInProject.Project = Flat.Project;
                    debt.SupplierInProject.Flat = Flat;
                }
            };

            tableViewModel.DetachEntity = (entity) =>
            {
                Debt debt = entity as Debt;

                if (debt.SupplierInProject != null)
                {
                    debt.SupplierInProject.Flat = null;
                }
                if (debt.CustomerInProject != null)
                {
                    debt.CustomerInProject.Flat = null;
                }
            };

            tableViewModel.InitialFilter = (obj) =>
            {
                Debt debt = obj as Debt;

                return debt.CustomerInProject != null && debt.CustomerInProject.Project == Project && debt.CustomerInProject.Flat == Flat
                || debt.SupplierInProject != null && debt.SupplierInProject.Project == Project && debt.SupplierInProject.Flat == Flat;
            };
            tableViewModel.Init();
            tableViewModel.RemoveGroupDescription(".");

            //DebtsDetails = tableViewModel;
        }

        public override void Init()
        {
            base.Init();
            InitLinks();
            EntityTitle = Payment.Amount.ToString();
            //InitDebtsDetails();

            InitList(typeof(SenderType));
            InitList(typeof(PaymentRelation));
            InitList(typeof(PaymentMethod));
            InitList(typeof(Bank));
            duringAutoChanges = true;
            Amount = Payment.Amount.HasValue ? Payment.Amount.Value : 0;
            PaymentRelation paymentRelation = Payment.PaymentRelation;

            if (IsEditEditor)
            {
                if (Payment.PaymentRelation != null)
                    if (Payment.PaymentRelation.FromSenderTypeId == 4)
                    {
                        PaymentType = DebtType.Expense;
                    }
                    else
                    {
                        PaymentType = DebtType.Revenue;
                    }
            }
            else
            {
                Payment.WithVat = true;
                PaymentMethod = PaymentMethods.FirstOrDefault(paymentMethod => paymentMethod.Id == 2);
                PaymentType = DebtType.Revenue;
                Payment.CreatedDate = DateTime.Now;
                Payment.PaymentDate = DateTime.Now;
            }

            if (paymentRelation != null)
                PaymentRelation = paymentRelation;

            FillBySenderFilter();

            //if (PaymentRelation.ToSenderTypeId == 1)
            //{
            //    IsSupplierSender = true;
            //}

            //SetPaymentSender(PaymentRelation.FromSenderTypeId.Value);

            //else if (PaymentRelation.FromSenderTypeId == 2 || PaymentRelation.FromSenderTypeId == 3)
            //{
            //    IsCustomerSender = true;
            //}

            if (Payment.CustomerInProject != null)
            {
                Customer = Payment.CustomerInProject.Customer;
            }
            if (Payment.SupplierInProject != null)
            {
                Supplier = Payment.SupplierInProject.Supplier;
            }
            Project = GetProject();
            Flat = GetFlat();

            if (!IsEditEditor)
            {
                Payment.CreatedDate = DateTime.Now;
            }

            foreach (PaymentItem paymentItem in Payment.PaymentItems)
            {
                PaymentItems.Add(new PaymentItemWrapper(paymentItem, Payment));
            }

            foreach (var item in PaymentItems)
            {
                ((NotifyPropertyChanged)item).PropertyChanged += PaymentViewModel_PropertyChanged;
            }

            PaymentItems.CollectionChanged += PaymentItems_CollectionChanged;
            selectedPaymentItems = new ObservableCollection<PaymentItemWrapper>();

            PaymentMethod = Payment.PaymentMethod;

            duringAutoChanges = false;
            OnPropertyChanged(null);
            // Customers = Project.CustomerInProjects.Select(c => c.Customer).ToList();
            //    Suppliers = Project.SupplierInProjects.Select(s => s.Supplier).ToList();
        }

        public override void RefreshData()
        {
            base.RefreshData();

            InitSenderTypes();
            PaymentRelation = Payment.PaymentRelation;
            RefreshPaymentItems();

            OnPropertyChanged(null);
        }

        private void PaymentItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                foreach (var item in e.OldItems)
                {
                    {
                        PaymentItem paymentItem = ((PaymentItemWrapper)item).PaymentItem;
                        if (Payment.PaymentItems.Contains(paymentItem))
                        {
                            Payment.PaymentItems.Remove(paymentItem);
                        }
                   ((NotifyPropertyChanged)item).PropertyChanged -= PaymentViewModel_PropertyChanged;
                    }
                }

            if (e.NewItems != null)
                foreach (var item in e.NewItems)
                {
                    PaymentItem paymentItem = ((PaymentItemWrapper)item).PaymentItem;
                    if (!Payment.PaymentItems.Contains(paymentItem))
                    {
                        Payment.PaymentItems.Add(paymentItem);
                    }

                     ((NotifyPropertyChanged)item).PropertyChanged += PaymentViewModel_PropertyChanged;
                }
            Amount = PaymentItems.Sum(paymentItem => paymentItem.Amount);
        }

        private void PaymentViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Amount")
                Amount = PaymentItems.Sum(paymentItem => paymentItem.Amount);
            else if (e.PropertyName == "IsRefund")
            {
                PaymentItemWrapper paymentItem = sender as PaymentItemWrapper;
                SetPaymentType(paymentItem);
                //if ()
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
            return Payment.Project;
            //return Payment.CustomerInProject != null ?
            //     Payment.CustomerInProject.Project : Payment.SupplierInProject != null ?
            //     Payment.SupplierInProject.Project : Payment.Project != null ?
            //     Payment.Project : null;
        }

        public Flat GetFlat()
        {
            return Payment.Flat;
            //return Payment.CustomerInProject != null ?
            //     Payment.CustomerInProject.Flat : Payment.SupplierInProject != null ?
            //     Payment.SupplierInProject.Flat : Payment.Flat != null ?
            //      Payment.Flat : null;
        }

        private void FillListsByProject()
        {
            if (Project != null)
            {
                if (SenderFilter == SenderFilter.ByProject)
                {
                    Customers = Project.CustomerInProjects.Select(project => project.Customer).Distinct().ToList();
                    Suppliers = Project.SupplierInProjects.Where(sInP => sInP != null && sInP.Supplier != null).Select(project => project.Supplier).Distinct().ToList();
                    //Flats = Project.Flats.Where(flat => flat.CustomerInProjects?.Count > 0).ToList();
                    Flats = Project.Flats.ToList();
                }
                else
                {
                    if (IsCustomerSender)
                    {
                        Flats = Project.Flats.Where(flat => flat.CustomerInProjects.Any(cInP => cInP.Customer == Customer)).ToList();
                    }
                    else if (IsSupplierSender)
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
            //  bool isSupplierSender = false;
            if (FromSenderType.Id == 2 || ToSenderType.Id == 2)
                ProjectTypeId = 1;
            else if (FromSenderType.Id == 3 || ToSenderType.Id == 3)
                ProjectTypeId = 2;
            else
            {
                ProjectTypeId = null;
                //isSupplierSender = true;
            }

            if (SenderFilter == SenderFilter.BySender)
            {
                if (IsSupplierSender)
                    InitList(typeof(Supplier));
                else
                {
                    Customers = new CustomersBL().GetCustomers().Where(customer => customer.CustomerInProjects
                    .Any(cInP => cInP?.Project?.ProjectTypeId == ProjectTypeId)).ToList();
                    //FillProjectsBySender();
                }
                FillProjectsBySender();
            }
            else
            {
                if (IsCustomerSender)
                {
                    Projects = new ProjectsBL().GetProjects().Where(project =>
                      project.ProjectTypeId == ProjectTypeId).ToList();
                }
                else
                {
                    InitList(typeof(Project));
                }

                FillListsByProject();
            }
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

            string UriString = "/Views/Payments/PaymentDetails/PaymentMainDetails.xaml";
            UriString = $"{UriString}#{EditorMetaData.PageType}${EditorKey}$Details";
            OpenedEditors.Add("Details", this);
            Link link = new ModernLink() { Source = new ModernUri(UriString, UriKind.Relative), DisplayName = "פרטים", ViewModel = this };
            Links.Add(link);

            selectedSource = new ModernUri(UriString, UriKind.Relative);

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

        private void AddPaymentItemsLink()
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.PaymentItems, this, "PaymentItems", false) as TableViewModel;
            tableViewModel.EntityTitle = EntityTitle;
            tableViewModel.DisplayCommands = false;
            //tableViewModel.DisplayCommands = false;

            tableViewModel.InitListSource = () =>
            {
                return Payment.PaymentItems.ToList();
            };
            //   tableViewModel.Fields = tableViewModel.TableEditorMetadata.Fields;
            tableViewModel.Fields = new List<ColumnMetadata>(tableViewModel.TableEditorMetadata.Fields);

            tableViewModel.Fields.RemoveAt(7);
            tableViewModel.Fields.RemoveAt(2);
            tableViewModel.Fields.RemoveAt(1);

            tableViewModel.Init();
        }

        //private void AddDebtsLink()
        //{
        //    TableViewModel tableViewModel = RealEstateRepository.Instance.AddEditor(EditorType.AllDebts, this, "Debts", false) as TableViewModel;
        //    tableViewModel.EntityTitle = EntityTitle;
        //    //tableViewModel.IsSubEditor = false;
        //    //tableViewModel.DisplayCommands = false;
        //    //tableViewModel.AfterAddEntity = (entity) =>
        //    //{
        //    //    Debt debt = entity as Debt;
        //    //    if (debt.SupplierInProject != null && debt.SupplierInProject.Supplier == null)
        //    //    {
        //    //        debt.SupplierInProject.Supplier = Supplier;
        //    //    }
        //    //};

        //    //tableViewModel.DetachEntity = (entity) =>
        //    //{
        //    //    Debt debt = entity as Debt;

        //    //    if (debt.SupplierInProject != null)
        //    //    {
        //    //        debt.SupplierInProject = null;
        //    //    }
        //    //};

        //    //tableViewModel.AfterCreateNewEditor = (editorViewModel) =>
        //    //{
        //    //    DebtViewModel debtViewModel = editorViewModel as DebtViewModel;

        //    //    debtViewModel.DebtType = DebtType.Expense;
        //    //    debtViewModel.SenderFilter = SenderFilter.BySender;
        //    //    debtViewModel.Supplier = Supplier;
        //    //};
        //    //tableViewModel.Fields.RemoveAt(6);
        //    //tableViewModel.Fields.RemoveAt(6);
        //    //tableViewModel.Fields.RemoveAt(6);
        //    //tableViewModel.Fields.RemoveAt(6);

        //    tableViewModel.InitialFilter = (obj) =>
        //    {
        //        Debt debt = obj as Debt;

        //        return debt.PaymentItems.Any(paymentItem => paymentItem.Payment == Payment);
        //    };
        //    tableViewModel.Init();
        //}

        #endregion Methods

    }

    public class PaymentItemWrapper : NotifyPropertyChanged
    {
        public PaymentItemWrapper(PaymentItem paymentItem, Payment payment)
        {
            PaymentItem = paymentItem;
            if (paymentItem != null)
            {
                PaymentType = paymentItem.PaymentType;
                Debt = paymentItem.Debt;
                Amount = paymentItem.Amount.HasValue ? paymentItem.Amount.Value : 0;
                //    IsRefund = paymentItem.IsRefuned.HasValue ? paymentItem.IsRefuned.Value : false;
                IsRefund = paymentItem.IsRefuned.HasValue ? paymentItem.IsRefuned.Value : false;
            }
            this.payment = payment;
        }

        private Payment payment;

        private bool isRefund = false;
        public bool IsRefund
        {
            get { return isRefund; }
            set
            {
                isRefund = value;
                PaymentItem.IsRefuned = value;
                OnPropertyChanged("IsRefund");
            }
        }

        public PaymentItem PaymentItem { get; set; }

        public int amount;
        public int Amount
        {
            get { return amount; }
            set
            {
                if (value != amount)
                {
                    //if (payment?.PaymentRelation.FromSenderTypeId == 4)
                    //{
                    //    if (value > 0)
                    //        value *= -1;
                    //}
                    //else
                    //{
                    //    if (value < 0)
                    //        value *= -1;
                    //}

                    amount = value;
                    PaymentItem.Amount = value;
                    OnPropertyChanged("Amount");

                    if (Debt != null)
                    {
                        Debt.CalculateDebtAmount();
                    }
                }
            }
        }

        public Debt debt;
        public Debt Debt
        {
            get { return debt; }
            set
            {
                if (debt != value)
                {
                    if (debt != null)
                    {
                        if (debt.PaymentItems.Contains(PaymentItem))
                        {
                            debt.PaymentItems.Remove(PaymentItem);
                        }
                        debt.CalculateDebtAmount();
                    }
                    debt = value;
                    PaymentItem.Debt = value;
                    HasDebt = value != null;

                    if (debt != null)
                    {
                        //{
                        //    if (debt.PaymentItems.Contains(PaymentItem))
                        //    {
                        //        debt.PaymentItems.Remove(PaymentItem);
                        //    }
                        //    PaymentType = debt.PaymentType;
                        //    Amount = debt.AmountPaid.HasValue ? debt.AmountPaid.Value : 0;
                        //}
                        //else
                        //{
                        if (!debt.PaymentItems.Contains(PaymentItem))
                        {
                            debt.PaymentItems.Add(PaymentItem);
                        }
                        PaymentType = debt.PaymentType;
                        Amount = debt.Amount.Value - (debt.AmountPaid.HasValue ? debt.AmountPaid.Value : 0);
                        debt.CalculateDebtAmount();
                    }
                    //}

                    PaymentItem.Debt = debt;
                    OnPropertyChanged("Debt");
                }
            }
        }

        public bool hasDebt = false;
        public bool HasDebt
        {
            get { return hasDebt; }
            set
            {
                hasDebt = value;
                OnPropertyChanged("HasDebt");
            }
        }

        public PaymentType paymentType;
        public PaymentType PaymentType
        {
            get { return paymentType; }
            set
            {
                paymentType = value;
                PaymentItem.PaymentType = value;
                OnPropertyChanged("PaymentType");
            }
        }

        //public IList<PaymentType> paymentTypes;
        //public IList<PaymentType> PaymentTypes
        //{
        //    get { return paymentTypes; }
        //    set
        //    {
        //        paymentTypes = value;
        //        if (!paymentTypes.Contains(PaymentType))
        //        {
        //            PaymentType = null;
        //        }
        //        OnPropertyChanged("PaymentTypes");
        //    }
        //}
    }
}

