using Pixeye.Actors;

public class ProcessorUI : Processor, IReceive<SignalChangeHealth>
{

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

}
