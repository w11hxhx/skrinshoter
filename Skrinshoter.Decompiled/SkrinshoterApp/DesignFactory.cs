using System;

namespace SkrinshoterApp;

public abstract class DesignFactory<T> : IDesignFactory
{
	public abstract T Create(IServiceProvider serviceProvider);

	object IDesignFactory.Create(IServiceProvider serviceProvider)
	{
		return Create(serviceProvider);
	}
}
