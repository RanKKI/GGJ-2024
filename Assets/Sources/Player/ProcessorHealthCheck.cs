using System;
using System.Collections.Generic;
using Pixeye.Actors;
using UnityEngine;

public class ProcessorHealthCheck : Processor, ITick, IReceive<SignalChangeDead>, IReceive<SignalBuffRemoved>
{
    readonly Group<ComponentObject, ComponentPlayer> source;


    public void Tick(float dt)
    {
        if (source.length <= 0) return;
        List<ent> winners = new List<ent>();
        for (var i = 0; i < source.length; i++)
        {
            ref var entity = ref source.entities[i];
            var health = entity.ComponentHealth();
            if (health.count <= 0)
            {
                var cPlayer = entity.ComponentPlayer();
                if (cPlayer.IsDead())
                {
                    continue;
                }
                GameLayer.Send(new SignalChangeDead { target = entity });
            }

            var happiness = entity.ComponentHappiness();
            if (happiness.count >= Config.MaxHappiness)
            {
                winners.Add(entity);
            }
        }
        GameLayer.Send(new SignalGameEnd { winner = winners.ToArray() });
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
        GameLayer.Send(new SignalChangeHealth { target = arg.player, count = 5, ignoreDeath = true});
    }
}
