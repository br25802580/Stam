using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Data;
using System.Data.Entity;

namespace RealEstate.BL
{
    public class CustomersBL
    {
        public IList<Customer> GetCustomers()
        {
            return RealEstateProvider.Instance.Customers.ToList();
        }

        public IList<CustomerInProject> GetCustomerInProjects()
        {
            return RealEstateProvider.Instance.CustomerInProjects.ToList();
        }
    }
}
