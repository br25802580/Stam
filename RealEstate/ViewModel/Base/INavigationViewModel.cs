using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
  public  interface INavigationViewModel
    {
        LinkCollection Links { get; set; }
        Dictionary<string, EditorViewModel> OpenedEditors { get; set; }
       // PageType PageType { get; set; }
    }
}
