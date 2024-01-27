using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using Pixeye.Actors;
using UnityEngine;


public class ComponentSpawner
{
    public Dictionary<GameObject, float> prefabDict;
    public BoxCollider2D spawnArea;
    public float timeSinceLastSpawn;
    public float spawnInterval;
    public float lifeTime;
}
 
#region HELPERS
 
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
static partial class Component
{
    public const string Spawner = "ComponentSpawner";
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref ComponentSpawner ComponentSpawner(in this ent entity) =>
        ref Storage<ComponentSpawner>.components[entity.id];
}

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
sealed class StorageComponentSpawner : Storage<ComponentSpawner>
{
    public override ComponentSpawner Create() => new ComponentSpawner();
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
 
