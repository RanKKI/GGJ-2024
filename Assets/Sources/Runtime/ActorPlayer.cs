using Pixeye.Actors;
using UnityEngine;

sealed class ActorPlayer : Actor
{
    protected override void Setup()
    {
        var cPlayer = entity.Set<ComponentPlayer>();
        entity.Set<ComponentObject>();
        entity.InitComponentObject();
        cPlayer.rigidbody = entity.GetMono<Rigidbody2D>(); ;
    }

}