   M S B A M L   `   `   `  ����E  ARealEstate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RealEstate
RealEstate  5      /clr-namespace:AdornedControlAdornedControl  5      #    RealEstate.ProjectMainDetails   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      (localclr-namespace:RealEstate    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +acclr-namespace:AdornedControl     5      N JSpicyTaco.AutoGrid, Version=1.1.19.0, Culture=neutral, PublicKeyToken=null%sthttp://schemas.spicytaco.io/  5      U QMaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
      Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  5      $��RightToLeft=�6A     � 	Resources  5         BoolToVisibilityConverter4   .RealEstate.OppositveBoolToVisibilityConverters ' #OppositveBoolToVisibilityConverters"   RealEstate.PaymentsConverter  PaymentsConverter"   RealEstate.RevenuesConverter  RevenuesConverter"   RealEstate.ExpensesConverter  ExpensesConverter   RealEstate.DebtsConverter  DebtsConverter��%�   &	        &	      &	 &     &	 9     &	 L     &	 _     �� 5   
   6'     5   
   64     5   
   6"     5   
   6"     5   
   6"     5   
   6   5      .��5      � .+�5   
   �� .��5      �� $	��200��6   $��Stretch=�6(   .+�5      �� $��	0,10,0,20q�6   $��Center=�60    �� Orientation$ 
Horizontal=�6K   $��Stretch=�6d   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=null]]   Kind$ 
HomeModern=�6.   $��35��6A   $��35��6M   $��0,0,20,0q�6X   $��Center=�6j   �� 5      $��Center=�6    $��פרטי פרויקט��6;     Heading1#��[ 6N   $��0,0,15,0q�6p   �� 5      $��0,0,0,5q�6    $��Bottom=�61     Heading2#��[ 6j   ��6L   �� *Project.Name+6j   5      �� 5       � ToolTip$ 
שמירה��6   $��0,0,0,20q�6J     CommandButton#��[ 6\   ��6*   �� *SaveCommand+y�5      �� *IsEditEditor+ �� 	Converter# [   �� FallbackValue$ 
Visibility��.��5       �� $ 
Horizontal=�6!   .+�5!        $ ContentSave=�62   $��18��6F   $��18��6R   $��0,0,5,0q�6]   �� 5"      
שמירה6+   5#      5$      �� 5&      -   5         ��Name$
 Form��5&      $ Vertical=�6+   "   RealEstate.EnterKeyTraversal   	IsEnabled . .	  (System.Windows.Interactivity.Interaction 	  	Behaviors 5'      2
   ,RealEstate.SelectAllTextOnFocusMultiBehavior
  5(        5+      	 �� 
TargetType
 �� Property �� Value �� BasedOn��%�  '��       '��      7��'�� �     7��'	� G    7	��� 50      "	 ��6    .�51      �� 
 �@��Orientation$ 
Horizontal=�6<   �� 52      
 � ��$ 7q�67   53      �� 54      "	 ��6    8   .�55      �� 
 � ��$ Center=�6B   �� 56      
 � ��$ Center=�6I   57      �� 58      "	 ��6    8   .�59      �� 
 � ��$
 -3,3q�68   5:      �� 5;      "	 	�6    8   .�5<      �� 
 � ��$	 0,3q�68   5=      5>      .+�5?      �  � RowDefinitions 5@      �� 5A      $
��auto �6,   6<   �� 5B      $
��auto �6,   6<    � ColumnDefinitions 5D      �� 5E      $��* �6/   6;   �� 5F      $��* �6/   6;   �� 5G      $��* �6/   6;   .+�5I      �� $��
הערות��6#   $��2��63   .��5J      �� -   5        $��tbMultiLine��5J   &    �� TextWrapping$
 Wrap=�69   $	��130��6M   $��7q�6[    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�5K   2   ��6U   �� *Project.Comment+.��6v   5L      5M      �� 5N      $��פרטי פרויקט��6$   .��5O        AutoGrid.AutoGrid     Rows$
 auto��6*      Columns$ 2*,4*��66   .+�5P   "   �� $
��Bold"�6(   $��שם פרויקט��6:   
������  �� Target 6N   ��  �� ElementName$ TextName��6v   #   AdornedControl.AdornedControl  5Q      !   HorizontalAdornerPlacement$ Outside=�5R         IsAdornerVisible . .��5U   &   �� -   5         ��Name$ TextName��5U   .   ��6@   �� *Project.Name+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors .    AdornerContent 5W   &   d� 5Y   *   $��25��6[   $��25��6g   $��5,-10,0,-10q�6r   $��Right=�6�    	 ModernToggleButton#��[	 6�    d� 	IsChecked 67   �� *NameByAddress+.��5Z   .     $ שם לפי כתובת��6F   $ 	MapMarker=�6]   $��15��6o   $��15��6{   $��0,0,0,0q�6�   $��Center=�6�   5[   +   5_   #   �� 5c   "   $��סוג פרויקט��6(   
������  6=   �� $ ComboProjectType��6m   �� 5d   "   -   5         ��Name$ ComboProjectType��5d   +    �� DisplayMemberPath$
 Name��6�   $��Center=�6�   ��6E   �� *ProjectTypes+ �� SelectedItem 6j   �� *Project.ProjectType+$ TwoWay=�6�   �� 5f   "   $��
תיאור��6(   
������  68   �� $ TextName��6`   �� 5g   "   ��6*   �� *Project.Description+$ TwoWay=� . �� 5i   "    6(   �� $ TextChequeNumber��.��6X   מעלית?5k   #   �� 5l   "   $��LeftToRight=�6+   $
��Left=�6G    6b   �� *Project.HasElevator+5m      5n      �� 5o      $��
כתובת��6$   $��1��63   .��5p        $ auto,auto,auto,auto��6*   $ 2*,4*��6E   .+�5r   "   �� $��ארץ��6(    66   �� $ 	ComboCity���� 5s   "   $
 Name��6�   $��Center=�6�   ��6,   �� *	Countries+ 6N   �� *Project.Country+$ TwoWay=�6�   �� 5u   "   $��עיר��6(    66   �� $ 	ComboCity���� 5v   "   -   5        $ 	ComboCity��5v   ,   $
 Name��6�   $��Center=�6�   ��6?   �� *Cities+ 6^   �� *Project.City+$ TwoWay=�6�   �� 5x   "   $��רחוב��6(   
������  67   �� $ 
TextStreet��6b   �� 5y   "   -   5        $ 
TextStreet��5y   *   ��6?   �� *Project.Street+$ TwoWay=� . �� 5{   "   $��מס' בית��6(    6:   �� $ TextHouseNumber���� 5|   "   -   5        $ TextHouseNumber��5|   *   ��6C   �� *Project.HouseNumber+$ TwoWay=� . 5}      5~      �� 5      $��3��6#   $��1��67   $��פרטי תשלום��6D   y�6X   �� *IsEditEditor+# [  $ 
Visibility��.��5�        $
 auto��6*   $# 2*,3*,*,2*,3*,*,2*,3*,*,2*,3*��66       Orientation$  Vertical=�6^   .+�5�   "   �� $
��7,15q�6)   $��תשלומים��67   �� 5�   "    
 
AmountText#��[
 6,   ��6P   �� *	Project+# [ ! �� StringFormat$! C��$ OneWay=��� 5�   "   �� 5�   "   $��הכנסות��6)   �� 5�   "   #��[
 6,   ��6P   �� *	Project+# [ $! C��$ OneWay=��� 5�   "   �� 5�   "   $��הוצאות��6*   �� 5�   "   #��[
 6,   ��6P   �� *	Project+# [ $! C��$ OneWay=��� 5�   "   �� 5�   "   $��
חובות��6)   �� 5�   "   #��[
 6,   ��6P   �� *	Project+# [ $! C��$ OneWay=�5�      5�      5�      5�      5�      5�      5�      5�      