   M S B A M L   `   `   `  ����O  KWPFPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfPresentationLayerWpfPresentationLayer  5   	   (    "WpfPresentationLayer.frmUpdateSeat   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfPresentationLayer     5   	     � Title$  Update Seat��5   	   $	��450��6   $	��500��6*   .��5	      �  � RowDefinitions 5
   
   �� 5      $��60 �6   6*   �� 5      $��60 �6   6*   �� 5      6   �� 5      $��20 �6   6*   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      $	��190 �6   6-   �� 5      $��10 �6   6,   �� 5      6!   .+�5   
   �� -   5         ��Name$ lblUpdateSeat��5      $��3��6'   
�������$��Center=�6N   $��Center=�6p   $��30(�6�   $��Update seat��6�   6�   �� 5   
   -   5        $ 	lblSeatID��5      $��1��6#   $��3��60   $��30(�6D   $��Center=�6R   $��Center=�6t   6�   �� 5   
   -   5        $ lblCurrentDescription��5      $��2��6/   $��30(�6<   $��Center=�6J   $��Right=�6l   $��   Current
Seat type��6�   6�   �� 5   
   -   5        $ lblDescription��5      $��2��6(   $��2��65   $��30(�6E   $��Center=�6S   $��Center=�6u   6�   �� 5   
   -   5        $ lblNewDescription��5      $��4��6+   $��30(�68   $��Center=�6F   $��Right=�6h   $��       New
Seat type��6�   6�   �� 5   
   -   5         ��Name$ cboDescriptions��5      $��4��6,   $	��200��69   $��40��6E   $��2��6Q   $��Center=�6a   $��Center=�6|   6�   �� 5   
   -   5         ��Name$ 	btnUpdate��5      $��5��6$   $��3��61   $��260 20 80 20q�6E   $��30(�6[   $��Update��6i   6�   �� 5   
   -   5        $ 	btnCancel��5      $��5��6$   $��3��61   $��80 20 260 20q�6E   $��30(�6[   $��Cancel��6i   6�   5      5       