using Pixeye.Actors;

using UnityEngine;

public class ItemBanana : Item
{

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
        var targetDir = new Vector2(dir.normalized.x, 0.5f);
        rb.AddForce(targetDir * 100);

        return true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var otherEntity = collision.gameObject.GetEntity();
        if (otherEntity.Has(Tag.Player))
        {
            OnStepOn(otherEntity);
        }
    }

    protected void OnStepOn(ent targetPlayer)
    {
        if (entity.Has(Tag.Item))
        {
            return;
        }
        Buff[] buffs = {
                Buff.banana,
            };
        GameLayer.Send(new SignalBuffAdded
        {
            player = targetPlayer,
            buffs = buffs,
        });
        GameLayer.Send(new SignalDisposeItem
        {
            item = entity,
            obj = gameObject,
        });
    }

}