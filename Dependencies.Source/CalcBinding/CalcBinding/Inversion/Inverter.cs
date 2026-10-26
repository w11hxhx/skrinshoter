using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using CalcBinding.ExpressionParsers;
using DynamicExpresso;

namespace CalcBinding.Inversion;

public class Inverter
{
	internal enum NodeType
	{
		Variable,
		Constant
	}

	internal enum ConstantPlace
	{
		Left,
		Right,
		Wherever
	}

	private class RecursiveInfo
	{
		public string FoundedParamName;

		public string InvertedExp;
	}

	private delegate string FuncExpressionDelegate(string constant);

	private class ExpressionFuncsDictionary<T> : Dictionary<T, ConstantPlace, FuncExpressionDelegate>
	{
		public override FuncExpressionDelegate this[T key1, ConstantPlace key2]
		{
			get
			{
				Dictionary<ConstantPlace, FuncExpressionDelegate> dictionary = base[key1];
				if (dictionary.ContainsKey(key2))
				{
					return dictionary[key2];
				}
				if (dictionary.ContainsKey(ConstantPlace.Wherever))
				{
					return dictionary[ConstantPlace.Wherever];
				}
				return dictionary[key2];
			}
		}
	}

	private static readonly ExpressionFuncsDictionary<ExpressionType> inversedFuncs = new ExpressionFuncsDictionary<ExpressionType>
	{
		{
			ExpressionType.Add,
			ConstantPlace.Wherever,
			(string constant) => "({0})-" + constant
		},
		{
			ExpressionType.Subtract,
			ConstantPlace.Left,
			(string constant) => constant + "-({0})"
		},
		{
			ExpressionType.Subtract,
			ConstantPlace.Right,
			(string constant) => "({0})+" + constant
		},
		{
			ExpressionType.Multiply,
			ConstantPlace.Wherever,
			(string constant) => "({0})/" + constant
		},
		{
			ExpressionType.Divide,
			ConstantPlace.Left,
			(string constant) => constant + "/({0})"
		},
		{
			ExpressionType.Divide,
			ConstantPlace.Right,
			(string constant) => "({0})*" + constant
		}
	};

	private static readonly ExpressionFuncsDictionary<string> inversedMathFuncs = new ExpressionFuncsDictionary<string>
	{
		{
			"Math.Sin",
			ConstantPlace.Wherever,
			(string dummy) => "Math.Asin({0})"
		},
		{
			"Math.Asin",
			ConstantPlace.Wherever,
			(string dummy) => "Math.Sin({0})"
		},
		{
			"Math.Cos",
			ConstantPlace.Wherever,
			(string dummy) => "Math.Acos({0})"
		},
		{
			"Math.Acos",
			ConstantPlace.Wherever,
			(string dummy) => "Math.Cos({0})"
		},
		{
			"Math.Tan",
			ConstantPlace.Wherever,
			(string dummy) => "Math.Atan({0})"
		},
		{
			"Math.Atan",
			ConstantPlace.Wherever,
			(string dummy) => "Math.Tan({0})"
		},
		{
			"Math.Pow",
			ConstantPlace.Left,
			(string constant) => "Math.Log(({0}), " + constant + ")"
		},
		{
			"Math.Pow",
			ConstantPlace.Right,
			(string constant) => "Math.Pow(({0}), 1.0/" + constant + ")"
		},
		{
			"Math.Log",
			ConstantPlace.Left,
			(string constant) => "Math.Pow(" + constant + ", 1.0/({0}))"
		},
		{
			"Math.Log",
			ConstantPlace.Right,
			(string constant) => "Math.Pow(" + constant + ", ({0}))"
		}
	};

	private const string RES = "({0})";

	private IExpressionParser _interpreter;

	public Inverter(IExpressionParser interpreter)
	{
		_interpreter = interpreter;
	}

	public Lambda InverseExpression(Expression expression, ParameterExpression parameter)
	{
		RecursiveInfo recursiveInfo = new RecursiveInfo();
		string constantExpression = null;
		InverseExpressionInternal(expression, recursiveInfo, ref constantExpression);
		if (recursiveInfo.FoundedParamName == null)
		{
			throw new InverseException($"Parameter was not found in expression '{expression}'!");
		}
		string name = parameter.Name;
		string expressionText = string.Format(recursiveInfo.InvertedExp, name);
		return _interpreter.Parse(expressionText, new Parameter(parameter.Name, parameter.Type));
	}

