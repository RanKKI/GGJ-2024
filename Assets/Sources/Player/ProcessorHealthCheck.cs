using System;
using Pixeye.Actors;

public class ProcessorHealthCheck : Processor, ITick, IReceive<SignalChangeDead>, IReceive<SignalBuffRemoved>
{
    readonly Group<ComponentObject, ComponentPlayer> source;


    public void Tick(float dt)
    {
        if (source.length <= 0) return;
        for (var i = 0; i < source.length; i++)
        {
            ref var entity = ref source.entities[i];
            var health = entity.ComponentHealth();
            if (health.count <= 0)
            {
                var cPlayer = entity.ComponentPlayer();
                var isDead = cPlayer.buffs.FindIndex(b => b.name == BuffName.dead) >= 0;
                if (isDead)
                {
                    continue;
                }
                GameLayer.Send(new SignalChangeDead { target = entity });
            }
        }

    }

    public void HandleSignal(in SignalChangeDead arg)
    {
        var target = arg.target;
        var cPlayer = target.ComponentPlayer();
        if (cPlayer == null) return;

        cPlayer.AddBuff(new Buff
        {
            name = BuffName.dead,
            duration = 6,
            vertigo = true,
        }.Start());
    }

    public void HandleSignal(in SignalBuffRemoved arg)
    {
        if (arg.buff.name != BuffName.dead) return;
        GameLayer.Send(new SignalChangeHealth { target = arg.player, count = 5 });
    }
}
