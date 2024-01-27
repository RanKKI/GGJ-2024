using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using UnityEngine;


public class ComponentDamaging
{
    public List<Collider2D> colliders = new List<Collider2D>();
    
    public SignalChangeHealth signal;
}
 
#region HELPERS
 
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
static partial class Component
{
    public const string Damaging = "Game.Source.ComponentDamaging";
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref ComponentDamaging DamagingComponent(in this ent entity) =>
        ref Storage<ComponentDamaging>.components[entity.id];
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
sealed class StorageDamagingComponent : Storage<ComponentDamaging>
{
    public override ComponentDamaging Create() => new ComponentDamaging();
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
