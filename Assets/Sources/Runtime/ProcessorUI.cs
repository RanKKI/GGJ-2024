using Pixeye.Actors;

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

        var id = GetPlayerID(entity);
        var health = cHealth.count += arg.count;

        if (entity.Has<ComponentPlayer>())
        {
            var hpBar = GameLayer.GetObj("HP Bar " + id).GetComponent<HPBar>();
            hpBar.SetHealth(health);
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
