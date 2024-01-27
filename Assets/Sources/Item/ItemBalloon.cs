
using System;
using DG.Tweening;
using Pixeye.Actors;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemBalloon : Item
{
    public float speedModifier = 0.7f;
    public float minDuration = 1.5f;
    public float maxDuration = 3f;
    public Ease easeMode = Ease.InCubic;
    private Action<Collision2D> handleFallCollision;

    protected override void Setup()
    {
        base.Setup();
        var sideEffect = entity.Set<ComponentSideEffect>();
        sideEffect.speed = speedModifier;
        
        var obj = entity.Get<ComponentItem>();
        obj.canFire = false;
    }
    
    public override void OnPickUp()
    {
        base.OnPickUp();
        // target position being the top of the screen
        var screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0)).y;
        var randDuration = Random.Range(minDuration, maxDuration);
        var targetY = transform.position.y + randDuration / maxDuration * screenTop;
        var holder = entity.Get<ComponentItem>().holder;
        var cPlayer = holder.Get<ComponentPlayer>();
        cPlayer.rigidbody.gravityScale = 0;
        holder.transform.DOMoveY(targetY, randDuration).SetEase(easeMode).OnComplete(() =>
        {
            cPlayer.rigidbody.gravityScale = 1;
            handleFallCollision = collision =>
            {
                var fallSpeed = collision.relativeVelocity.y;
                if (fallSpeed > 0 && cPlayer.rigidbody.velocity.y == 0)
                {
                    FallingDamage(holder, collision.relativeVelocity.y);
                    cPlayer.onCollidedWithGround -= handleFallCollision;
                }
            };
            cPlayer.onCollidedWithGround += handleFallCollision;
            GameLayer.Send(new SignalChangeHappiness
            {
                count = 1,
                target = holder,
            });
            GameLayer.Send(new SignalDisposeItem
            {
                item = entity,
                obj = gameObject,
            });
        });
    }

    void FallingDamage(ent fallingEntity, float fallSpeed)
    {
        float damage = Mathf.Sqrt(fallSpeed) / 2f;
        damage = Mathf.Clamp(damage, 0, 2);
        Debug.Log("falling damage: " + damage);
        GameLayer.Send(new SignalChangeHealth
        {
            count = Mathf.RoundToInt(-damage),
            target = fallingEntity,
        });
    }
}