using Pixeye.Actors;

public class ComponentItem
{
	public bool isUsed;
	public bool canSnatch = false;
}

#region HELPERS

static partial class Component
{
	public const string Item = "ComponentItem";

	public static ref ComponentItem ComponentItem(in this ent entity) =>
		ref Storage<ComponentItem>.components[entity.id];
}

sealed class StorageComponentItem : Storage<ComponentItem>
{
	public override ComponentItem Create() => new();

	// Use for cleaning components that were removed at the current frame.
	public override void Dispose(indexes disposed)
	{
		foreach (var id in disposed)
		{
			ref var component = ref components[id];
		}
	}
}

#endregion