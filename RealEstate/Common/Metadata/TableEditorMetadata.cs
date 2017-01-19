using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace RealEstate
{
    public class TableEditorMetadata : EditorMetaData
    {
        public TableEditorMetadata(Type entityType, Type viewModelType, EditorType editorType, PageType pageType, string uriString, Type entityWrapperType = null)
            : base(viewModelType, editorType, pageType, uriString)

        {
            EntityType = entityType;
            //  NewEditorType = newEditorType;
            EntityWrapperType = entityWrapperType;
            Fields = new List<ColumnMetadata>();
            Groups = new List<ColumnMetadata>();
            AvailableGroups = new List<ColumnMetadata>();
            EditInPopup = false;
            Buttons = new List<ButtonMetadata>();
        }

        public Type EntityType { get; set; }
        //public Type NewEditorType { get; set; }
        public Type EntityWrapperType { get; set; }
        public IList<ColumnMetadata> Fields { get; set; }
        public Func<IList> InitList { get; set; }
        public Action<EditorViewModel> AfterCreateNewEditor { get; set; }
        public Func<IList, IList> GetReadOnlyItems { get; set; }
        public Action<IList> BeforeDelete { get; set; }
        public EditorType ChildEditorType { get; set; }
        public EditorType NewEditorType { get; set; }
        public IList<ColumnMetadata> Groups { get; set; }
        public IList<ColumnMetadata> AvailableGroups { get; set; }
        public Func<object, EditorType> GetEditorTypeByEntity { get; set; }
        public bool EditInPopup { get; set; }
        public IValueConverter RowStateConverter { get; set; }
        public IValueConverter RowBackgroundConverter { get; set; }
        public IList<ButtonMetadata> Buttons { get; set; }
    }

    public class ButtonMetadata
    {
        public ButtonMetadata( string header, ICommand command, PackIconKind iconKind)
        {
            Header = header;
            Command = command;
            IconKind = iconKind;
        }

        public string Header { get; set; }
        public ICommand Command { get; set; }
        public PackIconKind IconKind { get; set; }
    }

    public class ColumnMetadata
    {
        public ColumnMetadata(string property, string header, IValueConverter converter, bool isAmountFormat = false) : this(property, header)
        {
            Converter = converter;
            IsAmountFormat = isAmountFormat;
        }

        public ColumnMetadata(string property, string header)
        {
            Property = property;
            Header = header;
        }

        public string Header { get; set; }
        public string Property { get; set; }
        public bool IsAmountFormat { get; set; }
        public IValueConverter Converter { get; set; }
        public DataGridLength Width { get; set; }

    }
}