	private NodeType InverseExpressionInternal(Expression expr, RecursiveInfo recInfo, ref string constantExpression)
	{
		switch (expr.NodeType)
		{
		case ExpressionType.Add:
		case ExpressionType.Divide:
		case ExpressionType.Multiply:
		case ExpressionType.Subtract:
		{
			BinaryExpression binaryExpression = expr as BinaryExpression;
			string constantExpression6 = null;
			string constantExpression7 = null;
			NodeType nodeType5 = InverseExpressionInternal(binaryExpression.Left, recInfo, ref constantExpression6);
			NodeType nodeType6 = InverseExpressionInternal(binaryExpression.Right, recInfo, ref constantExpression7);
			int num;
			if (nodeType5 != NodeType.Variable && nodeType6 != NodeType.Variable)
			{
				num = 1;
				if (num != 0)
				{
					constantExpression = $"({constantExpression6}{NodeTypeToString(binaryExpression.NodeType)}{constantExpression7})";
					return (NodeType)num;
				}
			}
			else
			{
				num = 0;
			}
			ConstantPlace key = ((nodeType5 != NodeType.Constant) ? ConstantPlace.Right : ConstantPlace.Left);
			string constant = ((nodeType5 == NodeType.Constant) ? constantExpression6 : constantExpression7);
			recInfo.InvertedExp = string.Format(recInfo.InvertedExp, inversedFuncs[expr.NodeType, key](constant));
			return (NodeType)num;
		}
		case ExpressionType.Parameter:
		{
			ParameterExpression parameterExpression = expr as ParameterExpression;
			if (recInfo.FoundedParamName == null)
			{
				recInfo.FoundedParamName = parameterExpression.Name;
				recInfo.InvertedExp = "({0})";
				return NodeType.Variable;
			}
			if (recInfo.FoundedParamName == parameterExpression.Name)
			{
				throw new InverseException($"Variable {recInfo.FoundedParamName} is defined more than one time!");
			}
			throw new InverseException($"More than one variables are defined in expression: {recInfo.FoundedParamName} and {parameterExpression.Name}");
		}
		case ExpressionType.Constant:
		{
			ConstantExpression constantExpression8 = expr as ConstantExpression;
			constantExpression = string.Format(CultureInfo.InvariantCulture, "({0})", new object[1] { constantExpression8.Value });
			return NodeType.Constant;
		}
		case ExpressionType.Convert:
		{
			UnaryExpression unaryExpression3 = expr as UnaryExpression;
			string constantExpression9 = null;
			NodeType nodeType7 = InverseExpressionInternal(unaryExpression3.Operand, recInfo, ref constantExpression9);
			if (nodeType7 == NodeType.Constant)
			{
				constantExpression = "((" + unaryExpression3.Type.Name + ")" + constantExpression9 + ")";
			}
			else
			{
				recInfo.InvertedExp = string.Format(recInfo.InvertedExp, "((" + unaryExpression3.Operand.Type.Name + ")({0}))");
			}
			return nodeType7;
		}
		case ExpressionType.Negate:
		{
			UnaryExpression unaryExpression2 = expr as UnaryExpression;
			string constantExpression3 = null;
			NodeType nodeType2 = InverseExpressionInternal(unaryExpression2.Operand, recInfo, ref constantExpression3);
			if (nodeType2 == NodeType.Constant)
			{
				constantExpression = "(-" + constantExpression3 + ")";
				return nodeType2;
			}
			recInfo.InvertedExp = string.Format(recInfo.InvertedExp, "(-({0}))");
			return nodeType2;
		}
		case ExpressionType.Not:
		{
			UnaryExpression unaryExpression = expr as UnaryExpression;
			string constantExpression2 = null;
			NodeType nodeType = InverseExpressionInternal(unaryExpression.Operand, recInfo, ref constantExpression2);
			if (nodeType == NodeType.Constant)
			{
				constantExpression = "(" + NodeTypeToString(ExpressionType.Not) + constantExpression2 + ")";
				return nodeType;
			}
			recInfo.InvertedExp = string.Format(recInfo.InvertedExp, "(" + NodeTypeToString(ExpressionType.Not) + "({0}))");
			return nodeType;
		}
		case ExpressionType.Call:
		{
			MethodCallExpression methodCallExpression = expr as MethodCallExpression;
			string text = methodCallExpression.Method.DeclaringType.Name + "." + methodCallExpression.Method.Name;
			if (!inversedMathFuncs.ContainsKey(text))
			{
				throw new InverseException($"Unsupported method call expression: {expr}");
			}
			string constantExpression4 = null;
			string constantExpression5 = null;
			NodeType nodeType3 = InverseExpressionInternal(methodCallExpression.Arguments[0], recInfo, ref constantExpression4);
			NodeType? nodeType4 = null;
			_ = methodCallExpression.Arguments[0];
			if (methodCallExpression.Arguments.Count == 2)
			{
				nodeType4 = InverseExpressionInternal(methodCallExpression.Arguments[1], recInfo, ref constantExpression5);
				_ = methodCallExpression.Arguments[1];
			}
			string text2 = null;
			if (nodeType3 == NodeType.Variable)
			{
				text2 = inversedMathFuncs[text, ConstantPlace.Right](constantExpression5);
			}
			else if (nodeType4.HasValue && nodeType4.Value == NodeType.Variable)
			{
				text2 = inversedMathFuncs[text, ConstantPlace.Left](constantExpression4);
			}
			else
			{
				constantExpression = text + "(" + constantExpression4;
				if (nodeType4.HasValue)
				{
					constantExpression = constantExpression + ", " + constantExpression5;
				}
				constantExpression += ")";
			}
			if (text2 != null)
			{
				recInfo.InvertedExp = string.Format(recInfo.InvertedExp, text2);
			}
			if (text2 != null)
			{
				return NodeType.Variable;
			}
			return NodeType.Constant;
		}
		case ExpressionType.MemberAccess:
		{
			MemberExpression memberExpression = expr as MemberExpression;
			if (memberExpression.Member.DeclaringType.Name == "Math")
			{
				constantExpression = string.Format(CultureInfo.InvariantCulture, "({0})", new object[1] { memberExpression.Member.DeclaringType.Name + "." + memberExpression.Member.Name });
				return NodeType.Constant;
			}
			throw new InverseException($"Unsupported method call expression: {expr}");
		}
		default:
			throw new InverseException($"Unsupported expression: {expr}");
		}
	}

	private string NodeTypeToString(ExpressionType nodeType)
	{
		return nodeType switch
		{
			ExpressionType.Divide => "/", 
			ExpressionType.Multiply => "*", 
			ExpressionType.Subtract => "-", 
			ExpressionType.Add => "+", 
			ExpressionType.Not => "!", 
			_ => throw new Exception(string.Concat("Unkwnown binary node type: ", nodeType, "!")), 
		};
	}
}
