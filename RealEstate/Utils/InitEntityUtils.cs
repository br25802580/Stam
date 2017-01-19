using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public static class EntityCreatorUtils
    {
      
        public static CustomerInProject NewCustomerInProject(Customer customer, Project project, Flat flat)
        {
            CustomerInProject customerInProject = new CustomerInProject()
            {
                Customer = customer,
                Project = project,
                Flat = flat,
                CreatedDate=DateTime.Now,
                StartDate= DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
            };

            if (project.ProjectTypeId == 1)
                customerInProject.LeaseContract = new LeaseContract()
                {
                    Amount = 0,
                    MonthlyPaymentDay = 10
                };
            else
                customerInProject.SaleContract = new SaleContract()
                {
                     GettingKeyDate= DateTime.Now.AddYears(1)
                };

            return customerInProject;
        }
    }
}
