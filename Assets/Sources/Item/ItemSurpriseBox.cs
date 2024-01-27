using Pixeye.Actors;
using UnityEngine;

public class ItemSurpriseBox : ItemBanana
{
    protected override string GetName()
    {
        return "SurpriseBox";
    }


    protected override bool OnStepOn(ent targetPlayer)
    {
        if (entity.Has(Tag.Item))
        {
            return false;
        }
        SendKarma(targetPlayer);

        GameLayer.Send(new SignalPlaySound
        {
            name = "box_open",
            volume = 2,
            pos = transform.position,
        });
        PlayAnimator(0.8f, () => { Dispose(); SendBuff(targetPlayer); });
        return true;
    }

    protected override void AfterStepOn()
    {

    }

    protected override Buff[] BuffsWhenStepOn(ent targetPlayer)
    {

        var playerPos = targetPlayer.transform.localPosition;
        var itemPos = transform.localPosition;
        var dirX = playerPos.x > itemPos.x ? 1 : -1;
        return new Buff[] {
        new() {
            name = BuffName.surpriseBox,
            duration = 2,
            speed = 1.3f,
            autoWalkDir = new Vector2(dirX, 0),
        }};
    }
}