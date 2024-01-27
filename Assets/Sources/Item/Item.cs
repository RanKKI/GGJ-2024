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

        var cItem = entity.Get<ComponentItem>();
        cItem.name = GetName();
    }

    protected virtual string GetName()
    {
        return "";
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

    public virtual void OnPickUp()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        var cItem = entity.Get<ComponentItem>();
        if (!cItem.isActive)
        {
            return;
        }
        // if (cItem.holder != default) return;

        var otherEntity = collision.gameObject.GetEntity();
        if (otherEntity.Has(Tag.Player))
        {
            OnHitPlayer(otherEntity);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("GameBoard"))
        {
            OnHitGround();
        }
    }

    protected virtual void OnHitGround()
    {

    }

    protected virtual void OnHitPlayer(ent player)
    {

    }

}