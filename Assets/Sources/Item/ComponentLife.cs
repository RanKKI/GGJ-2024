using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
 

public class ComponentLife
{
    public float lifeTime;
    public float liveTime;
}
 
#region HELPERS
 
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
static partial class Component
{
    public const string Life = "Game.Source.ComponentLife";
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref ComponentLife ComponentLife(in this ent entity) =>
        ref Storage<ComponentLife>.components[entity.id];
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
sealed class StorageComponentLife : Storage<ComponentLife>
{
    public override ComponentLife Create() => new ComponentLife();
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
 
