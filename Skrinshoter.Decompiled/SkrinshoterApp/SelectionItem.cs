namespace SkrinshoterApp;

public sealed class SelectionItem<T>
{
	public T Id { get; }

	public string Name { get; }

	public bool IsAccessible { get; }

	public SelectionItem(T id, string name, bool isAccessible)
	{
		Id = id;
		Name = name;
		IsAccessible = isAccessible;
	}
}
