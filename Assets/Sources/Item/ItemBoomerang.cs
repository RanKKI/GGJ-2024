using DG.Tweening;
using Pixeye.Actors;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Sequence = DG.Tweening.Sequence;

public class ItemBoomerang : Item
{
    public int happinessHitSelf = 4;
    public int happinessHitOther = 2;
    public Rigidbody2D rigidbody;

    protected SpriteRenderer sp;

    public float rotateInterval = 0.5f;
    private Tween rotateTween;

    protected override void Setup()
    {
        base.Setup();
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    public override bool Fire(Vector2 dir)
    {
        base.Fire(dir);
        GameObject parent = transform.parent.gameObject;
        if (parent == null) return false;
        transform.SetParent(null);
        bool left = false;
        var lastDir = entity.Get<ComponentItem>().holder.Get<ComponentPlayer>().lastDir;
        if (dir.x != 0)
        {
            left = dir.x < 0;
        }
        else if (lastDir.x != 0)
        {
            left = lastDir.x < 0;
        }
        
        Vector2 xDir = left ? Vector2.left : Vector2.right;
        rigidbody.AddForce(xDir * 200);
        transform.position = parent.transform.position + (Vector3)xDir;
        rotateTween = sp.transform.DORotate(new Vector3(0f, 0f, 360f), rotateInterval, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart).Play();
        
        GameLayer.Send(new SignalPlaySound
        {
            name = "boomerang",
            volume = 3,
            pos = transform.position,
        });
        
        return true;
    }

    public override void OnPickUp()
    {
        base.OnPickUp();
        rotateTween?.Kill();
        UpdateVelocity(Vector2.zero);
    }

    private int bounceCount;
    
    public override void OnOutOfScreen()
    {
        base.OnOutOfScreen();
        if (bounceCount >= 1)
        {
            Dispose();
            return;
        }
        Vector2 reverseDir = rigidbody.velocity;
        sp.flipX = !sp.flipX;
        reverseDir.Scale(new Vector2(-1, 0));
        UpdateVelocity(reverseDir);
        bounceCount++;
    }

    private void UpdateVelocity(Vector2 vec)
    {
        DOTween.Sequence().AppendInterval(1f).OnComplete(() =>
        {
            GameLayer.Send(new SignalPlaySound
            {
                name = "boomerang",
                volume = 3,
                pos = transform.position,
            });
        }).Play();
        
        rigidbody.velocity = vec;
    }

    protected override void OnHitPlayer(ent targetPlayer)
    {
        base.OnHitPlayer(targetPlayer);
        var cItem = entity.ComponentItem();
        GameLayer.Send(new SignalChangeHappiness
        {
            count = targetPlayer == cItem.owner ? happinessHitSelf : happinessHitOther,
            target = cItem.owner,
        });
        var targetRigidbody2D = targetPlayer.GetMono<Rigidbody2D>();
        if (targetRigidbody2D != null)
        {
            targetRigidbody2D.AddForce(rigidbody.velocity * 100);
        }
        Dispose();
    }

}