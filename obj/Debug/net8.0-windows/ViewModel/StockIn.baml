   M S B A M L   `   `   `  ����H  DVB_THESIS_WPS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:VB_THESIS_WPSVB_THESIS_WPS  5   	       VB_THESIS_WPS.StockIn   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:VB_THESIS_WPS     5   	     � Title$  StockIn��5   	   $	��300��6   $	��400��6&   $��Roboto)�5	   	    � WindowStyle$
 None=�5
   
   
������  � AllowsTransparency$
 True��5       � WindowStartupLocation$ CenterScreen=�5       � 
ResizeMode$ NoResize=�5   
   .��5      ��  �� CornerRadius$ 10��6   
������ ._�5   
   �� $ 10��6   $��3q�5      ��5      �� 5       �� Color$ #F4F5F6��6"   ._�5      �  � RowDefinitions 5      �� 5      $��40 �6$   �� 5      .+�5$      �  � ColumnDefinitions 5%      �� 5'      �� 5(      $��35 �6+   �� 5)      $��35 �6+   �� 5*      $��35 �6+   .+�50      �� $��Stock In��6    ���#383838$��14(�52   )   $��Roboto)�53   )   $��0��54   )   $��Center=�55   )   $��10q�56   )   $��Medium"�59   )   �� 5>      -   5        	 ��Name$	 btnClose��5>      $��0q�5?      
�������$��12(�5A      $��x��5B      
 � Cursor$

 Hand��5C      $��3��5D      $��Center=�5F      ��5G      �� 5H       �� 
TargetType" ��6$   .�5I   "   ��  �� Property � �� �� Value �#06D6A06?    �� Triggers 5J   "   P� 5K   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5L   *   ��  � �� �#00CA4E6G   5M   '   5O      ��5R      �� 5S       �� 
TargetType" ��6.   .R�5T   "   �� $��20��6)   $��20��64   $ 10��5U      $��0px 2px 0px 2pxq�5V      #��z��5W      ._�5X   &   �� $��Center=�67   $��Center=�5Y      5[   #   5\      5_      5a      �� 5c      $+��%D:\VB_THESIS_WPS\Images\shoesIcon.PNG��6   $��1��6G   $��160,10,160,182q�6T   �� 5f      -   5         ��Name$ newstock��5f         	TextBoxes#��[  6,   $��1��5g      $	��150��5h      $��Center=�6&   $��	0,0,0,115q�5i      $��25��5j       �� 	MaxLength$ 4��5k      $��Bottom=�5l      5n      �� 5p      -   5        $	 	updatebtn��5p      $��1��5q      $	��150��5r        	addButton#��[ 5s      $��Update��5t      $��Center=�6*   $��0,0,0,64q�6G   $��30��6Y   $��Bottom=�5u      $

 Hand��5v      5w      5}      5�      5�      5�      