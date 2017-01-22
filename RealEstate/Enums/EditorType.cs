using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public enum EditorType
    {
        Undefined,
        AllProjects,
        AllFlats,
        LeaseAllProjects,
        LeaseAllFlats,
        LeaseProject,
        LeaseProjectNew,
        LeaseFlat,
        LeaseFlatNew,
        SaleAllProjects,
        SaleAllFlats,
        SaleProject,
        SaleProjectNew,
        SaleFlat,
        SaleFlatNew,
        AllCustomers,
        Customer,
        CustomerNew,
        AllSuppliers,
        Supplier,
        SupplierNew,
        AllPayments,
        AllRevenues,
        AllExpenses,
        Payment,
        PaymentNew,
        AllDebts,
        Debt,
        DebtNew,
        ConstantPayments,
        ConstantPaymentNew,
        PaymentItems,
        Files,
        Map,
        AllContracts,
        Contract,
        ContractNew,
        Countries,
        Country,
        Cities,
        City,
        Senders,
        Sender,
        PaymentRelations,
        PaymentRelation,
        Banks,
        Bank,
        ServiceTypes,
        ServiceType,
        PaymentTypes,
        PaymentType,
        PaymentTypeForServices,
        PaymentTypeForService
    }
}
