   M S B A M L   `   `   `  ����H  DMicro_Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Micro_ProjectMicro_Project  5   	       Micro_Project.Info   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Micro_Project     5   	     � Title$
  Info��5   	   $	��450��6   $	��800��6#   .��5	      � ���	#FF262626 � RowDefinitions 5
   
   �� 5      $
��172* �6   �� 5      $
��247* �6   .+�5   
   �� $*��$Информация об отеле��6   $
��Left=�6.   $��	10,10,0,0q�6I   $	��Top=�6\   $��Century Gothic)�6t   $
��Bold"�6�   
�������$��18(�6�   �� 5   
   -   5        $]��WКол-во зарегистрированых номеров по категориям��5      $
��Left=�6J   $��	16,45,0,0q�6e   $	��Top=�6x   $	��598��6�   $��24��6�   
�������
�������$
��Bold"�6�   ��6�   Z� 6�   �� 5   
   -   5         ��Name$ 
CBCategory��5      $
��Left=�6'   $��
619,45,0,0q�6B   $	��Top=�6V   $	��157��6n   
���   �
�������
���   �6�   �� 5   
   -   5        $T��NВывод регистрации пользователей и номеров��5      $
��Left=�6E   $��	16,74,0,0q�6`   $	��Top=�6s   $	��598��6�   $��24��6�   
�������
�������$
��Bold"�6�   ��6�   Z� 6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dateTimePickerStart��5      $
��Left=�62   $��
40,109,0,0q�6M   $	��Top=�6a   $	��162��6y     5   
   -   5        $ dateTimePickerEnd��5      $
��Left=�60   $��452,109,0,0q�6K   $	��Top=�6`   $	��162��6x   �� 5   
   $��C��6   $
��Left=�6   $��
16,108,0,0q�67   $	��Top=�6K   $��Century Gothic)�6c   $
��Bold"�6   
�������6�   �� 5   
   $
��До��6   $
��Left=�6   $��411,109,0,0q�68   $	��Top=�6M   $��Century Gothic)�6e   $
��Bold"�6�   
�������6�   �� 5   
   -   5        $R��LИнформация о регистрации и оплате по дате��5      $
��Left=�6E   $��
16,144,0,0q�6`   $	��Top=�6t   $	��598��6�   $��24��6�   
�������
�������$
��Bold"�6�   ��6�   Z� 6�   �� 5   
   -   5        $h��bИнформация о регистрации и оплате по дате и категории��5      $
��Left=�6Q   $��16,2.667,0,0q�6l   $	��Top=�6�   $	��598��6�   $��24��6�   
�������
�������$
��Bold"�6�   $��1��6  ��6�   Z� 6  �� 5   
   -   5        $ CBCategory2��5      $
��Left=�6(   $��619,2.667,0,0q�6C   $	��Top=�6Z   $	��157��6r   
���   �
�������
���   �$��1��6�   &   System.Windows.Controls.DataGrid  5   
   -	   5          Name$ dataGridView��5      $
��Left=�6)   $	��206��6D   $��0,40.667,0,0q�6Q   $	��Top=�6g   $	��792��6   $��1��6�   5      5      