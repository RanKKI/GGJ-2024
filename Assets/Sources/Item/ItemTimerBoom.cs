using System.Collections;
using UnityEngine;

public class ItemTimerBoom : Item
{
    public float minDuration = 6f;
    public float maxDuration = 9f;
    public float damage = 2f;
    public int happiness = 6;
    
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
        float randDuration = Random.Range(minDuration, maxDuration);
        StartCoroutine(DelayTrigger(randDuration));
    }

    private IEnumerator DelayTrigger(float duration)
    {
        yield return new WaitForSeconds(duration);
        Trigger();
    }

    private void Trigger()
    {
        var obj = entity.Get<ComponentItem>();
        PlayAnimator(() =>
        {
            GameLayer.Send(new SignalChangeHealth
            {
                count = -damage,
                target = obj.holder,
            });
            GameLayer.Send(new SignalChangeHappiness
            {
                count = happiness,
                target = obj.holder,
            });
            Dispose();
        });
        GameLayer.Send(new SignalPlaySound
        {
            name = "bomb",
            volume = 1,
            pos = transform.position,
        });
    }

}