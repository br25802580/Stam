using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class PageMetaData
    {
        #region Ctor

        public PageMetaData( PageType pageType, string uriString)
        {
            UriString = uriString;
            PageType = pageType;
        }

        #endregion Ctor

        #region Properties

        public PageType PageType { get; set; }

        public string UriString { get; set; }

        #endregion Properties
    }
}
