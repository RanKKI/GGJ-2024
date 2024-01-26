using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
 


public class ComponentHappiness
{
    public int count;
}
 
#region HELPERS
 
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
static partial class Component
{
    public const string Happiness = "ComponentHappiness";
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref ComponentHappiness ComponentHappiness(in this ent entity) =>
        ref Storage<ComponentHappiness>.components[entity.id];
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
sealed class StorageComponentHappiness : Storage<ComponentHappiness>
{
    public override ComponentHappiness Create() => new ComponentHappiness();
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