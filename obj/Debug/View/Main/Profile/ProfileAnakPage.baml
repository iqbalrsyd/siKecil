   M S B A M L   `   `   `  ����B  >siKecil, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:siKecil.View.Main.ProfilesiKecil.View.Main.Profile  5      /    )siKecil.View.Main.Profile.ProfileAnakPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:siKecil.View.Main.Profile     5        N� Title$  ProfileAnakPage��5      .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��100��6   �� 5      $	��850 �6   �� 5      $	��100��6    � RowDefinitions 5   
   �� 5      $��75��6   �� 5      $	��355 �6   �� 5      $	��100��6   .+�5   
   � $��1��6   $��1��6   .+�5      �� $��	Nama Anak��6   $��18(�6)   $��Medium"�67   
���   �$
��Left=�6^   $��
5,48,0,264q�6y   �� 5      $��Nama Panggilan Anak��6   $��18(�63   $��Medium"�6A   
���   �$
��Left=�6h   $��4,105,0,207q�6�   �� 5      $��Tanggal Lahir Anak��6   $��18(�62   $��Medium"�6@   
���   �$
��Left=�6g   $��5,165,0,147q�6�   �� 5      $��Jenis Kelamin Anak��6   $��18(�62   $��Medium"�6@   
���   �$
��Left=�6g   $��
3,235,0,77q�6�   �� 5      -   5         ��Name$ txtEditNamaAnak��5         TextBoxVer2#��[  6/   $
��Left=�6T   $��
199,40,0,0q�6o    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��641��6�   $��32��6�   �� 5      -   5        $ txtEditNamaPanggilanAnak��5      #��[  68   $
��Left=�6]   $��199,102,0,0q�6x   $
 Wrap=�6�   $	��Top=�6�   $	��641��6�   $��32��6�   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ DateTanggalLahirAnak��5        DatePickerStyle#��[ 67   $
��Left=�6`   $��Center=�6{   $��32��6�   $	��200��6�   $��	200,0,0,0q�6�   �� 5      -   5         ��Name$ JenisKelaminAnakComboBox��5        ComboBoxTextStyle#��[ 69   
���   �$
��Left=�6w   $��200,228,0,0q�6�   $	��Top=�6�   $	��640��6�   $��32��6�   .@�5      �� $��	Laki-Laki��6   �� 5      $��	Perempuan��6   5       �� 5!      -   5        $��Simpan��5!        CustomButtonStyle#��[ 6F   $
��Left=�6r   $��21(�6�   $��377,292,0,0q�6�   $	��Top=�6�   $��50��6�   $	��131��6�   5"      5#      5$      