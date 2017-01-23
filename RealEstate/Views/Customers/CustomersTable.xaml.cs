using log4net;
using MaterialDesignThemes.Wpf;
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
    public partial class CustomersTable : ModernUserControl
    {
        ILog log = LogManager.GetLogger(typeof(CustomersTable));
        public CustomersTable()
        {
            InitializeComponent();

            DataContextChanged += CustomersTable_DataContextChanged;
        }

        private void SetDataTriggers(TableViewModel tableViewModel)
        {
            if (tableViewModel.TableEditorMetadata.RowStateConverter != null || tableViewModel.TableEditorMetadata.RowBackgroundConverter != null)
            {
                Style rowStyle = new Style(typeof(DataGridRow));

                if (tableViewModel.TableEditorMetadata.RowStateConverter != null)
                {
                    DataTrigger dataTrigger = new DataTrigger();
                    dataTrigger.Binding = new Binding() { Converter = tableViewModel.TableEditorMetadata.RowStateConverter };
                    dataTrigger.Value = EntityState.Marked;
                    //  SolidColorBrush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f1f1f1"));
                    //     dataTrigger.Setters.Add(new Setter(Control.BackgroundProperty, brush));
                    SolidColorBrush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#59ff66"));
                    //SolidColorBrush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#5cce22"));
                    dataTrigger.Setters.Add(new Setter(Control.BorderBrushProperty, brush));
                    dataTrigger.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0, 0, 0, 2)));

                    rowStyle.Triggers.Add(dataTrigger);

                    dataTrigger = new DataTrigger();
                    dataTrigger.Binding = new Binding() { Converter = tableViewModel.TableEditorMetadata.RowStateConverter };
                    dataTrigger.Value = EntityState.Problem;
                    brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff5959"));
                    //brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ff0000"));
                    dataTrigger.Setters.Add(new Setter(Control.BorderBrushProperty, brush));
                    dataTrigger.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0, 0, 0, 2)));
                    rowStyle.Triggers.Add(dataTrigger);

                    dataTrigger = new DataTrigger();
                    dataTrigger.Binding = new Binding() { Converter = tableViewModel.TableEditorMetadata.RowStateConverter };
                    dataTrigger.Value = EntityState.Note;
                    brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#ffff59"));
                    dataTrigger.Setters.Add(new Setter(Control.BorderBrushProperty, brush));
                    dataTrigger.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0, 0, 0, 2)));

                    rowStyle.Triggers.Add(dataTrigger);
                }


                if (tableViewModel.TableEditorMetadata.RowBackgroundConverter != null)
                {
                    DataTrigger dataTrigger = new DataTrigger();
                    dataTrigger.Binding = new Binding() { Converter = tableViewModel.TableEditorMetadata.RowBackgroundConverter, ConverterParameter = tableViewModel };
                    dataTrigger.Value = true;
                    SolidColorBrush brush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f1f1f1"));
                    dataTrigger.Setters.Add(new Setter(Control.BackgroundProperty, brush));

                    rowStyle.Triggers.Add(dataTrigger);

                    rowStyle.Triggers.Add(dataTrigger);
                }

                DG1.RowStyle = rowStyle;
            }
        }

        private void SetFields(TableViewModel tableViewModel)
        {
            IList<ColumnMetadata> fields = tableViewModel?.Fields;
            if (fields != null)
            {
                foreach (var field in fields)
                {
                    var col = new DataGridTextColumn();
                    col.Header = field.Header;
                    //col.MaxWidth = 150;
                    col.Width = field.Width;

                    col.Binding = new Binding(field.Property);

                    if (field.Property.Contains("Date"))
                    {
                        col.Binding.StringFormat = "d";
                    }
                    if (field.Property.Contains("Amount") || field.IsAmountFormat)
                    {
                        col.Binding.StringFormat = "c";
                        col.ElementStyle = Application.Current.Resources["TextBlockLTR"] as Style;
                        col.EditingElementStyle = Application.Current.Resources["TextBoxLTR"] as Style;
                    }

                    if (field.Converter != null)
                    {
                        ((Binding)col.Binding).Converter = field.Converter;
                    }

                    if (field.BackgroundBinding != null)
                    {
                        Style defaultStyle = Application.Current.TryFindResource(typeof(TextBlock)) as Style;
                        Style elementStyle = new Style(typeof(TextBlock), defaultStyle);
                        Setter setter = new Setter(TextBlock.BackgroundProperty, field.BackgroundBinding);
                        elementStyle.Setters.Add(setter);
                        elementStyle.Setters.Add(new Setter(TextBlock.PaddingProperty, new Thickness(5,2,5,2)));
                        elementStyle.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Stretch));
                        elementStyle.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center));
                        col.Foreground = new SolidColorBrush(Colors.Black);
                        col.ElementStyle = elementStyle; 
                    }

                    DG1.Columns.Add(col);
                }
            }
        }

        private void SetButtons(TableViewModel tableViewModel)
        {
            IList<ButtonMetadata> buttons = tableViewModel?.Buttons;
            if (buttons != null)
            {
                foreach (var buttonData in buttons)
                {
                    var button = new Button();
                    button.ToolTip = buttonData.Header;
                    button.Command = buttonData.Command;
                    button.Style = Application.Current.Resources["CommandButton"] as Style;
                    button.CommandParameter = DataContext;
                    StackPanel StackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
                    StackPanel.Children.Add(new PackIcon()
                    {
                        Kind = buttonData.IconKind,
                        Height = 18,
                        Width = 18,
                        BorderThickness = new Thickness(0, 0, 5, 0)
                    });
                    StackPanel.Children.Add(new TextBlock()
                    {
                        Text = buttonData.Header
                    });

                    button.Content = StackPanel;
                    ButtonsPanel.Children.Add(button);
                }
            }
        }

        private void CustomersTable_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            TableViewModel tableViewModel = DataContext as TableViewModel;

            if (tableViewModel != null)
            {
                try
                {
                    SetFields(tableViewModel);
                    SetButtons(tableViewModel);
                    SetDataTriggers(tableViewModel);
                }
                catch (Exception ex)
                {
                    log.HandleError(ex);
                }
            }
        }
    }
}
