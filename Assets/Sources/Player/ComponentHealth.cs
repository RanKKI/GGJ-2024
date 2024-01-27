using Pixeye.Actors;

public class ComponentHealth
{
    public float count = 10;
    public int maxHealth = 10;
}

#region HELPERS

static partial class Component
{
    public const string Health = "ComponentHealth";

    public static ref ComponentHealth ComponentHealth(in this ent entity) =>
        ref Storage<ComponentHealth>.components[entity.id];
}

sealed class StorageComponentHealth : Storage<ComponentHealth>
{
    public override ComponentHealth Create() => new();

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