   M S B A M L   `   `   `  ����E  ARealEstate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RealEstate
RealEstate  5      #    RealEstate.PaymentMainDetails   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      (localclr-namespace:RealEstate    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      N JSpicyTaco.AutoGrid, Version=1.1.19.0, Culture=neutral, PublicKeyToken=null%sthttp://schemas.spicytaco.io/  5      U QMaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  5
      $��RightToLeft=�6A   .��5      �   � 	Resources  5   
   ,   &RealEstate.CustomerOrSupplierConverter   CustomerOrSupplierConverter  BoolToVisibility'   !RealEstate.EnumToBooleanConverter  EnumToBooleanConverter4   .RealEstate.OppositveBoolToVisibilityConverters & "OppositveBoolToVisibilityConverter*   $RealEstate.EnumToVisibilityConverter  EnumToVisibilityConverter"   RealEstate.CustomerConverter  CustomerConverter"   RealEstate.SupplierConverter  SupplierConverter  BoolToVisibilityConverter��%�   &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �       5      60   �� 5      6+     5      6+     5      6e     5      6.     5      6&     5      6&   �� 5      6+   5      .+�5   
   �� .��5      �� $	��200��6   .+�5      �� $��	0,10,0,20q�6   $��Center=�60    �� Orientation$ 
Horizontal=�6K   $��Stretch=�6d   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=null]]   Kind$ Database=�6.   $��35��6?   $��35��6K   $��0,0,20,0q�6V   $��Center=�6h   �� 5      $��Center=�6    $��פרטי תשלום��6;     Heading1#��[ 6M   $��0,0,15,0q�6o   �� 5      $��0,0,0,5q�6     	 Heading2#��[	 61   $��Bottom=�6S   $��LeftToRight=�5   P   $��CharacterEllipsis=�6l   ��6    �� *Payment.Amount+ �� StringFormat$ C��y�5       �� *IsEditEditor+ �� 	Converter# [  �� FallbackValue$ 
Visibility���� 5      $��7,0,0,5q�6    #��[	 61   $��Bottom=�6S   y�5        �� *IsEditEditor+# [ $ 
Visibility����5!      `� 5"       `� 	Converter# [  6+   .1�5#   "   ��  �� Path$ IsCustomerSender�6*   �� 5$   "   $ Customer�6*   �� 5%   "   $ Supplier�6*   5&      5(      �� 5)      $��2,0,0,5q�6    $��Bottom=�61   #��[	 6L   $��CharacterEllipsis=�5*       ��5)   n   �� *Project.Name+$ , {0}��y�5*   A   �� *IsEditEditor+# [ $ 
Visibility��5+      �� 5,      $��0,0,0,20q�6   $ 
Horizontal=�60   y�6J   �� *IsEditEditor+# [ $ 
Visibility��.+�5-      ��  � ToolTip$ 
שמירה��6    
 CommandButton#��[
 6N   ��6.   �� *SaveCommand+.��5.      �� $ 
Horizontal=�6%   .+�5/        $ ContentSave=�66   $��18��6J   $��18��6V   $��0,0,5,0q�6a   �� 50      
שמירה6/   51      52      �� 53      $ פתיחת פרויקט��6   #��[
 6\   ��65   �� *OpenProjectCommand+.��54      �� $ 
