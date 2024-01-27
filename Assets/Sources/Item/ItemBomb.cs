using Pixeye.Actors;

using UnityEngine;

public class ItemBomb : Item
{

    public Rigidbody2D rb;
    public int damage = 5;
    public int happiness = 3;

    protected override void Setup()
    {
        base.Setup();
    }

    protected override void SetTag()
    {
        entity.Set(Tag.ItemTrigger);
    }

    public override void OnOutOfScreen()
    {
        base.OnOutOfScreen();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetEntity(out var otherEntity);
        if (otherEntity.Has(Tag.Player))
        {
            OnHitPlayer(otherEntity);
        }
    }

    protected override void OnHitPlayer(ent targetPlayer)
    {
        Debug.Log("on Bomb Hit Player");
        GameLayer.Send(new SignalChangeHealth
        {
            target = targetPlayer,
            count = -damage,
        });
        GameLayer.Send(new SignalChangeHappiness
        {
            target = targetPlayer,
            count = happiness,
        });
        GameLayer.Send(new SignalPlaySound
        {
            name = "bomb",
            volume = 1,
            pos = transform.position,
        });
        Dispose();
    }

}