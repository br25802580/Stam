using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Data;
using System.Data.Entity;
using System.Collections;

namespace RealEstate.BL
{
    public class GeneralBL
    {
        public IList<Country> GetCountries()
        {
            return RealEstateProvider.Instance.Countries.ToList();
        }

        public IList<City> GetCities()
        {
            return RealEstateProvider.Instance.Cities.OrderBy(city => city.Name).ToList();
        }

        public IList<SenderType> GetSenderTypes()
        {
            return RealEstateProvider.Instance.SenderTypes.ToList();
        }

        public IList<ProjectType> GetProjectTypes()
        {
            return RealEstateProvider.Instance.ProjectTypes.ToList();
        }

        public IList<ServiceType> GetServiceTypes()
        {
            return RealEstateProvider.Instance.ServiceTypes.ToList();
        }

        public IList<Gender> GetGenders()
        {
            return RealEstateProvider.Instance.Genders.ToList();
        }

        public void AddEntity(object entity)
        {
            var context = RealEstateProvider.Instance;
            try
            {
                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
            }
            catch (Exception)
            {
                context.Entry(entity).State = EntityState.Detached;
            }
        }

        public void DeleteEntities(IList entities, bool needSave = true)
        {
            var context = RealEstateProvider.Instance;

            foreach (var entity in entities)
            {
                DeleteEntity(entity, false);
            }

            if (needSave)
                context.SaveChanges();
        }

        public EntityState GetEntityState(object entity)
        {
            var context = RealEstateProvider.Instance;

            if (entity != null)
                return context.Entry(entity).State;

            return EntityState.Detached;
        }

        public void SaveEntity(object entity)
        {
            var context = RealEstateProvider.Instance;

            if (entity != null)
                context.Entry(entity).State = EntityState.Modified;

            context.SaveChanges();
        }

        public void Save()
        {
            var context = RealEstateProvider.Instance;

            context.SaveChanges();
        }

        public void DeleteEntity(object entity, bool needSave = true)
        {
            var context = RealEstateProvider.Instance;

            if (entity == null || context.Entry(entity).State == EntityState.Deleted)
                return;

            Type entityType = entity.GetType();

            if (typeof(Customer).IsAssignableFrom(entityType))
            {
                Customer customer = entity as Customer;

                DeleteEntities(customer.CustomerInProjects.ToList(), false);
            }
            else if (typeof(Supplier).IsAssignableFrom(entityType))
            {
                Supplier supplier = entity as Supplier;
                DeleteEntities(supplier.SupplierInProjects.ToList(), false);
            }
            else if (typeof(Project).IsAssignableFrom(entityType))
            {
                Project project = entity as Project;
                DeleteEntities(project.CustomerInProjects.ToList(), false);
                DeleteEntities(project.SupplierInProjects.ToList(), false);
                DeleteEntities(project.Flats.ToList(), false);
                DeleteEntities(project.ConstantPayments.ToList(), false);
            }
            else if (typeof(Flat).IsAssignableFrom(entityType))
            {
                Flat flat = entity as Flat;
                DeleteEntities(flat.CustomerInProjects.ToList(), false);
                DeleteEntities(flat.SupplierInProjects.ToList(), false);
            }
            else if (typeof(Payment).IsAssignableFrom(entityType))
            {
                Payment payment = entity as Payment;
                //DeleteEntity(payment.CustomerInProject, false);
                //DeleteEntity(payment.SupplierInProject, false);
                DeleteEntities(payment.PaymentItems.ToList(), false);
            }
            else if (typeof(Debt).IsAssignableFrom(entityType))
            {
                Debt debt = entity as Debt;
                foreach (var item in debt.PaymentItems.ToList())
                {
                    item.Debt = null;
                    SaveEntity(item);
                }
                //DeleteEntity(debt.CustomerInProject, false);
                //DeleteEntity(debt.SupplierInProject, false);
            }
            else if (typeof(CustomerInProject).IsAssignableFrom(entityType))
            {
                CustomerInProject customerInProject = entity as CustomerInProject;
                DeleteEntities(customerInProject.Debts.ToList(), false);
                DeleteEntities(customerInProject.Payments.ToList(), false);
            }
            else if (typeof(SupplierInProject).IsAssignableFrom(entityType))
            {
                SupplierInProject supplierInProject = entity as SupplierInProject;
                DeleteEntities(supplierInProject.Debts.ToList(), false);
                DeleteEntities(supplierInProject.Payments.ToList(), false);
            }

            context.Entry(entity).State = EntityState.Deleted;

            //else if (entity.GetType().BaseType == typeof(CustomerInProject))
            //{
            //    CustomerInProject customerInProject = entity as CustomerInProject;
            //    DeleteEntities(customerInProject.Payments.ToList(), false);
            //    DeleteEntities(customerInProject.Debts.ToList(), false);
            //}
            //else if (entity.GetType().BaseType == typeof(SupplierInProject))
            //{
            //    SupplierInProject supplierInProject = entity as SupplierInProject;
            //    DeleteEntities(supplierInProject.Payments.ToList(), false);
            //    DeleteEntities(supplierInProject.Debts.ToList(), false);
            //}

            if (needSave)
                context.SaveChanges();
        }
    }
}
