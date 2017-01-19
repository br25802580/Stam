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
    public class SalesProjectViewModel : ProjectViewModel
    {
        #region Ctor

        public SalesProjectViewModel() : base()
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

        protected override EditorType FlatsEditor
        {
            get
            {
                return EditorType.SaleAllFlats;
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

        #region Methods

        public override void Init()
        {
            base.Init();
        }

   

        #endregion Methods
    }
}
