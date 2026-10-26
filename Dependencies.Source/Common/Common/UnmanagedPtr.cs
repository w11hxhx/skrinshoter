namespace Common;

public readonly struct UnmanagedPtr<T> where T : unmanaged
{
	public unsafe T* Ptr { get; }

	public unsafe UnmanagedPtr(T* ptr)
	{
		Ptr = ptr;
	}

	public unsafe static implicit operator UnmanagedPtr<T>(T* ptr)
	{
		return new UnmanagedPtr<T>(ptr);
	}
}
