   M S B A M L   `   `   `  ����H  DVB_THESIS_WPS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:VB_THESIS_WPSVB_THESIS_WPS  5   	   g cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137akOclr-namespace:MahApps.Metro.IconPacks;assembly=MahApps.Metro.IconPacks.MaterialMahApps.Metro.IconPacks 63       VB_THESIS_WPS.EditProduct   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:VB_THESIS_WPS     5   	   `IconOclr-namespace:MahApps.Metro.IconPacks;assembly=MahApps.Metro.IconPacks.Material     63   -   5          � Title$  Add Product��5   	   $	��338��6   $	��800��6*    � WindowStyle$
 None=�5	   	    � 
ResizeMode$ NoResize=�5
   	   $��Roboto)�5   	   
������  � AllowsTransparency$
 True��5   	    � WindowStartupLocation$ CenterScreen=�5   	   $	��400��5   	   $	��800��6   .��5      �� $	��400��6   $	��800��6    �� CornerRadius$ 10,10,10,10��6,   ���#073b4C$��2q�6]   ��5   
   �� 5       �� Color$ #F4F5F6��6   ._�5   
   �  � RowDefinitions 5      �� 5       $��40 �6    �� 5!      .+�5%      �� $��0��6   ���#073b4c$��0,0,0,0q�5'      $ 7,7,0,0��5(         
dropShadow J� Effect# [  5)      5-      �� 5/      $��1��6   
�������$ 10��51      $��0,10,0,0q�52      # [  53      57      � 59      	 � ColumnDefinitions	 5:      �� 5<      �� 5=      $��35 �6'   �� 5>      $��35 �6'   .+�5F      �� $��Add Product��6   
�������$��14(�5H      $��Roboto)�5I      $��0��5J      $��Center=�5K      $��10q�5L      $��Medium"�5O      �� 5R      -   5        
 ��Name$
 btnMinimize��5R      $��0q�5S      
�������$��13(�5U      $��-��5V       � Cursor$
 Hand��5W      $��1��5X      $��Center=�5Y      ��5\      �� 5]       �� 
TargetType" ��6    .�5^      ��  �� Property � �� �� Value �#EF476F6;    �� Triggers 5_      P� 5`   "    P� Property �@J�IsMouseOver P� Value$
 True��6A   . �5a   &   ��  � �� �#FF606C   5b   #   5d      ��5g      �� 5h       �� 
TargetType" ��6*   .R�5i      �� $��20��6%   $��20��60   $ 10��5j      #��z��5k      ._�5l   "   �� $��Center=�63   $��Center=�5m      5o      5p      5r      �� 5u      -   5        $
 btnClose��5u      $��0q�5v   
   
�������$��12(�5x   
   $��x��5y   
   $
 Hand��5z   
   $��3��5{   
   $��Center=�5}   
   ��5~      �� 5      " ��6    .�5�      ��  � �� �#06D6A06;    5�      P� 5�   "    �@J�IsMouseOver$
 True��6A   . �5�   &   ��  � �� �#00CA4E6C   5�   #   5�      ��5�      �� 5�      " ��6*   .R�5�      �� $��20��6%   $��20��60   $ 10��5�      $��0px 2px 0px 2pxq�5�      #��z��5�      ._�5�   "   �� $��Center=�63   $��Center=�5�      5�      5�      5�      5�      �� 5�      -   5         ��Name$ ProductName��5�        	TextBoxes#��[ 6+   $
��Left=�5�      $��
109,48,0,0q�5�      $��1��5�       �� TextWrapping$
 Wrap=�5�      $	��Top=�5�      $	��146��5�       �� 	MaxLength$ 20��5�      �� 5�      $
��Left=�6   $��
109,27,0,0q�5�      $��1��5�      $
��Wrap=�5�      $��ProductName��5�      $	��Top=�5�      �� 5�      -   5        $ Description��5�      #��[ 5�      $
��Left=�5�      $��109,190,0,0q�5�      $��1��6+   $
 Wrap=�5�      $	��Top=�5�      $	��146��5�      $ 20��5�      �� 5�      $
��Left=�6   $��	109,0,0,0q�5�      $��1��6,   $
��Wrap=�5�      $��Description��5�      $��Center=�5�      �� 5�      -   5         ��Name$ ComboCat��5�        ComboBoxStyle#��[ 5�      $
��Left=�5�      $��346,119,0,0q�5�      $��1��5�      $	��Top=�5�      $	��146��5�      $��23��6$   5�      �� 5�      $
��Left=�6   $��
346,99,0,0q�5�      $��1��5�      $
��Wrap=�5�      $��Category��5�      $	��Top=�5�      $��16��60   �� 5�      -   5        $ Brand��5�      #��[ 5�      $
��Left=�5�      $��
555,48,0,0q�5�      $��1��5�      $	��Top=�5�      $	��146��5�      $ 20��5�      5�      �� 5�      $
��Left=�6   $��
555,30,0,0q�5�      $��1��6,   $
��Wrap=�5�      $��Brand��5�      $	��Top=�5�      �� 5�      $
��Left=�6   $��
107,96,0,0q�5�      $��1��5�      $
��Wrap=�5�      $
��Size��5�      $	��Top=�5�      �� 5�      -   5        $
 Insert��5�        SubmitButton#��[ 6%   $��Update��6K   $
��Left=�6\   $��107,278,0,38q�6w   $��1��6�   
�������
������ $
 Hand��5�   S   $	��266��6a   �� 5�      -	   5        $��Clear��5�        ClearButton#��[ 6%   $
��Left=�5�      $��435,278,0,0q�6,   $��1��6A   $	��Top=�5�      $
 Hand��6)   $	��266��67   �� 5�      -
   5        $
 Size��5�      #��[ 5�      $
��Left=�5      $��109,117,0,0q�5     $��1��5     $
 Wrap=�5     $	��Top=�5     $	��146��5     $ 20��5     �� 5     -   5        $ Price��5     #��[ 5     $
��Left=�5     $��555,190,0,0q�5     $��1��5     $
 Wrap=�5     $	��Top=�5     $	��146��5     $ 20��5     �� 5     $
��Left=�6   $��555,169,0,0q�5     $��1��6-   $
��Wrap=�5     $��Price��5     $	��Top=�5     �� 5     -   5        $ Color��5     #��[ 5      $
��Left=�5!     $��555,117,0,0q�5"     $��1��5#     $
 Wrap=�5$     $	��Top=�5%     $	��146��5&     $ 20��5'  ]   �� 5(     $
��Left=�6   $��
555,96,0,0q�63   $��1��6G   $
��Wrap=�6T   $��Color��6h   $	��Top=�6u   5,     5.     50     