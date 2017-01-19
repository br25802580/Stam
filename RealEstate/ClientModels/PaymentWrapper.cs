using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class PaymentWrapper : IEntityWrapper
    {
        public Payment Payment { get; set; }
        public bool IsExpanded { get; set; }

        public string ProjectName
        {
            get
            {
                return Project != null ? Project.Name : "";
            }
        }

        public Project Project
        {
            get
            {
                return Payment.CustomerInProject != null ?
                     Payment.CustomerInProject.Project : Payment.SupplierInProject != null ?
                     Payment.SupplierInProject.Project : null;
            }
        }
        public Flat Flat
        {
            get
            {
                return Payment.CustomerInProject != null ?
                     Payment.CustomerInProject.Flat : Payment.SupplierInProject != null ?
                     Payment.SupplierInProject.Flat : null;
            }
        }

        public object Entity
        {
            get
            {
                return Payment;
            }

            set
            {
                Payment = value as Payment;
            }
        }
    }
}