Horizontal=�6%   .+�55        $ 
HomeModern=�66   $��18��6I   $��18��6U   $��0,0,5,0q�6`   �� 56      פתיחת פרויקט66   57      58      �� 59      $ פתיחת דירה��6   #��[
 6W   ��63   �� *OpenFlatCommand+.��5:      �� $ 
Horizontal=�6%   .+�5;        $ HomeMapMarker=�66   $��18��6L   $��18��6X   $��0,0,5,0q�6c   �� 5<      פתיחת דירה64   5=      5>      �� 5?      $ פתיחת ספק��6   #��[
 6Z   ��62   �� *OpenSupplierCommand+y�5@      �� *IsSupplierSender+# [ .��5A      �� $ 
Horizontal=�6%   .+�5B        $	 Run=�66   $��18��6B   $��18��6N   $��0,0,5,0q�6Y   �� 5C      פתיחת ספק63   5D      5E      �� 5F      $ פתיחת לקוח��6   #��[
 6[   ��63   �� *OpenCustomerCommand+y�5G      �� *IsCustomerSender+# [ .��5H      �� $ 
Horizontal=�6%   .+�5I        $ HumanHandsdown=�66   $��18��6M   $��18��6Y   $��0,0,5,0q�6d   �� 5J      פתיחת לקוח64   5K      5L      5M      �� 5O      -   5        	 ��Name$
	 Form��5O      $ Vertical=�6+     5P      
 �� 
TargetType �� Property �� Value �� BasedOn��%�  '��       '��      7��'�� �     7��'	� E    7	��� 5Q      "
 ��6    .�5R      ��  �@��Orientation$ 
Horizontal=�6<   �� 5S       � ��$ 7q�67   5T      �� 5U      "
 ��6    8   .�5V      ��  � ��$ Stretch=�6B   �� 5W       � ��$ Center=�6I   5X      �� 5Y      "
 ��6    8   .�5Z      ��  � ��$ 0q�68   5[      �� 5\      "
 	�6    8   .�5]      ��  � ��$ 0q�68   5^      5_      .+�5`      �  � RowDefinitions 5a      �� 5b      $
��auto �6,   6<   �� 5c      $
��auto �6,   6<   �� 5d      $
��auto �6,   6<    � ColumnDefinitions 5f      �� 5g      $��7* �6/   6<   �� 5h      $��8* �60   6=   �� 5i      $��7* �6/   6<   .+�5p      �� $��סוג תשלום��6$   .��5q      	  AutoGrid.AutoGrid	   	  Rows$
 auto��6*    	  Columns$ 2*,4*��66   .+�5r   "   �� $��סוג תשלום��6(    �� Target 6<   ��  �� ElementName$ 
TextAmount���� 5s   "   $ 
Horizontal=�6.   .+�5t   &   	� $��23��63    d� 	IsChecked 6?   �� $ PaymentType�# [ 
   RealEstate.DebtType 
  Revenue �� ConverterParameter# Z .��5u   *   �� 
הכנסה6;   5v   '   	� 5w   &   $
��10,0q�6�    62   �� $ PaymentType�# [  
  Expense# Z .��5x   *   �� 
