using UnityEngine;

public class ItemHourglass : ItemBanana
{
    protected override string GetName()
    {
        return "ItemHourglass";
    }


    public override bool Fire(Vector2 dir)
    {
        PlayAnimator(1f, () => { }, 0.5f);
        return base.Fire(dir);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length <= 0) return;
        Dispose();
        var cItem = entity.Get<ComponentItem>();
        var player = cItem.owner.ComponentPlayer();
        GameLayer.Send(new SignalTransport
        {
            playerType = player.playerType == PlayerType.Player1 ? PlayerType.Player2 : PlayerType.Player1,
            position = collision.contacts[0].point
        });
    }

}