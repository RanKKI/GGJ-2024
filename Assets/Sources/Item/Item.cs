using Pixeye.Actors;
using UnityEngine;

public class Item : Actor
{
    protected override void Setup()
    {
        Debug.Log("Item: Setup");
        var obj = entity.Set<ComponentObject>();
        obj.name = "Item";
        entity.InitComponentObject();
        entity.Set<ComponentItem>();
        entity.Set(Tag.Item);
    }

    public virtual void Fire(Vector2 dir)
    {
        entity.Get<ComponentItem>().isUsed = true;
        Debug.Log("ItemBoomerang: Fire");
    }

    public virtual void OnOutOfScreen()
    {

    }


    public virtual void Reset() {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var entity = collision.gameObject.GetEntity();
        if (entity.Has(Tag.Player))
        {
            OnHit(ref entity);
        }
    }

    protected virtual void OnHit(ref ent entity)
    {

    }

}