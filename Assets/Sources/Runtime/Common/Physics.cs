using Pixeye.Actors;
using Pixeye.Source;
using UnityEngine;

public static class Physics
{
    public static readonly BufferSortedEntities buffer = new BufferSortedEntities(256);

    // public static readonly RaycastHit2D[] hits = new RaycastHit2D[64];
    // public static readonly Collider2D[] colliders = new Collider2D[64];

    public static int OverlapPoint2D(Vector2 pos, int mask, ent[] entities, float min = float.NegativeInfinity, float max = float.PositiveInfinity)
    {
        Collider2D[] colliders = new Collider2D[entities.Length];
        for (var i = 0; i < entities.Length; i++)
        {
            var entity = entities[i];
            if (entity.Has<ComponentObject>())
            {
                var cObject = entity.ComponentObject();
                colliders[i] = cObject.collider;
            }
            else
            {
                colliders[i] = null;
            }
        }
        return Physics2D.OverlapPointNonAlloc(pos, colliders, mask, min, max);
    }

    public static bool HasSolidColliderInPoint(Vector2 pos, int mask, ent[] entities, out ent entity)
    {
        entity = default;
        var hit = OverlapPoint2D(pos, mask, entities);

        if (hit > 0)
        {
            entity = entities[hit];
            return true;
            // var index = HelperArray.BinarySearch(ref buffer.pointers, colliders[0].GetHashCode(), 0, buffer.length);
            // if (index != -1)
            //     entity = buffer.entities[index];
            // if (colliders[0].isTrigger)
            //     return false;

        }

        return false;
    }
}