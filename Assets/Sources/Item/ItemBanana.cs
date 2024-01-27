using System;
using Pixeye.Actors;

using UnityEngine;

public class ItemBanana : Item
{
    public float faintTime = 2f;
    public int happiness = 2;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;

    protected override string GetName()
    {
        return "Banana";
    }

    public override bool Fire(Vector2 dir)
    {
        Debug.Log("Fire Banana");
        base.Fire(dir);
        entity.Remove(Tag.Item);
        entity.Set(Tag.ItemTrigger);
        rb.gravityScale = 0.5f;
        boxCollider.isTrigger = false;

        GameObject parent = transform.parent.gameObject;
        if (parent == null) return false;
        transform.SetParent(null);
        transform.position = parent.transform.position + (Vector3)dir;

        // add force to 45deg
        var targetDir = new Vector2(Math.Sign(dir.x), 1f);
        rb.AddForce(targetDir * 100);

        return true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetEntity(out var otherEntity);
        if (otherEntity.Has(Tag.Player))
        {
            OnStepOn(otherEntity);
        }
    }

    protected virtual bool OnStepOn(ent targetPlayer)
    {
        if (entity.Has(Tag.Item))
        {
            return false;
        }
        SendKarma(targetPlayer);
        GameLayer.Send(new SignalBuffAdded
        {
            player = targetPlayer,
            buffs = BuffsWhenStepOn(targetPlayer),
        });
        AfterStepOn();
        return true;
    }

    protected virtual void AfterStepOn()
    {
        Dispose();
    }

    protected virtual void SendKarma(ent player)
    {
        var cItem = entity.ComponentItem();
        GameLayer.Send(new SignalChangeHappiness
        {
            target = cItem.owner,
            count = cItem.owner == player ? 0 : happiness,
        });
    }

    protected virtual Buff[] BuffsWhenStepOn(ent targetPlayer)
    {
        Buff buff = Buff.banana;
        buff.duration = faintTime;
        return new Buff[] { buff };
    }

}