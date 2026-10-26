using System;

namespace SkrinshoterApp;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, Inherited = false)]
public sealed class AnyThreadAttribute : Attribute
{
}
