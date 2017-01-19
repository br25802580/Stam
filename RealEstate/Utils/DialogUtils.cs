using FirstFloor.ModernUI.Windows.Controls;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RealEstate
{
    public class DialogUtils
    {
        public static MessageBoxResult DisplayBeforeDeleteMessage()
        {
            return DisplayYesNoMessage("האם אתה בטוח שברצונך למחוק את הפריטים הנבחרים?", "מחיקת פריטים");
        }

        public static bool? DisplayBeforeDetachMessage()
        {
            var dialog = new ModernDialog
            {
                Title = "הסרת פריטים מקושרים",
                Content = "האם אתה בטוח שברצונך להסיר פריטים מקושרים?"
            };
            Button okButton = GetButton(dialog.YesButton, "כן", PackIconKind.Check);
            Button cancelButton = GetButton(dialog.NoButton, "לא", PackIconKind.Close);

            okButton.SetResourceReference(Button.BorderBrushProperty, "Accent");
            okButton.BorderThickness = new Thickness(0, 0, 0, 3);
            dialog.Buttons = new Button[] { cancelButton, okButton };
            dialog.Style = Application.Current.Resources["ModernDialog"] as Style;
            dialog.ShowDialog();
            //dialog.Buttons.
            //  RealEstateRepository.Instance.OpenNewEditor(editorType);
            //     RefreshData();
            return dialog.DialogResult;
        }

        public static bool? DisplayMessage(string message, string title)
        {
            var dialog = new ModernDialog
            {
                Title = title,
                Content = message
            };

            Button okButton = GetButton(dialog.YesButton, "אישור", PackIconKind.Check);
            okButton.SetResourceReference(Button.BorderBrushProperty, "Accent");
            okButton.BorderThickness = new Thickness(0, 0, 0, 3);
            dialog.Buttons = new Button[] { okButton };
            dialog.Style = Application.Current.Resources["ModernDialog"] as Style;

            dialog.ShowDialog();

            return dialog.DialogResult;
        }

        public static MessageBoxResult DisplayYesNoMessage(string message, string title)
        {
            var dialog = new ModernDialog
            {
                Title = title,
                Content = message
            };

            Button okButton = GetButton(dialog.YesButton, "כן", PackIconKind.Check);
            Button cancelButton = GetButton(dialog.NoButton, "לא", PackIconKind.Close);

            okButton.SetResourceReference(Button.BorderBrushProperty, "Accent");
            okButton.BorderThickness = new Thickness(0, 0, 0, 3);
            dialog.Buttons = new Button[] { cancelButton, okButton };
            dialog.Style = Application.Current.Resources["ModernDialog"] as Style;
            dialog.ShowDialog();

            return dialog.MessageBoxResult;
        }

        public static bool? DisplaySaveErrorMessage(string message)
        {
            string saveError = "השמירה נכשלה.";
            saveError += Environment.NewLine;
            saveError += message + ".";

            var dialog = new ModernDialog
            {
                Title = "שמירה",
                Content = saveError
            };
            Button okButton = GetButton(dialog.YesButton, "אישור", PackIconKind.Check);
            //   Button cancelButton = GetButton(dialog.NoButton, "לא", PackIconKind.Close);

            okButton.SetResourceReference(Button.BorderBrushProperty, "Accent");
            okButton.BorderThickness = new Thickness(0, 0, 0, 3);
            dialog.Buttons = new Button[] { okButton };
            dialog.Style = Application.Current.Resources["ModernDialog"] as Style;
            dialog.ShowDialog();
            //dialog.Buttons.
            //  RealEstateRepository.Instance.OpenNewEditor(editorType);
            //     RefreshData();
            return dialog.DialogResult;
        }

        public static Button GetButton(Button btn, string text, PackIconKind iconKind)
        {
            btn.Content = "שמירה";
            btn.Style = Application.Current.Resources["CommandButton"] as Style;
            StackPanel stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            stackPanel.Children.Add(new PackIcon() { Kind = iconKind, Height = 18, Width = 18, Margin = new Thickness(0, 0, 5, 0) });
            stackPanel.Children.Add(new TextBlock() { Text = text });
            btn.Content = stackPanel;

            return btn;
        }

        //public static IList DisplayAttachItemsDialog(EditorType editorType, IList existList = null, Func<object, bool> initialFilter = null, bool enableGrouping = false)
        public static IList DisplayAttachItemsDialog(EditorType editorType, IList existList = null, Func<IList> initList = null, bool enableGrouping = false)
        {
            TableViewModel tableViewModel = RealEstateRepository.Instance.GetTableViewModel(editorType);
            //   tableViewModel.List. peopleList2.Except(peopleList1)
            //   existList.co

            //if (existList != null && initialFilter == null)
            if (existList != null)
            {
                tableViewModel.InitialFilter = (obj) =>
                {
                    return !existList.Contains(obj);
                };
            }

            if (initList != null)
            {
                tableViewModel.InitListSource = initList;
            }

            //if (initialFilter != null)
            //{
            //    tableViewModel.InitialFilter = initialFilter;
            //}

            tableViewModel.DisplayCommands = false;
            tableViewModel.EnableGrouping = enableGrouping;
            tableViewModel.Init();
            // var result = peopleList2.Where(p => !peopleList1.Any(p2 => p2.ID == p.ID));

            CustomersTable tableView = new CustomersTable() { DataContext = tableViewModel };
            IList selectedItems = null;

            var dialog = new ModernDialog
            {
                Title = "קישור פריטים",
                Content = tableView,
                Width = 700
            };
            Button okButton = GetButton(dialog.YesButton, "קישור", PackIconKind.LinkVariant);
            Button cancelButton = GetButton(dialog.CancelButton, "ביטול", PackIconKind.Close);

            okButton.SetResourceReference(Button.BorderBrushProperty, "Accent");
            okButton.BorderThickness = new Thickness(0, 0, 0, 3);
            dialog.Buttons = new Button[] { cancelButton, okButton };
            dialog.Style = Application.Current.Resources["ModernDialog"] as Style;
            dialog.ShowDialog();

            if (dialog.DialogResult == true)
                selectedItems = tableViewModel.SelectedItems;

            return selectedItems;
        }
    }
}
