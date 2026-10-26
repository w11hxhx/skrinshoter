namespace SkrinshoterApp;

public sealed class PreprocessedFrame
{
	public int Width { get; }

	public int Height { get; }

	public int Stripes { get; }

	public uint[] RowHash { get; }

	public ulong[] Key { get; }

	internal PreprocessedFrame(int width, int height, int stripes, uint[] rowHash, ulong[] key)
	{
		Width = width;
		Height = height;
		Stripes = stripes;
		RowHash = rowHash;
		Key = key;
	}
}
