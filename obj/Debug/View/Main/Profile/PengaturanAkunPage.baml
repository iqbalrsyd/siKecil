   M S B A M L   `   `   `  ����B  >siKecil, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:siKecil.View.Main.ProfilesiKecil.View.Main.Profile  5      2    ,siKecil.View.Main.Profile.PengaturanAkunPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:siKecil.View.Main.Profile     5        N� Title$  PengaturanAkunPage��5      .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��150��6   �� 5      $	��750 �6   �� 5      $	��150��6    � RowDefinitions 5   
   �� 5      $��80��6   �� 5      $	��390 �6   �� 5      $��60��6   .+�5   
   � $��1��6   $��1��6   .+�5      �� -   5         ��Name$ ProfileImage��5      $��/Asset/user.png��6*   $
��Left=�6C   $	��200��6^   $��60,7,0,0q�6k   $	��Top=�6}   $	��200��6�   $��UniformToFill=�6�    J� Clip 5      @� 5       @� RadiusX$	 100Q�6&    @� RadiusY$	 100Q�64    @� Center$ 100,100&�6B   5      �� 5      -   5         ��Name$ txtUsername��5         TextBoxVer2#��[  6+   $
��Left=�6P   $��
381,73,0,0q�6k   $	��Top=�6   $	��319��6�   $��45��6�   �� 5      -   5        $ txtEditEmailAddress��5      #��[  64   $
��Left=�6Y   $��380,158,0,0q�6t   	 �� TextWrapping$
	 Wrap=�6�   $	��Top=�6�   $	��321��6�   $��48��6�   G� 5      -   5        
 G�Name$
 txtEditPassword��5      $
��Left=�63   $��382,246,0,0q�6N   $	��Top=�6c   $	��319��6{   $��46��6�   �� 5      $��Username��6   $��18(�6(   $��Medium"�66   
���   �$
��Left=�6]   $��84��6x   $��389,45,0,260q�6�   6�   �� 5      $��Alamat Email��6   $��18(�6,   $��Medium"�6:   
���   �$
��Left=�6a   $	��111��6|   $��389,131,0,174q�6�   �� 5       $��Password��6   $��18(�6(   $��Medium"�66   
���   �$
��Left=�6]   $��81��6x   $��389,218,0,87q�6�   �� 5!      -   5        $��Sign Out��5!        CustomButtonStyle#��[ 6>   $
��Left=�6i   $��21(�6�   $��
94,232,0,0q�6�   $	��Top=�6�   $��50��6�   $	��131��6�   �� 5"      -   5        $��Delete Account��5"        CustomButtonStyleDelete#��[ 6J   $
��Left=�6{   $��
74,290,0,0q�6�   $	��Top=�6�   $	��175��6�   $��55��6�   �� 5#      -   5          ImageButtonStyle#��[ 5#   /   $��235,182,490,163q�6Y   $��25��6r   $��25��6~   �� 5$      -   5        $��Simpan��5$      #��[ 6?   $
��Left=�6k   $��21(�6�   $��471,340,0,0q�6�   $	��Top=�6�   $��50��6�   $	��131��6�   �� 5%      -	   5         ��Name$ Overlay��5%      $��2��6&   
������� J� Opacity$	 0.5Q�6L   $y�	Collapsed=�6Z    � 5&      -
   5          �Name$ deleteAccFrame��5&        � NavigationUIVisibility$ Hidden=�6,   $	��350��6l   $	��600��6y    � 5'      -   5        $ OtpFrame��5'      $	��400��6&   $	��250��62   $��170,70,180,70q�6?   $ Hidden=�6V   6v   5(      5)      5*      