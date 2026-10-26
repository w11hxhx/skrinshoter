using System.Collections.Generic;
using DynamicExpresso;

namespace CalcBinding.ExpressionParsers;

public sealed class ExpressionParser : IExpressionParser
{
	private Interpreter _interpreter;

	public ExpressionParser()
	{
		_interpreter = new Interpreter();
	}

	public Lambda Parse(string expressionText, Parameter[] parameters)
	{
		return _interpreter.Parse(expressionText, parameters);
	}

	public void SetReference(IEnumerable<ReferenceType> referencedTypes)
	{
		_interpreter.Reference(referencedTypes);
	}
}
