using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace SkrinshoterApp;

public class RichTextBoxHelper : DependencyObject
{
	private static List<Guid> _recursionProtection = new List<Guid>();

	public static readonly DependencyProperty DocumentXamlProperty = DependencyProperty.RegisterAttached("DocumentXaml", typeof(string), typeof(RichTextBoxHelper), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (DependencyObject obj, DependencyPropertyChangedEventArgs e) =>
	{
		RichTextBox richTextBox = (RichTextBox)obj;
		if (richTextBox.Tag == null || !_recursionProtection.Contains((Guid)richTextBox.Tag))
		{
			try
			{
				string documentXaml = GetDocumentXaml(richTextBox);
				MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(documentXaml));
				FlowDocument document = (string.IsNullOrEmpty(documentXaml) ? new FlowDocument() : ((FlowDocument)XamlReader.Load(stream)));
				richTextBox.Document = document;
			}
			catch (Exception)
			{
				richTextBox.Document = new FlowDocument();
			}
			richTextBox.TextChanged += (object obj2, TextChangedEventArgs e2) =>
			{
				if (obj2 is RichTextBox richTextBox2)
				{
					SetDocumentXaml(richTextBox, XamlWriter.Save(richTextBox2.Document));
				}
			};
		}
	}));

	public static string GetDocumentXaml(DependencyObject obj)
	{
		return (string)obj.GetValue(DocumentXamlProperty);
	}

	public static void SetDocumentXaml(DependencyObject obj, string value)
	{
		FrameworkElement frameworkElement = (FrameworkElement)obj;
		if (frameworkElement.Tag == null || (Guid)frameworkElement.Tag == Guid.Empty)
		{
			frameworkElement.Tag = Guid.NewGuid();
		}
		_recursionProtection.Add((Guid)frameworkElement.Tag);
		obj.SetValue(DocumentXamlProperty, value);
		_recursionProtection.Remove((Guid)frameworkElement.Tag);
	}
}
