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
    public class CityViewModel : EditorViewModel
    {
        #region Ctor

        public CityViewModel()
        {
            InitList(typeof(Country));
        }

        #endregion Ctor

        #region Properties

        public City City
        {
            get { return Entity as City; }
        }

        #endregion Properties

        #region Methods

        public override void Init()
        {
            base.Init();

            if (!IsEditEditor)
            {
            }

        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (string.IsNullOrEmpty(City.Name))
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר שם";
                return beforeSaveResult;
            }

            if (City.Country == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר מדינה";
                return beforeSaveResult;
            }

            return beforeSaveResult;
        }

        #endregion Methods
    }

}
