   M S B A M L   `   `   `  ����	  Hotel    Hotel.pages.UserDataBase   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          N� Title$  UserDataBase��5      $	��745��6   $
��1045��6)    N� FontSize$ 18(�5	         MaterialDesignFont N� 
FontFamily# �   6   .��5
      � .+�5   
   �� $��2��6   $��2��6"   
������� �� CornerRadius$ 10��5   $   ._�5      �  � RowDefinitions 5      �� 5      $
��0.3* �6$   �� 5      $
��3.8* �6$    � ColumnDefinitions 5      �� 5      $��1* �6'   �� 5      $��1* �6'   .+�5      A� $	��300��6   $��2��6&   $	��300��5      $��Bottom=�5      ���#e0c1c3$��291,0,254,-184q�5       J� Opacity$	 0.3Q�5      $��3��6,   A� 5      $	��400��6   $��2��6&   $	��400��5      $��Bottom=�5      ���#e0b1c3$��23,0,422,-169q�5       $	 0.6Q�5!      $��2��6,   $��1��6=   A� 5"      $	��250��6   $��2��6&   $	��250��5#      $��Bottom=�5$      ���#e1c1b9$��-102,0,697,-52q�5&      $	 0.5Q�5'      $��2��6,   $��2��6=   A� 5(      $	��400��6   $��2��6&   $	��400��5)      $��Bottom=�5*      ���#e1b9b9$��612,0,-167,-127q�5,      $	 0.8Q�5-      $��2��6,   $��1��6=   A� 5.      $	��250��6   $��2��6&   $	��250��5/      $��Bottom=�50      ���#e1c1b9$��715,0,-120,106q�52      $	 0.5Q�53      $��3��6#   $��1��64   ]� 54      $��0��6   $��2��6)   .+�55      �� $��Пользователи��6    $
��Bold"�56       $��30(�57       ���#846267$��20 0 0 0q�58       $��Center=�59       $
��Left=�5:       �� 5;      -   5         ��Name$ 	ReturnBut��5;       � ToolTip$
 Icon��5<      $��Center=�5=      $��720 10 0 10q�5>      $��0q�5?      
������ $��40��5A      $��40��5B      $	 0.4Q�5C      ��5E      �� 5F      	 �� 
TargetType"	 ��6$   .�5G   "   �� 
 �� Property
 � �� �� Value 6<   0  *MaterialDesignThemes.Wpf.PackIconExtension     Kind$ 	ArrowBack=�5H      5J      �� 5K      -   5        $ CloseBut��5K      $
 Icon��5L      $��Center=�5M      $��Right=�5N      $��0q�5O      
������ $��40��5Q      $��40��6(   $��10q�5R      $	 0.4Q�5S      ��5U      �� 5V      "	 ��6$   .�5W   "   �� 
 � �� 6<     $ Close=�5X      5Z      5[      &   System.Windows.Controls.DataGrid  5\      -   5          Name$ DataUser��5\      $��1��6-   $��1��6=   
������    GridLinesVisibility$ Vertical=�5]         
IsReadOnly .    CanUserResizeColumns .     VerticalGridLinesBrush �	#FF846267$��10q�5_   >      SelectionMode$ Single=�5a         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 125 62   �� 5b      $��0��6   $��1��6-   $��Center=�5c      $��10q�5d      .+�5e      �� -   5         ��Name$ UsernameBox��5e       % !MaterialDesignFloatingHintTextBox#��[ 5f      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Username��5g         FloatingOffset$ 0, -20&�5h      ���#aeb4a9$��10q�5i   3   $��
 0 0 0 1.5q�5j      �� 5k      -   5        $ LoginBox��5k      #��[ 5l      $ Login��5m      $ 0, -20&�5n      ���#aeb4a9$��10q�5p      $��
 0 0 0 1.5q�5q      G� 5r      -   5         G�Name$ 	PasswdBox��5r   "    ) %MaterialDesignFloatingHintPasswordBox#��[ 5s   !   $ Password��5t   !   $ 0, -18&�5u   !   ���#aeb4a9$��
 0 0 0 1.5q�5v   7   $��10q�5w   !   '  !MaterialDesignThemes.Wpf.PackIcon  5x      -   5          Name$ GeneratePwd��5x   .      Kind$
 Sync=�5y   -   $��10q�69   $��30��5z   -   $��30��69   $	 0.4Q�5{   -    � Cursor$
 Hand��6;   5|   -   �� 5}      $��Bottom=�6!   $��Right=�5~   !    �� Orientation$ 
Horizontal=�5   !   .+�5�        -	   5        $ ExpBut��5�   2   $��Right=�5�   2   $��Bottom=�5�   2   $ ExportVariant=�5�   2   $
 Hand��6G   $��35��5�   2   $��35��6>   $	 0.4Q�5�   2     5�      -
   5        $ 	DeleteBut��5�   2   $ Delete=�5�   2   $
��10 0q�6@   $��35��5�   2   $��35��6>   $	 0.4Q�5�   2   $
 Hand��6@   5�   2     5�      -   5        $ DoneBut��5�   2   $
 Done=�5�   -   $
 Hand��69   $��35��5�   -   $��35��69   $	 0.4Q�5�   -   5�   -   5�      5�      5�      5�      5�      5�      