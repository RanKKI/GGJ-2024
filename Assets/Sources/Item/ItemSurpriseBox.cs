using Pixeye.Actors;
using UnityEngine;

public class ItemSurpriseBox : ItemBanana
{
    protected override string GetName()
    {
        return "SurpriseBox";
    }

    protected override Buff[] BuffsWhenStepOn(ent targetPlayer)
    {

        var playerPos = targetPlayer.transform.localPosition;
        var itemPos = transform.localPosition;
        var dirX = playerPos.x > itemPos.x ? 1 : -1;
        return new Buff[] {
        new() {
            name = "SurpriseBox",
            duration = 2,
            speed = 1.3f,
            autoWalkDir = new Vector2(dirX, 0),
        }};
    }
}