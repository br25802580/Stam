using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealEstate
{
    /// <summary>
    /// Interaction logic for FlatCustomers.xaml
    /// </summary>
    public partial class Table : UserControl
    {
        public Table()
        {
            InitializeComponent();
            DataContextChanged += CustomersTable_DataContextChanged;
        }

        private void CustomersTable_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TableViewModel tableViewModel = DataContext as TableViewModel;
            TableEditorMetadata tableEditorMetadata = tableViewModel?.EditorMetaData as TableEditorMetadata;
            IList<ColumnMetadata> fields = tableEditorMetadata?.Fields;
            if (fields != null)
            {
                foreach (var field in fields)
                {
                    var col = new DataGridTextColumn();
                    col.Header = field.Header;

                    col.Binding = new Binding(field.Property);

                    if (field.Property.Contains("Date"))
                    {
                        col.Binding.StringFormat = "d";
                    }

                    if (field.Converter != null)
                    {
                        ((Binding)col.Binding).Converter = field.Converter;
                    }

                    DG1.Columns.Add(col);
                }
            }

       
        }
    }
}
