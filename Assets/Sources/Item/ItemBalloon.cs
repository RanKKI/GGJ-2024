
using System;
using DG.Tweening;
using Pixeye.Actors;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemBalloon : Item
{
    public float speedModifier = 0.7f;
    public float minDuration = 1.5f;
    public float maxDuration = 3f;
    public Ease easeMode = Ease.InCubic;
    private Action<Collision2D> handleFallCollision;
    private Animator ani;

    protected override void Setup()
    {
        base.Setup();
        var obj = entity.Get<ComponentItem>();
        obj.canFire = false;
        Buff[] buffs = {
            new() {
                speed = speedModifier,
            }
        };
        obj.onHoldBuffs = buffs;
        ani = GetComponent<Animator>();
        ani.enabled = false;
    }

    public override void OnPickUp()
    {
        base.OnPickUp();
        // target position being the top of the screen
        var screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1, 0)).y;
        var randDuration = Random.Range(minDuration, maxDuration);
        var targetY = randDuration / maxDuration * screenTop;
        var holder = entity.Get<ComponentItem>().holder;
        var cPlayer = holder.Get<ComponentPlayer>();
        cPlayer.rigidbody.gravityScale = 0;
        cPlayer.rigidbody.velocity = Vector2.zero;
        holder.transform.DOMoveY(targetY, randDuration).SetEase(easeMode).OnComplete(OnMoveEnd);
    }

    void OnMoveEnd()
    {
        var holder = entity.Get<ComponentItem>().holder;
        var cPlayer = holder.Get<ComponentPlayer>();
        cPlayer.rigidbody.gravityScale = 1;
        handleFallCollision = collision =>
        {
            FallingDamage(holder, collision.relativeVelocity.y);
            cPlayer.onCollidedWithGround -= handleFallCollision;
        };
        cPlayer.onCollidedWithGround += handleFallCollision;
        GameLayer.Send(new SignalChangeHappiness
        {
            count = 1,
            target = holder,
        });
        ani.enabled = true;
        // set the parent to the root of the scene
        transform.SetParent(null);
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