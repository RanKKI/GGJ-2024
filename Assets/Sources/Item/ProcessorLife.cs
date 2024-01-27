using Pixeye.Actors;
using UnityEngine;

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
        if (entity == null || entity == default || !entity.transform) return;
        var life = entity.ComponentLife();
        dt = UnityEngine.Time.deltaTime;
        float startFlashTime = life.lifeTime * 0.8f;
        if (life.liveTime < startFlashTime && life.liveTime + dt >= startFlashTime)
        {
            SpriteRenderer spriteRenderer = entity.transform.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.FlashSprite(Color.clear, life.lifeTime * 0.2f);
            }
        }
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