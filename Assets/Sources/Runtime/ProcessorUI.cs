using Pixeye.Actors;

public class ProcessorUI : Processor, IReceive<SignalChangeHealth>, IReceive<SignalChangeHappiness>
{
    private ent hapObserver;

    public void HandleSignal(in SignalChangeHealth arg)
    {
        var entity = arg.target;
        var cHealth = entity.ComponentHealth();

        var health = cHealth.count += arg.count;

        if (entity.Has<ComponentPlayer>())
        {
            var hpBar = GameLayer.GetObj("HP Bar 1").GetComponent<HPBar>();
            hpBar.SetHealth(health);
        }
    }

    public void HandleSignal(in SignalChangeHappiness arg)
    {
        var entity = arg.target;
        var cHappiness = entity.ComponentHappiness();

        var happiness = cHappiness.count += arg.count;
        if (hapObserver.exist) return;

        if (entity.Has<ComponentPlayer>())
        {
            var hapBar = GameLayer.GetObj("HAP Bar 2").GetComponent<HAPBar>();
            hapBar.SetValue(happiness);
        }
    }
}
