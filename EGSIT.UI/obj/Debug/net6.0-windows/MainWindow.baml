   M S B A M L   `   `   `  ����C  ?EGSIT.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:EGSIT.UIEGSIT.UI  5   	   5clr-namespace:EGSIT.UI.ControlsEGSIT.UI.Controls  5   	       EGSIT.UI.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:EGSIT.UI     5   	   4Controlsclr-namespace:EGSIT.UI.Controls     5   	   -   5          � Title$  
MainWindow��5
   	   $	��960��6   $
��1280��6)   ���	#BFFFFFFF.��5      �  � RowDefinitions 5      �� 5      $
��Auto �6   6$   �� 5      $��* �6   6    �� 5      $��30 �6   6!    � ColumnDefinitions 5      �� 5      $��0.75* �6   6&   .+�5      � -   5         �Name$ 
TopToolBar��5   	   $��0��6   $��1��6*   $��1��6:   $
��Auto��6N    5      �� 5      $
��Auto �6   6&   �� 5      $��* �6   6#   �� 5      $
��Auto �6   6&    5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5       �� -   5         ��Name$ OpenFolderButton��5       $��5,0,5,0q�6'   $	��5 0q�68   $��Select folder��6F   $��20��6}   $��Center=�6�   �� 5!      -   5         ��Name$ BrowsePathTextBlock��5!      $��1��6*   $��Center=�6:   $	��5 0q�6U   
������� � 
InputScope$ FullFilePath��6v   $��0,0,0,0q�6�   $��20��6�   .��6�   C:\6�   �� 5"      -   5        $ 
ScanButton��5"      $��5q�6!   $	��5 0q�6,   $
��Scan��6:   $��2��6J   $��20��6s   � 5#      $
��Left=�6   $
��Auto��6*   $��5,0,0,0q�68    � Orientation$ 
Horizontal=�6I   $��1��6b   $��99��6o   $��Center=�6�   .+�5$      �� $y�	Collapsed=�6   $��0,0,10,0q�6=   $!��Scan subfolders recursively��6O   $
��Left=�6u   $��Center=�6�    d� 	IsChecked$
 TrueY�6�   �� 5%      $y�	Collapsed=�6   $'��!Use Windows Search (experimental)��6=   $
��Left=�6j   $��Center=�6�   5&      5'      � 5)      -   5        $ ScanSourceList��5)   	   $��1��6"   $��0��6/   $��Stretch=�6?   !   EGSIT.UI.Controls.EntryList  5*      -   5        	  Name$	 EntryListControl��5*      $��1��62   $��1��6?   $��Stretch=�6P   $��5q�6l   �� 5+      $��Stretch=�6   $��2��6*   $��99��67   $
��Auto��6L   $��5,0,5,0q�6p   ��6Z   Z� .@�5,      �� -   5        
 ��Name$
 ScanProgressBarItem��5,      $y�	Collapsed=�61   $��0,0,0,0q�6_   .��5-      � .+�5.      � $	��200��6   $��Stretch=�6   $��Stretch=�6B   $��20��6g    � IsIndeterminate$
 True��6s    � Value$ 50Q�6�   
�������6�   �� 5/      $��Scanning folder for games��6   $��Center=�62   $	��5 0q�6M   50      51      �� 52      $��Right=�6   .��53      �� -	   5        $ ImportButton��53      $	��5 0q�6#   $��Import selected��61   $��24��6f   $��Right=�6r   $
��Auto��6�   ���	#FF96FF9F$
��Bold"�6�   55      56      58      59      