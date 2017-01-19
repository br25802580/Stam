using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
 public   class BeforeSaveResult
    {
        public BeforeSaveResult()
        {
            IsValidData = true;
        }
        public bool IsValidData { get; set; }
        public string ErrorMessage { get; set; }
    }
}
