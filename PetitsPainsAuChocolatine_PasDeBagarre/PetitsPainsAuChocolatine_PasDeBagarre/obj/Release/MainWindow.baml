   M S B A M L   `   `   `  ���� `  \PetitsPainsAuChocolatine_PasDeBagarre, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]3clr-namespace:PetitsPainsAuChocolatine_PasDeBagarre%PetitsPainsAuChocolatine_PasDeBagarre  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 6    0PetitsPainsAuChocolatine_PasDeBagarre.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     Elocal3clr-namespace:PetitsPainsAuChocolatine_PasDeBagarre     6sys&clr-namespace:System;assembly=mscorlib       � Title$  
MainWindow��$	��450��$	��800��.��� .+��� $
��Left=�$	��420��$	��Top=�$	��794��$��
0,0,-0.4,0q�.@��� $��Ordonancement��$��-2,-2,1.2,0q�.��� ���#EEE$��
0,0,-2,0.2q�.+�&   System.Windows.Controls.DataGrid  -   $��CurrentPeopleList��   
IsReadOnly . $
��Left=�$	��308��$��	10,73,0,0q�$	��Top=�$	��770��   AutoGenerateColumns .  ����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Prénom��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	FirstName+  $	 Nom�� �� *
LastName+  $ Dernière livraison�� �� *LastDelivery+  $ 	Livraison�� ��  �� Path$ Delivery� �� StringFormat$ dd MMMM yyyy��   RowStyle �� 	 �� 
TargetType)  #System.Windows.Controls.DataGridRow"	  
 �� Triggers
 ��  �� Binding �� *IsDeliveryPast+ �� Value true.a���  �� Property � �� �� Value �#CCC�� $��Date de démarrage :��$
��Left=�$��	10,24,0,0q�$	��Top=�$��24�� �� Target ��  �� ElementName$ BeginningDate��(  "System.Windows.Controls.DatePicker  -   $��BeginningDate��
 �� Now   SelectedDate# Z $
��Left=�$��
133,25,0,0q�$	��Top=��� $��Liste des concernés��$��1.8,-2,-2.4,0.2q�.��� ���#EEE.+�� -   $��AddUserGrid�����#0070ad$
��Left=�$	��138��$��	10,10,0,0q�$	��Top=�$	��768�� � 	Resources ��%`   '��       '�� &     �� "	 ��.���  � �� �#FFF�� "	 ��.���  � �� �#FFF.+��� $
��Left=�$��	10,10,0,0q�$
��Wrap=�$%��Ajouter un nouvel utilisateur :��$	��Top=�$	��748���� $��
Prénom : ��$
��Left=�$��	10,41,0,0q�$	��Top=� �� $ 	firstName���� -   $��	firstName��$
��Left=�$��23��$��	75,44,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��120���� $��Nom : ��$
��Left=�$��	10,69,0,0q�$	��Top=� �� $ lastName���� -   $��lastName��$
��Left=�$��23��$��	75,72,0,0q�$
 Wrap=�$�� ��$	��Top=�$	��120���� -   $��Ajouter��$
��Left=�$��
226,44,0,0q�$	��Top=�$	��122��$��51�����#FFF�� -   $��people��$
��Left=�$	��218��$��
10,163,0,0q�$	��Top=�$	��768������ ���� "	 ��
 P�  P� Property �@J�IsKeyboardFocusWithin P� Value . . ���  �@��
IsSelected . ���� .R�� $��Right=�.+��� $��UserName��-   $	��300��
������ .����`� $ {0} {1}��.1��� $ 	FirstName��� $ LastName�.���� $��RemoveUserButton��-	   $��Right=�$��10,0,0,0q�$��X�����#FFF
���  ��$
��Bold"�
���  ��$��1q�$��20��