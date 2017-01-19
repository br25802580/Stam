using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Data
{
    public class RealEstateProvider
    {
       // private static volatile RealEstateEntities instance;

        private static readonly RealEstateEntities instance = new RealEstateEntities();
        private RealEstateProvider() { }

        public static RealEstateEntities Instance
        {
            get
            {
                return instance;
            }
        }

        //public static RealEstateEntities Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            lock (syncRoot)
        //            {
        //                if (instance == null)
        //                    instance = new RealEstateEntities();
        //            }
        //        }

        //        return instance;
        //    }
        //}
    }
}
