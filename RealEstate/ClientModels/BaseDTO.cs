using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class BaseDTO : NotifyPropertyChanged
    {
        public virtual object ToEntity()
        {
            return null;
        }

        public virtual  void ToDTO(object entity)
        {
        }
    }
}
