using Pixeye.Actors;

public class ProcessorLife : Processor, ITick
{
    readonly Group<ComponentLife> objects;
        
    public void Tick(float dt)
    {
        if (objects.length <= 0) return;
        for (var i = 0; i < objects.length; i++)
        {
            ref var entity = ref objects.entities[i];
            Process(ref entity, dt);
        }
    }
        
    void Process(ref ent entity, float dt)
    {
        if (entity == null || entity == default) return;
        var life = entity.ComponentLife();
        life.liveTime += dt;
        if (life.liveTime > life.lifeTime)
        {
            GameLayer.Send(new SignalDisposeItem
            {
                item = entity,
                obj = entity.transform.gameObject,
            });
        }
    }
}