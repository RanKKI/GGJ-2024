using Pixeye.Actors;
using UnityEngine;

sealed class ActorPlayer : Actor
{

    public GameObject itemHolder;

    protected override void Setup()
    {
        var cPlayer = entity.Set<ComponentPlayer>();
        cPlayer.rigidbody = entity.GetMono<Rigidbody2D>(); ;

        entity.Set<ComponentObject>();
        entity.Set<ComponentHealth>();
        entity.Set<ComponentHappiness>();
        entity.InitComponentObject();
        entity.Set(Tag.Player);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        var entity = collision.gameObject.GetEntity();
        if (entity.Has(Tag.Item))
        {
            HoldItem(entity.GetMono<Item>());
        }
    }


    public void HoldItem(Item item)
    {
        if (item == null) return;
        var cPlayer = entity.ComponentPlayer();
        if (cPlayer.item != null) return;

        item.Reset();
        // var itemComponent = item.entity.ComponentItem();
        // if (itemComponent.isUsed) return;
        Debug.Log("HoldItem");
        cPlayer.item = item;
        item.transform.SetParent(itemHolder.transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

}