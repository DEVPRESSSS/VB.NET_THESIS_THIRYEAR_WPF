   M S B A M L   `   `   `  ����H  DVB_THESIS_WPS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:VB_THESIS_WPSVB_THESIS_WPS  5   	       VB_THESIS_WPS.StockIn   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:VB_THESIS_WPS     5   	     � Title$  StockIn��5   	   $	��300��6   $	��400��6&   $��Roboto)�5	   	    � WindowStyle$
 None=�5
   
   
������  � AllowsTransparency$
 True��5       � WindowStartupLocation$ CenterScreen=�5       � 
ResizeMode$ NoResize=�5   
   .��5      ��  �� CornerRadius$ 10��6   
������ ._�5   
   �� $ 10��6   $��3q�5      ��5      �� 5       �� Color$ #F4F5F6��6"   ._�5      �  � RowDefinitions 5      �� 5      $��40 �6$   �� 5      .+�5$      �� $��0��6   ���#073b4c$��0,0,0,0q�5&      $ 7,7,0,0��5'         
dropShadow J� Effect# [  5(      5,      � 5/      	 � ColumnDefinitions	 50      �� 52      �� 53      $��35 �6+   �� 54      $��35 �6+   �� 55      $��35 �6+   .+�5;      �� $��Stock In��6    ���#FFFFFF$��14(�5=   )   $��Roboto)�5>   )   $��0��5?   )   $��Center=�5@   )   $��10q�5A   )   $��Medium"�5D   )   �� 5I      -   5        
 ��Name$
 btnClose��5I      $��0q�5J      
�������$��12(�5L      $��x��5M       � Cursor$
 Hand��5N      $��3��5O      $��Center=�5Q      ��5R      �� 5S       �� 
TargetType" ��6$   .�5T   "   ��  �� Property � �� �� Value �#06D6A06?    �� Triggers 5U   "   P� 5V   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5W   *   ��  � �� �#00CA4E6G   5X   '   5Z      ��5]      �� 5^       �� 
TargetType" ��6.   .R�5_   "   �� $��20��6)   $��20��64   $ 10��5`      $��0px 2px 0px 2pxq�5a      #��z��5b      ._�5c   &   �� $��Center=�67   $��Center=�5d      5f   #   5g      5j      5l      �� 5n      $+��%D:\VB_THESIS_WPS\Images\shoesIcon.PNG��6   $��1��6G   $��160,19,160,173q�6T   �� 5q      -   5         ��Name$ newstock��5q        	TextBoxes#��[ 6,   $��1��5r      $	��150��5s      $��Center=�6&   $��	0,0,0,115q�5t      $��25��5u       �� 	MaxLength$ 4��5v      $��Bottom=�5w      5y      �� 5{      -   5        $
 	updatebtn��5{      $��1��5|      $	��150��5}        	addButton#��[ 5~      $��Update��5      $��Center=�6*   $��0,0,0,64q�6G   $��30��6Y   $��Bottom=�5�      $
 Hand��5�      5�      5�      5�      5�      5�      