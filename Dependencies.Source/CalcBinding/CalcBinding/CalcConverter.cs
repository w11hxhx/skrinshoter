using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using CalcBinding.ExpressionParsers;
using CalcBinding.Inversion;
using CalcBinding.Trace;
using DynamicExpresso;

namespace CalcBinding;

public class CalcConverter : IValueConverter, IMultiValueConverter
{
	private IExpressionParser _parser;

	private readonly object _fallbackValue;

	private Lambda _compiledExpression;

	private Lambda _compiledInversedExpression;

	private Type[] _sourceValuesTypes;

	private static readonly Tracer Tracer = new Tracer(TraceComponent.CalcConverter);

	public bool StringFormatDefined { get; set; }

	public FalseToVisibility FalseToVisibility { get; set; } = FalseToVisibility.Collapsed;

	public CalcConverter(IExpressionParser parser, object fallbackValue, Dictionary<string, Type> enums)
	{
		_parser = parser;
		_fallbackValue = fallbackValue;
		if (parser != null && enums != null && enums.Any())
		{
			parser.SetReference(enums.Select((KeyValuePair<string, Type> ep) => new ReferenceType(ep.Key, ep.Value)));
		}
	}

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return Convert(new object[1] { value }, targetType, parameter, culture);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (_compiledExpression == null && (_compiledExpression = CompileExpression(null, (string)parameter, convertBack: true, new List<Type> { targetType })) == null)
		{
			return null;
		}
		if (_compiledInversedExpression == null)
		{
			try
			{
				ParameterExpression parameter2 = System.Linq.Expressions.Expression.Parameter(_compiledExpression.Expression.Type, "Path");
				_compiledInversedExpression = new Inverter(_parser).InverseExpression(_compiledExpression.Expression, parameter2);
			}
			catch (Exception ex)
			{
				Tracer.TraceError(string.Concat("Can't convert back expression ", parameter, ": ", ex.Message));
			}
		}
		if (_compiledInversedExpression != null)
		{
			try
			{
				if (targetType == typeof(bool) && value.GetType() == typeof(Visibility))
				{
					value = new BoolToVisibilityConverter(FalseToVisibility).ConvertBack(value, targetType, null, culture);
				}
				if (value is string && _compiledExpression.Expression.Type != value.GetType())
				{
					value = ParseStringToObject((string)value, _compiledExpression.Expression.Type);
				}
				return _compiledInversedExpression.Invoke(value);
			}
			catch (Exception ex2)
			{
				Tracer.TraceError(string.Concat("Can't invoke back expression ", parameter, ": ", ex2.Message));
			}
		}
		return null;
	}

	private object ParseStringToObject(string value, Type type)
	{
		return System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
	}

	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values == null)
		{
			return null;
		}
		if (_sourceValuesTypes == null)
		{
			_sourceValuesTypes = GetTypes(values);
		}
		else
		{
			Type[] types = GetTypes(values);
			if (!_sourceValuesTypes.SequenceEqual(types))
			{
				_sourceValuesTypes = types;
				_compiledExpression = null;
				_compiledInversedExpression = null;
			}
		}
		if (_compiledExpression == null && (_compiledExpression = CompileExpression(values, (string)parameter)) == null)
		{
			return _fallbackValue;
		}
		try
		{
			object obj = _compiledExpression.Invoke(values);
			if (!StringFormatDefined)
			{
				if (targetType == typeof(Visibility) && !(obj is Visibility))
				{
					obj = new BoolToVisibilityConverter(FalseToVisibility).Convert(obj, targetType, null, culture);
				}
				if (targetType == typeof(string))
				{
					obj = string.Format(CultureInfo.InvariantCulture, "{0}", new object[1] { obj });
				}
			}
			return obj;
		}
		catch (Exception ex)
		{
			Tracer.TraceError("Can't invoke expression " + _compiledExpression.ExpressionText + ": " + ex.Message);
			return null;
		}
	}

	private Type[] GetTypes(object[] values)
	{
		return values.Select((object v) => v?.GetType()).ToArray();
	}

	private Lambda CompileExpression(object[] values, string expressionTemplate, bool convertBack = false, List<Type> targetTypes = null)
	{
		try
		{
			Lambda result = null;
			bool flag = false;
			if (convertBack)
			{
				flag = true;
			}
			else if (values.Contains(DependencyProperty.UnsetValue))
			{
				Tracer.TraceError("One of source fields is Unset");
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				List<Type> argumentsTypes = (convertBack ? targetTypes : _sourceValuesTypes.Select((Type t) => t ?? typeof(object)).ToList());
				result = CompileExpression(argumentsTypes, expressionTemplate);
			}
			return result;
		}
		catch (Exception ex)
		{
			Tracer.TraceError("Can't convert expression " + expressionTemplate + ": " + ex.Message);
			return null;
		}
	}

	private Lambda CompileExpression(List<Type> argumentsTypes, string expressionTemplate)
	{
		List<Parameter> list = new List<Parameter>();
		for (int i = 0; i < argumentsTypes.Count(); i++)
		{
			string variableName = GetVariableName(i);
			expressionTemplate = expressionTemplate.Replace("{" + i + "}", variableName);
			list.Add(new Parameter(variableName, argumentsTypes[i]));
		}
		return _parser.Parse(expressionTemplate, list.ToArray());
	}

	private string GetVariableName(int i)
	{
		return $"p{++i}";
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
