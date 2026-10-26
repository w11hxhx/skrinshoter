using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Microsoft.Extensions.DependencyInjection;

namespace SkrinshoterApp;

[MarkupExtensionReturnType(typeof(object))]
[DebuggerStepThrough]
[DebuggerNonUserCode]
public sealed class DesignViewModelExtension : MarkupExtension
{
	private static readonly Lazy<IServiceProvider> serviceProvider = new Lazy<IServiceProvider>(() =>
	{
		ServiceCollection services = new ServiceCollection();
		new Startup().ConfigureServices(services);
		return services.BuildServiceProvider();
	});

	public Type ViewModelType { get; }

	public DesignViewModelExtension(Type viewModelType)
	{
		ViewModelType = viewModelType;
	}

	public override object ProvideValue(IServiceProvider _)
	{
		if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
		{
			return Binding.DoNothing;
		}
		if (ViewModelType == null)
		{
			return null;
		}
		IServiceProvider value = serviceProvider.Value;
		Type type = ViewModelType.Assembly.GetType(ViewModelType.FullName + "DesignFactory", throwOnError: false);
		if (type != null && typeof(IDesignFactory).IsAssignableFrom(type))
		{
			return ((IDesignFactory)ActivatorUtilities.CreateInstance(value, type)).Create(value);
		}
		return ActivatorUtilities.CreateInstance(value, ViewModelType);
	}
}
