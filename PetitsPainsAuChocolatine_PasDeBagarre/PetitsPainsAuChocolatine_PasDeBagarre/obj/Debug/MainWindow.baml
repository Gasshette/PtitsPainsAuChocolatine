   M S B A M L   `   `   `  ����`  \PetitsPainsAuChocolatine_PasDeBagarre, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:PetitsPainsAuChocolatine_PasDeBagarre%PetitsPainsAuChocolatine_PasDeBagarre  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   6    0PetitsPainsAuChocolatine_PasDeBagarre.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Elocal3clr-namespace:PetitsPainsAuChocolatine_PasDeBagarre     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$   Ptits pains au chocolatine��5	   	   $	��450��6,   $	��800��69    � 
ResizeMode$ NoResize=�6E   .��5
      � .+�5   
   �� $
��Left=�6   $	��420��60   $	��Top=�6=   $	��794��6U   $��
0,0,-0.4,0q�6a   ���#EEE.@�5      �� $��Ordonancement��6   $��-2,-2,1.2,0q�6-   .��5      � $��
0,0,0,-3.8q�6   $	��788��6+   .+�5      �� $&�� Assets/Images/Capgemini_Logo.png��6   $	��100��6F   $	��100��6R   $��Right=�6_   $	��Top=�6{   $��0,0,13,0q�6�   &   System.Windows.Controls.DataGrid  5      -   5        $��CurrentPeopleList��5       J� 	AllowDrop .    AutoGenerateColumns .     
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5      $��Stretch=�5      $��Stretch=�67      HorizontalGridLinesBrush �#0070ad   VerticalGridLinesBrush �#0070ad$��Bottom=�5   :   $	��267��5      $
��auto��6'   $��
10,0,10,10q�64   ���#FFF$��0q�6Z   ��5      ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Prénom��61   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6A   �� *	FirstName+  5      $		 Nom��61   
 6>   �� *
LastName+  5      $	 Dernière livraison��61   
 6M   �� *LastDelivery+  5      $	 	Livraison��61   
 6D   ��  �� Path$ Delivery� �� StringFormat$ dd MMMM yyyy��   RowStyle 5      �� 5        �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6$    �� Triggers 5!   "   �� 5"   &    �� Binding 62   �� *IsDeliveryPast+ �� Value true6U   .a�5#   *   ��  �� Property � �� �� Value
 ���� 6G   �� 5$   *    � ��$	 300��6C   5%   '   5'      5.      �� 5/      $��Date de démarrage :��6I   $
��Left=�6g   $��	12,41,0,0q�6�   $	��Top=�6�   $��24��6�    �� Target 6   ��  �� ElementName$ BeginningDate��6�   (  "System.Windows.Controls.DatePicker  50      -   5        $��BeginningDate��50   w   
 �� Now   SelectedDate# Z 6�   $
��Left=�6�   $��
135,42,0,0q�6�   $	��Top=�6�   51      52      �� 54      $��Liste des concernés��6   $��1.8,-2,-2.4,0.2q�63   $
��auto��6L   $
��auto��6Y   .��55      � $
��auto��6   $
��auto��6%   $��
0,0,0,-0.8q�62   .+�56      �� $&�� Assets/Images/Capgemini_Logo.png��6   $	��100��6F   $	��100��6R   $��Right=�6_   $	��Top=�6{   $��0,0,13,0q�6�   � 57      -   5        $��AddUserGrid��57      
������ $
��Left=�6G   $	��109��6b   $��10,2,0,0q�6o   $	��Top=�6�   $	��765��6�   .+�5@      �� $
��Left=�6$   $��	10,10,0,0q�6?   $
��Wrap=�6R   $%��Ajouter un nouvel utilisateur :��6f   $	��Top=�6�   $	��636��6�   �� 5A      $��
Prénom : ��6I   $
��Left=�6]   $��	10,45,0,0q�6x   $	��Top=�6�    6    �� $ 	firstName��6�   �� 5B      -   5        $��	firstName��5B   "   $
��Left=�63   $��23��6N   $��	67,48,0,0q�6Z    �� TextWrapping$
 Wrap=�6m   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5C      $��Nom : ��6H   $
��Left=�6Y   $��	10,73,0,0q�6t   $	��Top=�6�    6    �� $ lastName��6�   �� 5D      -   5        $��lastName��5D   "   $
��Left=�62   $��23��6M   $��	67,76,0,0q�6Y   $
 Wrap=�6l   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5E      -   5        $��Ajouter��5E   !   $
��Left=�63   $��
218,48,0,0q�6N   $	��Top=�6b   $	��122��6z   $��51��6�   ���#0070ad���#FFF$
��Bold"�6�   5F      �� 5H      -   5        $��people��5I      $
��Left=�5H      $	��255��69   $��10,156,0,-19q�6F   $	��Top=�6\   $	��768��6t   $��0q�6�   ��5J      �� ��5K      �� 5L      " ��6$    5M   "   P� 5N   &    P� Property �@J�IsKeyboardFocusWithin P� Value . . �5O   *   ��  �@��
IsSelected . 6G   5P   '   5R      ��5T      �� 5U      .R�5V   "   � $��Right=�6'   .+�5W   &   �� $��UserName��6.   -   5        $	��300��5W   >   
������ .��6�   ��5X   *   `� 5Y   .   $ {0} {1}��6;   .1�5Z   2   �� $ 	FirstName�6:   �� 5[   2   $ LastName�6:   5\   /   .��5]   8   5^   '   �� 5_   &   $��RemoveUserButton��6-   -	   5        $��Right=�5_   d   $��10,0,0,0q�6�   $��X��6�   ���#FFF
���  ��$
��Bold"�6�   
���  ��$��1q�6�   $��20��6�   5`   #   5a      5c      �� 5d      -
   5        $��GlobalMessageTextBlock��5f      $
��Left=�5g      $��
10,116,0,0q�64   $
 Wrap=�6H   $	��Top=�6\   $	��768��6t   $��32��6�   
������ $��0q�5h   2   |�.  6F   5j      5k      5l      5m      5n      