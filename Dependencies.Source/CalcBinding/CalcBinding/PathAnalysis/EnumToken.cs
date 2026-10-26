using System;

namespace CalcBinding.PathAnalysis;

public class EnumToken : PathToken
{
	private PathTokenId id;

	public Type Enum { get; private set; }

	public string EnumMember { get; private set; }

	public string Namespace { get; private set; }

	public override PathTokenId Id => id;

	public EnumToken(int start, int end, string @namespace, Type @enum, string enumMember)
		: base(start, end)
	{
		Enum = @enum;
		EnumMember = enumMember;
		Namespace = @namespace;
		id = new PathTokenId(PathTokenType.Enum, $"{Namespace}:{@enum.Name}.{EnumMember}");
	}
}
