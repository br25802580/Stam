   M S B A M L   `   `   `  ����E  ARealEstate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RealEstate
RealEstate  5      $    RealEstate.CustomerMainDetails   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      (localclr-namespace:RealEstate    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      N JSpicyTaco.AutoGrid, Version=1.1.19.0, Culture=neutral, PublicKeyToken=null%sthttp://schemas.spicytaco.io/  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      U QMaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	      Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  5      $��RightToLeft=�6A     � 	Resources  5         BoolToVisibilityConverter"   RealEstate.PaymentsConverter  PaymentsConverter   RealEstate.DebtsConverter  DebtsConverter��%W   &	        &	      &	 &     �� 5   
   6'     5   
   6"     5   
   6   5      .��5      � .+�5   
   �� .��5      �� $	��200��6   .+�5      �� $��	0,10,0,20q�6   $��Center=�60    �� Orientation$ 
Horizontal=�6K   $��Stretch=�6d   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=null]]   Kind$ HumanHandsdown=�6.   $��35��6E   $��35��6Q   $��0,0,20,0q�6\   $��Center=�6n   �� 5      $��Center=�6    $��פרטי לקוח��6;     Heading1#��[ 6L   $��0,0,15,0q�6n   �� 5      $��0,0,0,5q�6    $��Bottom=�61     Heading2#��[ 6L   ��5      `� 5       �� StringFormat$ {0} {1}��6+   .1�5   "   ��  �� Path$ Customer.Name�6*   �� 5   "   $ Customer.Family�6*   5      5      5!      �� 5"       � ToolTip$ 
שמירה��6   $��0,0,0,20q�6J     CommandButton#��[ 6\   ��6*   �� *SaveCommand+y�5#      �� *IsEditEditor+ �� 	Converter# [   �� FallbackValue$ 
Visibility��.��5$      �� $ 
Horizontal=�6!   .+�5%        $ ContentSave=�62   $��18��6F   $��18��6R   $��0,0,5,0q�6]   �� 5&      
שמירה6+   5'      5(      �� 5*      -   5         ��Name$
 Form��5*      $ Vertical=�6+   "   RealEstate.EnterKeyTraversal	   	IsEnabled	 . .  (System.Windows.Interactivity.Interaction
   	Behaviors
 5+      2   ,RealEstate.SelectAllTextOnFocusMultiBehavior  5,        5.       �� 
TargetType �� Property �� Value �� BasedOn��%�  '��       '��      7��'�� �     7��'	� G    7	��� 5/      " ��6    .�50      ��  �@��Orientation$ 
Horizontal=�6<   �� 51       � ��$ 7q�67   52      �� 53      " ��6    8   .�54      ��  � ��$ Stretch=�6B   �� 55       � ��$ Center=�6I   56      �� 57      " ��6    8   .�58      ��  � ��$	 0,3q�68   59      �� 5:      " 	�6    8   .�5;      ��  � ��$	 0,3q�68   5<      5=      .+�5>      �  � RowDefinitions 5?      �� 5@      $
��auto �6,   6<   �� 5A      $
��auto �6,   6<   �� 5B      $
��auto �6,   6<    � ColumnDefinitions 5D      �� 5E      $��* �6/   6;   �� 5F      $��* �6/   6;   �� 5G      $��* �6/   6;   .+�5I      �� $��
הערות��6$   $��2��63   $��1��6C   .��5J      �� -   5        $��tbMultiLine��5J   ^   ��.  $	��Top=�68   $	��128��6Q    �� TextWrapping$
 Wrap=�6r   $��7,15,7,7q�6�    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�5K   2   ��6U   �� *Customer.Comment+.��6w   .��5U   4   5V      5W      �� 5X      $��פרטים כלליים��6$   .��5Y        AutoGrid.AutoGrid     Rows$ auto,auto,auto,auto��6*      Columns$ 2*,4*��6E   .+�5[   "   �� $
��Bold"�6(   $��מס' זהות��6:    �� Target 6M   ��  �� ElementName$ TextTZ���� 5\   "   -   5         ��Name$ TextTZ��5\   *   ��6:   �� *Customer.TZ+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . �� 5^   "   $��מין��6(   �� 5_   "    �� DisplayMemberPath$
 Name��6�   $��Center=�6�   ��6+   �� *	Genders+ �� SelectedItem 6K   �� *Customer.Gender+$ TwoWay=�6�   �� 5a   "   $
��Bold"�6(   $
��שם��6:    6G   �� $ TextName���� 5b   "   -   5        $ TextName��5b   *   ��6<   �� *Customer.Name+$ TwoWay=� . �� 5d   "   $
��Bold"�6(   $��
משפחה��6:    6J   �� $ 
TextFamily���� 5e   "   -   5        $ 
TextFamily��5e   *   ��6>   �� *Customer.Family+$ TwoWay=� . 5f      5g      �� 5h      $��
כתובת��6$   $��1��63   .��5i        $ auto,auto,auto,auto��6*   $ 2*,4*��6E   .+�5k   "   �� $��ארץ��6(    66   �� $ 	ComboCity���� 5l   "   $
 Name��6�   $��Center=�6�   ��6+   �� *	Countries+ 6M   �� *Customer.Country+$ TwoWay=�6�   �� 5n   "   $��עיר��6(    66   �� $ 	ComboCity���� 5o   "   -   5         ��Name$ 	ComboCity��5o   +   $
 Name��6�   $��Center=�6�   ��6>   �� *Cities+ 6]   �� *Customer.City+$ TwoWay=�6�   �� 5q   "   $��רחוב��6(    67   �� $ 
TextStreet���� 5r   "   -   5        $ 
TextStreet��5r   *   ��6>   �� *Customer.Street+$ TwoWay=� . �� 5t   "   $��מס' בית��6(    6:   �� $ TextHouseNumber���� 5u   "   -   5        $ TextHouseNumber��5u   *   ��6C   �� *Customer.HouseNumber+$ TwoWay=� . 5v      5w      �� 5x      $��יצירת קשר��6#   $��1��66   $��3��6C   .��5y        $
 auto��6*   $ 2*,4*,45,2*,4*,45,2*,4*��66      Orientation$ Vertical=�6X   .+�5{   "   �� $��טלפון 1��6(    6:   �� $ 
TextPhone1���� 5|   "   -	   5        $ 
TextPhone1��5|   *   ��6>   �� *Customer.Phone1+$ TwoWay=� . �� 5}   "   �� 5   "   $��טלפון 2��6)    6;   �� $ 
TextPhone2���� 5�   "   -
   5        $ 
TextPhone2��5�   *   ��6>   �� *Customer.Phone2+$ TwoWay=� . �� 5�   "   �� 5�   "   $��מייל��6)    68   �� $ 
TextPhone2���� 5�   "   ��6*   �� *Customer.Email+$ TwoWay=� . 5�      5�      �� 5�      $��3��6#   $��2��67   $��תשלומים��6D   y�6U   �� *IsEditEditor+# [  $ 
Visibility��.��5�        $
 auto��6*   $ 2*,4*,45,2*,4*,45,2*,4*��66   $ Vertical=�6X   .+�5�   "   �� $��תשלומים��6)   $
��7,15q�6;   �� 5�   "     
AmountText#��[ 6,   ��6P   �� *
Customer+# [ $ C��$ OneWay=��� 5�   "   �� 5�   "   $��
חובות��6)   �� 5�   "   #��[ 6,   ��6P   �� *
Customer+# [ $ C��$ OneWay=�5�      5�      5�      5�      5�      5�      5�      5�      