   M S B A M L   `   `   `  ����D  @RSAkeygen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:RSAkeygen	RSAkeygen  5   	       RSAkeygen.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:RSAkeygen     5   	     � Title$  RSA Key Generator��5   	   $	��350��6#   $	��525��60   .��5	      � $��0,0,2,-1q�6   .+�5
   
   �� -   5        $��Generate RSA key pair��5
      $
��Left=�61   $	��Top=�6L   $	��129��6d    J� RenderTransformOrigin$ 3.524,3.976&�6p   $��118,114,0,0q�6�   6�   �� 5   
   -   5         ��Name$ ImportButton��5      $#��Load RSA public key from file��6'   $
��Left=�6O   $	��Top=�6j   $	��164��6�   $ 3.524,3.976&�6�   $��118,164,0,0q�6�   6�   �� 5   
   -   5        $��Send RSA public key��5      $
��Left=�6/   $	��Top=�6J   $	��118��6b   $ 3.524,3.976&�6n   $��118,239,0,0q�6�   6�   �� 5   
   -   5         ��Name$ tblock1��5      $
��Left=�6%   $
��Wrap=�6@   $	��Top=�6T   $��
118,64,0,0q�6l   $	��387��6�   $��20��6�   �� 6�   $�No Key Loaded��6�   �� 6�   �� 6�   6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $	��Top=�6C   $��118,139,0,0q�6[   $��20��6p   �� 6}   $�Loaded public key��6�   �� 6�   $�:��6�   6�   �� 5   
   -   5        $ loadedstatusblock��5      $
��Left=�6/   $
��Wrap=�6J   $	��Top=�6^   $��222,139,0,0q�6v   $��20��6�   �� 6�   $	�non��6�   �� 6�   $�e loaded��6�   6�   �� 5   
   -   5        $ EmailButtonLabel��5      $
��Left=�6.   $
��Wrap=�6I   $	��Top=�6]   $��118,189,0,0q�6u   $	��387��6�   $��20��6�   �� 6�   $(�"Click to send key to email address��6�   �� 6�   �� 6�   6�   �� 5   
   -   5         ��Name$ KeySizeField��5      $
��Left=�6(   $��20��6C   $��
118,89,0,0q�6O    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $��66��6�   $ 0.504,3.392&�6�   �� 5   
   -   5        $ 
EmailField��5      $
��Left=�6&   $��20��6A   $��118,214,0,0q�6M   $
 Wrap=�6b   $	��Top=�6v   $	��240��6�   �� 5   
   -	   5        $ KeySizeLabel��5      $
��Left=�6*   $��20��6E   $��	33,89,0,0q�6Q   $
��Wrap=�6d   $��Enter Key Size:��6x   $	��Top=�6�   $��80��6�   �� 5   
   -
   5        $ 
EmailLabel��5      $
��Left=�6(   $��20��6C   $��
47,214,0,0q�6O   $
��Wrap=�6c   $��Enter Email:��6w   $	��Top=�6�   $��66��6�   �� 5   
   -   5        $ BehavButton��5      $��Enable malicious behavior��6&   $
��Left=�6J   $��20��6e   $��
118,39,0,0q�6q   $	��Top=�6�   $	��166��6�   6�   5      5      