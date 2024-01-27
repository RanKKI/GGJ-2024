using Pixeye.Actors;
using UnityEngine;

public class ProcessorUI : Processor, IReceive<SignalChangeHealth>, IReceive<SignalChangeHappiness>
{
    private ent hapObserver;


    private int GetPlayerID(ent entity)
    {
        var cPlayer = entity.ComponentPlayer();
        return cPlayer.playerType == PlayerType.Player1 ? 1 : 2;
    }

    public void HandleSignal(in SignalChangeHealth arg)
    {
        var entity = arg.target;
        var cHealth = entity.ComponentHealth();

        Debug.Log("Health Change by: " + cHealth);

        var id = GetPlayerID(entity);
        var newHealth = Mathf.Clamp(cHealth.count + arg.count, 0, cHealth.maxHealth); // bound the health
        cHealth.count = newHealth;

        if (entity.Has<ComponentPlayer>())
        {
            var hpBar = GameLayer.GetObj("HP Bar " + id).GetComponent<HPBar>();
            hpBar.SetHealth(cHealth.count);
        }
    }

    public void HandleSignal(in SignalChangeHappiness arg)
    {
        var entity = arg.target;
        var id = GetPlayerID(entity);
        var cHappiness = entity.ComponentHappiness();

        var happiness = cHappiness.count += arg.count;
        if (hapObserver.exist) return;

        if (entity.Has<ComponentPlayer>())
        {
            var hapBar = GameLayer.GetObj("HAP Bar " + id).GetComponent<HAPBar>();
            hapBar.SetValue(happiness);
        }
    }
}
