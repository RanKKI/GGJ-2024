using System;
using DG.Tweening;
using Pixeye.Actors;
using UnityEngine;
using Random = Pixeye.Actors.Random;

public class ProcessorUI : Processor, IReceive<SignalChangeHealth>, IReceive<SignalChangeHappiness>
{
    private int GetPlayerID(ent entity)
    {
        var cPlayer = entity.ComponentPlayer();
        return cPlayer.playerType == PlayerType.Player1 ? 1 : 2;
    }

    public void HandleSignal(in SignalChangeHealth arg)
    {
        var entity = arg.target;

        var cPlayer = entity.ComponentPlayer();
        if (cPlayer.IsDead() && !arg.ignoreDeath)
        {
            return;
        }

        var cHealth = entity.ComponentHealth();

        Debug.Log("Health Change by: " + cHealth);

        var id = GetPlayerID(entity);
        var newHealth = Mathf.Clamp(cHealth.count + arg.count, 0, cHealth.maxHealth); // bound the health
        if (arg.count > 0 && Math.Abs(newHealth - cHealth.count) > 0.2f)
        {
            GameLayer.Send(new SignalPlaySound
            {
                name = "healing",
                volume = 2,
                pos = entity.transform.position,
            });
        }

        if (entity.Has<ComponentPlayer>())
        {
            var hpBar = GameLayer.GetObj("HP Bar " + id).GetComponent<HPBar>();
            hpBar.SetHealth(newHealth);
        }

        cHealth.count = newHealth;
    }

    public void HandleSignal(in SignalChangeHappiness arg)
    {
        var entity = arg.target;
        
        var cPlayer = entity.ComponentPlayer();
        if (cPlayer.IsDead())
        {
            return;
        }


        var id = GetPlayerID(entity);
        var cHappiness = entity.ComponentHappiness();
        var newHappiness = Mathf.Clamp(cHappiness.count + arg.count, 0, Config.MaxHappiness); // bound the happiness
        if (cHappiness.count < Config.MaxHappiness / 2 && newHappiness > Config.MaxHappiness / 2)
        {
            GameLayer.Send(new SignalPlaySound
            {
                name = "halfVictory",
                volume = 1,
                pos = entity.transform.position,
            });
            var gvc = GameLayer.GetObj("GlobalVolumeController").GetComponent<GlobalVolumeController>();
            float newSaturation = newHappiness / (float)Config.MaxHappiness;
            float value = -50 + newSaturation * 100;
            var saturation = gvc.colorAdjustments.saturation;
            DOTween.To(() => saturation.value, x => saturation.value = x, value, 3)
                .SetEase(Ease.Linear); // Optional callback when the animation completes
        }
        
        if (entity.Has<ComponentPlayer>())
        {
            var hapBar = GameLayer.GetObj("HAP Bar " + id).GetComponent<HAPBar>();
            hapBar.SetValue(newHappiness);
        }

        cHappiness.count = newHappiness;
    }
}
