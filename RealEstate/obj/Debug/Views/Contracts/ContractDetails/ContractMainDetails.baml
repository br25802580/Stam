   M S B A M L   `   `   `  ����E  ARealEstate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RealEstate
RealEstate  5      $    RealEstate.ContractMainDetails   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      (localclr-namespace:RealEstate    5      N JSpicyTaco.AutoGrid, Version=1.1.19.0, Culture=neutral, PublicKeyToken=null%sthttp://schemas.spicytaco.io/  5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      U QMaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  5
      $��RightToLeft=�6A     � 	Resources  5      "   RealEstate.CustomerConverter   CustomerConverter  BoolToVisibilityConverter4   .RealEstate.OppositveBoolToVisibilityConverters & "OppositveBoolToVisibilityConverter"   RealEstate.PaymentsConverter  PaymentsConverter   RealEstate.DebtsConverter  DebtsConverter��%�   &	        &	      &	 &     &	 9     &	 L       5   
   6"   �� 5   
   6'     5   
   6a     5   
   6"     5   
   6   5      .��5      � .+�5   
   �� .��5      �� $��Stretch=�6   $	��200��67   .+�5      �� $��	0,10,0,20q�6   $��Center=�60    �� Orientation$ 
Horizontal=�6K   $��Stretch=�6d   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=null]]   Kind$ FormatFloatLeft=�6.   $��35��6F   $��35��6R   $��0,0,20,0q�6]   $��Center=�6o   �� 5      $��Center=�6    $��0,0,15,0q�6;   ��5      �� 5       �� 
TargetType" ��6$     Heading1 �� BasedOn# [ 6;   .�5   "   ��  �� Property � �� �� Value$" פרטי חוזה מכירה��69    �� Triggers 5   "   �� 5   &    �� Binding 62   �� *IsLeaseProject+	 �� Value	 True6U   .a�5   *   ��  � ��$$ פרטי חוזה שכירות��6A   5    '   5"      5$      �� 5%      $��0,0,0,5q�6    $��Bottom=�61     Heading2#��[ 6L   y�6n   �� *IsEditEditor+
 �� 	Converter#
 [  �� FallbackValue$ 
Visibility����5&      `� 5'       �� StringFormat$ {0} {1}��6+   .1�5(   "   ��  �� Path$ Contract.Customer.Name�6*   �� 5)   "   $ Contract.Customer.Family�6*   5*      5,      �� 5-      $��2,0,0,5q�5.       $��Bottom=�61   #��[ 6L   $��CharacterEllipsis=�6�   y�5-       �� *IsEditEditor+#
 [ $ 
Visibility����5.   n   �� *Project.Name+$ , {0}��6�   50      �� 51       � ToolTip$ 
שמירה��6   $��0,0,0,20q�6J     CommandButton#��[ 6\   ��6*   �� *SaveCommand+y�52      �� *IsEditEditor+#
 [ $ 
Visibility��.��53      �� $ 
Horizontal=�6!   .+�54        $ ContentSave=�62   $��18��6F   $��18��6R   $��0,0,5,0q�6]   �� 55      
שמירה6+   56      57      �� 59      -   5         ��Name$
 Form��59      $ Vertical=�6+     5:      ��%�  '��       '��      7��'�� �     7��'	� C    7	��� 5;      " ��6    .�5<      ��  �@��Orientation$ 
Horizontal=�6<   �� 5=       � ��$ 7q�67   5>      �� 5?      " ��6    8   .�5@      ��  � ��$	 100��66   �� 5A       � ��$ Center=�6B   5B      �� 5C      " ��6    8   .�5D      ��  � ��$	 0,3q�68   5E      �� 5F      " 	�6    8   .�5G      ��  � ��$	 0,3q�68   5H      5I      .+�5J      �  � RowDefinitions 5K      �� 5L      $
��auto �6,   6<   �� 5M      $
��auto �6,   6<   �� 5N      $
��auto �6,   6<    � ColumnDefinitions 5P      �� 5Q      $��* �6/   6;   �� 5R      $��* �6/   6;   �� 5S      $��* �6/   6;   .+�5U      �� $��
הערות��6$   $��2��63   $��1��6C   $	��Top=�6T   .��5V      �� -   5        $��tbMultiLine��5V   '    �� TextWrapping$
 Wrap=�6:   $��92��6N   $��7q�6Z    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�5W   2   ��6U   �� *Customer.Comment+.��6w   5X      5Y      �� 5Z      $��פרטי לקוח��6$   .��5[        AutoGrid.AutoGrid     Rows$
 auto��6*      Columns$ 2*,4*��66   .+�5]   "   �� $��לקוח��6(    �� Target 67   ��  �� ElementName$ ComboCustomer���� 5^   "   -   5         ��Name$ ComboCustomer��5^   +   $��Center=�5_   +   ��5^   B   �� *	Customers+ �� SelectedItem 6d   �� *Contract.Customer+ �� Mode$ TwoWay=���5`   &   �� 5a   *   .R�5b   .   �� ��68   �� #
 [  5c   +   5e   #   �� 5g   "   
�������$��
כתובת��6:   �� 5h   "    �� 
IsReadOnly . $��0,0,0,5q�6<   $��Bottom=�6M   .��6j   ��5i   &   `� 5j   *   $ {0}, {1}, {2} {3}��67   .1�5k   .   �� $$ Contract.Customer.Country.Name�66   �� 5l   .   $! Contract.Customer.City.Name�66   �� 5m   .   $ Contract.Customer.Street�66   �� 5n   .   $# Contract.Customer.HouseNumber�66   5o   +   .��5p   4   5q   #   �� 5s   "   
�������$��תעודת זהות��6;    6P   �� $ TextTZ���� 5t   "   -   5         ��Name$ TextTZ��5t   <    . ��6L   �� *Contract.Customer.TZ+$ TwoWay=� �� ValidatesOnDataErrors . 5u      5v      �� 5w      $��פרטי פרויקט��6$   $��1��69   .��5x        $
 auto��6*   $ 2*,4*��66   .+�5y   "   �� $��פרויקט��6(    69   �� $ ComboProject���� 5z   "   -   5        $ ComboProject��5z   +    �� DisplayMemberPath$
 Name��6�   $��Center=�6�   ��6A   �� *
Projects+ 6b   �� *	Project+$ TwoWay=�6�   �� 5|   "   
�������$��
כתובת��6:   �� 5}   "    . $��0,0,0,5q�6<   $��Bottom=�6M   .��6j   ��5~   &   `� 5   *   $ {0}, {1}, {2} {3}��67   .1�5�   .   �� $ Project.Country.Name�66   �� 5�   .   $ Project.City.Name�66   �� 5�   .   $ Project.Street�66   �� 5�   .   $ Project.HouseNumber�66   5�   +   .��5�   4   5�   #   �� 5�   "   $��דירה��6(   �� 5�   "   $��Center=�6u   ��6,   �� *Flats+ 6J   �� *Flat+$ TwoWay=���5�   &   �� 5�   *   .R�5�   .   �� $ 
Horizontal=�69   .+�5�   2   �� $��	דירה ��6<   6K   �� 5�   2   ��6<   �� *
FlatNumber+6Z   5�   /   5�   +   5�   #   5�      5�      �� 5�      $��3��6#   $��1��67   $��פרטי חוזה��6D   .��5�      � $��7q�6#    5�   "   �� 5�   &   $��2* �67   �� 5�   &   $��4* �67   �� 5�   &   $��45 �67   �� 5�   &   $��2* �67   �� 5�   &   $��4* �67   �� 5�   &   $��45 �67   �� 5�   &   $��2* �67   �� 5�   &   $��4* �67    5�   "   �� 5�   &   $
��auto �64   �� 5�   &   $
��auto �64   .+�5�   "   �� $	��5,7q�6(   $��תחילת חוזה��65    6J   �� $ ComboPaymentMethod��(  "System.Windows.Controls.DatePicker  5�   "   $	��5,7q�6-   $��1��6:   $��23��6J       SelectedDate  6V   �� *Contract.StartDate+�� 5�   "   $	��5,7q�6(   $��3��65   $��סיום חוזה��6E    6Y   �� $ ComboPaymentMethod��  5�   "   $	��5,7q�6-   $��4��6:   $��23��6J     6V   �� *	EndDate+�� 5�   "   $	��5,7q�6(   $��6��65   
�������$��תאריך יצירה��6W    6m   �� $ ComboPaymentMethod��  5�   "   $	��5,7q�6-   $��7��6:   {�.  $��23��6\     6h   �� *Contract.CreatedDate+�� 5�   "   $	��5,7q�6(   $��1��65    6B   �� $ TextChequeNumber��.��6r   	פעיל?5�   #   �� 5�   "   $	��5,7q�6+   $��1��68   $��1��6E   $
��Left=�6U   $��LeftToRight=�6p   ! d� 	IsChecked! 6�   �� *Contract.IsActive+�� 5�   "   $	��5,7q�6(   $��1��65   $��3��6B   $��דמי שכירות��6R    6g   �� $ ComboPaymentMethod��y�5�   (   �� *IsLeaseProject+#
 [ �� 5�   "   $	��5,7q�6*   $��1��67   $��4��6D     AmountTextBox#��[ 6T   $��23��6{   ��6�   �� *LeaseContract.Amount+$ C��y�5�   *   �� *IsLeaseProject+#
 [ �� 5�   "   $��5,7,-7,7q�6(   $ יום תשלום (1-30)��6:   $��0q�6U   $��1��6a   $��6��6n   $��יום תשלום (1-30)��6~    6�   �� $ ComboPaymentMethod��y�5�   (   �� *IsLeaseProject+#
 [ �� 5�   "   $	��5,7q�6*   $��1��67   $��7��6D   $��23��6T   ��6`   �� *!LeaseContract.MonthlyPaymentDay+y�5�   *   �� *IsLeaseProject+#
 [ �� 5�   "   $��5,7,0,7q�6(   $��1��69   $��3��6F   $��קבלת מפתח��6V    6j   �� $ ComboPaymentMethod��y�5�   (   �� *IsLeaseProject+#
 [   5�   "   $	��5,7q�6-   $��1��6:   $��4��6G   $��23��6W     6c   �� *SaleContract.GettingKeyDate+$ TwoWay=�y�5�   -   �� *IsLeaseProject+#
 [ 5�      5�      �� 5�      $��תשלומים��5�   #   $��2��64   $��3��6A   y�5�   #   �� *IsEditEditor+#
 [ $ 
Visibility��.��5�        $
 auto��6*   $ 2*,4*,45,2*,4*,45,2*,4*��66   "   Orientation$" Vertical=�6X   .+�5�   "   �� $��תשלומים��6)   �� 5�   &   $��5,0,0,0q�60    	 
AmountText#��[	 6A   ��6e   �� *
Contract+#
 [ $ C��$ OneWay=��� 5�   "   �� 5�   "   $��
חובות��6)   �� 5�   &   $	��5,0q�60   #��[	 6=   ��6a   �� *
Contract+#
 [ $ C��$ OneWay=�5�      5�      5�      5�      5�      5�      5�      5�      