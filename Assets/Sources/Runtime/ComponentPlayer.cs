using System;
using System.Collections.Generic;
using Pixeye.Actors;
using UnityEngine;


public class ComponentPlayer
{

    public string name = "";
    public ent observer = default;

    public PlayerType playerType = default;

    public Rigidbody2D rigidbody;

    public Item item = default;

    public Vector2 dir = default;

    public Action<Collision2D> onCollidedWithGround;

    public List<Buff> buffs;

}

#region HELPERS

static partial class Component
{
    public const string Player = "ComponentPlayer";

    public static ref ComponentPlayer ComponentPlayer(in this ent entity) =>
        ref Storage<ComponentPlayer>.components[entity.id];
}

sealed class StorageComponentPlayer : Storage<ComponentPlayer>
{
    public override ComponentPlayer Create() => new ComponentPlayer();

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