using FirstFloor.ModernUI.Presentation;
using RealEstate.BL;
using RealEstate.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class PaymentTypeForServiceViewModel : EditorViewModel
    {
        #region Ctor

        public PaymentTypeForServiceViewModel()
        {
            InitList(typeof(ServiceType));
        }

        #endregion Ctor

        #region Properties

    
        public PaymentTypeForService PaymentTypeForService
        {
            get { return Entity as PaymentTypeForService; }
        }

        #endregion Properties

        #region Methods

        public override void Init()
        {
            bool isEditEditor = IsEditEditor;
            base.Init();

            IsEditEditor = isEditEditor;
            if (!IsEditEditor)
            {
                PaymentTypeForService.PaymentType = new PaymentType() { PaymentRelation = new PaymentsBL().GetPaymentRelation(4, 1) } ;
            }
           // else
            ////{
            //    PaymentRelation paymentRelation = PaymentType.PaymentRelation;
            //    PaymentRelationType = paymentRelation.FromSenderTypeId == 4 ? DebtType.Expense : DebtType.Revenue;
            //    PaymentRelation = paymentRelation;
            //}

        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (PaymentTypeForService.ServiceType == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר סוג שירות";
                return beforeSaveResult;
            }

            //if (string.IsNullOrWhiteSpace(PaymentType.Name))
            //{
            //    beforeSaveResult.IsValidData = false;
            //    beforeSaveResult.ErrorMessage = "נא הגדר סוג תשלום";
            //    return beforeSaveResult;
            //}

            return beforeSaveResult;
        }

        #endregion Methods
    }

}
