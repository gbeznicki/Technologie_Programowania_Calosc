   M S B A M L   `   `   `  ����C  ?Zadanie4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Zadanie4.ViewZadanie4.View  5   	   7 clr-namespace:Zadanie4.ViewModelZadanie4.ViewModel  5   	       Zadanie4.View.Adder   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Zadanie4.View     5   	   6	viewModel clr-namespace:Zadanie4.ViewModel     5   	     � Title$  Adder��5	   	   $	��250��6   $	��500��6$    � 	Resources 5
      &    Zadanie4.ViewModel.MainViewModel   UserViewModel��%   &	          5   
   6"   5      .��5      �  � DataContext# [  6   .+�5   
   �� $��ID Produktu:��6   $
��Left=�6'   $	��Top=�6B   $��	10,19,0,0q�6Z   $	��136��6m   �� 5   
   $��Imię i Nazwisko:��6   $
��Left=�6+   $	��Top=�6F   $��	10,48,0,0q�6^   �� 5   
   $��Adres e-mail:��6   $
��Left=�6(   $	��Top=�6C    J� RenderTransformOrigin$ 1.174,6.955&�6[   $��	10,97,0,0q�6   �� 5   
   $��Ocena:��6   $
��Left=�6!   $	��Top=�6<   $ 1.626,4.462&�6T   $��
10,123,0,0q�6x   �� 5   
   $��
Komentarz:��6   $
��Left=�6%   $	��Top=�6@   $��
10,149,0,0q�6X   $ 0.685,-2.59&�6l   �� 5   
   $
��Left=�6   $��23��6-   $��
146,22,0,0q�69    �� TextWrapping$
 Wrap=�6M   $	��Top=�6�   $	��328��6�   ��6a   �� *NewReview.ProductID+6�   �� 5   
   $
��Left=�6   $��23��6-   $��
146,48,0,0q�69   $
 Wrap=�6M   $	��Top=�6�   $	��328��6�   ��6a   �� *NewReview.ReviewerName+6�   �� 5   
   $
��Left=�6   $��23��6-   $��146,101,0,0q�69   $
 Wrap=�6N   $	��Top=�6�   $	��328��6�   ��6b   �� *NewReview.EmailAddress+6�   �� 5   
   $
��Left=�6   $��23��6-   $��146,129,0,0q�69   $
 Wrap=�6N   $	��Top=�6�   $	��328��6�   ��6b   �� *NewReview.Rating+6�   �� 5   
   $
��Left=�6   $��23��6-   $��146,157,0,0q�69   $
 Wrap=�6N   $	��Top=�6�   $	��328��6�   ��6b   �� *NewReview.Comments+6�   �� 5   
   $��Zapisz��6   $
��Left=�6"   $��20��6=   $��391,189,0,0q�6I   $	��Top=�6^   $��91��6v   ��5      �� *SaveProductReviewCommand+5      5      