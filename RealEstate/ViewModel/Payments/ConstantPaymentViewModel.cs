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
    public class ConstantPaymentViewModel : EditorViewModel
    {
        #region Ctor

        public ConstantPaymentViewModel()
        {
            //     InitList(typeof(PaymentType));
        }

        #endregion Ctor

        #region Properties

        public ConstantPayment ConstantPayment
        {
            get { return Entity as ConstantPayment; }
            set { Entity = value; }
        }

        #endregion Properties

        #region Methods

        public override void Init()
        {
            base.Init();

            PaymentRelation paymentRelation = new PaymentsBL().GetPaymentRelations().FirstOrDefault(_paymentRelation => _paymentRelation
            .FromSenderTypeId == 2 && _paymentRelation.ToSenderTypeId == 4);

            if (paymentRelation != null)
                PaymentTypes = paymentRelation.PaymentTypes.ToList();
         // EntityTitle= $"{Customer.Name} {Customer.Family}";
        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (ConstantPayment.DueDate == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר תאריך יעד";
                return beforeSaveResult;
            }

            if (ConstantPayment.PaymentType == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר סוג תשלום";
                return beforeSaveResult;
            }

            if (ConstantPayment.Amount == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר סכום";
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
