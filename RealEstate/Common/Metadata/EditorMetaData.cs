using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class EditorMetaData
    {
        #region Ctor

        public EditorMetaData(Type viewModelType, EditorType editorType, PageType pageType, string uriString, string displayName = "")
        {
            ViewModelType = viewModelType;
            EditorType = editorType;
            UriString = uriString;
            PageType = pageType;
            DisplayName = displayName;
        }

        #endregion Ctor
        #region Properties
        public PackIconKind IconKind { get; set; }
        public string DisplayName { get; set; }
        public string SingleCaption { get; set; }
        public string PluralCaption { get; set; }
        public EditorType EditorType { get; set; }
        public PageType PageType { get; set; }

        public Type ViewModelType { get; set; }

        public string UriString { get; set; }

        #endregion Properties
    }
}
