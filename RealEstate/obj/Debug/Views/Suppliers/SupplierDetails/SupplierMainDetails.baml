   M S B A M L   `   `   `  ����E  ARealEstate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RealEstate
RealEstate  5      $    RealEstate.SupplierMainDetails   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      (localclr-namespace:RealEstate    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      N JSpicyTaco.AutoGrid, Version=1.1.19.0, Culture=neutral, PublicKeyToken=null%sthttp://schemas.spicytaco.io/  5      U QMaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	      Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  5      $��RightToLeft=�6A     � 	Resources  5         BoolToVisibilityConverter"   RealEstate.PaymentsConverter  PaymentsConverter   RealEstate.DebtsConverter  DebtsConverter��%W   &	        &	      &	 &     �� 5   
   6'     5   
   6"     5   
   6   5      .��5      � .+�5   
   �� .��5      �� $	��200��6   .+�5      �� $��	0,10,0,20q�6   $��Center=�60    �� Orientation$ 
Horizontal=�6K   $��Stretch=�6d   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=null]]   Kind$	 Run=�6.   $��35��6:   $��35��6F   $��0,0,20,0q�6Q   $��Center=�6c   �� 5      $��Center=�6    $��פרטי ספק��6;     Heading1#��[ 6K   $��0,0,15,0q�6m   �� 5      $��0,0,0,5q�6    $��Bottom=�61     Heading2#��[ 6L   ��5      `� 5       �� StringFormat$ {0} {1}��6+   .1�5   "   ��  �� Path$ Supplier.Name�6*   �� 5   "   $ Supplier.Family�6*   5      5      5       �� 5!       � ToolTip$ 
שמירה��6   $��0,0,0,20q�6J     CommandButton#��[ 6\   ��6*   �� *SaveCommand+y�5"      �� *IsEditEditor+ �� 	Converter# [   �� FallbackValue$ 
Visibility��.��5#      �� $ 
Horizontal=�6!   .+�5$        $ ContentSave=�62   $��18��6F   $��18��6R   $��0,0,5,0q�6]   �� 5%      
שמירה6+   5&      5'      �� 5)      -   5         ��Name$
 Form��5)      $ Vertical=�6+   "   RealEstate.EnterKeyTraversal	   	IsEnabled	 . .  (System.Windows.Interactivity.Interaction
   	Behaviors
 5*      2   ,RealEstate.SelectAllTextOnFocusMultiBehavior  5+        5-       �� 
