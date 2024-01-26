using Pixeye.Actors;
using UnityEngine;

public static partial class Game
{
    public static void InitComponentObject(this ent entity)
    {
        var cObject = entity.ComponentObject();

        cObject.position = entity.transform.position;
        cObject.collider = entity.GetMono<Collider2D>();
        // cObject.renderer = entity.GetMono<SpriteRenderer>("view");
    }


    public static void ChangeHealth(in ent entity, int count)
    {
        GameLayer.Send(new SignalChangeHealth
        {
            target = entity,
            count = count
        });
    }
    
    public static void ChangeHappiness(in ent entity, int count)
    {
        GameLayer.Send(new SignalChangeHappiness
        {
            target = entity,
            count = count
        });
    }
}