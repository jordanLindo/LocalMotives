   M S B A M L   `   `   `  ����O  KWPFPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfPresentationLayerWpfPresentationLayer  5   	   ,    &WpfPresentationLayer.frmUpdateCustomer   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfPresentationLayer     5   	     � Title$!  Update Customer Information��5   	   $	��450��6-   $	��400��6:   .��5	      �  � RowDefinitions 5
   
   �� 5      $��10 �6   6*   �� 5      $��90 �6   6*   �� 5      6   �� 5      6   �� 5      6   �� 5      $��30 �6   6*    � ColumnDefinitions 5   
   �� 5      $	��110 �6   6-   �� 5      $��10 �6   6,   �� 5      6!   �� 5      $��20 �6   6,   .+�5   
   � $��1��6   $��4��6   
�������6E   �� 5   
   -   5         ��Name$ 
lblMessage��5      $��1��6$   $��0��61   $��3��6A   $��Center=�6U   $��Center=�6y   $��25(�6�   $&�� Update Customer
     Information��6�   6�   �� 5   
   -   5        $ lblUpdateFirstName��5      $��2��6,   $��Right=�69   $��Center=�6U   $��15(�6p   $��First Name:��6~   6�   �� 5   
   -   5         ��Name$ txtFirstName��5      $��2��6(   $��2��65   $��5 0 5 0q�6E   $��Center=�6V   $��40��6x   
���   �$��1q�6�   6�   �� 5   
   -   5        $ lblUpdateLastName��5      $��3��6+   $��Right=�68   $��Center=�6T   $��15(�6o   $��
Last Name:��6}   6�   �� 5   
   -   5        $ txtLastName��5      $��3��6'   $��2��64   $��5 0 5 0q�6D   $��Center=�6U   $��40��6w   
���   �$��1q�6�   6�   �� 5   
   -   5         ��Name$ 	btnSubmit��5      $��4��6$   $��2��61   $��50 20q�6A   
���   �$��2q�6d   $��30(�6x   $��Submit��6�   6�   � 5   
   $��5��6   $��4��6   
�������6I   5       5!      