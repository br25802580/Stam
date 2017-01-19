using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class DebtWrapper : IEntityWrapper
    {
        public Debt Debt { get; set; }
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
                return Debt.CustomerInProject != null ?
                    Debt.CustomerInProject.Project : Debt.SupplierInProject != null ?
                    Debt.SupplierInProject.Project : null;
            }
        }
        public Flat Flat
        {
            get
            {
                return Debt.CustomerInProject != null ?
                    Debt.CustomerInProject.Flat : Debt.SupplierInProject != null ?
                    Debt.SupplierInProject.Flat : null;
            }
        }

        public object Entity
        {
            get
            {
                return Debt;
            }

            set
            {
                Debt = value as Debt;
            }
        }
    }
}
