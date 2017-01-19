using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate
{
    public static class EditorUtils
    {
        public static string GetEditorKey(object entity, EditorType editorType)
        {
            string editorKey = string.Empty;
            Type entityType = entity.GetType();
            string entityId = Convert.ToString(entityType.GetProperty("Id").GetValue(entity, null));

            editorKey = string.Format("{0}-{1}", editorType.ToString(), entityId);

            return editorKey;
        }
    }
}
