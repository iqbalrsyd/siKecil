   M S B A M L   `   `   `  ����B  >siKecil, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:siKecil.View.MainsiKecil.View.Main  5           siKecil.View.Main.ChatPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:siKecil.View.Main     5        N� Title$  siKecil��5      .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��300 �6   �� 5      $��* �6   $	��685��6)   �� 5      $��65 �6    � RowDefinitions 5   
   �� 5      $	��100 �6   �� 5      $��* �6   $	��430��6'   �� 5      $��40 �6   .+�5   
   �� $��0��6   $��0��6$   
���   �$��0,3,0,0q�6@   ���#FFFFFF$	��0.2��6`   �� 5   
   $��1��6   $��2��6$   $��0,3,0,0q�69   
���   ����#FFFFFF$	��0.2��6h   �� 5   
   -   5         ��Name$ 	UserImage��5      $��/Asset/user.png��6#   $
��Left=�6<   $��50��6W   $��	20,28,0,0q�6c   $	��Top=�6v   $��50��6�   $��UniformToFill=�6�    J� Clip 5      @� 5       @� RadiusX$ 25Q�6"    @� RadiusY$ 25Q�6/    @� Center$ 25,25&�6<   5      �� 5   
   -   5         ��Name$ UserName��5      $��25(�6&   $��Medium"�64   ���#1450A3$��126,40,475,33q�6]   $��1��6t   �� 5!   
   -   5        $ ReceiverImage��5!      $��/Asset/user.png��6'   $
��Left=�6@   $��50��6[   $��50,0,0,0q�6g   $��Center=�6y   $��50��6�   $��UniformToFill=�6�   $��1��6�    5"      @� 5#      $ 25Q�6"   $ 25Q�6/   $ 25,25&�6<   5%      �� 5&   
   -   5        $ ReceiverName��5&      $��25(�6*   $��Medium"�68   ���#1450A3$��11,40,205,33q�6a   �� 5)   
   $��1��6   $��2��6%   $	��685��62   $��Stretch=�6A   $��Stretch=�6_   .+�5*      �� -   5        	 ��Name$	 pesanTextBox��5*         CustomTextBoxStyle#��[  6,   
 �� TextWrapping$

 Wrap=�6X   $��Center=�6l   $��30��6�   ���	#FFEEEEEE$��Stretch=�6�   $��Normal"�6�   $��18(�6�   5+      �� 5-   
   $��3��6   $��2��6"   .+�5.      �� -   5         ��Name$ 
ButtonSend��5.        CustomButtonStyle#��[ 6)   $��Stretch=�6T   $��Kirim��6�   $��64��6�   $��2,2,2,2q�6�   5/      �� 53   
   -   5         ��Name$ kontakItemsControl��53      $��0��6/   $��1��6?   $��3��6L   ���#FFFFFF$��0,0,0,0q�6�   $��Inter)�6�   $��18(�6�   $��Medium"�6�   ��6�   ��  �� View 54      �� 55      .P�56      �� $��Kontak��6%    �� CellTemplate 57      �� 58      .R�59   "   �� ��6,   �� *UserNameKontak+5:      5<      5=      5?      �� 5B   
   -   5         ��Name$ 
ScrollChat��5B      $��1��6+   $��1��6;   $��2��6H   $
��Auto=�6\   
�������$��Inter)�6�   .��5C      �� -	   5         ��Name$ chatItemsControl��5C      $��0,0,0,0q�65   ��5D      �� 5E      .R�5F      ��  ��Name$ chatItemsStackPanel��6%    �� Orientation$ Vertical=�6B   $��3q�6Y   .+�5G      �� $
��Bold"�6D   
����  �$��Bottom=�6�   ��6(   �� *
SenderName+��6h   �� *MessageAlignment+6�   �� 5H      $��5q�6�   $	��400��6�   ��6)   �� *MessageBackground+��6R   �� *MessageAlignment+.+�5I   "   �� $��Inter)�6E   $
��Wrap=�6X   $��5q�6l   ��6,   �� *	Message+6l   �� 5J   "   $��10(�6x   $��0,5,0,0q�6�   ��6,   �� *	Timestamp+��6G   �� *MessageAlignment+6�   5K      5L      5M      5O      5P      5Q      5R      