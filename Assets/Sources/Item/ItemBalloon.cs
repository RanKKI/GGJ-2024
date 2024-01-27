
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
        cPlayer.rigidbody.DOMoveY(targetY, randDuration).SetEase(easeMode).OnComplete(() =>
        {
            cPlayer.rigidbody.gravityScale = 1;
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
}