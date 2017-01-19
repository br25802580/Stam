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
    public class SalesFlatViewModel : FlatViewModel
    {
        #region Ctor

        public SalesFlatViewModel() : base()
        {
        }

        #endregion Ctor

        #region Properties

        protected override int ProjectTypeId
        {
            get
            {
                return 1;
            }
        }

        public override string CustomersTitle
        {
            get
            {
                return "קונים";
            }
        }

        #endregion Properties
    }
}
