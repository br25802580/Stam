   M S B A M L   `   `   `  ����E  ARealEstate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:RealEstate
RealEstate  5          RealEstate.CustomersTable   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Z VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a90mui&http://firstfloorsoftware.com/ModernUI  5      U QMaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $localclr-namespace:RealEstate  5        � 	Resources  5      '   !RealEstate.GroupsToTotalConverter   GroupsToTotalConverter*   $RealEstate.GroupsToRevenuesConverter  GroupsToRevenuesConverter*   $RealEstate.GroupsToExpensesConverter  GroupsToExpensesConverter  BoolToVisibilityConverter4   .RealEstate.OppositveBoolToVisibilityConverters & "OppositveBoolToVisibilityConverter*   $RealEstate.GroupTotalHeaderConverter  GroupTotalHeaderConverter��%�   &	        &	      &	 &     &	 9     &	 L     &	 _       5   
   6'     5   
   6*     5   
   6*   �� 5   
   6'     5   
   64     5   
   6*   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5      �� $��	0,10,0,20q�6   $��Center=�6,    �� Orientation$ 
Horizontal=�6G   $��Stretch=�6`   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon  $��35��6E   $��35��6Q   $��0,0,20,0q�6\   $��Center=�6n   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=null]]   Kind 6*   �� *
IconKind+6n   �� 5      $��Center=�6     Heading1#��[ 6R   $��0,0,15,0q�6t   ��67   �� *	MainTitle+6t   �� 5      $��Bottom=�6     Heading2#��[ 6T   $��0,0,0,5q�6v   ��67   �� *EntityTitle+6v     5      $��Center=�6R   $��20��6n   $��20��6z   $��5,10,0,0q�6�    6*   �� *
ParentIcon+ �� Mode$ OneWay=���5      �� 5       �� 
TargetType"  6     �� Triggers 5      �� 5    "    �� Binding 6.   �� *
ParentIcon+ �� Value 6M   Z� .a�5!   &   �� 	 �� Property	 � y�
 �� Value$
 	Collapsed=�6C   5"   #   5'      5)      5*      � 5+      $��1��6    � ColumnDefinitions 5,      �� 5-      6)    5/      �� 50      $��25 �6$   62   �� 51      $��5 �6$   61   �� 52      $��* �6$   61   .+�54      �� -   5         ��Name$ ButtonsPanel��54   6   $ 
Horizontal=�6   y�55      �� *DisplayCommands+ �� 	Converter# [ .+�56      ��  � ToolTip$ 
הוספה��6     CommandButton#��[ 6L   ��6-   �� *
AddCommand+y�57      �� *IsSubEditor+# [ .��58      �� $ 
Horizontal=�6%   .+�59        $
 Plus=�66   $��18��6C   $��18��6O   $��0,0,5,0q�6Z   �� 5:      
