using System;
using System.Collections;
using Pixeye.Actors;
using UnityEngine;

public class ProcessorItem : Processor, ITick, IReceive<SignalHoldItem>,
    IReceive<SignalFireItem>, IReceive<SignalDisposeItem>, IReceive<SignalTouchItem>, IReceive<SignalTransport>
{

    readonly Group<ComponentObject> objects;
    readonly Group<ComponentObject, ComponentPlayer> players;

    public void Tick(float dt)
    {
        if (objects.length <= 0) return;
        for (var i = 0; i < objects.length; i++)
        {
            ref var entity = ref objects.entities[i];
            Process(ref entity, dt);
        }
    }

    void Process(ref ent entity, float dt)
    {
        if (entity == null || entity == default) return;
        // var pos = entity.transform.localPosition;
        // Camera camera = Camera.main;
        // if (Math.Abs(pos.x) >= ScreenSize.width)
        // {
        //     Item item = entity.GetMono<Item>();
        //     if (item != null)
        //     {
        //         item.OnOutOfScreen();
        //     }
        // }
    }

    public void HandleSignal(in SignalHoldItem arg)
    {
        var item = arg.item;
        var player = arg.holder;

        var now = UnityEngine.Time.timeAsDouble;

        var cItem = item.ComponentItem();
        var cPlayer = player.ComponentPlayer();

        Debug.Log(cPlayer.name + " try hold item " + cItem.name);
        var itemComponent = item.GetMono<Item>();

        if (cPlayer.item != null)
        {
            Debug.Log(cPlayer.name + " currently hold an item, skip");
            return;
        }

        if (cItem.holdAt > 0 && now - cItem.holdAt < 1)
        {
            return;
        }

        Debug.Log(cPlayer.name + " hold item " + cItem.name);

        if (cItem.holder != default)
        {
            var cPlayerOld = cItem.holder.ComponentPlayer();
            cPlayerOld.item = null;
        }

        cItem.isActive = true;
        cItem.holder = cItem.owner = player;
        cItem.holdAt = now;
        cPlayer.item = itemComponent;

        itemComponent.OnPickUp();

        var actor = player.GetMono<ActorPlayer>();

        itemComponent.SetConstraint(actor.itemHolder);
        itemComponent.transform.SetParent(actor.itemHolder.transform);
        itemComponent.transform.localPosition = Vector3.zero;
        itemComponent.transform.localRotation = Quaternion.identity;
    }

    public void HandleSignal(in SignalFireItem arg)
    {

        var item = arg.item;
        var player = arg.holder;

        var cItem = item.Get<ComponentItem>();

        if (!cItem.canFire) return;

        var cPlayer = player.ComponentPlayer();
        var itemComponent = item.GetMono<Item>();

        Debug.Log("itemComponent.Fire(cPlayer.dir);");
        itemComponent.RemoveConstraint();
        itemComponent.Fire(cPlayer.lastHorDir);

        cItem.holder = default;
        cPlayer.item = null;
    }

    public void HandleSignal(in SignalDisposeItem arg)
    {
        var item = arg.item;
        var cItem = item.Get<ComponentItem>();
        Debug.Log("SignalDisposeItem: " + cItem.name);
        if (cItem.holder != default)
        {
            var cPlayer = cItem.holder.ComponentPlayer();
            cPlayer.item = null;
        }
        cItem.owner = default;
        cItem.holder = default;
        cItem.isActive = false;
        item.Release();
        GameLayer.Destroy(arg.obj);
    }

    public void HandleSignal(in SignalTouchItem arg)
    {
        var cPlayer = arg.player.ComponentPlayer();
        var cItem = arg.item.ComponentItem();
        Debug.Log(cPlayer.name + " Has Touched" + cItem.name);
    }

    public void HandleSignal(in SignalTransport arg)
    {
        var playerType = arg.playerType;
        var playerIdx = players.entities.FindIndex(x => x.ComponentPlayer().playerType == playerType);
        if (playerIdx < 0) return;
        var player = players.entities[playerIdx];
        var transport = Game.Create.Transport();
        transport.TransportObjectTo(player, arg.position);
    }

}
