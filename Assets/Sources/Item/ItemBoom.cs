using Pixeye.Actors;
using UnityEngine;

public class ItemBoom : Item
{

    protected override void Setup()
    {
        base.Setup();
        var sideEffect = entity.Set<ComponentSideEffect>();
        sideEffect.speed = 0.4f;
    }

    public override void Fire(Vector2 dir)
    {
        base.Fire(dir);
    }

    public override void Reset()
    {
        base.Reset();
    }

    public override void OnOutOfScreen()
    {
        base.OnOutOfScreen();
    }

    protected override void OnHit(ref ent entity)
    {
        base.OnHit(ref entity);
    }

}