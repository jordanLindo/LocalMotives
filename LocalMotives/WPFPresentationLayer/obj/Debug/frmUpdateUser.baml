   M S B A M L   `   `   `  ����O  KWPFPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfPresentationLayerWpfPresentationLayer  5   	   (    "WpfPresentationLayer.frmUpdateUser   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfPresentationLayer     5   	     � Title$  Update User��5   	   $	��500��6   $	��450��6*   .��5	      �  � RowDefinitions 5
   
   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*   �� 5      $��25 �6   6*   �� 5      $	��120 �6   6+   �� 5      $��40 �6   6*   �� 5      $��40 �6   6*    � ColumnDefinitions 5   
   �� 5      6!   �� 5      6!   .+�5   
   � $��2��6   
�������68   �� 5   
   $��Center=�6   $��Right=�6+   $��1��6G   $��0 0 10 0q�6T   .��6f   Employee ID:6t   �� 5   
   $��Center=�6   $��Right=�6+   $��2��6G   $��0 0 10 0q�6T   .��6f   First Name:6s   �� 5   
   $��Center=�6   $��Right=�6+   $��3��6G   $��0 0 10 0q�6T   .��6f   
Last Name:6r   �� 5   
   $��Center=�6   $��Right=�6+   $��4��6G   $��0 0 10 0q�6T   .��6f   Email Address:6v   �� 5    
   $��Center=�6   $��Right=�6+   $��5��6G   $��0 0 10 0q�6T   .��6f   Phone Number:6u   �� 5!   
   -   5         ��Name$ lblEmployeeID��5!      $��Center=�6'   $
��Left=�6B   $��1��6]   $��1��6j   6|   �� 5"   
   -   5        $ lblFirstName��5"      $��Center=�6&   $
��Left=�6A   $��2��6\   $��1��6i   6{   �� 5#   
   -   5        $ lblLastName��5#      $��Center=�6%   $
��Left=�6@   $��3��6[   $��1��6h   6z   �� 5$   
   -   5        $ lblEmail��5$      $��Center=�6"   $
��Left=�6=   $��4��6X   $��1��6e   6w   �� 5%   
   -   5        $ lblPhone��5%      $��Center=�6"   $
��Left=�6=   $��5��6X   $��1��6e   6w   �� 5&   
   $��Center=�6   $��Right=�6+   $��6��6G   $��0 0 10 0q�6T   .��6f   	Active:6o   �� 5'   
   -   5         ��Name$ 	chkActive��5'      $��Center=�6&   $��6��6A   $��1��6N   6^   �� 5)   
   $��7��6   $��10(�6   $
��20 0q�6+   $��Right=�69   .��6V   Unassigned Roles6h   �� 5*   
   $��7��6   $��10(�6   $
��20 0q�6+   $��1��69   $
��Left=�6I   .��6e   Employee Roles6u   �� 5+   
   -   5         ��Name$ lstUnassignedRoles��5+      $��8��6.   $��0��6;   $��5q�6K   $��Right=�6V   $	��200��6r   6~   �� 5,   
   -   5        $ lstAssignedRoles��5,      $��8��6,   $��1��69   $��5q�6I   $
��Left=�6T   $	��200��6o   6{   �� 5-   
   -	   5         ��Name$ 	btnCancel��5-      $��9��6$   $	��100��61   $��5q�6=   $
��Left=�6H   $��1��6d   $��Cancel��6t   6�   5.      5/      