using FirstFloor.ModernUI.Presentation;
using RealEstate.BL;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class LeaseFlatViewModel : FlatViewModel
    {
        #region Ctor

        public LeaseFlatViewModel() : base()
        {
        }

        #endregion Ctor

        #region Properties

        protected override int ProjectTypeId
        {
            get
            {
                return 2;
            }
        }

        public override string CustomersTitle
        {
            get
            {
                return "שוכרים";
            }
        }

        #endregion Properties  
    }
}
