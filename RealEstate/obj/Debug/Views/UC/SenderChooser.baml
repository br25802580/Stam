   M S B A M L   `   `   `  ����E  ARealEstate, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:RealEstate.Views.UCRealEstate.Views.UC  5      '    !RealEstate.Views.UC.SenderChooser   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:RealEstate.Views.UC     5        � 	Resources  5	         BoolToVisibility��%   &	        �� 5
   
   6'   5      .��5      �� .+�5   
   �� y�6   �� *IsCustomerSender+ �� 	Converter# [   �� FallbackValue$ Hidden��.+�5      �� $��לקוח��6    �� Target 6#   ��  �� ElementName$ ComboCustomer���� 5      -   5         ��Name$ ComboCustomer��5       �� DisplayMemberPath$
 Name��6   $��Center=�6�   $	��150��6�   ��6.   �� *	Customers+ �� SelectedItem 6P   �� *
Customer+ �� Mode$ TwoWay=�6�   5      �� 5   
   y�6   �� *IsSupplierSender+# [  $ Hidden��.+�5      �� $��ספק��6    6"   �� $ ComboSupplier���� 5      -   5        $ ComboSupplier��5      $
 Name��6   $��Center=�6�   $	��150��6�   ��6.   �� *	Suppliers+ 6P   �� *
Supplier+$ TwoWay=�6�   5      �� 5   
   .+�5      �� $��פרויקט��6    6%   �� $ ComboProject���� 5      -   5        $ ComboProject��5      $
 Name��6|   $��Center=�6�   $	��150��6�   ��6-   �� *
Projects+ 6N   �� *	Project+$ TwoWay=�6�   5      �� 5   
   .+�5      �� $��דירה��6    6#   �� $ 	ComboFlat���� 5      -   5        $ 	ComboFlat��5      $
 Name��6s   $��Center=�6�   $	��150��6�   ��6*   �� *Flats+ 6H   �� *Flat+$ TwoWay=�6�   5      5      5      