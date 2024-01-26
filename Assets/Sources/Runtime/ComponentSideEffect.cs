
using Pixeye.Actors;

public class ComponentSideEffect
{

	public float speed; // in percentage, e.g. 0.5 == 50%

}

#region HELPERS

static partial class Component
{
	public const string SideEffect = "ComponentSideEffect";

	public static ref ComponentSideEffect ComponentSideEffect(in this ent entity) =>
		ref Storage<ComponentSideEffect>.components[entity.id];
}

sealed class StorageComponentSideEffect : Storage<ComponentSideEffect>
{
	public override ComponentSideEffect Create() => new();

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