using Pixeye.Actors;
using UnityEngine;

sealed class ActorPlayer : Actor
{

    public GameObject itemHolder;

    protected override void Setup()
    {
        var cPlayer = entity.Set<ComponentPlayer>();
        cPlayer.rigidbody = entity.GetMono<Rigidbody2D>(); ;
        cPlayer.col = entity.GetMono<Collider2D>();
        cPlayer.ani = entity.GetMono<Animator>();

        void Callback(bool action)
        {
            Debug.Log(cPlayer.name + " isActive changed " + action);
            cPlayer.col.enabled = action;
            cPlayer.rigidbody.simulated = action;
        }

        cPlayer.observer = GameLayer.Observer.Add(cPlayer, src => src.isActive, Callback);

        entity.Set<ComponentObject>();
        entity.Set<ComponentHealth>();
        var cHap = entity.Set<ComponentHappiness>();
        entity.InitComponentObject();
        entity.Set(Tag.Player);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollisionWithItem(collision.collider);
        var cPlayer = entity.Get<ComponentPlayer>();
        if (collision.relativeVelocity.y > 0 && Mathf.Abs(cPlayer.rigidbody.velocity.y) < 0.1f)
        {
            cPlayer.onCollidedWithGround?.Invoke(collision);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollisionWithItem(collision);
    }

    void HandleCollisionWithItem(Collider2D collision)
    {
        var cPlayer = entity.ComponentPlayer();
        Debug.Log(name + " Collision " + collision.gameObject.name);
        Debug.Log(name + " Hold " + cPlayer.item);
        collision.gameObject.TryGetEntity(out var otherEntity);
        bool isSuccessHoldItem = false;
        ent itemEnt = default;
        if (otherEntity.Has(Tag.Item) || otherEntity.Has(Tag.ItemTrigger))
        {
            itemEnt = otherEntity;
            if (otherEntity.Has(Tag.Item) && cPlayer.item == null)
            {
                var cItem = otherEntity.Get<ComponentItem>();
                isSuccessHoldItem = !cItem.isActive;
            }
        }
        else if (otherEntity.Has(Tag.Player))
        {
            var item = StealItemFromPlayer(otherEntity);
            isSuccessHoldItem = item != default;
            itemEnt = item;
        }

        if (itemEnt == default) return;

        if (isSuccessHoldItem)
        {
            GameLayer.Send(new SignalHoldItem
            {
                item = itemEnt,
                holder = entity
            });
        }
        else
        {
            GameLayer.Send(new SignalTouchItem
            {
                item = itemEnt,
                player = entity
            });
        }
    }

    public ent StealItemFromPlayer(ent otherEntity)
    {
        // Check if can steal
        var cPlayer = otherEntity.Get<ComponentPlayer>();
        if (cPlayer == null) return default;
        var item = cPlayer.item;
        if (item == null) return default;
        var cItem = item.entity.Get<ComponentItem>();
        if (cItem == null) return default;
        if (!cItem.canSnatch) return default;
        return item.entity;
    }


}