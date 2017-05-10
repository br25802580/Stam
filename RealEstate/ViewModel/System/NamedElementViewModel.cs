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
    public class NamedElementViewModel : EditorViewModel
    {
        #region Ctor

        public NamedElementViewModel()
        {
            //     InitList(typeof(PaymentType));
        }

        #endregion Ctor

        #region Properties



        #endregion Properties

        #region Methods

        public override void Init()
        {
            base.Init();

        }

        public override BeforeSaveResult BeforeSave()
        {
            BeforeSaveResult beforeSaveResult = new BeforeSaveResult();

            Country country = Entity as Country;
            if (country != null)
            {
                if (string.IsNullOrWhiteSpace(country.Name))
                {
                    beforeSaveResult.IsValidData = false;
                    beforeSaveResult.ErrorMessage = "נא הגדר שם מדינה";
                    return beforeSaveResult;
                }
            }
            else
            {
                SenderType senderType = Entity as SenderType;
                if (senderType != null)
                {
                    if (string.IsNullOrWhiteSpace(senderType.Name))
                    {
                        beforeSaveResult.IsValidData = false;
                        beforeSaveResult.ErrorMessage = "נא הגדר שם נמען";
                        return beforeSaveResult;
                    }
                }
                else
                {
                    Bank bank = Entity as Bank;
                    if (bank != null)
                    {
                        if (string.IsNullOrWhiteSpace(bank.Name))
                        {
                            beforeSaveResult.IsValidData = false;
                            beforeSaveResult.ErrorMessage = "נא הגדר שם בנק";
                            return beforeSaveResult;
                        }
                    }
                    else
                    {
                        ServiceType serviceType = Entity as ServiceType;
                        if (serviceType != null)
                        {
                            if (string.IsNullOrWhiteSpace(serviceType.Name))
                            {
                                beforeSaveResult.IsValidData = false;
                                beforeSaveResult.ErrorMessage = "נא הגדר סוג שירות";
                                return beforeSaveResult;
                            }
                        }
                    }
                }
            }

            return beforeSaveResult;
        }

        #endregion Methods
    }

}
