   M S B A M L   `   `   `  ����O  KWPFPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfPresentationLayerWpfPresentationLayer  5   	   ,    &WpfPresentationLayer.frmUpdatePassword   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfPresentationLayer     5   	     � Title$  Update Password��5   	   $	��450��6!   $	��400��6.   .��5	      �  � RowDefinitions 5
   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   �� -   5         ��Name$ lblOldPassword��5      $��Current Password:��6(   $��Center=�6D   $��Right=�6_   $��18(�6{   6�   G� 5   
   -   5         G�Name$ pwdOld��5      $��Center=�6&   $��24(�6A   $��1��6O   
���   �$��1q�6s   $
��15 0q�6�   6�   �� 5   
   -   5        $ lblNewPassword��5      $��1��6(   $��New Password:��65   $��Center=�6M   $��Right=�6h   $��18(�6�   6�   G� 5   
   -   5        $ pwdNew��5      $��Center=�6&   $��24(�6A   $��1��6O   $��1��6\   
���   �$��1q�6�   $
��15 0q�6�   6�   �� 5   
   -   5        $ lblRetypePassword��5      $��2��6+   $��Retype Password:��68   $��Center=�6S   $��Right=�6n   $��18(�6�   6�   G� 5   
   -   5        $ 	pwdRetype��5      $��Center=�6)   $��24(�6D   $��2��6R   $��1��6_   
���   �$��1q�6�   $
��15 0q�6�   6�   �� 5   
   -   5         ��Name$ 	btnCancel��5      $��3��6$   $��20 30q�61   $��Cancel��6@   $��24(�6Q   $��DemiBold"�6_   6�   �� 5   
   -   5        $ 	btnSubmit��5      $��3��6$   $��1��61   $��20 30q�6A   $��Submit��6P   $��24(�6a   $��DemiBold"�6o   6�   5      5      