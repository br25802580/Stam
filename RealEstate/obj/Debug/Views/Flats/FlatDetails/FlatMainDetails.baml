   M S B A M L   `   `   `  ����E  ARealEstate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RealEstate
RealEstate  5           RealEstate.FlatMainDetails   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      (localclr-namespace:RealEstate    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      U QMaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  5
      N JSpicyTaco.AutoGrid, Version=1.1.19.0, Culture=neutral, PublicKeyToken=null%sthttp://schemas.spicytaco.io/  5      $	��380��5	      $��RightToLeft=�5
   A     � 	Resources  5      4   .RealEstate.OppositveBoolToVisibilityConverters '  #OppositveBoolToVisibilityConverters  BoolToVisibilityConverter"   RealEstate.PaymentsConverter  PaymentsConverter"   RealEstate.RevenuesConverter  RevenuesConverter"   RealEstate.ExpensesConverter  ExpensesConverter   RealEstate.DebtsConverter  DebtsConverter+   %RealEstate.FlatPriceForMeterConverter  FlatPriceForMeterConverter    RealEstate.StatusConverter  StatusConverter��%�   &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �       5   
   64   �� 5   
   6'     5   
   6"     5   
   6"     5   
   6"     5   
   6     5   
   6+     5   
   6    5      .��5      � .+�5   
   �� .��5      �� $	��200��6   .+�5      �� $��	0,10,0,20q�6   $��Center=�60    �� Orientation$ 
Horizontal=�6K   $��Stretch=�6d   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �	  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=null]] 	  Kind$ HomeMapMarker=�6.   $��35��6D   $��35��6P   $��0,0,20,0q�6[   $��Center=�6m   �� 5      $��Center=�6    $��פרטי דירה��6;     Heading1#��[ 6L   $��0,0,15,0q�6n   �� 5      $��0,0,0,5q�6    $��Bottom=�61    	 Heading2#��[	 6j   ��6L   �� *Project.Name+6j   �� 5      $��0,0,0,5q�6    $��Bottom=�61   $��, דירה ��6L   #��[	 6[   �� 5      $��0,0,0,5q�6    $��Bottom=�61   #��[	 6m   ��6L   �� *Flat.FlatNumber+6m   5       �� 5"       � ToolTip$ 
שמירה��6   $��0,0,0,20q�6J    
 CommandButton#��[
 6\   ��6*   �� *SaveCommand+y�5#      �� *IsEditEditor+ �� 	Converter# [  �� FallbackValue$ 
Visibility��.��5$      �� $ 
Horizontal=�6!   .+�5%        $ ContentSave=�62   $��18��6F   $��18��6R   $��0,0,5,0q�6]   �� 5&      
שמירה6+   5'      5(      �� 5+      -   5         ��Name$
 Form��5+      $ Vertical=�6+   "
   RealEstate.EnterKeyTraversal 
  	IsEnabled . .  (System.Windows.Interactivity.Interaction   	Behaviors 5,      2   ,RealEstate.SelectAllTextOnFocusMultiBehavior  5-        5/      	 �� 
TargetType
 �� Property �� Value �� BasedOn��%m  '��       '��      7��'	� �     7	��� 50      "	 ��6    .�51      �� 
 �@��Orientation$ 
Horizontal=�6<   �� 52      
 � ��$ 7q�67   53      �� 54      "	 ��6    8   .�55      �� 
 � ��$ Stretch=�6B   �� 56      
 � ��$ Center=�6I   57      �� 58      "	 	�6    8   .�59      �� 
 � ��$	 0,3q�68   5:      5;      .+�5<      � $	��420��6   $��0,-15q�6+    � RowDefinitions 5=      �� 5>      $
��auto �6,   6<   �� 5?      $
��auto �6,   6<   �� 5@      $
��auto �6,   6<    � ColumnDefinitions 5B      �� 5C      $��* �6/   6;   �� 5D      $��* �6/   6;   �� 5E      $��* �6/   6;   .+�5G      �� $��0��6#   $��2��60   $��פרטי פרויקט��6D   .��5H        AutoGrid.AutoGrid     Rows$ auto,auto,auto��6*      Columns$ 2*,4*,45,2*,4*��6@   .+�5I   "   �� $��סוג דירה��6(    �� Target 6;   ��  �� ElementName$ ComboProjectType���� 5J   "   -   5         ��Name$ ComboProjectType��5J   +    �� DisplayMemberPath$
 Name��6�   $��Center=�6�   ��6E   �� *ProjectTypes+ �� SelectedItem 6j   �� *ProjectType+ �� Mode$ TwoWay=�6�   �� 5K   "   �� 5M   "   $��פרויקט��6(    69   �� $ ComboProject���� 5N   "   -   5        $ ComboProject��5N   +   $
 Name��6�   $��Center=�6�   ��6A   �� *
Projects+ 6b   �� *	Project+$ TwoWay=�6�   �� 5P   "   
�������$��ארץ��6:    6H   �� $ TextCity���� 5Q   "   ��.   �� 
IsReadOnly . ��6N   �� *Project.Country.Name+$ TwoWay=� �� ValidatesOnDataErrors . �� 5R   "   �� 5T   "   
�������$��עיר��6:    6H   �� $ TextCity���� 5U   "   -   5         ��Name$ TextCity��5U   N   ��.   . ��6`   �� *Project.City.Name+$ TwoWay=� . �� 5W   "   ��.  
�������$��רחוב��6L    6[   �� $ 
TextStreet���� 5X   "   -   5        $ 
TextStreet��5X   <    . ��6P   �� *Project.Street+$ TwoWay=� . �� 5Y   "   �� 5[   "   ��.  
�������$��מס' בית��6L    6^   �� $ TextHouseNumber���� 5\   "   -   5        $ TextHouseNumber��5\   <    . ��6U   �� *Project.HouseNumber+$ TwoWay=� . 5]      5^      �� 5_      $��
הערות��6#   $��2��63   $��0��6C   .��5a      �� -   5        $��tbMultiLine��5a   2   $��93��6&    �� TextWrapping$
 Wrap=�6E   $��7q�6Y    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�5b   2   ��6U   �� *Flat.Comment+.��6s   5c      5d      �� 5e      $��3��6#   $��1��67   $��פרטי דירה��6D   .��5f        $ auto,auto,auto,auto��6*   $ 2*,4*,45,2*,4*,45,2*,4*��6E   .+�5g   "   �� $
��Bold"�6(   $��מס' דירה��6:    6M   �� $ TextFlatNumber���� 5h   "   -   5        $ TextFlatNumber��5h   *   ��6B   �� *Flat.FlatNumber+$ TwoWay=� . �� 5i   "   �� 5k   "   $��קומה��6)    68   �� $ 	TextFloor���� 5l   "   -	   5        $ 	TextFloor��5l   *   ��6=   �� *
Flat.Floor+$ TwoWay=� . �� 5m   "   �� 5o   "   $��
סטטוס��6)    69   �� $ TextAirDirection���� 5p   "   $
 Name��6�   $��Center=�6�   ��6+   �� *Flat.Status.Id+# [ $ OneWay=���6�   �� *
