   M S B A M L   `   `   `  ����O  KWPFPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfPresentationLayerWpfPresentationLayer  5   	   )    #WpfPresentationLayer.frmEditStation   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfPresentationLayer     5   	     � Title$  Edit Station��5   	   $	��450��6   $	��500��6+   .��5	      �  � RowDefinitions 5
   
   �� 5      $��50 �6   6*   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      $��10 �6   6,   �� 5      6!   .+�5   
   � $��3��6   
�������68   �� 5   
   -   5         ��Name$ lblTitle��5      $��30(�6"   $��Center=�60   $��Right=�6K   $��Edit Station��6g   6�   �� 5   
   -   5        $ 
lblStation��5      $��30(�6$   $��2��62   $��Center=�6B   $
��Left=�6]   6z   �� 5   
   -   5        $ lblOldStationName��5      $��25(�6+   $��1��69   $��Center=�6F   $��Right=�6a   $��Old Station Name:��6}   6�   �� 5   
   -   5        $ lblName��5      $��25(�6!   $��1��6/   $��2��6<   $��Center=�6L   $
��Left=�6g   6�   �� 5   
   -   5        $ lblNewStationName��5      $��25(�6+   $��2��69   $��Center=�6F   $��Right=�6a   $��New Station Name:��6}   6�   �� 5   
   -   5         ��Name$ txtStationName��5      $��20(�6*   $��2��68   $��2��6E   $��Center=�6U   $��5q�6p   $��10q�6|   6�   �� 5   
   -   5        $ lblOldStationType��5      $��25(�6+   $��3��69   $��Center=�6F   $��Right=�6a   $��Old Station Type:��6}   6�   �� 5    
   -   5        $ lblStationType��5       $��25(�6(   $��3��66   $��2��6C   $��Center=�6S   6p   �� 5!   
   -	   5        $ lblNewStationType��5!      $��25(�6+   $��4��69   $��Center=�6F   $��Right=�6a   $��New Station Type:��6}   6�   �� 5"   
   -
   5         ��Name$ cboStationType��5"      $��20(�6+   $��4��69   $��2��6F   $��Center=�6V   $��Center=�6x   $
��Left=�6�   $��5q�6�   $��10q�6�   6�   �� 5#   
   -   5         ��Name$ 	btnCancel��5#      $��30(�6$   $��DemiBold"�62   $��6��6H   $��Cancel��6U   $��30 10q�6f   6�   �� 5$   
   -   5        $ 	btnSubmit��5$      $��30(�6$   $��DemiBold"�62   $��6��6H   $��2��6U   $��Submit��6e   $��30 10q�6v   6�   5%      5&      