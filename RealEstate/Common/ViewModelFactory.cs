using FirstFloor.ModernUI.Presentation;
using log4net;
using MaterialDesignThemes.Wpf;
using RealEstate.BL;
using RealEstate.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RealEstate
{
    public class ViewModelFactory
    {
        ILog log = LogManager.GetLogger(typeof(ViewModelFactory));

        #region Ctor

        public ViewModelFactory()
        {
            InitEditorsMetaData();
            InitPagesMetaData();
        }

        #endregion Ctor

        #region Public Methods

        public EditorViewModel GetViewModel(object entity, EditorType editorType)
        {
            EditorViewModel viewModel = null;
            Type entityType = entity.GetType().BaseType;
            string editorKey = GetEditorKey(entity, editorType);
            EditorMetaData editorMetaData = EditorsMetaData[editorType];

            if (editorMetaData != null)
            {
                //if (RealEstateRepository.Instance.OpenedEditors.ContainsKey(editorKey))
                //{
                //    viewModel = RealEstateRepository.Instance.OpenedEditors[editorKey];
                //}
                //else
                //{
                //    Type viewModelType = editorMetaData.ViewModelType;
                //    viewModel = Activator.CreateInstance(viewModelType) as EditorViewModel;
                //    viewModel.EntityMetaData = editorMetaData;
                //    viewModel.Entity = entity;
                //    RealEstateRepository.Instance.OpenedEditors.Add(editorKey, viewModel);
                //    viewModel.EditorKey = editorKey;
                //}
            }

            return viewModel;
        }

        #endregion Public Methods

        #region Private Methods

        private string GetEditorKey(object entity, EditorType editorType)
        {
            string editorKey = string.Empty;
            Type entityType = entity.GetType();
            string entityId = Convert.ToString(entityType.GetProperty("Id").GetValue(entity, null));

            editorKey = string.Format("{0}-{1}", editorType.ToString(), entityId);

            return editorKey;
        }

        private void InitEditorsMetaData()
        {
            EditorsMetaData = new Dictionary<EditorType, EditorMetaData>();

            AddProjectsEditor();
            AddLeaseProjectsEditor();
            AddSaleProjectsEditor();

            AddFlatsEditor();
            AddLeaseFlatsEditor();
            AddSaleFlatsEditor();

            AddCustomersEditor();
            AddContractsEditor();
            AddSuppliersEditor();

            AddPaymentsEditor();
            AddRevenuesEditor();
            AddExpensesEditor();
            AddDebtsEditor();

            AddPaymentItemsEditor();

            AddConstantPaymentsEditor();

            //PageType.Leases

            Type type = typeof(Data.Project);
            EditorType editorType = EditorType.LeaseProject;
            PageType pageType = PageType.Leases;
            EditorMetaData editorMetaData = new EditorMetaData(typeof(LeaseProjectViewModel), editorType, pageType, "/Views/Projects/ProjectDetails/ProjectView.xaml");
            editorMetaData.IconKind = PackIconKind.HomeModern;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Project);
            editorType = EditorType.LeaseProjectNew;
            editorMetaData = new NewEditorMetadata(type, typeof(LeaseProjectViewModel), editorType, pageType, typeof(ProjectMainDetails));
            editorMetaData.IconKind = PackIconKind.HomeModern;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Flat);
            editorType = EditorType.LeaseFlat;
            editorMetaData = new EditorMetaData(typeof(LeaseFlatViewModel), editorType, pageType, "/Views/Flats/FlatDetails/FlatView.xaml");
            editorMetaData.IconKind = PackIconKind.HomeMapMarker;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Flat);
            editorType = EditorType.LeaseFlatNew;
            editorMetaData = new NewEditorMetadata(type, typeof(LeaseFlatViewModel), editorType, pageType, typeof(FlatMainDetails));
            editorMetaData.IconKind = PackIconKind.HomeMapMarker;
            EditorsMetaData.Add(editorType, editorMetaData);

            //PageType.Sale

            type = typeof(Data.Project);
            editorType = EditorType.SaleProject;
            pageType = PageType.Sale;
            editorMetaData = new EditorMetaData(typeof(SalesProjectViewModel), editorType, pageType, "/Views/Projects/ProjectDetails/ProjectView.xaml");
            editorMetaData.IconKind = PackIconKind.HomeModern;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Project);
            editorType = EditorType.SaleProjectNew;
            editorMetaData = new NewEditorMetadata(type, typeof(SalesProjectViewModel), editorType, pageType, typeof(ProjectMainDetails));
            editorMetaData.IconKind = PackIconKind.HomeModern;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Flat);
            editorType = EditorType.SaleFlat;
            editorMetaData = new EditorMetaData(typeof(SalesFlatViewModel), editorType, pageType, "/Views/Flats/FlatDetails/FlatView.xaml");
            editorMetaData.IconKind = PackIconKind.HomeMapMarker;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Flat);
            editorType = EditorType.SaleFlatNew;
            editorMetaData = new NewEditorMetadata(type, typeof(SalesFlatViewModel), editorType, pageType, typeof(FlatMainDetails));
            editorMetaData.IconKind = PackIconKind.HomeMapMarker;
            EditorsMetaData.Add(editorType, editorMetaData);

            //PageType.Customers

            pageType = PageType.Customers;
            type = typeof(Data.Customer);
            editorType = EditorType.Customer;
            editorMetaData = new EditorMetaData(typeof(CustomerViewModel), editorType, pageType, "/Views/Customers/CustomerDetails/CustomerView.xaml");
            editorMetaData.IconKind = PackIconKind.HumanHandsdown;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Customer);
            editorType = EditorType.CustomerNew;
            editorMetaData = new NewEditorMetadata(type, typeof(CustomerViewModel), editorType, pageType, typeof(CustomerMainDetails));
            editorMetaData.IconKind = PackIconKind.HumanHandsdown;
            EditorsMetaData.Add(editorType, editorMetaData);

            //PageType.Contracts

            pageType = PageType.Contracts;
            type = typeof(Data.CustomerInProject);
            editorType = EditorType.Contract;
            editorMetaData = new EditorMetaData(typeof(ContractViewModel), editorType, pageType, "/Views/Contracts/ContractDetails/ContractView.xaml");
            editorMetaData.IconKind = PackIconKind.FormatFloatLeft;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.CustomerInProject);
            editorType = EditorType.ContractNew;
            editorMetaData = new NewEditorMetadata(type, typeof(ContractViewModel), editorType, pageType, typeof(ContractMainDetails));
            editorMetaData.IconKind = PackIconKind.FormatFloatLeft;
            EditorsMetaData.Add(editorType, editorMetaData);

            //PageType.Suppliers

            pageType = PageType.Suppliers;
            type = typeof(Data.Supplier);
            editorType = EditorType.Supplier;
            editorMetaData = new EditorMetaData(typeof(SupplierViewModel), editorType, pageType, "/Views/Suppliers/SupplierDetails/SupplierView.xaml");
            editorMetaData.IconKind = PackIconKind.Run;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Supplier);
            editorType = EditorType.SupplierNew;
            editorMetaData = new NewEditorMetadata(type, typeof(SupplierViewModel), editorType, pageType, typeof(SupplierMainDetails));
            editorMetaData.IconKind = PackIconKind.Run;
            EditorsMetaData.Add(editorType, editorMetaData);

            //PageType.Payments

            pageType = PageType.Payments;
            type = typeof(Data.Payment);
            editorType = EditorType.Payment;
            editorMetaData = new EditorMetaData(typeof(PaymentViewModel), editorType, pageType, "/Views/Payments/PaymentDetails/PaymentView.xaml");
            editorMetaData.IconKind = PackIconKind.Database;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Payment);
            editorType = EditorType.PaymentNew;
            editorMetaData = new NewEditorMetadata(type, typeof(PaymentViewModel), editorType, pageType,
                typeof(PaymentMainDetails));
            //editorMetaData = new NewEditorMetadata(type, typeof(PaymentViewModel), editorType, pageType,
            //   typeof(PaymentMainDetails), typeof(PaymentWrapper));
            editorMetaData.IconKind = PackIconKind.Database;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Debt);
            editorType = EditorType.Debt;
            editorMetaData = new EditorMetaData(typeof(DebtViewModel), editorType, pageType, "/Views/Debts/DebtDetails/DebtView.xaml");
            editorMetaData.IconKind = PackIconKind.CodeNotEqual;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(Data.Debt);
            editorType = EditorType.DebtNew;
            editorMetaData = new NewEditorMetadata(type, typeof(DebtViewModel), editorType, pageType,
             typeof(DebtMainDetails));
            editorMetaData.IconKind = PackIconKind.CodeNotEqual;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(ConstantPayment);
            editorType = EditorType.ConstantPaymentNew;
            editorMetaData = new NewEditorMetadata(type, typeof(ConstantPaymentViewModel), editorType, pageType,
             typeof(ConstantPaymentMainDetails));
            editorMetaData.IconKind = PackIconKind.Database;
            EditorsMetaData.Add(editorType, editorMetaData);

            //            pageType = PageType.Map;

            pageType = PageType.Undefined;
            type = typeof(Map);
            editorType = EditorType.Map;
            editorMetaData = new NewEditorMetadata(type, typeof(MapViewModel), editorType, pageType,
             typeof(MapMainDetails));
            editorMetaData.IconKind = PackIconKind.Map;
            editorMetaData.PluralCaption = "מפה";
            editorMetaData.UriString = "/Views/Map/MapMainDetails.xaml";
            EditorsMetaData.Add(editorType, editorMetaData);

            AddCountriesEditor();
            AddCitiesEditor();
            AddSendersEditor();
            AddPaymentRelationsEditor();

            pageType = PageType.System;

            type = typeof(Country);
            editorType = EditorType.Country;
            editorMetaData = new NewEditorMetadata(type, typeof(NamedElementViewModel), editorType, pageType,
             typeof(CountryDetails));
            editorMetaData.IconKind = PackIconKind.Database;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(City);
            editorType = EditorType.City;
            editorMetaData = new NewEditorMetadata(type, typeof(CityViewModel), editorType, pageType,
             typeof(CityDetails));
            editorMetaData.IconKind = PackIconKind.City;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(SenderType);
            editorType = EditorType.Sender;
            editorMetaData = new NewEditorMetadata(type, typeof(NamedElementViewModel), editorType, pageType,
             typeof(CountryDetails));
            editorMetaData.IconKind = PackIconKind.AccountSettingsVariant;
            EditorsMetaData.Add(editorType, editorMetaData);

            type = typeof(PaymentRelation);
            editorType = EditorType.PaymentRelation;
            editorMetaData = new NewEditorMetadata(type, typeof(PaymentRelationViewModel), editorType, pageType,
             typeof(PaymentRelationDetails));
            editorMetaData.IconKind = PackIconKind.AccountSwitch;
            EditorsMetaData.Add(editorType, editorMetaData);
        }

        private void InitPagesMetaData()
        {
            PagesMetaData = new Dictionary<PageType, PageMetaData>();

            PageType pageType = PageType.Leases;
            PageMetaData pageMetaData = new PageMetaData(pageType, "/Views/Lease/LeasesPage.xaml");
            PagesMetaData.Add(pageType, pageMetaData);

            pageType = PageType.Sale;
            pageMetaData = new PageMetaData(pageType, "/Views/Lease/SalesPage.xaml");
            PagesMetaData.Add(pageType, pageMetaData);

            pageType = PageType.Customers;
            pageMetaData = new PageMetaData(pageType, "/Views/Customers/CustomersPage.xaml");
            PagesMetaData.Add(pageType, pageMetaData);

            pageType = PageType.Suppliers;
            pageMetaData = new PageMetaData(pageType, "/Views/Suppliers/SuppliersPage.xaml");
            PagesMetaData.Add(pageType, pageMetaData);

            pageType = PageType.Payments;
            pageMetaData = new PageMetaData(pageType, "/Views/Payments/PaymentsPage.xaml");
            PagesMetaData.Add(pageType, pageMetaData);

            pageType = PageType.System;
            pageMetaData = new PageMetaData(pageType, "/Views/Payments/PaymentsPage.xaml");
            PagesMetaData.Add(pageType, pageMetaData);
        }

        private void AddCustomersEditor()
        {
            PageType pageType = PageType.Customers;
            Type type = typeof(Data.Customer);
            EditorType editorType = EditorType.AllCustomers;
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "לקוחות";
            tableEditorMetadata.SingleCaption = "לקוח";
            tableEditorMetadata.Fields.Add(new ColumnMetadata("TZ", "תעודת זהות"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Name", "שם"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Family", "משפחה"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("City.Name", "עיר"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Street", "רחוב"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("HouseNumber", "מס' בית"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Phone1", "טלפון 1"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Phone2", "טלפון 2"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "תשלומים", new PaymentsConverter(), true));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "חובות", new DebtsConverter(), true));

            tableEditorMetadata.IconKind = PackIconKind.HumanHandsdown;
            tableEditorMetadata.InitList = () => { return new ObservableCollection<Customer>(new CustomersBL().GetCustomers()); };
            tableEditorMetadata.ChildEditorType = EditorType.Customer;
            tableEditorMetadata.NewEditorType = EditorType.CustomerNew;

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddContractsEditor()
        {
            PageType pageType = PageType.Contracts;
            Type type = typeof(Data.CustomerInProject);
            EditorType editorType = EditorType.AllContracts;
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "חוזים";
            tableEditorMetadata.SingleCaption = "חוזה";
            tableEditorMetadata.Fields.Add(new ColumnMetadata("IsActive", "פעיל?", new YesNoConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Customer", "שם לקוח", new CustomerConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Project.Name", "שם פרויקט"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Project", "כתובת פרויקט", new ProjectAddressConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Flat.FlatNumber", "מס' דירה"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("StartDate", "תחילת חוזה"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("EndDate", "סיום חוזה"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "תשלומים", new PaymentsConverter(), true));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "חובות", new DebtsConverter(), true));

            tableEditorMetadata.IconKind = PackIconKind.FormatFloatLeft;
            tableEditorMetadata.InitList = () => { return new ObservableCollection<CustomerInProject>(new CustomersBL().GetCustomerInProjects()); };
            tableEditorMetadata.ChildEditorType = EditorType.Contract;
            tableEditorMetadata.NewEditorType = EditorType.ContractNew;
            tableEditorMetadata.RowBackgroundConverter = new ContractHighlightConverter();

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddSuppliersEditor()
        {
            PageType pageType = PageType.Suppliers;
            Type type = typeof(Data.Supplier);
            EditorType editorType = EditorType.AllSuppliers;
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "ספקים";
            tableEditorMetadata.SingleCaption = "ספק";
            tableEditorMetadata.Fields.Add(new ColumnMetadata("CNPJ", "ח.פ"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("ServiceType.Name", "סוג שירות"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Name", "שם"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Family", "משפחה"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("City.Name", "עיר"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Street", "רחוב"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("HouseNumber", "מס' בית"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Phone1", "טלפון 1"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Phone2", "טלפון 2"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "תשלומים", new PaymentsConverter(), true));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "חובות", new DebtsConverter(), true));

            tableEditorMetadata.IconKind = PackIconKind.Run;
            tableEditorMetadata.InitList = () => { return new ObservableCollection<Supplier>(new SuppliersBL().GetSuppliers()); };
            tableEditorMetadata.ChildEditorType = EditorType.Supplier;
            tableEditorMetadata.NewEditorType = EditorType.SupplierNew;

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddConstantPaymentsEditor()
        {
            EditorType editorType = EditorType.ConstantPayments;
            Type type = typeof(ConstantPayment);
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, PageType.Payments, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.IconKind = PackIconKind.Database;

            tableEditorMetadata.Fields.Add(new ColumnMetadata("DueDate", "תאריך יעד"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Amount", "סכום"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("PaymentType.Name", "סוג תשלום"));

            tableEditorMetadata.ChildEditorType = EditorType.ConstantPaymentNew;
            tableEditorMetadata.NewEditorType = EditorType.ConstantPaymentNew;
            tableEditorMetadata.EditInPopup = true;

            tableEditorMetadata.PluralCaption = "תשלומים קבועים";
            tableEditorMetadata.SingleCaption = "תשלום קבוע";

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddPaymentsEditor()
        {
            EditorType editorType = EditorType.AllPayments;
            TableEditorMetadata tableEditorMetadata = GetPaymentsMetadata(editorType);

            tableEditorMetadata.IconKind = PackIconKind.Database;
            tableEditorMetadata.InitList = () => { return new ObservableCollection<Payment>(new PaymentsBL().GetPayments()); };
            tableEditorMetadata.PluralCaption = "תשלומים";
            tableEditorMetadata.SingleCaption = "תשלום";
            //    tableEditorMetadata.RowStateConverter = new PaymentStateConverter();
            tableEditorMetadata.RowBackgroundConverter = new PaymentHighlightConverter();

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddRevenuesEditor()
        {
            EditorType editorType = EditorType.AllRevenues;
            TableEditorMetadata tableEditorMetadata = GetPaymentsMetadata(editorType);

            tableEditorMetadata.PluralCaption = "הכנסות";
            tableEditorMetadata.SingleCaption = "הכנסה";
            tableEditorMetadata.IconKind = PackIconKind.DatabasePlus;
            tableEditorMetadata.InitList = () =>
            {
                return new ObservableCollection<Payment>(new PaymentsBL().GetPayments()
.Where(payment => payment.PaymentRelation != null && payment.PaymentRelation.ToSenderTypeId == 4).ToList());
            };

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddExpensesEditor()
        {
            EditorType editorType = EditorType.AllExpenses;
            TableEditorMetadata tableEditorMetadata = GetPaymentsMetadata(editorType);

            tableEditorMetadata.PluralCaption = "הוצאות";
            tableEditorMetadata.SingleCaption = "הוצאה";
            tableEditorMetadata.IconKind = PackIconKind.DatabaseMinus;
            tableEditorMetadata.InitList = () =>
            {
                return new ObservableCollection<Payment>(new PaymentsBL().GetPayments()
                .Where(payment => payment.PaymentRelation != null && payment.PaymentRelation.FromSenderTypeId == 4).ToList());
            };
            tableEditorMetadata.AfterCreateNewEditor = (editorViewModel) =>
            {
                PaymentViewModel paymentViewModel = editorViewModel as PaymentViewModel;

                if (paymentViewModel != null)
                {
                    paymentViewModel.PaymentType = DebtType.Expense;
                }

            };

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private TableEditorMetadata GetPaymentsMetadata(EditorType editorType)
        {
            Type type = typeof(Payment);
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, PageType.Payments, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.IconKind = PackIconKind.HomeModern;

            tableEditorMetadata.Fields.Add(new ColumnMetadata("PaymentDate", "תאריך"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Amount", "סכום"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("PaymentRelation", "סוג הוצאה", new PaymentTypeConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "פרויקט", new PaymentProjectConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "דירה", new PaymentFlatConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "מקור/יעד", new CustomerOrSupplierByEntityConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("PaymentMethod.Name", "אמצעי תשלום"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "סוג תשלום", new PaymentTypesConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Comment", "הערה"));

            tableEditorMetadata.ChildEditorType = EditorType.Payment;
            tableEditorMetadata.NewEditorType = EditorType.PaymentNew;

            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata(".", "פרויקט", new PaymentProjectConverter()));
            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata(".", "לקוח/ספק", new CustomerOrSupplierByEntityConverter()));
            tableEditorMetadata.Groups.Add(tableEditorMetadata.AvailableGroups[0]);
            tableEditorMetadata.Groups.Add(tableEditorMetadata.AvailableGroups[1]);

            tableEditorMetadata.BeforeDelete = (list) =>
            {
                foreach (Payment payment in list)
                {
                    if (payment?.PaymentItems?.Count > 0)
                    {
                        foreach (PaymentItem paymentItem in payment?.PaymentItems)
                        {
                            if (paymentItem?.Debt != null)
                            {

                                paymentItem.Debt.AmountPaid = paymentItem.Debt.AmountPaid - paymentItem.Amount;
                                paymentItem.Debt.DelinquentAmount = paymentItem.Debt.Amount - paymentItem.Debt.AmountPaid;
                            }
                        }
                    }
                }
            };

            return tableEditorMetadata;
        }

        private void AddPaymentItemsEditor()
        {
            PageType pageType = PageType.Payments;
            Type type = typeof(PaymentItem);
            EditorType editorType = EditorType.PaymentItems;
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "סעיפי תשלום";
            tableEditorMetadata.SingleCaption = "סעיף תשלום";

            tableEditorMetadata.Fields.Add(new ColumnMetadata("Amount", "סכום"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Payment", "סוג תשלום", new PaymentTypesConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Payment.Amount", "סך חשבונית"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Payment.Date", "תאריך תשלום"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Debt.Amount", "סכום חוב"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Debt.DelinquentAmount", "יתרת חוב"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Debt.DueDate", "תאריך יעד חוב"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Payment.PaymentMethod.Name", "אמצעי תשלום"));

            tableEditorMetadata.IconKind = PackIconKind.Database;

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddDebtsEditor()
        {
            PageType pageType = PageType.Payments;
            Type type = typeof(Data.Debt);
            EditorType editorType = EditorType.AllDebts;
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "חובות";
            tableEditorMetadata.SingleCaption = "חוב";

            tableEditorMetadata.Fields.Add(new ColumnMetadata("DueDate", "תאריך יעד"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Amount", "סכום"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("AmountPaid", "סכום ששולם"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("DelinquentAmount", "יתרת חוב"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("PaymentType.Name", "סוג תשלום"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "פרויקט", new DebtProjectConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "דירה", new DebtFlatConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "לקוח/ספק", new CustomerOrSupplierByEntityConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("PaymentRelation", "סוג חוב", new PaymentTypeConverter()));

            tableEditorMetadata.RowStateConverter = new DebtStateConverter();
            tableEditorMetadata.RowBackgroundConverter = new DebtHighlightConverter();

            tableEditorMetadata.IconKind = PackIconKind.CodeNotEqual;
            tableEditorMetadata.InitList = () => { return new ObservableCollection<Debt>(new PaymentsBL().GetDebts()); };
            tableEditorMetadata.ChildEditorType = EditorType.Debt;
            tableEditorMetadata.NewEditorType = EditorType.DebtNew;
            //tableEditorMetadata.EntityWrapperType = typeof(PaymentWrapper);

            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata(".", "פרויקט", new DebtProjectConverter()));
            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata(".", "לקוח/ספק", new CustomerOrSupplierByEntityConverter()));
            tableEditorMetadata.Groups.Add(tableEditorMetadata.AvailableGroups[0]);
            tableEditorMetadata.Groups.Add(tableEditorMetadata.AvailableGroups[1]);

            ICommand addPayment = new RelayCommand((parameter) =>
            {
                try
                {
                    TableViewModel tableViewModel = parameter as TableViewModel;
                    PaymentsUtils.OpenAddPayment(tableViewModel.SelectedItems as IList<Debt>);
                }
                catch (Exception ex)
                {
                    log.HandleError(ex);
                }
            },
               (parameter) =>
               {
                   TableViewModel tableViewModel = parameter as TableViewModel;

                   return tableViewModel?.SelectedItems?.Count > 0;
               });

            tableEditorMetadata.Buttons.Add(new RealEstate.ButtonMetadata("בצע תשלום", addPayment, PackIconKind.Database));

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddCountriesEditor()
        {
            PageType pageType = PageType.System;
            Type type = typeof(Data.Country);
            EditorType editorType = EditorType.Countries;
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "מדינות";
            tableEditorMetadata.SingleCaption = "מדינה";
            tableEditorMetadata.EditInPopup = true;

            tableEditorMetadata.Fields.Add(new ColumnMetadata("Name", "שם") { Width = new DataGridLength(230) });

            tableEditorMetadata.IconKind = PackIconKind.FlagVariant;
            tableEditorMetadata.InitList = () => { return new ObservableCollection<Country>(new GeneralBL().GetCountries()); };
            tableEditorMetadata.ChildEditorType = EditorType.Country;
            tableEditorMetadata.NewEditorType = EditorType.Country;

            tableEditorMetadata.GetReadOnlyItems = (sourceList) =>
            {
                IList<Country> list = sourceList as IList<Country>;
                return  list.Where(country => country.Id == 1).ToList();
            };

            tableEditorMetadata.EditInPopup = true;
            tableEditorMetadata.RowBackgroundConverter = new EntityLockedHighlightConverter();

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddSendersEditor()
        {
            PageType pageType = PageType.System;
            Type type = typeof(Data.SenderType);
            EditorType editorType = EditorType.Senders;
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "נמענים";
            tableEditorMetadata.SingleCaption = "נמען";
            tableEditorMetadata.EditInPopup = true;

            tableEditorMetadata.Fields.Add(new ColumnMetadata("Name", "שם") { Width = new DataGridLength(230) });

            tableEditorMetadata.IconKind = PackIconKind.AccountSettingsVariant;
            tableEditorMetadata.InitList = () => { return new ObservableCollection<SenderType>(new GeneralBL().GetSenderTypes()); };
            tableEditorMetadata.ChildEditorType = EditorType.Sender;
            tableEditorMetadata.NewEditorType = EditorType.Sender;

            tableEditorMetadata.GetReadOnlyItems = (sourceList) =>
            {
                IList<SenderType> list = sourceList as IList<SenderType>;
                return list.Where(sender => sender.Id <= 4).ToList();
            };

            tableEditorMetadata.EditInPopup = true;
            tableEditorMetadata.RowBackgroundConverter = new EntityLockedHighlightConverter();

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddPaymentRelationsEditor()
        {
            PageType pageType = PageType.System;
            Type type = typeof(Data.PaymentRelation);
            EditorType editorType = EditorType.PaymentRelations;
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "נמעני תשלום";
            tableEditorMetadata.SingleCaption = "נמעני תשלום";
            tableEditorMetadata.EditInPopup = true;

            tableEditorMetadata.Fields.Add(new ColumnMetadata("FromSenderType.Name", "נמען מקור"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("ToSenderType.Name", "נמען יעד"));

            tableEditorMetadata.IconKind = PackIconKind.AccountSwitch;
            tableEditorMetadata.InitList = () => { return new ObservableCollection<PaymentRelation>(new PaymentsBL().GetPaymentRelations()); };
            tableEditorMetadata.ChildEditorType = EditorType.PaymentRelation;
            tableEditorMetadata.NewEditorType = EditorType.PaymentRelation;

            tableEditorMetadata.EditInPopup = true;

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddCitiesEditor()
        {
            PageType pageType = PageType.System;
            Type type = typeof(Data.City);
            EditorType editorType = EditorType.Cities;
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "ערים";
            tableEditorMetadata.SingleCaption = "עיר";
            tableEditorMetadata.EditInPopup = true;

            tableEditorMetadata.Fields.Add(new ColumnMetadata("Name", "שם") { Width = new DataGridLength(220) });

            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata("Country.Name", "מדינה"));
            tableEditorMetadata.Groups.Add(tableEditorMetadata.AvailableGroups[0]);

            tableEditorMetadata.IconKind = PackIconKind.City;
            tableEditorMetadata.InitList = () => { return new ObservableCollection<City>(new GeneralBL().GetCities()); };
            tableEditorMetadata.ChildEditorType = EditorType.City;
            tableEditorMetadata.NewEditorType = EditorType.City;

            tableEditorMetadata.EditInPopup = true;

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddProjectsEditor()
        {
            PageType pageType = PageType.Undefined;
            EditorType editorType = EditorType.AllProjects;
            TableEditorMetadata tableEditorMetadata = GetProjectsMetadata(pageType, editorType);

            tableEditorMetadata.InitList = () => { return new ObservableCollection<Project>(new ProjectsBL().GetProjects()); };

            tableEditorMetadata.GetEditorTypeByEntity = (entity) =>
            {
                Project project = entity as Project;
                if (project != null && project.ProjectTypeId == 2)
                    return EditorType.LeaseProject;
                else
                    return EditorType.SaleProject;
            };

            tableEditorMetadata.NewEditorType = EditorType.SaleProjectNew;

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddLeaseProjectsEditor()
        {
            PageType pageType = PageType.Leases;
            EditorType editorType = EditorType.LeaseAllProjects;
            TableEditorMetadata tableEditorMetadata = GetProjectsMetadata(pageType, editorType);

            tableEditorMetadata.ChildEditorType = EditorType.LeaseProject;
            tableEditorMetadata.NewEditorType = EditorType.LeaseProjectNew;

            tableEditorMetadata.InitList = () =>
            {
                return new ObservableCollection<Project>(new ProjectsBL().GetProjects().Where(project => project != null &&
                project.ProjectTypeId == 2));
            };

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddSaleProjectsEditor()
        {
            PageType pageType = PageType.Sale;
            EditorType editorType = EditorType.SaleAllProjects;
            TableEditorMetadata tableEditorMetadata = GetProjectsMetadata(pageType, editorType);

            tableEditorMetadata.InitList = () =>
            {
                return new ObservableCollection<Project>(new ProjectsBL().GetProjects().Where(project => project != null &&
                project.ProjectTypeId == 1));
            };

            tableEditorMetadata.ChildEditorType = EditorType.SaleProject;
            tableEditorMetadata.NewEditorType = EditorType.SaleProjectNew;

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddFlatsEditor()
        {
            PageType pageType = PageType.Undefined;
            EditorType editorType = EditorType.AllFlats;
            TableEditorMetadata tableEditorMetadata = GetFlatsMetadata(pageType, editorType);

            tableEditorMetadata.InitList = () => { return new ObservableCollection<Flat>(new FlatsBL().GetFlats()); };

            tableEditorMetadata.GetEditorTypeByEntity = (entity) =>
            {
                Flat flat = entity as Flat;
                if (flat != null && flat.Project.ProjectTypeId == 2)
                    return EditorType.LeaseFlat;
                else
                    return EditorType.SaleFlat;
            };

            tableEditorMetadata.NewEditorType = EditorType.SaleFlatNew;

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddLeaseFlatsEditor()
        {
            PageType pageType = PageType.Leases;
            EditorType editorType = EditorType.LeaseAllFlats;
            TableEditorMetadata tableEditorMetadata = GetFlatsMetadata(pageType, editorType);

            tableEditorMetadata.Fields.Insert(7, new ColumnMetadata("Price", "דמי שכירות", null, true));

            tableEditorMetadata.InitList = () =>
            {
                return new ObservableCollection<Flat>(new LeasesBL().GetFlats().Where(flat => flat != null &&
                flat.Project?.ProjectTypeId == 2));
            };

            tableEditorMetadata.ChildEditorType = EditorType.LeaseFlat;
            tableEditorMetadata.NewEditorType = EditorType.LeaseFlatNew;

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private void AddSaleFlatsEditor()
        {
            PageType pageType = PageType.Sale;
            EditorType editorType = EditorType.SaleAllFlats;
            TableEditorMetadata tableEditorMetadata = GetFlatsMetadata(pageType, editorType);

            tableEditorMetadata.Fields.Insert(7, new ColumnMetadata("Price", "מחיר", null, true));

            tableEditorMetadata.InitList = () =>
            {
                return new ObservableCollection<Flat>(new LeasesBL().GetFlats().Where(flat => flat != null &&
                flat.Project?.ProjectTypeId == 1));
            };

            tableEditorMetadata.ChildEditorType = EditorType.SaleFlat;
            tableEditorMetadata.NewEditorType = EditorType.SaleFlatNew;

            EditorsMetaData.Add(editorType, tableEditorMetadata);
        }

        private TableEditorMetadata GetProjectsMetadata(PageType pageType, EditorType editorType)
        {
            Type type = typeof(Project);
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "פרויקטים";
            tableEditorMetadata.SingleCaption = "פרויקט";
            tableEditorMetadata.IconKind = PackIconKind.HomeModern;

            tableEditorMetadata.Fields.Add(new ColumnMetadata("Name", "שם"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("ProjectType.Name", "סוג פרויקט"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("City.Name", "עיר"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Street", "רחוב"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("HouseNumber", "מס' בית"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "תשלומים", new PaymentsConverter(), true));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "הכנסות", new RevenuesConverter(), true));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "הוצאות", new ExpensesConverter(), true));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "חובות", new DebtsConverter(), true));

            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata("City.Name", "עיר"));
            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata("Customer", "לקוח"));
            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata("Supplier", "ספק"));
            tableEditorMetadata.Groups.Add(tableEditorMetadata.AvailableGroups[0]);

            return tableEditorMetadata;
        }

        private TableEditorMetadata GetFlatsMetadata(PageType pageType, EditorType editorType)
        {
            Type type = typeof(Flat);
            TableEditorMetadata tableEditorMetadata = new TableEditorMetadata(type, typeof(TableViewModel), editorType, pageType, "/Views/Customers/CustomersTable.xaml");

            tableEditorMetadata.PluralCaption = "דירות";
            tableEditorMetadata.SingleCaption = "דירה";
            tableEditorMetadata.IconKind = PackIconKind.HomeMapMarker;

            tableEditorMetadata.Fields.Add(new ColumnMetadata("Project.ProjectType.Name", "סוג"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Project.Name", "פרויקט"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Project", "כתובת", new ProjectAddressConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("FlatNumber", "מס' דירה"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("Floor", "קומה"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("SquareMeter", @"מ""ר"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("HasElevator", "מעלית?", new YesNoConverter()));
            tableEditorMetadata.Fields.Add(new ColumnMetadata("RoomsCount", "חדרים"));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "תשלומים", new PaymentsConverter(), true));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "הכנסות", new RevenuesConverter(), true));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "הוצאות", new ExpensesConverter(), true));
            tableEditorMetadata.Fields.Add(new ColumnMetadata(".", "חובות", new DebtsConverter(), true));

            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata("Project.City.Name", "עיר"));
            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata("Project.Name", "פרויקט"));
            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata("Customer", "לקוח"));
            tableEditorMetadata.AvailableGroups.Add(new ColumnMetadata("Supplier", "ספק"));
            tableEditorMetadata.Groups.Add(tableEditorMetadata.AvailableGroups[0]);
            tableEditorMetadata.Groups.Add(tableEditorMetadata.AvailableGroups[1]);

            return tableEditorMetadata;
        }

        #endregion Private Methods

        #region Properties

        public Dictionary<EditorType, EditorMetaData> EditorsMetaData { get; set; }
        public Dictionary<PageType, PageMetaData> PagesMetaData { get; set; }

        #endregion Properties
    }
}
