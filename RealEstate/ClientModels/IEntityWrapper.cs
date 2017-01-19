using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public interface IEntityWrapper
    {
        object Entity { get; set; }
        bool IsExpanded { get; set; }
    }
}
