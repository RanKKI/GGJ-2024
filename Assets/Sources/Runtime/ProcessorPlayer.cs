using Pixeye.Actors;
using UnityEngine;

public class ProcessorPlayer : Processor, ITick
{
    readonly Group<ComponentObject, ComponentPlayer> source;

    public void Tick(float dt)
    {
        if (source.length <= 0) return;
        for (var i = 0; i < source.length; i++)
        {
            ref var entity = ref source.entities[i];
            Process(ref entity, dt);
        }

    }

    void Process(ref ent entity, float dt)
    {

        var cPlayer = entity.ComponentPlayer();
        var cObject = entity.ComponentObject();

        var dir = cPlayer.playerType == PlayerType.Player1 ? CheckInput1() : CheckInput2();
        if (dir == default) return;

        if (dir == Vector2.up)
        {
            cPlayer.rigidbody.AddForce(Vector2.up * 200);
            return;
        }

        if (dir == Vector2.down)
        {
            cPlayer.rigidbody.AddForce(Vector2.down * 200);
            return;
        }

        var curr = entity.transform.position;
        var scale = 20;
        dir.Scale(new Vector2(dt * scale, dt * scale));
        var target = dir + (Vector2)curr;

        // var hasSolidColliderInPoint = Physics.HasSolidColliderInPoint(target, 1 << 10, out ent withEntity);

        // Debug.Log(hasSolidColliderInPoint);

        // if (hasSolidColliderInPoint) return;

        // if (withEntity.exist) return;

        Game.MoveTo(entity, target);

    }

    Vector2 CheckInput1()
    {
        var dir = default(Vector2);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            dir = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            dir = Vector2.down;
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = Vector2.left;
        else if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        return dir;
    }

    Vector2 CheckInput2()
    {
        var dir = default(Vector2);
        if (Input.GetKeyDown(KeyCode.W))
            dir = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S))
            dir = Vector2.down;
        else if (Input.GetKey(KeyCode.A))
            dir = Vector2.left;
        else if (Input.GetKey(KeyCode.D))
            dir = Vector2.right;
        return dir;
    }
}
