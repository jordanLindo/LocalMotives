   M S B A M L   `   `   `  ����O  KWPFPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfPresentationLayerWpfPresentationLayer  5   	   %    WpfPresentationLayer.frmAddSeat   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfPresentationLayer     5   	     � Title$  New Seat��5   	   $	��450��6   $	��500��6'   .��5	      �  � RowDefinitions 5
   
   �� 5      $��60 �6   6*   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      $��70 �6   6*    � ColumnDefinitions 5   
   �� 5      6!   �� 5      $��10 �6   6,   �� 5      6!   .+�5   
   �� -   5         ��Name$ lblAddSeatTitle��5      $��3��6)   $��35(�6=   
�������$��Center=�6^   $��Add new seats��6�   6�   �� 5   
   -   5        $ lblTrain��5      $��1��6"   $��Center=�6/   $��Right=�6Q   $��20(�6t   $��Select a Train:��6�   6�   �� 5   
   -   5         ��Name$ cboTrain��5      $��1��6%   $��2��62   $��Center=�6B   $��35��6]   $��10q�6i   6�   �� 5   
   -   5        $ lblCar��5      $��2��6    $��Center=�6-   $��Right=�6O   $��20(�6r   $��Select a Train Car:��6�   6�   �� 5   
   -   5        $ cboCar��5      $��2��6#   $��2��60   $��Center=�6@   {�.  $��35��6m   $��10q�6y   6�   �� 5   
   $��3��6   $��Center=�6   $��Right=�6?   $��20(�6b   $��Select Seat Type:��6p   6�   �� 5   
   -   5        $ cboSeatType��5      $��3��6(   $��2��65   $��Center=�6E   {�.  $��35��6r   $��10q�6~   6�   �� 5   
   $��4��6   $��Center=�6   $��Right=�6?   $��20(�6b   $��Count:��6p   6�   �� 5   
   -   5        $ cboCount��5      $��4��6%   $��2��62   $
��Left=�6B   $��Center=�6]    �� SelectedIndex$ 0��6x   $��10q�6�   $��70��6�   $��35��6�   6�   �� 5    
   -   5         ��Name$ 	btnCancel��5       $��5��6$   $��30(�61   $��DemiBold"�6?   $��Center=�6U   $
��40 0q�6r   $��10q�6�   
���   �$��1q�6�   $��Cancel��6�   6�   �� 5!   
   -	   5        $ 	btnCreate��5!      $��5��6$   $��2��61   $��30(�6A   $��DemiBold"�6O   {�.  $��Center=�6w   $
��40 0q�6�   $��10q�6�   
���   �$��1q�6�   $��Create��6�   6  5"      5#      