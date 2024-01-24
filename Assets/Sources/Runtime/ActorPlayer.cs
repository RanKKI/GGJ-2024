using Pixeye.Actors;
using UnityEngine;

sealed class ActorPlayer : Actor
{
    protected override void Setup()
    {
        var cPlayer = entity.Set<ComponentPlayer>();
        cPlayer.rigidbody = entity.GetMono<Rigidbody2D>(); ;

        entity.Set<ComponentObject>();
        entity.Set<ComponentHealth>();
        entity.InitComponentObject();

    }

}