using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public class NewEditorMetadata : EditorMetaData
    {
        public NewEditorMetadata(Type entityType, Type viewModelType, EditorType editorType, PageType pageType, Type newEditorType = null, Type entityWrapperType = null)
            : base(viewModelType, editorType, pageType, string.Empty)

        {
            EntityType = entityType;
            NewEditorType = newEditorType;
            EntityWrapperType = entityWrapperType;
        }

        public Type EntityType { get; set; }
        public Type NewEditorType { get; set; }
        public Type EntityWrapperType { get; set; }
    }
}
