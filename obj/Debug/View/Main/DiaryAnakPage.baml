   M S B A M L   `   `   `  ����B  >siKecil, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:siKecilsiKecil  5      %    siKecil.View.Main.DiaryAnakPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      'localclr-namespace:siKecil     5        N� Title$  DiaryAnakPage��5      .��5
   
   �  � ColumnDefinitions 5      �� 5      $	��525 �6#   �� 5      $��* �6#   $	��525��6-    � RowDefinitions 5   
   �� 5      $	��330 �6   �� 5      $��* �6   $	��250��6'   .+�5   
   �� $��0��6   $��0��6$      RectangleButtonStyle#��[  61   $	��Top=�6_   $��10q�6w   �� 5      $��1��6   $��0��6(   #��[  65   $	��Top=�6c   $
��Left=�6{   $��10q�6�   �� 5      $��0��6   $��0��6%   $��40 20 35 240q�62   .+�5      ��  K� ZIndex$ 1��6   $��50��6.   $��50��69    �� RadiusX$ 5��6E    �� RadiusY$ 5��6Q   ���#82C3EC J� Opacity$	 0.7Q�6l   $
��Left=�6z   $	��Top=�6�   6�   �� 5      $ 2��6    $��60 0 0 0q�62   $��20(�6D    �� TextAlignment$
 Left=�6R   $��SemiBold"�6g   $
��Wrap=�6}   $	��386��6�   �� 5      $%�Yuk, catat dan cek perkembangan��6    �� 5      .F�5      �� -   5         ��Name$ NamaAnakText��5   $   $
��Bold"�6:   ���#FFC436$
��Wrap=�6a   5       �� 5      $�	 di umur ��6    �� 5      .F�5      �� -   5        $ 
AgeTextBox��5   $   $
��Bold"�68   ���#FFC436$
��Wrap=�6_   5       �� 5       $� bulan!��6   5!      �� 5"      -   5        	 ��Name$	 BoyIcon��5"   '   $ 3��6   
 �� Left$
 7��68   
 �� Top$ 7��6H   ._�5#      �� $��/Asset/boy.png��6     ChildIconStyle#��[ 60   6Z   5$      5%      �� 5'      $��0��6   $��0��6)   $��40 80 35 110q�66    �� Orientation$ Vertical=�6L   $
��Left=�6c   .+�5(      �� $��Tanggal��6   $��18(�6+   $��Medium"�69   
���   �$
��Left=�6`   6}   (  "System.Windows.Controls.DatePicker  5)      -   5          Name$ 
datePicker��5)        DatePickerStyle#��[ 61   $��0 5 0 0q�6Z   6n   �� 5*      $��Tinggi badan (cm)��6   $��18(�65   $��Medium"�6C   
���   �$
��Left=�6j   $��0 15 0 0q�6�   6�   �� 5+      -   5         ��Name$ 	txtTinggi��5+        TextBoxVer2#��[ 6-   $��0 5 0 0q�6R   6e   5,      �� 5-      $��0��6   $��0��6)   $��285 80 35 110q�66   $ Vertical=�6M   $
��Left=�6d   .+�5.      �� $��Berat badan (kg)��6   $��18(�64   $��Medium"�6B   
���   �$
��Left=�6i   6�   �� 5/      -   5        $ txtBerat��5/      #��[ 6,   $��0 5 0 0q�6Q   6d   �� 50      $��Lingkar kepala (cm)��6   $��18(�67   $��Medium"�6E   
���   �$
��Left=�6l   $��0 15 0 0q�6�   6�   �� 51      -   5        $ txtLingkarKepala��51      #��[ 64   $��0 5 0 0q�6Y   6l   52      �� 53      $��0��6   $��0��6)   $��200 230 35 35q�66   $ 
Horizontal=�6M   $��Right=�6f   $	��Top=�6�   .+�54      �� -   5         ��Name$ 
SimpanData��54      $��Simpan��6-     CustomButtonStyle#��[ 6>   $��90��6i   $��35��6t   6�   �� 55      -	   5        $ 
UpdateData��55      $y�	Collapsed=�6-   $��Update��6D   #��[ 6U   $��90��6�   $��35��6�   6�   �� 56      -
   5        $��
Cek Status��56      #��[ 6.   $��90��6Y   $��35��6d   6�   57      �� 58      $��1��6   $��0��6)   $��30 20 30 270q�66   $ 
Horizontal=�6L   .+�59      �� $6��0Jangan lupa catat riwayat kesehatannya juga yaa!��6   $��0 0 0 0q�6T   $��20(�6e   $ Center=�6s   $��SemiBold"�6�   6�   5:      �� 5;      $��1��6   $��0��6)   $��40 55 35 20q�66   $ Vertical=�6K   $
��Left=�6b   .+�5<      �� $��Riwayat alergi��6   $��18(�62   $��Medium"�6@   
���   �$
��Left=�6g   6�   �� 5=      -   5        $ txtRiwayatAlergi��5=      #��[ 64   $��0 5 0 0q�6Y   $	��430��6j   $��40��6v   6�   �� 5>      $��Riwayat penyakit��6   $��18(�64   $��Medium"�6B   
���   �$
��Left=�6i   $��0 5 0 0q�6�   6�   �� 5?      -   5        $ txtRiwayatPenyakit��5?      #��[ 66   $��0 5 0 0q�6[   $	��430��6l   $��40��6x   6�   �� 5@      $��Golongan darah��6   $��18(�62   $��Medium"�6@   
���   �$
��Left=�6g   $��0 5 0 0q�6�   6�   �� 5A      -   5         ��Name$ GolonganDarahComboBox��5A        ComboBoxTextStyle#��[ 66   $��0 5 0 0q�6a   .@�5B      �� $��A��6   �� 5C      $��B��6   �� 5D      $��O��6   �� 5E      $��AB��6   5F      �� 5G      -   5        $ SimpanKesehatanAnak��5G      $��Simpan��62   $y�	Collapsed=�6C   #��[ 6Z   $��90��6�   $��35��6�   $��Right=�6�   $��0 15 0 0q�6�   6�   �� 5H      -   5        $ EditKesehatanAnak��5H      $
��Edit��60   #��[ 6?   $��90��6j   $��35��6u   $��Right=�6�   $��0 15 0 0q�6�   6�   5I      &   System.Windows.Controls.DataGrid  5K   
   -   5        $��dataGrid��5K      $��0��6#   $��2��63   $��1��6G   $
��Left=�6T     DataGridRowStyle   RowStyle# [ 6o     DataGridColumnHeaderStyle   ColumnHeaderStyle# [ 6�     DataGridCellStyle   	CellStyle# [ 6�    	 DataGridStyle#��[	 6
  $	��Top=�61  $��5q�6I     AutoGenerateColumns .     Columns 5L      0  *System.Windows.Controls.DataGridTextColumn  5M      ,  &System.Windows.Controls.DataGridColumn   Header$ No��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$ 45 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *No+6L     5N      $ Tanggal��6)   $	 180 6p    6:   �� *	Tanggal+ �� StringFormat$ d MMMM yyyy��6p     5O      $ Berat��6)   $ 90 6R    68   �� *Berat+6R     5P      $ Tinggi��6)   $ 90 6T    69   �� *Tinggi+6T     5Q      $ Lingkar Kepala��6%   $	 160 6_    6=   �� *LingkarKepala+6_     5R      $ Status Gizi��6%   $	 200 6\    6:   �� *[Status Gizi]+6\     5S      $ 	Status LK��6)   $	 190 6\    6<   �� *[Status LK]+6\   4  .System.Windows.Controls.DataGridTemplateColumn  5T         HeaderTemplate 5U      �� 5V      .R�5W   "   �� $�� ��6,   5X         CellTemplate 5Z      �� 5[      .R�5\   "   �� $ 
