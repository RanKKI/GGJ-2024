using Pixeye.Actors;
using UnityEngine;

public class Item : Actor
{

    protected override void Setup()
    {
        Debug.Log("Item: Setup");
        entity.Set<ComponentObject>();
        entity.InitComponentObject();
        entity.Get<ComponentObject>().name = "Item";
        entity.Set(Tag.Item);
    }


    public virtual void Fire()
    {
        Debug.Log("ItemBoomerang: Start");
    }

}