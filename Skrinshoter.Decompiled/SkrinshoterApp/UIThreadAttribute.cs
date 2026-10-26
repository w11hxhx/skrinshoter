using System;
using System.Reflection;
using MethodDecorator.Fody.Interfaces;

namespace SkrinshoterApp;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
public sealed class UIThreadAttribute : Attribute, IMethodDecorator
{
	private string typeName;

	private string memberName;

	public void Init(object instance, MethodBase method, object[] args)
	{
		typeName = method?.DeclaringType?.FullName ?? "<unknown>";
		memberName = method?.Name ?? "<unknown>";
	}

	public void OnEntry()
	{
		if (!AppDesignMode.Enabled)
		{
			UIThread.Ensure(typeName, memberName);
		}
	}

	public void OnExit()
	{
	}

	public void OnException(Exception exception)
	{
	}
}
