   M S B A M L   `   `   `  ����Z  VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a9V>clr-namespace:FirstFloor.ModernUI;assembly=FirstFloor.ModernUIFirstFloor.ModernUI  5      xOclr-namespace:FirstFloor.ModernUI.Windows.Controls;assembly=FirstFloor.ModernUI$FirstFloor.ModernUI.Windows.Controls  5      |Qclr-namespace:FirstFloor.ModernUI.Windows.Navigation;assembly=FirstFloor.ModernUI&FirstFloor.ModernUI.Windows.Navigation  5       
RealEstate   RealEstate.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0mui&http://firstfloorsoftware.com/ModernUI   5   	   Mmodernui>clr-namespace:FirstFloor.ModernUI;assembly=FirstFloor.ModernUI   5      ^controlsOclr-namespace:FirstFloor.ModernUI.Windows.Controls;assembly=FirstFloor.ModernUI   5      b
navigationQclr-namespace:FirstFloor.ModernUI.Windows.Navigation;assembly=FirstFloor.ModernUI   5        � Title$  ניהול מקרקעין��5   	   7   1FirstFloor.ModernUI.Windows.Controls.ModernWindow   IsTitleVisible . $��RightToLeft=�65   $
��1365��5	   	   $	��701��6    � WindowState$ 	Maximized=�6E      LogoData� ��|�{h���l�~���ӴL����$:�<��C<��p�<L^��������ф�̴�ӄ��n�$�W`�F�W���d�W<�w@Xt�}̴�� ~o�"%���:���C�:��P���:� -�"%��	h�����@Xt0�"��W��@��W�|�c��W|�{�n|�{h���܄��:�����:`-�m``-����`-��
����(d�܄�(d��P6i(d���B�
���B��Ӽ�Bm`P6i�:܄��:�$&��:Ӱt�:D�Dm`D�D���D�D�
��t(d�$&�(d�Ә׿(d��}��
��}���Ӡ}�m`�׿�:$&��:   aa#���   5   1      MenuLinkGroups 5	   #   �� *Links+   ContentSource 5   	   �� *ContentSource+ � 	Resources 5       �� 
TargetType �� Property  WindowBackgroundContent	 �� Value  DefaultFontFamily  DefaultFontSize  
WindowText  WindowBorder&   System.Windows.Media.TextOptions
 �� 
TargetType ��Name%  System.Windows.SystemParameters$   WindowNonClientFrameThickness �� Source  WindowBackgroundColor �� Color  Key �� 
TargetName �� TargetProperty	 �� To g� Duration �Name � RowDefinitions   BackgroundContent � ColumnDefinitions  ModernWindowTitle � DataContext  	 BooleanToVisibilityConverter �� 	Converter   
TitleLinks'  !System.Windows.Shell.WindowChrome   IsHitTestVisibleInChrome �� Orientation ��Name	 �� X1	 �� Y1	 �� X2	  �� Y2 
 SeparatorBackground  SystemButtonLink! �� ElementName" �� Triggers# �� Binding$ �� RelativeSource% �� Value& �� 
TargetName 
 Accent  NullToVisibilityConverter' �� ConverterParameter( �� Stretch#   FirstFloor.ModernUI.Resources)   Minimize* � ToolTip  SystemButton#  System.Windows.SystemCommands+   MinimizeWindowCommand, �� Mode- �� AncestorType. ��Name/   Restore0   RestoreWindowCommand1 � UseLayoutRounding2   Maximize3   MaximizeWindowCommand4   Close  SystemCloseButton5   CloseWindowCommand7   1FirstFloor.ModernUI.Windows.Controls.ModernButton6   EllipseDiameter7   	IconWidth8   
IconHeight9   IconData:   Back5   /FirstFloor.ModernUI.Windows.Controls.ModernMenu;   
LinkGroups<   SelectedSource= �� Mode6	   0FirstFloor.ModernUI.Windows.Controls.ModernFrame> 	 Name?   ContentLoader@ 	  ContentLoaderA 	  SourceB   ResizeGripDirectionC �� TriggersD P� PropertyE P� Value  WindowBorderActiveF ]� 
ConditionsG �� PropertyH �� ValueI   CornerRadiusJ   GlassFrameThicknessK   UseAeroCaptionButtonsL   NonClientFrameEdges��%T  &	        7  7 	 7  7  7  7  7  7  �� 5   
   "  6   .�5      ��  �@ BackgroundContent#	 �  62   �� 5       � ��#	 �  6+   �� 5       � ��#	 �  6)   �� 5       � ��#	 �  6+   �� 5       � ��#	 �  6,   �� 5       � ��$		 800��6&   �� 5       � ��$		 640��6'   �� 5       � ��$		 320��6)   �� 5       � ��$		 320��6*   �� 5       �@�
ResizeMode$	 CanResizeWithGrip=�6+   �� 5       �@�UseLayoutRounding	 . 62   �� 5       �@ TextFormattingMode$	 Display=�6?   �� 5       � ��	 5      �� 5      "
  6&   .R�5      �� $ WindowBorder��6!   ��67   �� # Z ��5      �� 5    "   WindowBorderBackground��# �  6R    5"      ���� 5#   "   BackgroundAnimation .��5$   &   �� $ WindowBorderBackground��65   $ Color�6d   # �  6�   $ 0:0:.6D�6�   5%   #   5&      ._�5(      �� #��z��6%   $��1q�6Q   ._�5)   "   �� .u�5*   &   � $ 
LayoutRoot��6+    5+   *   �� 5,   .   $
��Auto �6<   �� 5-   .   $
��Auto �6<   �� 5.   .   $��* �6<   .+�52   *   �� $��5��69   #��z 6J   � 55   *    56   .   �� 57   2   $��* �6C   �� 58   2   $
��Auto �6C   �� 59   2   $
��Auto �6C   �� 5:   2   $
��Auto �6C   .+�5>   .   �� #��z  68   $��18(�6W   $
��20,0q�6e   $��Center=�6s   8��  # z 5?   4   y�5@   4   �� 8  �� 5C   .   $��1��6;   #��z 6K   $��0,0,24,0q�6v    . ��5D   2   �� 5E   6   .R�5F   :   �� $ 
Horizontal=�6E   5G   7   ��5I   2   �� 5J   6   .R�5K   :   �� $ 
Horizontal=�6E   .+�5L   >   �� $ 	Separator��6C   $ .5��6V   $ 3��6^   $ .5��6e   $  12��6m   $	��5,0q�6u   $��Center=�6�   #��� 
 6�   �� 5N   >   $*��$navigation:LinkCommands.NavigateLink��5O   A   8�� ��5N   E   �� *DisplayName+��5P   A   �� *Source+��5Q   A   �� $! ContentFrame��5R   A   5S   ;   " 5T   :   �� 5U   >   # 6J   �� $ �� *PreviousData+% 6�   Z� .a�5V   B   �� $& 	Separator��6_    � y�$	 	Collapsed=�6v   5W   ?   5Y   7   5[   /   �� 5^   .   $��2��65   #���  6E   $��36��6k   $��36��6v   $	��8,0q�6�   # z 5_   1   y�5`   1   �� 8  $' inverse��._�5a   2   F� $
( Fill=�6H   
�������$��24��6d   $��24��6o   $��Center=�6{   $��Center=�6�   ��67   �� 6�   5b   /   �� 5e   .   $��3��69   $ 
Horizontal=�6I   $	��Top=�6b    . .+�5f   2   �� #* Z) 6�   8�� ��69   �� # Z+ ��5g   6   � 5h   :   $��13��6?   $��12��6J   $z�1,0,0,1,0,1W�6V   .+�5i   >   F� ����    ��[ � z ��[ 	$��8��6V   $��7��6`   $��Center=�6k   $��Center=�6�   $��2��5j   �   ��6?   �� *
Foreground+$ �� $, FindAncestor=�$- ButtonL�6�   5k   ;   5m   3   � 5n   2   $��1,0,1,0q�67   .+�5o   6   �� $. Restore��6=   #* Z/ 6�   8�� $y�	Collapsed=�6�   ��6N   �� # Z0 ��5p   :   � 5q   >   $��13��6C   $��12��6N   1 . $z�1,0,0,1,.5,.5W�6s   .+�5r   B   F� X���Ѐ�     � z     � z ��[ �    ��- р�[ ��- �    �� р�[ �� р�[  z �     z 	$��8��6}   $��8��6�   $��Center=�6�   $��Center=�6�   $��1��5s   �   ��6C   �� *
Foreground+$ �� $, FindAncestor=�$- ButtonL�6�   5t   ?   5v   7   �� 5w   6   $. Maximize��6=   #* Z2 6�   8�� ��6O   �� # Z3 ��5x   :   � 5y   >   $��13��6C   $��12��6N   .+�5z   B   F� +����    @B �@T� @B �@T�  z �     z 	$��9��6d   $��8��6n   $��Center=�6y   $��Center=�6�   $��2��5{   �   ��6C   �� *
Foreground+$ �� $, FindAncestor=�$- ButtonL�6�   5|   ?   5~   7   5   3   �� 5�   2   #* Z4 6�   8�� ��69   �� # Z5 ��5�   6   � 5�   :   $��13��6?   $��12��6J   $z�1,0,0,1,0,1W�6V   .+�5�   >   F� +����        � z ��j � z     �    ��j 	$��8��6`   $��7��6j   $��Center=�6u   $��Center=�6�   $	��1.5��5�   �   ��6?   �� *
Foreground+$ �� $, FindAncestor=�$- ButtonL�6�   5�   ;   5�   3   5�   /   5�   +   � 5�   *   $��1��6/    5�   .   �� 5�   2   $��72 �6C   �� 5�   2   $��* �6C   .+�5�   .     $��8,10,0,0q�6?   $
��Left=�6Q   $	��Top=�6l   $6 24Q�5�   @   $7 12Q�6U   $8 12Q�6d   a9 ��@����O�@������p\-�������@��� �8@����ا  6n� �8��� ������� �p\-��O�@����O#* Z: 5�   @    . ��5�   @   �� *BackCommand+5�   @     5�   .   $��	38,10,0,0q�6?   $
��Left=�6R   $	��Top=�6m   $��LeftToRight=�6�   $6 24Q�5�   @   $7 12Q�6U   $8 12Q�6d   a9 ��@����O�@������p\-�������@��� �8@����ا  6n� �8��� ������� �p\-��O�@����O#* Z: 5�   @    . ��5�   @   �� *ForwardCommand+5�   @     5�   .   $��1��6=   #; z 5�   >   < 5�   >   �� *Source+$! ContentFrame��$= TwoWay=�5�   >   5�   +   	  5�   *   $> ContentFrame��6:   $��3��6P   $��2��6]   $��
42,8,16,16q�6n   #@ z? 6�   A 6�   �� *ContentSource+$ �� *TemplatedParent+$= TwoWay=�6�   � 5�   *   $ 
ResizeGrip��6<   $��2��6/   
������ $y�	Collapsed=�6i   $��Right=�6�   $��Bottom=�6�   $B BottomRight=�6�   .+�5�   .   F� $��12��63   $��12��6>   $��1q�6J   #���  5�   /   $��1��5�   /   $
( None=�5�   /   �����@B ��� ���- ��� �@KL ��� ���j ��� �@T� ��� ��ا ��� Ѐ� @T� р� �ا Ѐ�[ @T� р�[ �ا Ѐ�� @T� р�� �ا �@KL ��[ ���j ��[ �@T� ��[ ��ا ��[ Ѐ�[ @KL р�[ ��j Ѐ�� @KL р�� ��j �@T� �� ��ا �� Ѐ�� @B р�� ��- 5�   /   5�   +   5�   '   5�   #   5�      5�      C 5�      P� 5�      D �@�IsActiveE . . �5�   "   �� #	 �  6@    � ��5�      P� 5�      D �@�WindowState$E 	Maximized=�6=   . �5�   "   �� $& Maximize��6)    � y�$	 	Collapsed=�6U   �� 5�   "   $& Restore��6)    � y�$	 Visible=�6T   �� 5�   "   $& 
LayoutRoot��6)    � ��$	 7q�6S   5�      P� 5�      D �@�WindowState$E Normal=�6=   . �5�   "   �� $& Maximize��6)    � y�$	 Visible=�6U   �� 5�   "   $& Restore��6)    � y�$	 	Collapsed=�6T   5�      ]� 5�      F 5�   "   �� 5�   &   G �@�
ResizeMode$H CanResizeWithGrip=�6F   �� 5�   &   G �@�WindowState$H Normal=�6G   ./�5�   "   �� $& 
ResizeGrip��6)    � y�$	 Visible=�6W   5�      5�      5�      �� 5�       �@ WindowChrome	 5�        5�      $I 0��6#   $J 1q�64   K .  $
L None=�6j   5�      5�      5�       5�   
   +
   %FirstFloor.ModernUI.Presentation.Link
  2   ,FirstFloor.ModernUI.Presentation.DisplayableM   DisplayName$M 
תצוגה��6   N 
  Source$'N !/Views/Settings/SettingsPage.xaml?�6'   
  5�   
   $M 
אודות��6   $ N /Views/Settings/About.xaml?�6'   5�      