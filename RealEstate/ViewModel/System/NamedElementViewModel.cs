﻿using FirstFloor.ModernUI.Presentation;
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
                if (string.IsNullOrEmpty(country.Name))
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
                    if (string.IsNullOrEmpty(senderType.Name))
                    {
                        beforeSaveResult.IsValidData = false;
                        beforeSaveResult.ErrorMessage = "נא הגדר שם נמען";
                        return beforeSaveResult;
                    }
                }
            }

            return beforeSaveResult;
        }

        #endregion Methods
    }

}