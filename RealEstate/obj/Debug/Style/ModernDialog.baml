   M S B A M L   `   `   `  ����Z  VFirstFloor.ModernUI, Version=1.0.9.0, Culture=neutral, PublicKeyToken=bc9b0c37bf06c6a9xOclr-namespace:FirstFloor.ModernUI.Windows.Controls;assembly=FirstFloor.ModernUI$FirstFloor.ModernUI.Windows.Controls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0897    1FirstFloor.ModernUI.Windows.Controls.ModernDialog  �� 
TargetType   ModernDialog �� Property  WindowBackgroundContent �� Value  DefaultFontFamily  DefaultFontSize  
WindowText&   System.Windows.Media.TextOptions �� 
TargetType  WindowBackground%  System.Windows.SystemParameters$   WindowNonClientFrameThickness �� Source  WindowBorder    BackgroundContent � RowDefinitions 
 Accent � Title	 � DataContext
   TextFormattingMode  ToUpperConverter �� 	ConverterF   @FirstFloor.ModernUI.Windows.Controls.TransitioningContentControl    Buttons �� Orientation 	 WindowBorderActive �� RelativeSource �� AncestorType  
 BooleanToVisibilityConverter'  !System.Windows.Shell.WindowChrome   CornerRadius   GlassFrameThickness   UseAeroCaptionButtons�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    \controlsOclr-namespace:FirstFloor.ModernUI.Windows.Controls;assembly=FirstFloor.ModernUI  5      0mui&http://firstfloorsoftware.com/ModernUI   5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      %�  &	        7  7 
 �� 5      "    6   .�5   
   ��  �@  BackgroundContent# �  6.   �� 5   
    � ��# �  6'   �� 5	   
    � ��# �  6%   �� 5
   
    � ��# �  6'   �� 5   
    � ��$
 1100��6%   �� 5   
    � ��$	 700��6&   �� 5   
    � ��$	 240��6%   �� 5   
    � ��$	 100��6&   �� 5   
    �@�ShowInTaskbar .  6*   �� 5   
    �@�
ResizeMode$ NoResize=�6'   �� 5   
    �@�SizeToContent$ WidthAndHeight=�6*   �� 5   
    �@�UseLayoutRounding . 6.   �� 5   
    � ��$ RightToLeft=�6*   �� 5   
    �@ TextFormattingMode$ Display=�6;   �� 5   
    � �� 5      �� 5      "   6"   .R�5      �� #���  6   ��6M   �� # Z ._�5      � .+�5      �� #���  6%   $��1q�6R   ._�5   "   �� .u�5   &   � .+�5   *   �� #��z 69   � 5&   *   $��24,24,24,24q�6/    5'   .   �� 5(   2   $
��Auto �6@   �� 5)   2   $��* �6@   �� 5*   2   $
��Auto �6@   .+�5.   .   �� $��0,3,0,0q�65   #���  6O   $
��Left=�6v   ._�5/   2   �� #	 z 6<   $��Segoe UI)�50   4   $��16(�51   4   $
 Ideal=�52   4   $��CharacterEllipsis=�53   4   ��5/   b   �� 8   53   4   54   /     57   .   $��1��6S   $
��0,20q�6`   #��z��6n   �� 5:   .   $��2��6;   #��z 6H   $��Right=�6p   ��5;   2   �� 5<   6   .R�5=   :   �� $ 
Horizontal=�6E   5>   7   5@   /   5B   +   5C   '   5D   #   5E      �� 5F      #��� 	 6%   $��1q�6X   y�6l   �� *
IsActive+ �� *FindAncestor+" �8  5G      5H      5I      5K      �� 5M   
    �@ WindowChrome 5N        5O      $ 0��6   $ 1q�60    .  6H   5Q      5R      5S      