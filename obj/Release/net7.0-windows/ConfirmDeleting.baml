   M S B A M L   `   `   `  ���� G  CGymSysyemWpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:GymSysyemWpfGymSysyemWpf  "    GymSysyemWpf.ConfirmDeleting   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:GymSysyemWpf     -     � Title$  Login��$	��350��$	��400�� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�
������  � AllowsTransparency$
 True��.����  �� CornerRadius$ 12������  �� ImageSource$ /images/backgym.png�� j� Stretch$
 None=�._��� $ 10��$��2q� J� Opacity$
 0.85Q����� 	 �� 
StartPoint$		 0,0&�
 �� EndPoint$	
 1,1&�.���  � Color$ #462AD8�� � Offset$ 0Q�� $ #DA34AE��$
 0.75Q�� $ #8A16C1��$ 1Q����� $		 0,1&�$	
 1,0&�.��� $ #060531��$ 0Q�� $ #1B1448��$ 1Q�._��  � RowDefinitions �� $��30 ��� .+�� $��0�� � ColumnDefinitions �� �� $��25 ��� $��25 ��� $��5 �.+��� -    ��Name$ btnMinimize��$��0q�$��-��
�������$��16(�$��
Montserrat)� � Cursor$
 Hand��$��1������  �� 
TargetType" ��.���  �� Property � �� �� Value �#28AEED �� Triggers P�  P� Property �@J�IsMouseOver P� Value$
 True��. ���  � �� �#278BEF����  �� 
TargetType" ��.R��� $��18��$��18��$ 9��#��z��._��� $��Center=�$��Center=��� -   $ btnClose��$��0q�$��X��
�������$��12(�$��
Montserrat)�$
 Hand��$��2��$��30��$	��Top=����� " ��.���  � �� �#DA34AE P�  �@J�IsMouseOver$
 True��. ���  � �� �#C62DAE���� " ��.R��� $��18��$��18��$ 9��#��z��._��� $��Center=�$��Center=��� $	��350��$��1�� �� Orientation$ Vertical=�$��0,0,0,0q�.+��� $ 
Horizontal=�$��Center=�.+��� $��/gymLog.png��$��70��$��40���� $	��Gym��
�������$��20(�$��Medium"�$��
Montserrat)�$��Center=�$��0,10,0,0q��� $��Center=�$��Center=�$��0,50,0,0q�.+��� $��الرقم السري��
�������$��20(�$��Medium"�$��
Montserrat)�$��Right=�$��0,0,10,5q�G� -    G�Name$ textFirstpass��$��15(�$��Medium"�$��
Montserrat)�
���   �
�������$��0,0,0,2q�$��30��$��Right=�$��0,5,0,0q�$	��250��$��10,4,0,0q�
�������$��RightToLeft=��� -   $ btnLogin��$��0q�$��
ارسال��
�������$��20(�$��
Montserrat)�$
 Hand��$��0,20,0,0q����� " ��.���  � �� �#462AD8 P�  �@J�IsMouseOver$
 True��. ���  � �� �#28AEED���� " ��.R��� $	��150��$��40��$ 20��#��z��._��� $��Center=�$��Center=