TargetType �� Property �� Value �� BasedOn��%�  '��       '��      7��'�� �     7��'	� G    7	��� 5.      " ��6    .�5/      ��  �@��Orientation$ 
Horizontal=�6<   �� 50       � ��$ 7q�67   51      �� 52      " ��6    8   .�53      ��  � ��$ Stretch=�6B   �� 54       � ��$ Center=�6I   55      �� 56      " ��6    8   .�57      ��  � ��$	 0,3q�68   58      �� 59      " 	�6    8   .�5:      ��  � ��$	 0,3q�68   5;      5<      .+�5=      �  � RowDefinitions 5>      �� 5?      $
��auto �6,   6<   �� 5@      $
��auto �6,   6<   �� 5A      $
��auto �6,   6<    � ColumnDefinitions 5C      �� 5D      $��* �6/   6;   �� 5E      $��* �6/   6;   �� 5F      $��* �6/   6;   .+�5H      �� $��
הערות��6$   $��2��63   $	��Top=�6C   .��5I      �� -   5        $��tbMultiLine��5I   '    �� TextWrapping$
 Wrap=�6:   ��.  $	��165��6`   $��10,15,10,14q�6n    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�5J   2   ��6U   �� *Supplier.Comment+.��6w   5K      5L      �� 5M      $��פרטים כלליים��6$   .��5N        AutoGrid.AutoGrid     Rows$ auto,auto,auto,auto,auto��6*      Columns$ 2*,4*��6J   .+�5O   "   �� $��סוג שירות��6(    �� Target 6<   ��  �� ElementName$ ComboServiceType���� 5P   "   -   5         ��Name$ ComboServiceType��5P   +    �� DisplayMemberPath$
 Name��6�   $��Center=�6�   ��6E   �� *ServiceTypes+ �� SelectedItem 6j   �� *Supplier.ServiceType+ �� Mode$ TwoWay=�6�   �� 5R   "   $��מין��6(   �� 5S   "   $
 Name��6�   $��Center=�6�   ��6+   �� *	Genders+ 6K   �� *Supplier.Gender+$ TwoWay=�6�   �� 5U   "   $
��Bold"�6(   $
��שם��6:    6G   �� $ TextName���� 5V   "   -   5         ��Name$ TextName��5V   +   ��6=   �� *Supplier.Name+$ TwoWay=� �� ValidatesOnDataErrors . �� 5X   "   $
��Bold"�6(   $��
משפחה��6:    6J   �� $ 
TextFamily���� 5Y   "   -   5        $ 
TextFamily��5Y   *   ��6>   �� *Supplier.Family+$ TwoWay=� . �� 5[   "   $��ח.פ��6(    66   �� $ 
TextFamily���� 5\   "   ��6*   �� *Supplier.CNPJ+$ TwoWay=� . 5]      5^      �� 5_      $��
כתובת��6$   $��1��63   .��5`        $ auto,auto,auto,auto��6*   $ 2*,4*��6E   .+�5a   "   �� $��ארץ��6(    66   �� $ 	ComboCity���� 5b   "   $
 Name��6�   $��Center=�6�   ��6+   �� *	Countries+ 6M   �� *Supplier.Country+$ TwoWay=�6�   �� 5d   "   $��עיר��6(    66   �� $ 	ComboCity���� 5e   "   -   5        $ 	ComboCity��5e   +   $
 Name��6�   $��Center=�6�   ��6>   �� *Cities+ 6]   �� *Supplier.City+$ TwoWay=�6�   �� 5g   "   $��רחוב��6(    67   �� $ 
TextStreet���� 5h   "   -   5        $ 
TextStreet��5h   *   ��6>   �� *Supplier.Street+$ TwoWay=� . �� 5j   "   $��מס' בית��6(    6:   �� $ TextHouseNumber���� 5k   "   -   5        $ TextHouseNumber��5k   *   ��6C   �� *Supplier.HouseNumber+$ TwoWay=� . 5l      5m      �� 5n      $��יצירת קשר��6$   $��1��67   $��3��6D   .��5o        $
 auto��6*   $ 2*,4*,45,2*,4*,45,2*,4*��66      Orientation$ Vertical=�6X   .+�5p   "   �� $��טלפון 1��6(    6:   �� $ 
TextPhone1���� 5q   "   -	   5        $ 
TextPhone1��5q   *   ��6>   �� *Supplier.Phone1+$ TwoWay=� . �� 5r   "   �� 5t   "   $��טלפון 2��6)    6;   �� $ 
TextPhone2���� 5u   "   -
   5        $ 
TextPhone2��5u   *   ��6>   �� *Supplier.Phone2+$ TwoWay=� . �� 5v   "   �� 5x   "   $��מייל��6)    68   �� $ 
TextPhone2���� 5y   "   ��6*   �� *Supplier.Email+$ TwoWay=� . 5z      5{      �� 5|      $��3��6#   $��2��67   $��תשלומים��6D   y�6U   �� *IsEditEditor+# [  $ 
Visibility��.��5}        $
 auto��6*   $ 2*,4*,45,2*,4*,45,2*,4*��66   $ Vertical=�6X   .+�5~   "   �� $
��7,15q�6)   $��תשלומים��67   �� 5   "     
AmountText#��[ 6,   ��6P   �� *
Supplier+# [ $ C��$ OneWay=��� 5�   "   �� 5�   "   $��
חובות��6)   �� 5�   "   #��[ 6,   ��6P   �� *
Supplier+# [ $ C��$ OneWay=��� 5�   "   �� 5�   "   �� 5�   "   5�      5�      5�      5�      5�      5�      5�      5�      