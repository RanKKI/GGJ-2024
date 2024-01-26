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

        if (useItem())
        {
            FireItem(ref entity);
        }

        if (dir == default) return;

        cPlayer.dir = dir;

        if (dir == Vector2.up)
        {
            cPlayer.rigidbody.AddForce(Vector2.up * Config.JumpForce);
            return;
        }

        if (dir == Vector2.down)
        {
            Game.ChangeHealth(entity, -1);
            Game.ChangeHappiness(entity, -1);
            return;
        }

        float walkSpeedSideEffect = 1;
        if (cPlayer.item != null)
        {
            var sideEffect = cPlayer.item.entity.ComponentSideEffect();
            walkSpeedSideEffect = sideEffect.speed;
        }
        var walkSpeed = Config.Speed * walkSpeedSideEffect;

        var curr = entity.transform.position;
        var target = (walkSpeed * dt * dir) + (Vector2)curr;

        Game.MoveTo(entity, target);

    }

    void FireItem(ref ent entity)
    {
        var cPlayer = entity.ComponentPlayer();
        var cObject = entity.ComponentObject();

        var item = cPlayer.item;
        if (item == null) return;
        cPlayer.item = null;

        item.Fire(cPlayer.dir);
    }


    bool useItem()
    {
        return Input.GetKeyDown(KeyCode.Space);
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
