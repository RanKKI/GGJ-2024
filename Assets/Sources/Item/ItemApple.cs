using Pixeye.Actors;

using UnityEngine;

public class ItemApple : Item
{

    public Rigidbody2D rb;
    public int recover = 3;

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
        Debug.Log("on Apple Hit Player");
        GameLayer.Send(new SignalChangeHealth
        {
            target = targetPlayer,
            count = recover,
        });
        Dispose();
    }

}