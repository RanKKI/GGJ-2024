using DG.Tweening;
using Pixeye.Actors;

using UnityEngine;

public class ItemBomb : Item
{

    public Rigidbody2D rb;
    public float velocity = 5f;
    public int damage = 5;
    public int happiness = 3;
    private Animator ani;

    protected override void Setup()
    {
        base.Setup();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * velocity;
        ani = GetComponentInChildren<Animator>();
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
        Debug.Log("on Bomb Hit Player");
        ani.SetBool("explode", true);
        var duration = ani.GetCurrentAnimatorStateInfo(0).length;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        DOTween.Sequence().AppendInterval(duration).OnComplete(Dispose).Play();
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
    }

}