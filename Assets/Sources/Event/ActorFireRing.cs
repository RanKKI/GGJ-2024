using System.Collections.Generic;
using Pixeye.Actors;
using UnityEngine;


public class ActorFireRing : Actor
{
    public List<Collider2D> happyAddingColliders = new List<Collider2D>();
    public List<Collider2D> damagingColliders = new List<Collider2D>();
    public SignalChangeHappiness signalChangeHappiness;
    public SignalChangeHealth signalChangeHealth;
    
    protected override void Setup()
    {
        base.Setup();
        var obj = entity.Set<ComponentObject>();
        var cHappyAdding = entity.Set<ComponentHappyAdding>();
        cHappyAdding.signal = signalChangeHappiness;
        cHappyAdding.colliders = happyAddingColliders;
        var cDamaging = entity.Set<ComponentDamaging>();
        cDamaging.signal = signalChangeHealth;
        cDamaging.colliders = damagingColliders;
        foreach (var col in happyAddingColliders)
        {
            col.OnTriggerEnter2DEvent(enteringCol =>
            {
                enteringCol.gameObject.TryGetEntity(out var playerEntity);
                if (playerEntity.Has<ComponentPlayer>())
                {
                    HappyAdding(playerEntity);
                    transform.gameObject.Release();
                    Destroy(this);
                }
            });
        }
        foreach (var col in damagingColliders)
        {
            col.OnTriggerEnter2DEvent(enteringCol =>
            {
                enteringCol.gameObject.TryGetEntity(out var playerEntity);
                if (playerEntity.Has<ComponentPlayer>())
                {
                    HappyAdding(playerEntity);
                    Damaging(playerEntity);
                    transform.gameObject.Release();
                    Destroy(this);
                }
            });
        }
    }
    
    void HappyAdding(ent playerEntity)
    {
        signalChangeHappiness.target = playerEntity;
        GameLayer.Send(signalChangeHappiness);
    }
    
    void Damaging(ent playerEntity)
    {
        signalChangeHealth.target = playerEntity;
        GameLayer.Send(signalChangeHealth);
    }
}