הוספה6/   5;      5<      �� 5=      $ 
קישור��6   #��[ 6O   ��6-   �� *AttachCommand+y�5>      �� *IsSubEditor+# [ .��5?      �� $ 
Horizontal=�6%   .+�5@        $ LinkVariant=�66   $��18��6I   $��18��6U   $��0,0,5,0q�6`   �� 5A      
קישור6/   5B      5C      �� 5D      $ הוספה וקישור��6   #��[ 6S   ��64   �� *
AddCommand+y�5E      �� *IsSubEditor+# [ .��5F      �� $ 
Horizontal=�6%   .+�5G        $
 Plus=�66   $��18��6C   $��18��6O   $��0,0,5,0q�6Z   �� 5H      הוספה וקישור66   5I      5J      �� 5K      $ 
עריכה��6   #��[ 6M   ��6-   �� *EditCommand+.��5L      �� $ 
Horizontal=�6%   .+�5M        $ BorderColor=�66   $��18��6J   $��18��6V   $��0,2,5,0q�6a   �� 5N      
עריכה6/   5O      5P      �� 5Q      $ 
מחיקה��6   #��[ 6O   ��6-   �� *DeleteCommand+y�5R      �� *IsSubEditor+# [ .��5S      �� $ 
Horizontal=�6%   .+�5T        $ Delete=�66   $��18��6E   $��18��6Q   $��0,0,5,0q�6\   �� 5U      
מחיקה6/   5V      5W      �� 5X      $ הסרת קישור��6   #��[ 6T   ��62   �� *DetachCommand+y�5Y      �� *IsSubEditor+# [ .��5Z      �� $ 
Horizontal=�6%   .+�5[        $ DeleteSweep=�66   $��18��6J   $��18��6V   $��0,0,5,0q�6a   �� 5\      הסרת קישור64   5]      5^      �� 5_      $ 
מחיקה��6   #��[ 6O   ��6-   �� *DeleteCommand+y�5`      �� *IsSubEditor+# [ .��5a      �� $ 
Horizontal=�6%   .+�5b        $ Delete=�66   $��18��6E   $��18��6Q   $��0,0,5,0q�6\   �� 5c      
מחיקה6/   5d      5e      5f      &   System.Windows.Controls.DataGrid  5g      -   5        $	��DG1��5g   j   $��2��6      
IsReadOnly .    CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .  ��6u   �� *ListCollectionView+(	   "RealEstate.MultiSelectorBehaviours  	  SynchronizedSelectedItems 5h      �� *SelectedItems+ �� 
GroupStyle 5i      (
  "System.Windows.Controls.GroupStyle
  5j       
  ContainerStyle 5k      �� 5l   "   " ��6(   .�5m   &   �� 	 � ��
 5n   *   �� 5o   .    �� 
TargetType" ��6>   .R�5p   2   9�  9� 
IsExpanded . ��5q   6   �� 5r   :   $ 
Horizontal=�6E   $��5q�6^   .+�5s   >   �� $
��Bold"�6I   ��6[   ��  �� Path$
 Name��� 5t   >   $
��10,0q�6H   y�6W   �� *DataContext.DisplaySummary+ �� RelativeSource �� *FindAncestor+ �� AncestorType$ UserControlL�# [ �� 5v   B   �6F   �� *Items+# [ $ OneWay=� �� 5w   B    �� FlowDirection$ LeftToRight=�6F   �6b   �� $ Items�$ OneWay=� �� StringFormat$ c�� �� ConverterCulture$ he-IL��# [  5x   ?   �� 5y   >   $
��10,0q�6H   y�6W   �� *$"DataContext.DisplayExtendedSummary+ �� *FindAncestor+$ UserControlL�# [ �� 5z   B   $�הכנסות: ��6F    �� 5{   B   $ LeftToRight=�6F   �6b   �� $ Items�$ OneWay=�$ c��$ he-IL��# [ 5|   ?   �� 5}   >   $
��10,0q�6H   y�6W   �� *$"DataContext.DisplayExtendedSummary+ �� *FindAncestor+$ UserControlL�# [ �� 5~   B   $�הוצאות: ��6F    �� 5   B   $ LeftToRight=�6F   �6b   �� $ Items�$ OneWay=�$ c��$ he-IL��# [ 5�   ?   �� 5�   >   $�� (��6H   �� 5�   >   ��6H   �� $ 	ItemCount��� 5�   >   $�� ��6H   �� 5�   >   $��פריטים��6H   �� 5�   >   $��)��6H   5�   ;   .��5�   6   �� 5�   3   5�   /   5�   '   5�   #   5�      
  5�       
  HeaderTemplate 5�      �� 5�   "   .R�5�   &   �� .+�5�   *   �� ��64   �� $
 Name�5�   '   5�   #    5�      �� 5�   "   " ��6(   .�5�   &   �� 	 � ��
 5�   *   �� 5�   .   " ��6>   .R�5�   2   9� $
��20,0q�6;    . ��5�   6   �� 5�   :   $ 
Horizontal=�6E   $��5q�6^   .+�5�   >   �� $
��Bold"�6I   ��6[   �� $
 Name��� 5�   >   $
��10,0q�6H   y�6W   �� *DataContext.DisplaySummary+ �� *FindAncestor+$ UserControlL�# [ �� 5�   B   �6F   �� *Items+# [ $ OneWay=� �� 5�   B   $ LeftToRight=�6F   �6b   �� $ Items�$ OneWay=�$ c��$ he-IL��# [  5�   ?   �� 5�   >   $
��10,0q�6H   y�6W   �� *$"DataContext.DisplayExtendedSummary+ �� *FindAncestor+$ UserControlL�# [ �� 5�   B   $�הכנסות: ��6F    �� 5�   B   $ LeftToRight=�6F   �6b   �� $ Items�$ OneWay=�$ c��$ he-IL��# [ 5�   ?   �� 5�   >   $
��10,0q�6H   y�6W   �� *$"DataContext.DisplayExtendedSummary+ �� *FindAncestor+$ UserControlL�# [ �� 5�   B   $�הוצאות: ��6F    �� 5�   B   $ LeftToRight=�6F   �6b   �� $ Items�$ OneWay=�$ c��$ he-IL��# [ 5�   ?   �� 5�   >   $�� (��6H   �� 5�   >   ��6H   �� $ 	ItemCount��� 5�   >   $�� ��6H   �� 5�   >   $��פריטים��6H   �� 5�   >   $��)��6H   5�   ;   .��5�   6   �� 5�   3   5�   /   5�   '   5�   #   5�      5�      5�      5�      5�      