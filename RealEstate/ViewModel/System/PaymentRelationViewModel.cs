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
    public class PaymentRelationViewModel : EditorViewModel
    {
        #region Ctor

        public PaymentRelationViewModel()
        {
            InitList(typeof(SenderType));
        }

        #endregion Ctor

        #region Properties

        public PaymentRelation PaymentRelation
        {
            get { return Entity as PaymentRelation; }
        }

        #endregion Properties

        #region Methods

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (PaymentRelation.FromSenderType == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר נמען מקור";
                return beforeSaveResult;
            }

            if (PaymentRelation.ToSenderType == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר נמען יעד";
                return beforeSaveResult;
            }

            return beforeSaveResult;
        }

        #endregion Methods
    }

}
