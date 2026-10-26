using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace SkrinshoterApp;

public sealed class HardWrapTextBoxBehavior : Behavior<TextBox>
{
	private TextBox textBox;

	private ScrollViewer contentHost;

	private string originalText = string.Empty;

	private bool updatingView;

	private int[] caretMap = Array.Empty<int>();

	private int columns = 80;

	private double charWidth = 8.0;

	protected override void OnAttached()
	{
		base.OnAttached();
		textBox = AssociatedObject;
		textBox.AcceptsReturn = true;
		textBox.TextWrapping = TextWrapping.NoWrap;
		ScrollViewer.SetHorizontalScrollBarVisibility(textBox, ScrollBarVisibility.Disabled);
		ScrollViewer.SetVerticalScrollBarVisibility(textBox, ScrollBarVisibility.Hidden);
		textBox.IsUndoEnabled = false;
		textBox.Loaded += OnLoaded;
		textBox.PreviewTextInput += OnPreviewTextInput;
		textBox.PreviewKeyDown += OnPreviewKeyDown;
		DataObject.AddPastingHandler(textBox, OnPaste);
	}

	protected override void OnDetaching()
	{
		base.OnDetaching();
		DataObject.RemovePastingHandler(textBox, OnPaste);
		textBox.Loaded -= OnLoaded;
		textBox.PreviewTextInput -= OnPreviewTextInput;
		textBox.PreviewKeyDown -= OnPreviewKeyDown;
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		contentHost = FindContentHost(textBox);
		charWidth = MeasureMonoCharWidth(textBox);
		double wrapWidth = GetWrapWidth();
		columns = Math.Max(1, (int)Math.Floor((wrapWidth + 0.01) / charWidth));
		originalText = Normalize(textBox.Text);
		ReflowSetCaret(OriginalIndexFromVisible(textBox.CaretIndex));
	}

	private static ScrollViewer FindContentHost(TextBox tb)
	{
		if (tb.Template?.FindName("PART_ContentHost", tb) is ScrollViewer result)
		{
			return result;
		}
		return FindDescendant<ScrollViewer>(tb);
	}

	private static T FindDescendant<T>(DependencyObject root) where T : DependencyObject
	{
		if (root == null)
		{
			return null;
		}
		int childrenCount = VisualTreeHelper.GetChildrenCount(root);
		for (int i = 0; i < childrenCount; i++)
		{
			DependencyObject child = VisualTreeHelper.GetChild(root, i);
			if (child is T result)
			{
				return result;
			}
			T val = FindDescendant<T>(child);
			if (val != null)
			{
				return val;
			}
		}
		return null;
	}

	private static string Normalize(string s)
	{
		if (string.IsNullOrEmpty(s))
		{
			return string.Empty;
		}
		s = s.Replace("\r\n", "\n");
		s = s.Replace("\r", "\n");
		return s;
	}

	private static double MeasureMonoCharWidth(TextBox tb)
	{
		int num = 100;
		return new FormattedText(new string('M', num), pixelsPerDip: VisualTreeHelper.GetDpi(tb).PixelsPerDip, culture: CultureInfo.CurrentUICulture, flowDirection: FlowDirection.LeftToRight, typeface: new Typeface(tb.FontFamily, tb.FontStyle, tb.FontWeight, tb.FontStretch), emSize: tb.FontSize, foreground: Brushes.Black).WidthIncludingTrailingWhitespace / (double)num;
	}

	private double GetWrapWidth()
	{
		if (contentHost != null)
		{
			double viewportWidth = contentHost.ViewportWidth;
			if (!double.IsNaN(viewportWidth) && viewportWidth > 0.0)
			{
				return viewportWidth;
			}
			return Math.Max(0.0, contentHost.ActualWidth);
		}
		Thickness padding = textBox.Padding;
		Thickness borderThickness = textBox.BorderThickness;
		return Math.Max(0.0, textBox.ActualWidth - padding.Left - padding.Right - borderThickness.Left - borderThickness.Right);
	}

