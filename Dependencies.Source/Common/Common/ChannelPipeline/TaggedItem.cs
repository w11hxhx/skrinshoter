namespace Common.ChannelPipeline;

public readonly struct TaggedItem<TItem, TTag>
{
	public TItem Item { get; }

	public TTag Tag { get; }

	internal TaggedItem(TItem item, TTag tag)
	{
		Item = item;
		Tag = tag;
	}
}
public static class TaggedItem
{
	public static TaggedItem<TItem, TTag> Create<TItem, TTag>(TItem item, TTag tag)
	{
		return new TaggedItem<TItem, TTag>(item, tag);
	}
}
