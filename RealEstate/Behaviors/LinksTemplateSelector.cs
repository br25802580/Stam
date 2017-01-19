using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RealEstate
{
    public class LinkTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate
            SelectTemplate(object item, DependencyObject container)
        {
                DataTemplate selectedTemplate=null;
            FrameworkElement element = container as FrameworkElement;

            if (element != null && item != null && item is ModernLink)
            {
                ModernLink link = item as ModernLink;
                EditorViewModel viewModel = link.ViewModel as EditorViewModel;
                switch (viewModel.EditorMetaData.EditorType)
                {
                    case EditorType.LeaseAllProjects:
                        selectedTemplate= element.FindResource("AllProjectsTemplate") as DataTemplate;
                        break;
                    case EditorType.LeaseAllFlats:
                        selectedTemplate= element.FindResource("AllFlatsTemplate") as DataTemplate;
                        break;
                    case EditorType.LeaseProject:
                        selectedTemplate= element.FindResource("ProjectTemplate") as DataTemplate;
                        break;
                    case EditorType.LeaseFlat:
                        selectedTemplate= element.FindResource("FlatTemplate") as DataTemplate;
                        break;
                    case EditorType.SaleAllProjects:
                        selectedTemplate = element.FindResource("AllProjectsTemplate") as DataTemplate;
                        break;
                    case EditorType.SaleAllFlats:
                        selectedTemplate = element.FindResource("AllFlatsTemplate") as DataTemplate;
                        break;
                    case EditorType.SaleProject:
                        selectedTemplate = element.FindResource("ProjectTemplate") as DataTemplate;
                        break;
                    case EditorType.SaleFlat:
                        selectedTemplate = element.FindResource("FlatTemplate") as DataTemplate;
                        break;
                    case EditorType.AllCustomers:
                        selectedTemplate = element.FindResource("CustomersTemplate") as DataTemplate;
                        break;
                    case EditorType.Customer:
                        selectedTemplate = element.FindResource("CustomerTemplate") as DataTemplate;
                        break;
                    case EditorType.AllContracts:
                        selectedTemplate = element.FindResource("ContractsTemplate") as DataTemplate;
                        break;
                    case EditorType.Contract:
                        selectedTemplate = element.FindResource("ContractTemplate") as DataTemplate;
                        break;
                    case EditorType.AllSuppliers:
                        selectedTemplate = element.FindResource("SuppliersTemplate") as DataTemplate;
                        break;
                    case EditorType.Supplier:
                        selectedTemplate = element.FindResource("SupplierTemplate") as DataTemplate;
                        break;
                    case EditorType.AllPayments:
                        selectedTemplate = element.FindResource("PaymentsTemplate") as DataTemplate;
                        break;
                    case EditorType.AllExpenses:
                        selectedTemplate = element.FindResource("ExpensesTemplate") as DataTemplate;
                        break;
                    case EditorType.AllRevenues:
                        selectedTemplate = element.FindResource("RevenuesTemplate") as DataTemplate;
                        break;
                    case EditorType.AllDebts:
                        selectedTemplate = element.FindResource("DebtsTemplate") as DataTemplate;
                        break;
                    case EditorType.Payment:
                        selectedTemplate = element.FindResource("PaymentTemplate") as DataTemplate;
                        break;
                    case EditorType.Debt:
                        selectedTemplate = element.FindResource("DebtTemplate") as DataTemplate;
                        break;
                    default:
                        selectedTemplate = null;
                        break;
                }
              
            }

            return selectedTemplate;

        }
    }
}
