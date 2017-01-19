using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Data;
using System.Data.Entity;

namespace RealEstate.BL
{
    public class PaymentsBL
    {
        public IList<Payment> GetPayments()
        {
            return RealEstateProvider.Instance.Payments.ToList();
        }

        //public IList<DebtExt> GetDebts()
        //{
        //    return RealEstateProvider.Instance.Debts.Select(debt => new DebtExt()
        //    { Debt = debt }).ToList();
        //}
        public IList<Debt> GetDebts()
        {
            return RealEstateProvider.Instance.Debts.ToList();
        }

        public IList<PaymentType> GetPaymentTypes()
        {
            return RealEstateProvider.Instance.PaymentTypes.ToList();
        }

        public IList<PaymentMethod> GetPaymentMethods()
        {
            return RealEstateProvider.Instance.PaymentMethods.ToList();
        }

        public IList<PaymentRelation> GetPaymentRelations()
        {
            return RealEstateProvider.Instance.PaymentRelations.ToList();
        }

        public PaymentRelation GetPaymentRelation(int fromSenderTypeId,int toSenderTypeId)
        {
            PaymentRelation paymentRelation= GetPaymentRelations().FirstOrDefault
                (_paymentRelation => _paymentRelation.FromSenderTypeId == fromSenderTypeId
                && _paymentRelation.ToSenderTypeId == toSenderTypeId);

            if (paymentRelation == null)
            {
                paymentRelation = new PaymentRelation() { FromSenderTypeId = fromSenderTypeId, ToSenderTypeId = toSenderTypeId };
                new GeneralBL().AddEntity(paymentRelation);
            }

            return paymentRelation;
        }
    }
}
