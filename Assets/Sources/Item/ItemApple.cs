using DG.Tweening;
using Pixeye.Actors;

using UnityEngine;

public class ItemApple : Item
{

    public Rigidbody2D rb;
    public float velocity = 5f;
    public int recover = 3;

    protected override void Setup()
    {
        base.Setup();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * velocity;
    }

    protected override void SetTag()
    {
        entity.Set(Tag.ItemTrigger);
    }

    public override void OnOutOfScreen()
    {
        base.OnOutOfScreen();
        Dispose();
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