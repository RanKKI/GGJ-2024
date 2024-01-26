using System;
using Pixeye.Actors;

public class ProcessorItem : Processor, ITick
{

    readonly Group<ComponentObject> objects;

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
        var pos = entity.transform.localPosition;
        if (Math.Abs(pos.x) >= ScreenSize.width)
        {
            Item item = entity.GetMono<Item>();
            if (item != null)
            {
                item.OnOutOfScreen();
            }
        }
    }
}
