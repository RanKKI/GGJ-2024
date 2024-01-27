using Pixeye.Actors;
using UnityEngine;

public class ItemTimerBoom : Item
{

    protected override void Setup()
    {
        base.Setup();
        var sideEffect = entity.Set<ComponentSideEffect>();
        sideEffect.speed = 0.4f;

        var obj = entity.Get<ComponentItem>();
        obj.canSnatch = true;
        obj.canFire = false;
    }

}