הוצאה6;   5y   '   5z   #   �� 5|   "   $��מאת��6(    66   �� $ 	ComboFrom���� 5}   "   -   5         ��Name$ 	ComboFrom��5~       �� DisplayMemberPath$
 Name��6�   $��Center=�6�   {�5}   +   �� $ PaymentType�# [ # Z ��5~   0   �� *FromSenderTypes+ �� SelectedItem 6X   �� *FromSenderType+ �� Mode$ TwoWay=�6�   �� 5�   "   $��עבור��6(    67   �� $ ComboTo���� 5�   "   -   5        $ ComboTo��5�   #   $
 Name��6�   $��Center=�6�   {�5�   +   �� $ PaymentType�# [ # Z ��5�   4   �� *ToSenderTypes+ 6Z   �� *ToSenderType+$ TwoWay=�6�   5�      5�      �� 5�      $��1��6#   $��יעד תשלום��63   .��5�      � $��7q�6#    5�   "   �� 5�   &   $��* �67   6C   �� 5�   &   $
��auto �67   6F   .+�5�   "   �� $��0,3,0,2q�6.   $��1��6?   $��Right=�6O   $ Vertical=�6k   y�6�   �� *DisplaySenderFilter+# [ .+�5�   &   d�   ModernToggleButton#��[ 6�    64   �� $ SenderFilter�# [    RealEstate.SenderFilter   BySender# Z $ TwoWay=�.��5�   *     $  פרויקט לפי ספק��6B   $	 Run=�6[   $��15��6g   $��15��6s   $��0,0,0,0q�6~   $��Center=�6�   5�   '   d� 5�   &   #��[ 6�    64   �� $ SenderFilter�# [    	ByProject# Z $ TwoWay=�.��5�   *     $  ספק לפי פרויקט��6B   $ 
HomeModern=�6[   $��15��6n   $��15��6z   $��0,0,0,0q�6�   $��Center=�6�   5�   '   5�   #   �� 5�   "   $��0q�6-   $��0��68   $ Vertical=�6E   .+�5�   &   	   	  ChildMargin$ 0X�62   $��0,2,0,0q�6B   $ auto,auto,auto��6S   $ 2*,4*��6i   y�6z   �� $ SenderFilter�# [ # Z .+�5�   *   �� $	��5,7q�60   $��פרויקט��6=    6N   �� $ ComboProject1���� 5�   *   -   5        $ ComboProject1��5�   @   $	��5,7q�63   $
 Name��6�   $��Center=�6�   ��6W   �� *
Projects+ 6x   �� *	Project+$ TwoWay=�6�   �� 5�   *   $	��5,7q�60   $��דירה��6=    6L   �� $ 
ComboFlat1���� 5�   *   -   5        $ 
ComboFlat1��5�   A   $	��5,7q�63   $��Center=�6�   ��6U   �� *Flats+ 6s   �� *Flat+$ TwoWay=���5�   .   �� 5�   2   .R�5�   6   �� $ 
Horizontal=�6A   .+�5�   :   �� ��6D   �� *Project.Name+6d   �� 5�   :   $��, דירה ��6D   6U   �� 5�   :   ��6D   �� *
FlatNumber+6b   5�   7   5�   3   5�   +   �� 5�   *     5�   *   $	��5,7q�6B   $��23��6O   $ 	ArrowDown=�6�   $��15��6�   $��Center=�6�   y�6[   �� *DisplaySenderFilter+# [ 6�   5�   '   	  5�   &   $��0q�62   $
 auto��6=   $ 2*,4*��6I   y�6Y   �� *IsCustomerSender+# [ $ Hidden��.+�5�   *   �� $��לקוח��60    6?   �� $ ComboCustomer���� 5�   *   -   5        $ ComboCustomer��5�   3   $��Center=�5�   +   ��5�   J   �� *	Customers+ 6l   �� *
Customer+$ TwoWay=���5�   .   �� 5�   2   .R�5�   6   �� ��6@   �� # [ 5�   3   5�   +   5�   '   	  5�   &   $��0q�62   $
 auto��6=   $ 2*,4*��6I   y�6Y   �� *IsSupplierSender+# [ $ Hidden��.+�5�   *   �� $��ספק��60    6>   �� $ ComboSupplier���� 5�   *   -   5        $ ComboSupplier��5�   3   $��Center=�5�   +   ��5�   J   �� *	Suppliers+ 6l   �� *
Supplier+$ TwoWay=���5�   .   �� 5�   2   .R�5�   6   �� ��6@   �� # [ 5�   3   5�   +   5�   '   	  5�   &   $ 0X�62   $��0,2,0,0q�6C   $ auto,auto,auto��6T   $ 2*,4*��6j   y�6z   �� $ SenderFilter�# [ # Z .+�5�   *   ��   5�   *   $	��5,7q�6B   $��Center=�5�   F   $ 	ArrowDown=�6c   $��23��6u   $��15��6�   y�5�   O   �� *DisplaySenderFilter+# [ 5�   �   �� 5�   *   $	��5,7q�60   $��פרויקט��6=    6N   �� $ ComboProject���� 5�   *   -   5        $ ComboProject��5�   @   $	��5,7q�63   $
 Name��6�   $��Center=�6�   ��6V   �� *
Projects+ 6w   �� *	Project+$ TwoWay=�6�   �� 5�   *   $	��5,7q�60   $��דירה��6=    6L   �� $ 	ComboFlat���� 5�   *   -	   5        $ 	ComboFlat��5�   @   $	��5,7q�63   $��Center=�6�   ��6S   �� *Flats+ 6q   �� *Flat+$ TwoWay=���5�   .   �� 5�   2   .R�5�   6   �� $ 
Horizontal=�6A   .+�5�   :   �� ��6D   �� *Project.Name+6d   �� 5�   :   $��, דירה ��6D   6U   �� 5�   :   ��6D   �� *
FlatNumber+6b   5�   7   5�   3   5�   +   5�   '   	  5�   &   $��0q�62   $
 auto��6=   $ 2*,4*��6I   y�6Y   �� *IsBankSender+# [ .+�5�   *   �� $��בנק��60    6>   �� $ 
ComboFlat1���� 5�   *   $
 Name��6�   $��Center=�6�   ��64   �� *Banks+ 6R   �� *Payment.Bank+$ TwoWay=�5�   +   5�   '   	  5�   &   $��0q�62   $
 auto��6=   $ 2*,4*��6I   y�6Y   �� *IsNoneSender+# [ .+�5�   *   �� $��מקור/יעד��60    6C   �� $ 
ComboFlat1���� 5�   *   ��62   �� *Payment.SenderDescription+$ TwoWay=�.��6k   5�   +   5�   '   5�   #   5�      5�      �� 5�      $��1��6#   $��2��67   $��0��6G   $��פרטי תשלום��6T   .��5�      	  $
 auto��6*   $ 2*,4*��66   .+�5�   "   �� $��סכום כללי��6(   $��14(�6f   $
��Bold"�6t    6<   �� $ 
TextAmount��6t   �� 5�   "   -
   5         ��Name$ 
TextAmount��5�   Q     AmountTextBox#��[ 6*   ��6e   �� *Amount+$ TwoWay=�  �� ValidatesOnDataErrors  . $ C��! �� UpdateSourceTrigger$! PropertyChanged=��� 5�   "   $��תאריך תשלום��6(    6>   �� $ DateDueDate��(  "System.Windows.Controls.DatePicker  5�   "   -   5        "  Name$" DateDueDate��5�   9   $��23��6-   #   SelectedDate# 6N   �� *Payment.PaymentDate+�� 5�   "    6(   �� $ TextChequeNumber��.��6X   חייב במע"ם?5�   #   �� 5�   "   $��23��6+   $	��5,7q�67   $
��Left=�6D   $��LeftToRight=�6_    6{   �� *Payment.WithVat+�� 5�   "   $��אופן תשלום��6(    6=   �� $ ComboPaymentMethod���� 5�   "   -   5        $ ComboPaymentMethod��5�   +   $
 Name��6�   $��Center=�6�   ��6G   �� *PaymentMethods+ 6n   �� *PaymentMethod+$ TwoWay=�6�   5�      5�      �� 5�      $��3��6#   $��1��67   $��פרטי צ'ק��6D   y�6V   �� *DisplayChequeDetails+# [ .��5�      	  $ 	auto,auto��6*   $ 2*,4*,45,2*,4*,75,2*,4*��6;   .+�5�   "   �� $��מס' צ'ק��6)    6;   �� $ TextChequeNumber���� 5�   "   -   5        $ TextChequeNumber��5�   *   ��6D   �� *Payment.ChequeNumber+$ TwoWay=�  . �� 5�   "   �� 5�   "   $��ת.יעד צ'ק��6(    6<   �� $ ComboPaymentMethod��  5   "   $��23��6-   # 69   �� *Payment.ChequeDueDate+5     5     �� 5     $��3��6#   $��1��67   $��פרטי אשראי��6D   y�6X   �� *DisplayCreditDetails+# [ .��5     	  $ 	auto,auto��6*   $ 2*,4*,45,2*,4*,75,2*,4*��6;   .+�5  "   �� $��מס' אשראי��6)    6=   �� $ TextChequeNumber���� 5	  "   ��6+   �� *Payment.CreditNumber+$ TwoWay=�  . �� 5
  "   �� 5  "   $��תוקף��6(    67   �� $ ComboPaymentMethod��  5  "   $��23��6-   # 69   �� *Payment.CreditValidity+�� 5  "   �� 5  "   $��סך תשלומים��6)    6>   �� $ TextChequeNumber���� 5  "   ��6+   �� *Payment.CreditPaymentsNumber+$ TwoWay=�  . 5     5     5     �� 5     $��10q�6    $��פריטי תשלום��6,   .��5     �� $ Vertical=�6%   .+�5     �� $��25��6)   $��7,7,7,0q�65   $ 
Horizontal=�6F   $
��Left=�6_   .+�5  "   �� $��0q�6)   $��5q�6=   $ 
הוספה��6I   ��6Y   �� *AddPaymentItemCommand+.��5  &     $
 Plus=�6>   $��18��6K   $��18��6W   $��0,0,0,0q�6b   5  #   �� 5  "   $��0q�6)   $��5q�6=   $ 
מחיקה��6I   ��6Y   �� *DeletePaymentItemsCommand+.��5  &     $ Delete=�6>   $��18��6M   $��18��6Y   $��0,0,0,0q�6d   5  #   5     &   System.Windows.Controls.DataGrid  5      -   5        $	��DG1��5   j   $   CanUserAddRows$ .  $��7,0,7,0q�6>   %   CanUserDeleteRows% .  &   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$& * 5!     $��Stretch=�6%   '   AutoGenerateColumns' .  ��5   u   �� *PaymentItems+(   "RealEstate.MultiSelectorBehaviours (   SynchronizedSelectedItems( 5!  E   �� *SelectedPaymentItems+)   Columns) 5"  "   4  .System.Windows.Controls.DataGridTemplateColumn  5#  &   ,  &System.Windows.Controls.DataGridColumn*   Header$* החזר��6=   +   CellTemplate+ 5$  *   �� 5%  .   .R�5&  2   ��   DataGridCheckBoxStyle#��[ 6;    6j   �� *
IsRefund+$ TwoWay=�5'  /   ,   CellEditingTemplate, 5)  *   �� 5*  .   .R�5+  2   ��    DataGridEditingCheckBoxStyle#��[ 6;    6q   �� *
IsRefund+$ TwoWay=�$! PropertyChanged=�5,  /   5.  '   0  *System.Windows.Controls.DataGridTextColumn  50  &   $* סכום��69     TextBlockLTR1  +System.Windows.Controls.DataGridBoundColumn-   ElementStyle#- [ 51  -     
TextBoxLTR.   EditingElementStyle#. [ 52  -   /   Binding/ 50  G   �� *Amount+$ C��53  '     54  &   $* סוג תשלום��6=   + 55  *   �� 56  .   .R�57  2   �� ��6<   �� *PaymentType.Name+6`   58  /   , 5:  *   �� 5;  .   .R�5<  2   �� $
 Name��6m   $��Center=�6�    6;   �� *PaymentType+$ TwoWay=���5=  6   �� 5>  :   "
 ��6@   0 �� Triggers0 5?  >   �� 5@  B   1 �� Binding1 6N   �� *
IsRefund+2 �� Value2 True6k   .a�5A  F   ��  � �� 6d   �� * DataContext.RefundPaymentTypes+3 �� RelativeSource3 �� *FindAncestor+4 �� AncestorType$4 DataGridL�5B  C   �� 5C  B   1 6N   �� *
IsRefund+	2 False6k   .a�5D  F   ��  � �� 6d   �� *DataContext.PaymentTypes+3 �� *FindAncestor+$4 DataGridL�5E  C   5G  ;   5I  3   5J  /   5L  '     5M  &   $* קישור לחוב��6=   + 5N  *   �� 5O  .   .R�5P  2   �� $ 
Horizontal=�6=   .+�5Q  6   �� $��0q�5R  4   
������ $ הוסף חוב��6Y   $��0q�6l   y�5Q  @   �� *	HasDebt+# [ ��5R  �   �� *DataContext.LinkDebtCommand+3 �� *FindAncestor+"4  ��5S  x   �� .��5T  :     $ LinkVariant=�6R   $��15��6f   $��15��6r   $��0,0,0,0q�6}   5U  7   �� 5V  6   $��5,0,0,0q�6@   $ ��תשלום עבור חוב��5W  -   y�5V  Q   �� *	HasDebt+# [ 5W  E   �� 5X  6   $��6,0,0,0q�6=   $��0q�6N   
������ $ צפה בחוב��6t   $��0q�6�   y�5Y  1   �� *	HasDebt+# [ ��6�   �� *DataContext.OpenDebtCommand+3 �� *FindAncestor+"4  ��5Z  x   �� .��5[  :     $ CodeNotEqual=�6R   $��16��6g   $��16��6s   $��0,0,0,0q�6~   5\  7   �� 5]  6   $��6,0,0,0q�6=   $��0q�6N   
������ $ הסר חוב��6t   $��0q�6�   y�5^  1   �� *	HasDebt+# [ ��6�   �� *DataContext.UnLinkDebtCommand+3 �� *FindAncestor+"4  ��5_  x   �� .��5`  :     $ DeleteSweep=�6R   $��16��6f   $��16��6r   $��0,0,0,0q�6}   5a  7   5b  3   5c  /   5e  '     5f  &   5   Width$	5 200 69   $* הערה��6E   / 6S   �� *PaymentItem.Comment+5h     5i     5j     5k     5v     5w     5x     5y     