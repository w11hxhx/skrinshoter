using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace SkrinshoterApp;

public static class InlineLinkTextHelper
{
	private const string LinkStartMarker = "<link>";

	private const string LinkEndMarker = "</link>";

	public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached("Text", typeof(string), typeof(InlineLinkTextHelper), new PropertyMetadata(null, OnPropertyChanged));

	public static readonly DependencyProperty LinkCommandProperty = DependencyProperty.RegisterAttached("LinkCommand", typeof(ICommand), typeof(InlineLinkTextHelper), new PropertyMetadata(null, OnPropertyChanged));

	public static readonly DependencyProperty LinkStyleProperty = DependencyProperty.RegisterAttached("LinkStyle", typeof(Style), typeof(InlineLinkTextHelper), new PropertyMetadata(null, OnPropertyChanged));

	public static string GetText(DependencyObject obj)
	{
		return (string)obj.GetValue(TextProperty);
	}

	public static void SetText(DependencyObject obj, string value)
	{
		obj.SetValue(TextProperty, value);
	}

	public static ICommand GetLinkCommand(DependencyObject obj)
	{
		return (ICommand)obj.GetValue(LinkCommandProperty);
	}

	public static void SetLinkCommand(DependencyObject obj, ICommand value)
	{
		obj.SetValue(LinkCommandProperty, value);
	}

	public static Style GetLinkStyle(DependencyObject obj)
	{
		return (Style)obj.GetValue(LinkStyleProperty);
	}

	public static void SetLinkStyle(DependencyObject obj, Style value)
	{
		obj.SetValue(LinkStyleProperty, value);
	}

	private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		RebuildInlines((d as TextBlock) ?? throw new InvalidOperationException("Attached properties of InlineLinkTextHelper can only be applied to a TextBlock."));
	}

	private static void RebuildInlines(TextBlock textBlock)
	{
		textBlock.Inlines.Clear();
		string text = GetText(textBlock);
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		int num = 0;
		while (true)
		{
			int num2 = text.IndexOf("<link>", num, StringComparison.Ordinal);
			if (num2 < 0)
			{
				break;
			}
			int num3 = num2 + "<link>".Length;
			int num4 = text.IndexOf("</link>", num3, StringComparison.Ordinal);
			if (num4 < 0)
			{
				throw new FormatException("Unclosed link marker in text: " + text);
			}
			if (num2 > num)
			{
				textBlock.Inlines.Add(new Run(text.Substring(num, num2 - num)));
			}
			textBlock.Inlines.Add(new Hyperlink(new Run(text.Substring(num3, num4 - num3)))
			{
				Style = GetLinkStyle(textBlock),
				Command = GetLinkCommand(textBlock)
			});
			num = num4 + "</link>".Length;
		}
		if (num < text.Length)
		{
			textBlock.Inlines.Add(new Run(text.Substring(num)));
		}
	}
}
