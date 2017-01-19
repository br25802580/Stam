using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Data;
using System.Data.Entity;

namespace RealEstate.BL
{
    public class LeasesBL
    {
        public IList<Project> GetProjects()
        {
            return RealEstateProvider.Instance.Projects.ToList();
            //return  RealEstateEntities.ReferenceEquals.
        }

        public IList<Flat> GetFlats()
        {
            return RealEstateProvider.Instance.Flats.ToList();
            //return  RealEstateEntities.ReferenceEquals.
        }
    }
}
