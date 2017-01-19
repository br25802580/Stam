﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Data;
using System.Data.Entity;

namespace RealEstate.BL
{
    public class SuppliersBL
    {
        public IList<Supplier> GetSuppliers()
        {
            return RealEstateProvider.Instance.Suppliers.ToList();
        }

        public IList<SupplierInProject> GetSupplierInProjects()
        {
            return RealEstateProvider.Instance.SupplierInProjects.ToList();
        }
    }
}