Horizontal=�6-   .+�5]   "   �� $ 
editButton��6)   -   5         
 gridEditButton#��[
 5]   V   .��5^   &   ��  ��Name$ EditIcon��6,   $��/Asset/edit.png��6>   $��18��6W   $��18��6b   $��Center=�6n   $��Center=�6�   ��5_   *   �� 5`   .    �� 
TargetType" ��64   .�5a   2   ��  �� Property �@J�Opacity �� Value$ 1Q�6L     �� Triggers  5b   2   P� 5c   6   ! P� Property! �@J�IsMouseOver" P� Value" . . �5d   :   ��  �@J�Opacity$ 1Q�6T   5e   7   5g   /   5i   '   5j   #   �� 5k   "   $ deleteButton��6)   -   5          gridRemoveButton#��[ 5k   Z   .��5l   &   �� $ 
RemoveIcon��6,   $��/Asset/remove.png��6@   $��18��6[   $��18��6f   $��Center=�6r   $��Center=�6�   ��5m   *   �� 5n   .   " ��64   .�5o   2   ��  �@J�Opacity$ 1Q�6L     5p   2   P� 5q   6   ! �@J�IsMouseOver" . . �5r   :   ��  �@J�Opacity$ 1Q�6T   5s   7   5u   /   5w   '   5x   #   5y   #   5z      5|      5~      5      5�      