   M S B A M L   `   `   `  ����O  KWPFPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfPresentationLayerWpfPresentationLayer  5   	   (    "WpfPresentationLayer.frmAddStation   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfPresentationLayer     5   	     � Title$  Add Station��5   	   $	��450��6   $	��500��6*   .��5	      �  � RowDefinitions 5
   
   �� 5      $��60 �6   6*   �� 5      6   �� 5      6   �� 5      6    � ColumnDefinitions 5   
   �� 5      6!   �� 5      $��10 �6   6,   �� 5      6!   .+�5   
   �� -   5         ��Name$ lblTitle��5      $��3��6"   
�������6K   �� 5   
   -   5        $ lblStationType��5      $��1��6(   $��Right=�65   $��Center=�6Q   $��30(�6l   $��Station Type:��6z   6�   �� 5   
   -   5         ��Name$ cboStationType��5      $��1��6+   $��2��68   $��Center=�6I   $��Center=�6m   $��Center=�6�   $��10q�6�   $��10q�6�   $��20(�6�   6  �� 5   
   -   5        $ lblStationName��5      $��2��6(   $��Right=�65   $��Center=�6Q   $��30(�6l   $��Station Name:��6z   6�   �� 5   
   -   5         ��Name$ txtStationName��5      $��2��6*   $��2��67   $��Center=�6G   $��Center=�6k   $��10q�6�   $��10q�6�   $��20(�6�   6�   �� 5   
   -   5         ��Name$ 	btnCancel��5      $��3��6$   $��Center=�61   $
��20 5q�6N   $��Center=�6]   $��30(�6x   $��Cancel��6�   6�   �� 5   
   -   5        $ 	btnSubmit��5      $��3��6$   $��2��61   $��Center=�6A   $
��20 5q�6^   $��Center=�6m   $��30(�6�   {�.  $��Submit��6�   6�   5      5      