using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class TextObject : UserControl
{
	public TextObject()
	{
		InitializeComponent();
	}

	private void UserControl_Loaded(object sender, RoutedEventArgs e)
	{
		(DataContext as VisualObjectVM).OnViewReady(MainElement);
	}

	private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		(sender as TextBox)?.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
	}

}
