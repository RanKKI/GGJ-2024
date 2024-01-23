//  Project : 2D Roguelike Actors
// Contacts : @Alexeee#8796 - https://discord.gg/zAJn9SX

using Pixeye.Actors;

sealed class ProcessorCollider : Processor
{
	readonly Group<ComponentObject> source;

	public override void HandleEcsEvents()
	{
		foreach (ent entity in source.added)
		{
			var cObject = entity.ComponentObject();

			Physics.buffer.Insert(entity, cObject.collider.GetHashCode());
		}

		foreach (ent entity in source.removed)
		{
			var cObject = entity.ComponentObject();

			Physics.buffer.Remove(cObject.collider.GetHashCode());
		}
	}
}