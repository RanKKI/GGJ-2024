using Pixeye.Actors;
using UnityEngine;

public class ItemTimerBoom : Item
{

    protected override void Setup()
    {
        base.Setup();
        var obj = entity.Get<ComponentItem>();
        obj.canSnatch = true;
        obj.canFire = false;

        Buff[] buffs = {
            new() {
                speed = 0.4f,
            }
        };
        obj.onHoldBuffs = buffs;
    }

}