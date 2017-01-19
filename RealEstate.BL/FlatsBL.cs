using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Data;
using System.Data.Entity;

namespace RealEstate.BL
{
    public class FlatsBL
    {
        public IList<Flat> GetFlats()
        {
            return RealEstateProvider.Instance.Flats.ToList();
        }
    }
}
