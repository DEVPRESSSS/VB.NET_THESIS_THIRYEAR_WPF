   M S B A M L   `   `   `  ����H  DVB_THESIS_WPS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:VB_THESIS_WPSVB_THESIS_WPS  5   	   g cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137akOclr-namespace:MahApps.Metro.IconPacks;assembly=MahApps.Metro.IconPacks.MaterialMahApps.Metro.IconPacks 63   &     VB_THESIS_WPS.ViewDetailsProduct   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:VB_THESIS_WPS     5   	   `IconOclr-namespace:MahApps.Metro.IconPacks;assembly=MahApps.Metro.IconPacks.Material     63     � Title$  Add Product��5   	   $	��486��6   $	��800��6*    � WindowStyle$
 None=�5	   	    � 
ResizeMode$ NoResize=�5
   	   $��Roboto)�5   	   
������  � AllowsTransparency$
 True��5   	    � WindowStartupLocation$ CenterScreen=�5   	   $	��480��5   	   $	��800��6   .��5      �� $	��400��6   $	��800��6    �� CornerRadius$ 10,10,10,10��6,   ���#073b4C$��2q�6]   ��5   
   �� 5       �� Color$ #F4F5F6��6   ._�5   
   �  � RowDefinitions 5      �� 5      $��40 �6    �� 5       .+�5$      �� $��0��6   ���#073b4c$��0,0,0,0q�5&      $ 7,7,0,0��5'         
dropShadow J� Effect# [  5(      5,      �� 5.      -   5        	 ��Name$	 BorderColorOutline��5.      $��1��61   
�������$ 10��50      $��2q�51      $��10,10,10,10q�52      # [  53      57      � 59      
 � ColumnDefinitions
 5:      �� 5<      �� 5=      $��35 �6'   �� 5>      $��35 �6'   .+�5F      �� $��Product Details��6   
�������$��14(�5H      $��Roboto)�5I      $��0��5J      $��Center=�5K      $��10q�5L      $��Medium"�5O      �� 5S      -   5         ��Name$ btnClose��5S      $��0q�5T      
�������$��12(�5V      $��x��5W       � Cursor$
 Hand��5X      $��3��5Y      $��Center=�5Z      ��5\      �� 5]       �� 
TargetType" ��6    .�5^      ��  �� Property � �� �� Value �#06D6A06;    �� Triggers 5_      P� 5`   "    P� Property �@J�IsMouseOver P� Value$
 True��6A   . �5a   &   ��  � �� �#00CA4E6C   5b   #   5d      ��5g      �� 5h       �� 
TargetType" ��6*   .R�5i      �� $��20��6%   $��20��60   $ 10��5j   %   $��0px 2px 0px 2pxq�5k   %   #��z��5l   %   ._�5m   "   �� $��Center=�63   $��Center=�5n   %   5p      5q      5t      5v      �� 5{      -   5         ��Name$ Imagepreview��5{      $
��Fill=�5|      $	��150��5}      $	��150��5~      $��334,14,312,278q�5      $��1��5�      �� 5�      -   5         ��Name$ productname��5�      $��	Shoes One��6-   $��18(�5�      $��1��5�      $	��100��5�      $��Roboto)�5�      $
��Bold"�5�      ���#073b4c$��271,184,425,224q�5�   -   $��Italic$�5�      �� 5�      -   5        $
 size��5�      $��75 cm��6&   $��18(�5�      $��1��5�      $��Roboto)�5�      $��DemiBold"�5�      ���#073b4c$��467,184,269,225q�5�      $��Italic$�5�      �� 5�      -   5        $ description��5�      $��this is test ��6-   $��14(�5�      $��1��5�      $��Roboto)�5�      $��DemiBold"�5�      ���#073b4c$��271,231,299,183q�5�      $��Italic$�5�       �� TextAlignment$ Center=�5�      $
��Auto��5�      �� 5�      -   5        $ Price��5�      $
��2000��6'   $��24(�5�      $��1��5�      $
��auto��5�      $��Roboto)�5�      $
��Bold"�5�      ���#EF476F$��378,276,356,132q�5�      $��Italic$�5�       J� RenderTransformOrigin$ 1.211,0.677&�5�      �� 5�      -   5        $ category��5�      $��Running shoes��6*   $��14(�5�      $��1��5�      $��Roboto)�5�      $��DemiBold"�5�      ���#073b4c$��Italic$�5�      $��271,325,281,97q�5�      $
��Auto��5�      $ Center=�5�      �� 5�      -	   5        $ brand��5�      $
��Nike��6'   $��24(�5�      $��1��5�      $��Roboto)�5�      $	��Top=�5�      $��DemiBold"�5�      $
��Auto��5�      $��Italic$�5�      ���#073b4c$��633,42,43,0q�5�      $��27��6&   �� 5�       �� Orientation$ 
Horizontal=�6   $��71,31,694,371q�62   $��1��6I   .+�5�      �� -
   5        $	 ColorBorder��5�      $��30��6.   $��30��69   $ 50��6E   
���   �$ -0.876,-0.681&�6j   5�      5�      �� 5�      -   5        $ 
Price_Copy��5�      $	��₱��6,   $��24(�5�      $��1��5�      $
��auto��5�      $��Roboto)�5�      $
��Bold"�5�      ���#EF476F$��Italic$�5�      $��355,276,425,142q�5�      $ 1.211,0.677&�5�      5�      5�      5�      