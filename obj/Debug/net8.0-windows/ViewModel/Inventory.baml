   M S B A M L   `   `   `  ����H  DVB_THESIS_WPS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:VB_THESIS_WPSVB_THESIS_WPS  5      g cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137akOclr-namespace:MahApps.Metro.IconPacks;assembly=MahApps.Metro.IconPacks.MaterialMahApps.Metro.IconPacks 68       VB_THESIS_WPS.Inventory   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -localclr-namespace:VB_THESIS_WPS     5      `IconOclr-namespace:MahApps.Metro.IconPacks;assembly=MahApps.Metro.IconPacks.Material     68   $��Roboto)�5      
������ .��5      �   � RowDefinitions  5   
   �� 5      $��40 �6   �� 5      .+�5   
   �� $��0��6   
�������$��0,0,0,0q�5       �� CornerRadius$ 4��5         
dropShadow J� Effect# [  5      5#      � 5&   
    � ColumnDefinitions 5(      �� 5+      .+�54      �� $��Product List��6   $��0��6,    �� TextAlignment$
 left=�55      $��Center=�6&   $��16(�56       �� Padding$ 6q�6   $��Medium"�6+   �� 5:      -   5          	addButton#��[ 5:      $��0,5,4,5q�68    � Cursor$
 Hand��6I   $��0��6s   $��88��6�   .��5;      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�5<      .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$
 Plus=�6,     addButtonIcon#��[ 68   $��0,0,10,0q�6_   �� 5=      $	��Add��6    $��Roboto)�6+   $��Center=�6?   5>      5?      �� 5E      -   5        	 ��Name$	 Search��5E        	TextBoxes#��[ 5F      $��	0,0,198,0q�5G      $��0��6#   
 �� TextWrapping$

 Wrap=�5H      $��Center=�5I       �� 	MaxLength$ 20��5J      $��Right=�5K   1   $	��146��6M   �� 5N      -   5        #��[ 5N      $��0,5,97,5q�68   $
 Hand��6J   $��0��6m   $��94��5O      .��5Q      �� $ 
Horizontal=�6   $��Center=�66   .+�5R        $ Magnify=�6,   #��[ 6;   $��0,0,10,0q�6b   �� 5S      $��Search��6    $��Roboto)�6.   $��Center=�6B   5T      5U      �� 5X      -   5         ��Name$ Print��5X      #��[ 6$   $��	0,5,353,5q�6G   $
 Hand��6Z   $��94��5Y      .��5[      �� $ 
Horizontal=�6   $��Center=�66   .+�5\        $ Printer=�6,   #��[ 6;   $��0,0,10,0q�6b   �� 5]      $��Print��6    $��Roboto)�6-   $��Center=�6A   5^      5_      5f      �� 5h   
   $��1��6   
�������$��0,10,0,0q�5j      $ 4��5k      ._�5m      &   System.Windows.Controls.DataGrid  -   5          Name$ productDataGrid��5m   �     DataGridRowStyle1   RowStyle# �  6     DataGridColumnHeaderStyle1   ColumnHeaderStyle# �  6F     DataGridCellStyle1   	CellStyle# �  6�     DataGridStyle1#���  6�      Columns 5n      0  *System.Windows.Controls.DataGridTextColumn  5p      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6)      
IsReadOnly$
 True��65      CanUserResize$ False��6G      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6j   �� *	ProductID+  5q      $
 Name��6)   $
 True��6W   $ * 6i    67   �� *ProductName+6i     5r      $ Description��6)   $
 True��6^   $ * 6p    6>   �� *Description+6p     5s      $ Category��6)   $
 True��6\   $ * 6n    6;   �� *CategoryName+6n     5t      $ Brand��6)   $
 True��6R   $ * 6d    68   �� *Brand+6d     5u      $
 Size��6)   $
 True��6P   $ * 6b    67   �� *Size+6b     5v      $ Color��6)   $
 True��6R   $ * 6d    68   �� *Color+6d     5w      $ Price��6)   $
 True��6R   $ * 6d    68   �� *Price+6d     5x      $ 	CreatedAt��6)   $
 True��6Z   $ * 6l    6<   �� *	CreatedAt+6l   4  .System.Windows.Controls.DataGridTemplateColumn  5z      $ Action��6-   $
 True��6=   $
 auto 6O      CellTemplate 5{      �� 5|      .R�5}   "   �� $ 
Horizontal=�6-   .+�5~   &   �� -   5          gridEditButton#��[ 5~   -   .��5   *     $ PencilOutline=�6@    	 gridButtonIcon#��[	 6U   5�   '   5�   #   5�      5�      5�      5�      5�      5�      