Statuses+ 6�   �� *Flat.Status+$ TwoWay=�5q   #   �� 5s   "   $
��Bold"�6(   $��מחיר��6:    6I   �� $ 	TextPrice��5t   #   �� 5u   "   -
   5        $ 	TextPrice��5u   Q     AmountTextBox#��[ 6*   ��6e   �� *
Flat.Price+$ TwoWay=� �� StringFormat$ C�� . �� 5v   "   �� 5x   "   $��שטח (מ"ר)��6(    6A   �� $ TextFlatNumber���� 5y   "   ��6*   �� *Flat.SquareMeter+$ TwoWay=� . �� 5z   "   �� 5|   "   $
��Bold"�6(   
�������$��מחיר למ"ר��6L    6e   �� $ TextAirDirection���� 5}   "   ��.   . #��[ 6N   .��6u   ��5~   &   `� 5   *   $ C��67    `� 	Converter# [ 6H   .1�5�   .   ��  �� Path$ 
Flat.Price�66   �� 5�   .   $ Flat.SquareMeter�66   5�   +   .��5�   4   5�   #   �� 5�   "   $��מרפסות (מ"ר)��6(   $��5,7,0,5q�6D    6U   �� $ TextAirDirection���� 5�   "   ��6+   �� *Flat.BalconyArea+$ TwoWay=� . �� 5�   "   �� 5�   "   $��גינה (מ"ר)��6(    6B   �� $ TextAirDirection���� 5�   "   ��6+   �� *Flat.GardenArea+$ TwoWay=� . �� 5�   "   �� 5�   "   $��מס' חדרים��6(    6<   �� $ TextFlatNumber���� 5�   "   ��6*   �� *Flat.RoomsCount+$ TwoWay=� . �� 5�   "   $��כיווני אוויר��6(    6?   �� $ TextAirDirection���� 5�   "   -   5        $ TextAirDirection��5�   *   ��6D   �� *Flat.AirDirections+$ TwoWay=� . 5�      5�      �� 5�      $��3��6#   $��2��67   $��תשלומים��6D   y�6U   �� *IsEditEditor+# [ $ 
Visibility��.��5�        $
 auto��6*   $# 2*,3*,*,2*,3*,*,2*,3*,*,2*,3*��66       Orientation$  Vertical=�6^   .+�5�   "   �� $
��7,15q�6)   $��תשלומים��67   �� 5�   "     
AmountText#��[ 6,   ��6P   �� *Flat+# [ $ C��$ OneWay=��� 5�   "   �� 5�   "   $��הכנסות��6)   �� 5�   "   #��[ 6,   ��6P   �� *Flat+# [ $ C��$ OneWay=��� 5�   "   �� 5�   "   $��הוצאות��6*   �� 5�   "   #��[ 6,   ��6P   �� *Flat+# [ $ C��$ OneWay=��� 5�   "   �� 5�   "   $��
חובות��6)   �� 5�   "   #��[ 6,   ��6P   �� *Flat+# [ $ C��$ OneWay=�5�      5�      5�      5�      5�      5�      5�      5�      