using System;
using System.Reflection;

namespace MethodDecorator.Fody.Interfaces;

// The shipped application was already woven by MethodDecorator.Fody.  The
// decompiled method bodies contain those calls, so rebuilding only needs the
// interface contract and must not run the weaver a second time.
public interface IMethodDecorator
{
	void Init(object instance, MethodBase method, object[] args);

	void OnEntry();

	void OnExit();

	void OnException(Exception exception);
}
