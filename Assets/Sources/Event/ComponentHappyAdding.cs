using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using UnityEngine;


public class ComponentHappyAdding
{
    public List<Collider2D> colliders = new List<Collider2D>();
    public SignalChangeHappiness signal;
    
}
 
#region HELPERS
 
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
static partial class Component
{
    public const string HappyAdding = "Game.Source.ComponentHappyAdding";
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref ComponentHappyAdding ComponentHappyAdding(in this ent entity) =>
        ref Storage<ComponentHappyAdding>.components[entity.id];
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
sealed class StorageComponentHappyAdding : Storage<ComponentHappyAdding>
{
    public override ComponentHappyAdding Create() => new ComponentHappyAdding();
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
