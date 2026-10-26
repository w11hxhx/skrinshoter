namespace CalcBinding.PathAnalysis;

public class MathToken : PathToken
{
	private PathTokenId id;

	public string MathMember { get; private set; }

	public override PathTokenId Id => id;

	public MathToken(int start, int end, string mathMember)
		: base(start, end)
	{
		MathMember = mathMember;
		id = new PathTokenId(PathTokenType.Math, string.Join(".", "Math", MathMember));
	}
}
