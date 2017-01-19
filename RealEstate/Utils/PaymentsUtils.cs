using RealEstate.BL;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public static class PaymentsUtils
    {
        public static void CalculateDebtAmount(this Debt debt)
        {
            if (debt != null)
            {
                //if (debt.PaymentRelation?.FromSenderTypeId != 4)
                //{
                debt.AmountPaid = debt.PaymentItems.Sum(paymentItem => paymentItem.Amount);
                //}
                debt.DelinquentAmount = debt.Amount - debt.AmountPaid;
            }
        }

        public static int GetPaymentsAmount(Project project)
        {
            return GetPaymentsAmount(GetPayments(project));
        }

        public static int GetPaymentsRevenues(Project project)
        {
            return GetPaymentsRevenues(GetPayments(project));
        }

        public static int GetPaymentsExpenses(Project project)
        {
            return GetPaymentsExpenses(GetPayments(project));
        }

        public static int GetPaymentsAmount(CustomerInProject customerInProject)
        {
            return GetPaymentsAmount(customerInProject.Payments);
        }

        public static int GetPaymentsAmount(Flat flat)
        {
            return GetPaymentsAmount(GetPayments(flat));
        }

        public static int GetPaymentsRevenues(Flat flat)
        {
            return GetPaymentsRevenues(GetPayments(flat));
        }

        public static int GetPaymentsExpenses(Flat flat)
        {
            return GetPaymentsExpenses(GetPayments(flat));
        }

        public static IEnumerable<Payment> GetPayments(Project project)
        {
            return GetPayments(project.CustomerInProjects, project.SupplierInProjects);
        }

        public static IEnumerable<Payment> GetPayments(Flat flat)
        {
            return GetPayments(flat.CustomerInProjects, flat.SupplierInProjects);
        }

        private static IEnumerable<Payment> GetPayments(IEnumerable<CustomerInProject> customerInProjects, IEnumerable<SupplierInProject> supplierInProjects)
        {
            IEnumerable<Payment> customerPayments = customerInProjects.SelectMany(cInP => cInP.Payments);
            IEnumerable<Payment> supplierPayments = supplierInProjects.SelectMany(sInP => sInP.Payments);

            return customerPayments.Union(supplierPayments);
        }

        public static int GetDebtsAmount(Project project)
        {
            return GetDebtsAmount(GetDebts(project));
        }

        public static int GetDebtsAmount(CustomerInProject customerInProject)
        {
            return GetDebtsAmount(customerInProject.Debts);
        }

        public static int GetDebtsAmount(Flat flat)
        {
            return GetDebtsAmount(GetDebts(flat));
        }

        public static IEnumerable<Debt> GetDebts(Project project)
        {
            return GetDebts(project.CustomerInProjects, project.SupplierInProjects);
        }

        public static IEnumerable<Debt> GetDebts(Flat flat)
        {
            return GetDebts(flat.CustomerInProjects, flat.SupplierInProjects);
        }

        private static IEnumerable<Debt> GetDebts(IEnumerable<CustomerInProject> customerInProjects, IEnumerable<SupplierInProject> supplierInProjects)
        {
            IEnumerable<Debt> customerPayments = customerInProjects.SelectMany(cInP => cInP.Debts);
            IEnumerable<Debt> supplierPayments = supplierInProjects.SelectMany(sInP => sInP.Debts);

            return customerPayments.Union(supplierPayments);
        }

        public static int GetPaymentsAmount(IEnumerable<Payment> payments)
        {
            Func<Payment, int> condition = payment => payment.PaymentRelation?.FromSenderTypeId == 4 ? payment.Amount.Value * -1 : payment.Amount.Value;
            return GetPaymentsAmountByCondition(payments, condition);
        }
        public static int GetPaymentsRevenues(IEnumerable<Payment> payments)
        {
            Func<Payment, int> condition = payment => payment.PaymentRelation?.ToSenderTypeId == 4 ? payment.Amount.Value : 0;
            return GetPaymentsAmountByCondition(payments, condition);
        }

        public static int GetPaymentsExpenses(IEnumerable<Payment> payments)
        {
            Func<Payment, int> condition = payment => payment.PaymentRelation?.FromSenderTypeId == 4 ? payment.Amount.Value : 0;
            return GetPaymentsAmountByCondition(payments, condition);
        }

        private static int GetPaymentsAmountByCondition(IEnumerable<Payment> payments, Func<Payment, int> condition)
        {
            {
                return payments.Sum(condition);
            }
        }

        public static int GetDebtsAmount(IEnumerable<Debt> debts)
        {
            return debts.Sum(debt =>
            debt.PaymentRelation?.FromSenderTypeId == 4 ?
            debt.DelinquentAmount.HasValue ? debt.DelinquentAmount.Value * -1 : 0
            :
            debt.DelinquentAmount.HasValue ? debt.DelinquentAmount.Value : 0);
        }

        public static void OpenAddPayment(IList<Debt> debts)
        {
            if (debts?.Count > 0)
            {
                Debt firstDebt = debts[0];

                CustomerInProject customerInProject = firstDebt.CustomerInProject;
                SupplierInProject supplierInProject = firstDebt.SupplierInProject;

                Project project = null;
                Flat flat = null;
                Customer customer = null;
                Supplier supplier = null;
                int fromSenderTypeId = 1;
                int toSenderTypeId = 4;
                bool isSameTarget = false;

                if (customerInProject != null)
                {
                    isSameTarget = debts.All(debt => debt.CustomerInProject == customerInProject);

                    if (isSameTarget)
                    {
                        project = customerInProject.Project;
                        flat = customerInProject.Flat;
                        customer = customerInProject.Customer;
                        fromSenderTypeId = project?.ProjectTypeId == 1 ? 2 : 3;
                        toSenderTypeId = 4;
                    }
                }
                else if (supplierInProject != null)
                {
                    isSameTarget = debts.All(debt => debt.SupplierInProject == supplierInProject);
                    if (isSameTarget)
                    {
                        project = supplierInProject.Project;
                        flat = supplierInProject.Flat;
                        supplier = supplierInProject.Supplier;
                        fromSenderTypeId = 4;
                        toSenderTypeId = 1;
                    }
                }

                if (debts.All(debt => debt.DelinquentAmount == 0))
                {
                    string message = debts.Count == 1 ? "החוב שבחרת שולם במלואו" : "החובות שבחרת שולמו במלואם";

                    DialogUtils.DisplayMessage($"אין אפשרות לבצע תשלום. {message}.", "ביצוע תשלום");
                    return;
                }

                if (isSameTarget)
                {
                    OpenAddPayment(project, flat, customer, supplier, fromSenderTypeId, toSenderTypeId, debts);
                }
                else
                {
                    DialogUtils.DisplayMessage("אין אפשרות לבצע תשלום. וודא שהחובות מכילים ערך זהה בשדות פרויקט, דירה, ספק/לקוח.", "ביצוע תשלום");
                }
            }
        }

        public static void OpenAddPayment(Project project, Flat flat, Customer customer, Supplier supplier, int fromSenderTypeId, int toSenderTypeId, IList<Debt> debts = null)
        {
            string title = $"הוספת תשלום- {project.Name}";

            if (flat != null)
                title = $"{title} דירה {flat.FlatNumber}";

            if (customer != null)
                title = $"{title}, {customer.Name} {customer.Family}";
            else if (supplier != null)
                title = $"{title}, {supplier.Name} {supplier.Family}";

            PaymentRelation paymentRelation = new PaymentsBL().GetPaymentRelations().FirstOrDefault
                (_paymentRelation => _paymentRelation.FromSenderTypeId == fromSenderTypeId && _paymentRelation.ToSenderTypeId == toSenderTypeId);

            Action<EditorViewModel> afterCreateNewEditor = (_editorViewModel) =>
            {
                PaymentViewModel paymentViewModel = _editorViewModel as PaymentViewModel;

                paymentViewModel.DuringAutoChanges = true;

                if (fromSenderTypeId == 4)
                    paymentViewModel.PaymentType = DebtType.Expense;

                paymentViewModel.PaymentRelation = paymentRelation;
                paymentViewModel.Customer = customer;
                paymentViewModel.Supplier = supplier;
                paymentViewModel.Project = project;
                paymentViewModel.Flat = flat;
                //paymentViewModel.Payment.da
                paymentViewModel.DuringAutoChanges = false;

                if (debts != null)
                {
                    foreach (var debt in debts)
                    {
                        if (debt.DelinquentAmount > 0)
                        {
                            PaymentItem paymentItem = new PaymentItem() { Debt = debt };
                            PaymentItemWrapper paymentItemWrapper = new PaymentItemWrapper(paymentItem, paymentViewModel.Payment);
                            //   paymentItem.Amount = debt.Amount;
                            //     paymentItem.Debt = debt;
                            paymentViewModel.PaymentItems.Add(paymentItemWrapper);
                        }
                    }
                    //PaymentItemWrapper paymentItemWrapper = new PaymentItemWrapper(paymentItem, paymentViewModel.Payment);
                }
            };

            DialogResult dialogResult = RealEstateRepository.Instance.OpenNewEditor(EditorType.PaymentNew, title, afterCreateNewEditor);

            if (dialogResult.Result != null)
            {
                //if (AfterAddEntity != null)
                //    AfterAddEntity(dialogResult.Result);

                new GeneralBL().AddEntity(dialogResult.Result);
                new GeneralBL().Save();
                RealEstateRepository.Instance.RefreshAllEditors();
                //  SelectedItems.Clear();
                //   SelectedItems.Add(entity);
            }
        }

        public static void OpenAddDebt(Project project, Flat flat, Customer customer, Supplier supplier, int fromSenderTypeId, int toSenderTypeId)
        {
            string title = $"הוספת חוב- {project.Name}";

            if (flat != null)
                title = $"{title} דירה {flat.FlatNumber}";

            if (customer != null)
                title = $"{title}, {customer.Name} {customer.Family}";
            else if (supplier != null)
                title = $"{title}, {supplier.Name} {supplier.Family}";

            PaymentRelation paymentRelation = new PaymentsBL().GetPaymentRelations().FirstOrDefault
                (_paymentRelation => _paymentRelation.FromSenderTypeId == fromSenderTypeId && _paymentRelation.ToSenderTypeId == toSenderTypeId);

            Action<EditorViewModel> afterCreateNewEditor = (_editorViewModel) =>
            {
                DebtViewModel debtViewModel = _editorViewModel as DebtViewModel;

                debtViewModel.DuringAutoChanges = true;

                if (fromSenderTypeId == 4)
                    debtViewModel.DebtType = DebtType.Expense;

                debtViewModel.PaymentRelation = paymentRelation;
                debtViewModel.Project = project;
                debtViewModel.Customer = customer;
                debtViewModel.Flat = flat;
                debtViewModel.Supplier = supplier;

                debtViewModel.DuringAutoChanges = false;
            };

            DialogResult dialogResult = RealEstateRepository.Instance.OpenNewEditor(EditorType.DebtNew, title, afterCreateNewEditor);

            if (dialogResult.Result != null)
            {
                //if (AfterAddEntity != null)
                //    AfterAddEntity(dialogResult.Result);

                new GeneralBL().AddEntity(dialogResult.Result);
                new GeneralBL().Save();
                RealEstateRepository.Instance.RefreshAllEditors();
                //  SelectedItems.Clear();
                //   SelectedItems.Add(entity);
            }
        }
    }
}
