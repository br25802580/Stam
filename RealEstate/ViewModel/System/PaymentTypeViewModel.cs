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
    public class PaymentTypeViewModel : EditorViewModel
    {
        #region Ctor

        public PaymentTypeViewModel()
        {
            InitList(typeof(SenderType));
        }

        #endregion Ctor

        #region Properties

        private SenderType fromSenderType;
        public SenderType FromSenderType
        {
            get { return fromSenderType; }
            set
            {
                if (value != fromSenderType)
                {
                    fromSenderType = value;
                    OnPropertyChanged("FromSenderType");

                    if (paymentRelation.FromSenderType != value)
                    {
                        PaymentRelation = new PaymentsBL().GetPaymentRelation(fromSenderType.Id, ToSenderType.Id);
                    }
                }
            }
        }

        private SenderType toSenderType;
        public SenderType ToSenderType
        {
            get { return toSenderType; }
            set
            {
                if (value != toSenderType)
                {
                    toSenderType = value;

                    OnPropertyChanged("ToSenderType");

                    if (paymentRelation.ToSenderType != value)
                    {
                        PaymentRelation = new PaymentsBL().GetPaymentRelation(FromSenderType.Id, toSenderType.Id);
                    }
                }
            }
        }

        private PaymentRelation paymentRelation;
        public PaymentRelation PaymentRelation
        {
            get { return paymentRelation; }
            set
            {
                if (value != paymentRelation)
                {
                    OnPropertyChanged("PaymentRelation");
                    paymentRelation = value;
                    PaymentType.PaymentRelation = value;

                    if (paymentRelation != null)
                    {
                        FromSenderType = paymentRelation.FromSenderType;
                        ToSenderType = paymentRelation.ToSenderType;
                    }
                }
            }
        }

        private DebtType paymentRelationType = DebtType.None;
        public DebtType PaymentRelationType
        {
            get { return paymentRelationType; }
            set
            {
                if (paymentRelationType != value)
                {
                    paymentRelationType = value;
                    OnPropertyChanged("PaymentRelationType");


                    FromSenderTypes = new ObservableCollection<SenderType>(SenderTypes);
                    ToSenderTypes = new ObservableCollection<SenderType>(SenderTypes);
                    if (paymentRelationType == DebtType.Revenue)
                    {
                        if (PaymentRelation == null || (PaymentRelation.FromSenderTypeId != 2 && PaymentRelation.FromSenderTypeId != 3
                            || PaymentRelation.ToSenderTypeId != 4))
                        {
                            PaymentRelation = new PaymentsBL().GetPaymentRelation(3, 4);
                        }

                        FromSenderTypes.Remove(FromSenderTypes.First(senderType => senderType.Id == 4));
                    }
                    else
                    {
                        if (PaymentRelation == null || (PaymentRelation.FromSenderTypeId != 4
                          || PaymentRelation.ToSenderTypeId != 1))
                        {
                            PaymentRelation = new PaymentsBL().GetPaymentRelation(4, 1);
                        }

                        ToSenderTypes.Remove(ToSenderTypes.First(senderType => senderType.Id == 4));
                    }
                }
            }
        }

        private ObservableCollection<SenderType> fromSenderTypes;
        public ObservableCollection<SenderType> FromSenderTypes
        {
            get { return fromSenderTypes; }
            set
            {
                fromSenderTypes = value;
                OnPropertyChanged("FromSenderTypes");
            }
        }

        private ObservableCollection<SenderType> toSenderTypes;
        public ObservableCollection<SenderType> ToSenderTypes
        {
            get { return toSenderTypes; }
            set
            {
                toSenderTypes = value;
                OnPropertyChanged("ToSenderTypes");
            }
        }

        public PaymentType PaymentType
        {
            get { return Entity as PaymentType; }
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
                PaymentRelationType = DebtType.Revenue;
            }
            else
            {
                PaymentRelation paymentRelation = PaymentType.PaymentRelation;
                PaymentRelationType = paymentRelation.FromSenderTypeId == 4 ? DebtType.Expense : DebtType.Revenue;
                PaymentRelation = paymentRelation;
            }

        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            if (PaymentType.PaymentRelation == null)
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר יחסי תשלום";
                return beforeSaveResult;
            }

            if (string.IsNullOrWhiteSpace(PaymentType.Name))
            {
                beforeSaveResult.IsValidData = false;
                beforeSaveResult.ErrorMessage = "נא הגדר סוג תשלום";
                return beforeSaveResult;
            }

            return beforeSaveResult;
        }

        #endregion Methods
    }

}