	private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		if (!updatingView)
		{
			(int start, int end) tuple = CurrentOriginalSelection();
			int item = tuple.start;
			int item2 = tuple.end;
			string text = e.Text;
			originalText = originalText.Remove(item, item2 - item).Insert(item, text);
			e.Handled = true;
			ReflowSetCaret(item + text.Length);
		}
	}

	private void OnPreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (updatingView)
		{
			return;
		}
		if (e.Key == Key.Return)
		{
			(int start, int end) tuple = CurrentOriginalSelection();
			int item = tuple.start;
			int item2 = tuple.end;
			originalText = originalText.Remove(item, item2 - item).Insert(item, "\n");
			e.Handled = true;
			ReflowSetCaret(item + 1);
		}
		else if (e.Key == Key.Space)
		{
			(int start, int end) tuple2 = CurrentOriginalSelection();
			int item3 = tuple2.start;
			int item4 = tuple2.end;
			originalText = originalText.Remove(item3, item4 - item3).Insert(item3, " ");
			e.Handled = true;
			ReflowSetCaret(item3 + 1);
		}
		else if (e.Key == Key.Back || e.Key == Key.Delete)
		{
			var (num, num2) = CurrentOriginalSelection();
			if (num2 > num)
			{
				originalText = originalText.Remove(num, num2 - num);
				e.Handled = true;
				ReflowSetCaret(num);
			}
			else if (e.Key == Key.Back && num > 0)
			{
				originalText = originalText.Remove(num - 1, 1);
				e.Handled = true;
				ReflowSetCaret(num - 1);
			}
			else if (e.Key == Key.Delete && num < originalText.Length)
			{
				originalText = originalText.Remove(num, 1);
				e.Handled = true;
				ReflowSetCaret(num);
			}
		}
	}

	private void OnPaste(object sender, DataObjectPastingEventArgs e)
	{
		if (!updatingView)
		{
			string text = null;
			if (e.SourceDataObject.GetDataPresent(DataFormats.UnicodeText))
			{
				text = (string)e.SourceDataObject.GetData(DataFormats.UnicodeText);
			}
			else if (e.SourceDataObject.GetDataPresent(DataFormats.Text))
			{
				text = (string)e.SourceDataObject.GetData(DataFormats.Text);
			}
			if (text != null)
			{
				text = Normalize(text);
				(int start, int end) tuple = CurrentOriginalSelection();
				int item = tuple.start;
				int item2 = tuple.end;
				originalText = originalText.Remove(item, item2 - item).Insert(item, text);
				e.CancelCommand();
				ReflowSetCaret(item + text.Length);
			}
		}
	}

	private (int start, int end) CurrentOriginalSelection()
	{
		int selectionStart = textBox.SelectionStart;
		int visibleCaretIndex = selectionStart + textBox.SelectionLength;
		int item = OriginalIndexFromVisible(selectionStart);
		int item2 = OriginalIndexFromVisible(visibleCaretIndex);
		return (start: item, end: item2);
	}

	private void ReflowSetCaret(int origCaretIndex)
	{
		string text = HardWrapToColumns(originalText, columns, out var caretMapOut);
		updatingView = true;
		try
		{
			caretMap = caretMapOut;
			textBox.Text = text;
			int val = VisibleIndexFromOriginal(origCaretIndex);
			val = Math.Max(0, Math.Min(val, textBox.Text.Length));
			textBox.CaretIndex = val;
			textBox.SelectionLength = 0;
		}
		finally
		{
			updatingView = false;
		}
	}

	private static string HardWrapToColumns(string original, int columns, out int[] caretMapOut)
	{
		StringBuilder stringBuilder = new StringBuilder(original.Length + 64);
		List<int> list = new List<int>(original.Length + 64);
		int num = 0;
		int num2 = 0;
		list.Add(num);
		int num3 = 0;
		while (num3 < original.Length)
		{
			char c = original[num3];
			if (c == '\n')
			{
				stringBuilder.Append('\r');
				list.Add(num);
				stringBuilder.Append('\n');
				num++;
				num3++;
				list.Add(num);
				num2 = 0;
			}
			else if (num2 == columns)
			{
				stringBuilder.Append('\r');
				list.Add(num);
				stringBuilder.Append('\n');
				list.Add(num);
				num2 = 0;
			}
			else
			{
				stringBuilder.Append(c);
				num++;
				num3++;
				list.Add(num);
				num2++;
			}
		}
		caretMapOut = list.ToArray();
		return stringBuilder.ToString();
	}

	private int OriginalIndexFromVisible(int visibleCaretIndex)
	{
		if (caretMap == null || caretMap.Length == 0)
		{
			return Math.Min(visibleCaretIndex, originalText.Length);
		}
		int num = Math.Max(0, Math.Min(visibleCaretIndex, caretMap.Length - 1));
		return caretMap[num];
	}

	private int VisibleIndexFromOriginal(int originalCaretIndex)
	{
		if (caretMap == null || caretMap.Length == 0)
		{
			return originalCaretIndex;
		}
		int num = Math.Max(0, Math.Min(originalCaretIndex, originalText.Length));
		for (int i = 0; i < caretMap.Length; i++)
		{
			if (caretMap[i] == num)
			{
				return i;
			}
		}
		return caretMap.Length - 1;
	}
}
