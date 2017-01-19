using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public partial class SupplierDTO : BaseDTO
    {
        public SupplierDTO()
        {
            //    this.SupplierInProjects = new HashSet<SupplierInProject>();
            // this.Payments = new HashSet<Payment>();
        }

        public override void ToDTO(object entity)
        {
          //  entity.clone(this);
            //   base.ToDTO(entity);
        }


        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private string tZ;
        public string TZ
        {
            get { return tZ; }
            set
            {
                tZ = value;
                OnPropertyChanged("TZ");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private string family;
        public string Family
        {
            get { return family; }
            set
            {
                family = value;
                OnPropertyChanged("Family");
            }
        }
        // public Nullable<int> CityId 
        private string street;
        public string Street
        {
            get { return street; }
            set
            {
                street = value;
                OnPropertyChanged("Street");
            }
        }
        private Nullable<int> houseNumber;
        public Nullable<int> HouseNumber
        {
            get { return houseNumber; }
            set
            {
                houseNumber = value;
                OnPropertyChanged("HouseNumber");
            }
        }
        private string phone1;
        public string Phone1
        {
            get { return phone1; }
            set
            {
                phone1 = value;
                OnPropertyChanged("Phone1");
            }
        }
        private string phone2;
        public string Phone2
        {
            get { return phone2; }
            set
            {
                phone2 = value;
                OnPropertyChanged("Phone2");
            }
        }
        //   public Nullable<int> ServiceTypeId 

        private City city;

        public City City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }


        //     public virtual City City 
        //   public virtual ServiceType ServiceType 
        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //   public virtual ICollection<SupplierInProject> SupplierInProjects 
        //   [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        ///    public virtual ICollection<Payment> Payments 
    }

    public static class Cloner
    {
        public static object clone(this object source, object target)
        {
            FieldInfo[] fis = source.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            target = Activator.CreateInstance(source.GetType());
            foreach (FieldInfo fi in fis)
            {
                if (fi.FieldType.Namespace != source.GetType().Namespace)
                    fi.SetValue(target, fi.GetValue(source));
                else
                {
                    object obj = fi.GetValue(source);

                    if (obj != null)
                        fi.SetValue(target, obj.clone(target));
                }
            }
            return target;
        }
    }
}
