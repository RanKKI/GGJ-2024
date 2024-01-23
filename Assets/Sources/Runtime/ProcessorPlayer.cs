using Pixeye.Actors;
using UnityEngine;

public class ProcessorPlayer : Processor, ITick
{
    readonly Group<ComponentObject, ComponentPlayer> source;

    public void Tick(float dt)
    {
        if (source.length <= 0) return;
        ref var entity = ref source[0];

        var cPlayer = entity.ComponentPlayer();
        var cObject = entity.ComponentObject();
        var dir = CheckInput();
        if (dir == default) return;

        var target = dir + new Vector2(cObject.position.x, cObject.position.y);

        var hasSolidColliderInPoint = Physics.HasSolidColliderInPoint(target, 1 << 10, out ent withEntity);

        if (hasSolidColliderInPoint) return;

        // if (withEntity.exist) return;

        Game.MoveTo(entity, target);

    }

    Vector2 CheckInput()
    {
        var dir = default(Vector2);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            dir = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            dir = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            dir = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            dir = Vector2.right;
        return dir;
    }
}
