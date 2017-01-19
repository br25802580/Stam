using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Data;
using System.Data.Entity;

namespace RealEstate.BL
{
    public class ProjectsBL
    {
        public IList<Project> GetProjects()
        {
            return RealEstateProvider.Instance.Projects.ToList();
        }
    }
}
