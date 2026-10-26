using System.Collections.Generic;
using System.Linq;

namespace CalcBinding.PathAnalysis;

public class PropertyPathToken : PathToken
{
	private PathTokenId id;

	public IEnumerable<string> Properties { get; private set; }

	public override PathTokenId Id => id;

	public PropertyPathToken(int start, int end, IEnumerable<string> properties)
		: base(start, end)
	{
		Properties = properties.ToList();
		id = new PathTokenId(PathTokenType.Property, string.Join(".", Properties));
	}
}
