using System.Collections.Generic;

namespace CalcBinding.PathAnalysis;

public class StaticPropertyPathToken : PropertyPathToken
{
	private PathTokenId id;

	public string Class { get; private set; }

	public string Namespace { get; private set; }

	public override PathTokenId Id => id;

	public StaticPropertyPathToken(int start, int end, string @namespace, string @class, IEnumerable<string> properties)
		: base(start, end, properties)
	{
		Class = @class;
		Namespace = @namespace;
		id = new PathTokenId(PathTokenType.StaticProperty, string.Format("{0}:{1}.{2}", Namespace, Class, string.Join(".", Properties)));
	}
}
