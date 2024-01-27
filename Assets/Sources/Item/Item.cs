using Pixeye.Actors;
using UnityEngine;

public class Item : Actor
{


    protected override void Setup()
    {
        base.Setup();
        Debug.Log("Item: Setup");
        var obj = entity.Set<ComponentObject>();
        obj.name = "Item";
        entity.InitComponentObject();
        entity.Set<ComponentItem>();
        SetTag();
    }

    protected virtual void SetTag()
    {
        entity.Set(Tag.Item);
    }

    public virtual bool Fire(Vector2 dir)
    {
        return false;
    }

    public virtual void OnOutOfScreen()
    {

    }

    public virtual void Reset()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var cItem = entity.Get<ComponentItem>();
        if (!cItem.isActive)
        {
            return;
        }
        if (cItem.holder != default) return;

        var otherEntity = collision.gameObject.GetEntity();
        if (otherEntity.Has(Tag.Player))
        {
            OnHitPlayer(otherEntity);
        }
    }

    protected virtual void OnHitPlayer(ent player)
    {

    }

}