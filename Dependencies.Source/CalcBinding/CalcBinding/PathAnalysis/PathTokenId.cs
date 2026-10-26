namespace CalcBinding.PathAnalysis;

public class PathTokenId
{
	public PathTokenType PathType { get; private set; }

	public string Value { get; private set; }

	public PathTokenId(PathTokenType pathType, string value)
	{
		PathType = pathType;
		Value = value;
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
		{
			return false;
		}
		if (!(obj is PathTokenId pathTokenId))
		{
			return false;
		}
		if (pathTokenId.PathType == PathType)
		{
			return pathTokenId.Value == Value;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return Value.GetHashCode() ^ PathType.GetHashCode();
	}
}
