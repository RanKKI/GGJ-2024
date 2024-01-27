using System.Collections;
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


    public override void OnPickUp()
    {
        StartCoroutine(DelayTrigger(5f));
    }

    private IEnumerator DelayTrigger(float duration)
    {
        yield return new WaitForSeconds(duration);
        Trigger();
    }

    private void Trigger()
    {
        var obj = entity.Get<ComponentItem>();
        PlayAnimator(1.08f, () =>
        {
            Dispose();
            GameLayer.Send(new SignalChangeHealth
            {
                count = -5,
                target = obj.holder,
            });
        });
    }

}