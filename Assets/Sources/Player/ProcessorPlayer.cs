using System;
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
        if (!cPlayer.isActive)
        {
            return;
        }

        var cObject = entity.ComponentObject();

        Vector2 dir = GetBuffWalk(cPlayer);

        var vel = cPlayer.rigidbody.velocity;
        // Debug.Log(cPlayer.name + " vel " + vel);
        int x = Mathf.RoundToInt(dir.x);
        int y = Mathf.RoundToInt(vel.y * 10);
        cPlayer.ani.SetInteger("xDir", x);
        cPlayer.ani.SetInteger("yDir", y);
        entity.GetMono<SpriteRenderer>().flipX = x > 0;

        if (dir.x != 0)
        {
            cPlayer.lastHorDir = dir;
        }

        if (UseItem(cPlayer))
        {
            FireItem(ref entity);
        }

        if (IsPlayerDisabledByVertigo(cPlayer))
        {
            return;
        }

        cPlayer.lastDir = dir;

        if (dir.y == Vector2.up.y && cPlayer.canJump)
        {
            cPlayer.rigidbody.AddForce(Vector2.up * Config.JumpForce);
            cPlayer.canJump = false;
            cPlayer.rigidbody.ExcludeLayer(LayerMask.NameToLayer("GameBoard"));

            cPlayer.jumpObserver = Observer.Add(cPlayer, c => c.rigidbody.velocity.y, value =>
            {
                if (value < 0)
                {
                    cPlayer.rigidbody.IncludeLayer(LayerMask.NameToLayer("GameBoard"));
                    cPlayer.jumpObserver.Release();
                }
            });
            cPlayer.toggleJump = (collision) =>
            {
                cPlayer.canJump = true;
                cPlayer.onCollidedWithGround -= cPlayer.toggleJump;
            };
            cPlayer.onCollidedWithGround = cPlayer.toggleJump;
            GameLayer.Send(new SignalPlaySound
            {
                name = "jump",
                volume = 0.2f,
                pos = entity.transform.position,
            });
        }
        
        if (dir.y == Vector2.down.y)
        {
            cPlayer.canJump = false;
            cPlayer.rigidbody.ExcludeLayer(LayerMask.NameToLayer("GameBoard"));
            cPlayer.toggleJump = (collision) =>
            {
                cPlayer.rigidbody.IncludeLayer(LayerMask.NameToLayer("GameBoard"));
                cPlayer.onCollidedWithGround -= cPlayer.toggleJump;
                cPlayer.canJump = true;
            };
            cPlayer.onCollidedWithGround = cPlayer.toggleJump;
        }

        if (dir.x == 0) return; // no horizontal movement

        var walkSpeed = Config.Speed * GetPlayerSpeed(cPlayer);

        cPlayer.rigidbody.velocity = new Vector2(dir.x * walkSpeed, vel.y);

    }

    float GetPlayerSpeed(ComponentPlayer cPlayer)
    {
        float baseFactor = 1;
        if (cPlayer.item != null)
        {
            var cItem = cPlayer.item.entity.ComponentItem();
            for (var i = 0; i < cItem.onHoldBuffs.Length; i++)
            {
                var buff = cItem.onHoldBuffs[i];
                baseFactor *= buff.speed;
            }

        }
        for (var i = 0; i < cPlayer.buffs.Length; i++)
        {
            var buff = cPlayer.buffs[i];
            baseFactor *= buff.speed;
        }
        return baseFactor;

    }

    bool IsPlayerDisabledByVertigo(ComponentPlayer cPlayer)
    {
        return cPlayer.buffs.FindIndex(buff => buff.vertigo) >= 0;
    }

    Vector2 GetBuffWalk(ComponentPlayer cPlayer)
    {
        var idx = cPlayer.buffs.FindIndex(buff => buff.autoWalkDir != Vector2.zero);
        var dir = cPlayer.playerType == PlayerType.Player1 ? CheckInput1() : CheckInput2();
        if (idx >= 0)
        {
            var autoDir = cPlayer.buffs[idx].autoWalkDir;
            return new Vector2(autoDir.x, dir.y);
        }
        return dir;
    }

    void FireItem(ref ent entity)
    {
        var cPlayer = entity.ComponentPlayer();
        var item = cPlayer.item;
        if (item == null)
        {
            Debug.Log(cPlayer.name + ", try to fire, but no item");
            return;
        }

        Debug.Log(cPlayer.name + ", Send Fire signal");
        GameLayer.Send(new SignalFireItem
        {
            item = item.entity,
            holder = entity
        });
    }


    bool UseItem(ComponentPlayer cPlayer)
    {
        return Input.GetKeyDown(
            cPlayer.playerType == PlayerType.Player1 ? KeyCode.RightShift : KeyCode.E);
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
