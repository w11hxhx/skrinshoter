using System;
using System.Collections.Generic;
using System.Linq;
using DynamicExpresso;

namespace CalcBinding.ExpressionParsers;

public sealed class CachedExpressionParser : IExpressionParser
{
	private struct ExpressionKey(string expressionText, Parameter[] parameters) : IEquatable<ExpressionKey>
	{
		private readonly string _expressionText = expressionText;

		private readonly Parameter[] _parameters = parameters;

		private static ParameterComparer _parameterComparer = new ParameterComparer();

		public override int GetHashCode()
		{
			return (_expressionText.GetHashCode() * 397) ^ _parameters.Length;
		}

		public bool Equals(ExpressionKey other)
		{
			if (string.Equals(_expressionText, other._expressionText))
			{
				return _parameters.SequenceEqual(other._parameters, _parameterComparer);
			}
			return false;
		}
	}

	private class ParameterComparer : IEqualityComparer<Parameter>
	{
		public bool Equals(Parameter x, Parameter y)
		{
			if (string.Equals(x.Name, y.Name))
			{
				return x.Type == y.Type;
			}
			return false;
		}

		public int GetHashCode(Parameter parameter)
		{
			return (parameter.Name.GetHashCode() * 397) ^ parameter.Type.GetHashCode();
		}
	}

	private Dictionary<ExpressionKey, WeakReference> _cachedExpressions = new Dictionary<ExpressionKey, WeakReference>();

	private IExpressionParser _innerParser;

	public CachedExpressionParser(IExpressionParser innerParser)
	{
		_innerParser = innerParser;
	}

	public Lambda Parse(string expressionText, Parameter[] parameters)
	{
		ExpressionKey expressionKey = new ExpressionKey(expressionText, parameters);
		Lambda lambda = FindInCache(expressionKey);
		if (lambda != null)
		{
			return lambda;
		}
		Lambda lambda2 = _innerParser.Parse(expressionText, parameters);
		SaveInCache(expressionKey, lambda2);
		return lambda2;
	}

	private void SaveInCache(ExpressionKey key, Lambda lambda)
	{
		_cachedExpressions[key] = new WeakReference(lambda);
	}

	private Lambda FindInCache(ExpressionKey expressionKey)
	{
		if (_cachedExpressions.ContainsKey(expressionKey))
		{
			if (_cachedExpressions[expressionKey].Target is Lambda result)
			{
				return result;
			}
			_cachedExpressions.Remove(expressionKey);
			RemoveDeadExpressions();
		}
		return null;
	}

	private void RemoveDeadExpressions()
	{
		foreach (ExpressionKey item in _cachedExpressions.Keys.ToList())
		{
			if (!_cachedExpressions[item].IsAlive)
			{
				_cachedExpressions.Remove(item);
			}
		}
	}

	public void SetReference(IEnumerable<ReferenceType> referencedTypes)
	{
		_innerParser.SetReference(referencedTypes);
	}
}
