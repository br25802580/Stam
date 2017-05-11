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
    public class PaymentChequeViewModel : EditorViewModel
    {
        #region Ctor

        public PaymentChequeViewModel()
        {
        }

        #endregion Ctor

        #region Properties

        public PaymentCheque PaymentCheque
        {
            get { return Entity as PaymentCheque; }
            set { Entity = value; }
        }

        #endregion Properties

        #region Methods

        public override void Init()
        {
            base.Init();
        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (PaymentCheque.Amount == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר סכום";
                return beforeSaveResult;
            }

            if (PaymentCheque.DueDate == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר תאריך יעד";
                return beforeSaveResult;
            }

            if (PaymentCheque.Number == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר מס' צ'ק";
                return beforeSaveResult;
            }

            return beforeSaveResult;
        }

        public override void RefreshData()
        {
            base.RefreshData();
        }

        #endregion Methods
    }

}
