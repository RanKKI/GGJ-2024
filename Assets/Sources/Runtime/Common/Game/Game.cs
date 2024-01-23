using Pixeye.Actors;
using UnityEngine;

public static partial class Game
{
    public static void InitComponentObject(this ent entity)
    {
        var cObject = entity.ComponentObject();

        cObject.position = entity.transform.position;
        cObject.collider = entity.GetMono<Collider2D>();
        // cObject.renderer = entity.GetMono<SpriteRenderer>("view");
    }
}