   M S B A M L   `   `   `  ���� J  FWPFCommandPanel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WPFCommandPanelWPFCommandPanel  Q-clr-namespace:WPFCommandPanel.StyleableWindowWPFCommandPanel.StyleableWindow  Q-clr-namespace:WPFCommandPanel.StyleableWindowWPFCommandPanel.StyleableWindow      WPFCommandPanel.App   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     /localclr-namespace:WPFCommandPanel     <sw-clr-namespace:WPFCommandPanel.StyleableWindow     =sw1-clr-namespace:WPFCommandPanel.StyleableWindow       �� 	Resources     WindowColor �� Color  WindowBackColor  WindowForeColor  WindowForeHighlightColor  WindowButtonColor  WindowButtonOverColor  WindowButtonPressedColor;   5WPFCommandPanel.StyleableWindow.WindowMinimizeCommand  MinimizeCommand;   5WPFCommandPanel.StyleableWindow.WindowMaximizeCommand  MaximizeCommand8   2WPFCommandPanel.StyleableWindow.WindowCloseCommand 	 CloseCommand �� 
TargetType 
 WindowButton �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value  
WindowIcon  WindowTemplate	 � RowDefinitions
 � ColumnDefinitions$   WPFCommandPanel.LoadingSpinner  Name � Title@   :WPFCommandPanel.StyleableWindow.ControlDoubleClickBehavior   ExecuteCommand<   6WPFCommandPanel.StyleableWindow.ShowSystemMenuBehavior   RightButtonShow8   2WPFCommandPanel.StyleableWindow.WindowDragBehavior   LeftMouseButtonDrag �� RelativeSource �� Mode �� AncestorType   ExecuteCommandParameter   TargetWindow �� Orientation ��Name ��Name n�Name � Cursor J� Opacity:   4WPFCommandPanel.StyleableWindow.WindowResizeBehavior   	TopResize   TopLeftResize   BottomResize   TopRightResize   RightResize    BottomRightResize!   
LeftResize"   BottomLeftResize# ]� 
Conditions$ �� Property% �� Value& �� 
TargetName  CustomWindowStyle  RedWindowStyle��%�  &	        &	      &	       &	 6     &	 L     &	 \     &	 q     &	 �     &	 �     &		 �     &	
 �     7  7  7  7   7  7  7  7  &	 �    &	 7    7  7  7  7 
 7  7 
 7  7 
 7 	 &	 
    7  &	     7  �� $	 Red���� $ Black���� $ 
WhiteSmoke���� $ 
WhiteSmoke���� $
 Gray���� $ 	LightBlue���� $ DarkBlue��      �� " ��.���  � ��$ 25����  � ��$ 25����  � ��8   ��  � ��8  ��  � �� �� " ��.R��� #��z��._��� $��Center=�$��Center=� P�  � {� .  . ��� 8   � ���� 8   � ��P�  �@J�IsMouseOver . . ��� 8   � ���� 8   � ��P�  � �� . . ��� 8   � ���� 8   � ���� " ��.���  � ��$ 25����  � ��$ 25���� " �.R�� 8��  .+�� 	 �� $��30 ��� $��* �.+�� $��0��$��5,0,5,0q�
 �� $
��auto ��� $��* ��� $��75 �.+�  $ spinner��$��0��$��12��$��12��$y�Hidden=�$��0 0 0 7q��� $��1��$
��Left=�#��z $��3,0,0,0q�$��Center=�$��Center=�$��Stretch=�$��Stretch=�8�� $��16(�8   .  ��  �� $ FindAncestor=�" � ��  �� $ FindAncestor=�" � ��  �� $ FindAncestor=�" ��� $��2��$ 
Horizontal=�.+��� $��Right=�8�� 8�� ����  �� $ FindAncestor=�" �.��_�� $��Right=�8�� 8�� ����  �� $ FindAncestor=�" �.��+�� $��Right=�8�� 8�� ����  �� $ FindAncestor=�" �.��X�� $��1��
�������$��0 1 0 0 q�#��z��._�� .+��� .u��� $ WindowContent���� $ WindowResizeGrip��$��Right=���.  $y�	Collapsed=�$��Bottom=�n� $ ThumbTop��$��6��$
��14,0q�$	��Top=�$ SizeNS��$ 0Q� ��  �� $ FindAncestor=�" �n� $ ThumbTopLeft��$��6��$��6��$	��Top=�$
��Left=�$ SizeNWSE��$ 0Q� ��  �� $ FindAncestor=�" �n� $ ThumbBottom��$��6��$
��14,0q�$��Bottom=�$ SizeNS��$ 0Q� ��  �� $ FindAncestor=�" �n� $ ThumbTopRight��$��6��$��6��$	��Top=�$��Right=�$ SizeNESW��$ 0Q� ��  �� $ FindAncestor=�" �n� $ 
ThumbRight��$��Right=�$
��0,14q�$��6��$ SizeWE��$ 0Q� ��  �� $ FindAncestor=�" �n� $ ThumbBottomRight��$��6��$��6��$��Bottom=�$��Right=�$ SizeNWSE��$ 0Q�  ��  �� $ FindAncestor=�" �n� $ 	ThumbLeft��$
��Left=�$
��0,14q�$��6��$ SizeWE��$ 0Q�! ��  �� $ FindAncestor=�" �n� $ ThumbBottomLeft��$��6��$��6��$��Bottom=�$
��Left=�$ SizeNESW��$ 0Q�" ��  �� $ FindAncestor=�" � ]� # �� $ �@�
ResizeMode$% CanResizeWithGrip=��� $ �@�WindowState$% Normal=�./��� $& WindowResizeGrip�� � y�$ Visible=��� " �.���  �@�WindowStyle$
 None=���  �@�AllowsTransparency . ��  � ��$	 200����  � ��$ 46����  � ��
 �   ���  � ��
 �   ���  � ��$ 1q���  � ��
 �������  � ��8   �� " �.���  �@�WindowStyle$
 None=���  �@�AllowsTransparency . ��  � ��$	 100����  � ��$ 46����  � ��
 �������  � ��
 �  ����  � ��$ 7q���  � ��
 �������  � ��8   