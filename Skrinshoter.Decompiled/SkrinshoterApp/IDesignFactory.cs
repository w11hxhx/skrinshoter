using System;

namespace SkrinshoterApp;

public interface IDesignFactory
{
	object Create(IServiceProvider serviceProvider);
}
