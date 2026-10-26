using System;
using System.Windows.Markup;

namespace SkrinshoterApp;

[MarkupExtensionReturnType(typeof(string))]
public sealed class ResolvedProductUrlExtension : MarkupExtension
{
	public string Url { get; }

	public ResolvedProductUrlExtension(string url)
	{
		Url = url;
	}

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return ProductUrl.Resolve(Url);
	}
}
