   M S B A M L   `   `   `  ����O  KWPFPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfPresentationLayerWpfPresentationLayer  5   	   '    !WpfPresentationLayer.frmEditTrain   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfPresentationLayer     5   	     � Title$  
Edit Train��5   	   $	��450��6   $	��500��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      $��50 �6   6*   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      $��10 �6   6,   �� 5      6!   .+�5   
   � $��3��6   
�������68   �� 5   
   -   5         ��Name$ lblTitle��5      $��30(�6"   $��Center=�60   $��Right=�6K   $��
Edit Train��6g   6~   �� 5   
   -   5        $ lblTrain��5      $��30(�6"   $��2��60   $��Center=�6@   $
��Left=�6[   6x   �� 5   
   -   5        $ lblOldTrainName��5      $��25(�6)   $��2��67   $��Center=�6D   $��Right=�6_   $��Old Train Name:��6{   6�   �� 5   
   -   5        $ lblName��5      $��25(�6!   $��2��6/   $��2��6<   $��Center=�6L   $
��Left=�6g   6�   �� 5   
   -   5        $ lblNewTrainName��5      $��25(�6)   $��3��67   $��Center=�6D   $��Right=�6_   $��New Train Name:��6{   6�   �� 5   
   -   5         ��Name$ txtTrainName��5      $��20(�6(   $��3��66   $��2��6C   $��Center=�6S   $��5q�6n   $��10q�6z   6�   �� 5   
   -   5        $ 	lblActive��5      $��4��6#   $��25(�60   $��Center=�6>   $��Right=�6Y   $��Active:��6u   6�   �� 5    
   -   5         ��Name$ cbActive��5       $��4��6%   $��2��62   $
��Left=�6B   $��Center=�6]    d� 	IsChecked$
 TrueY�6x   6�   �� 5!   
   -	   5         ��Name$ 	btnCancel��5!      $��30(�6$   $��DemiBold"�62   $��6��6H   $��Cancel��6U   $��30 10q�6f   6�   �� 5"   
   -
   5        $ 	btnSubmit��5"      $��30(�6$   $��DemiBold"�62   $��6��6H   $��2��6U   $��Submit��6e   $��30 10q�6v   6�   5$      5%      