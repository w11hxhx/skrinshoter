using System;
using System.Collections.Generic;

namespace SkrinshoterApp.Common;

public class DoubleComparer : IComparer<double>
{
	private readonly double epsilon;

	public DoubleComparer(double epsilon = 1E-10)
	{
		if (epsilon < 0.0)
		{
			throw new ArgumentOutOfRangeException("epsilon", "Epsilon must be non-negative.");
		}
		this.epsilon = epsilon;
	}

	public int Compare(double x, double y)
	{
		if (Math.Abs(x - y) <= epsilon)
		{
			return 0;
		}
		if (!(x < y))
		{
			return 1;
		}
		return -1;
	}
}
