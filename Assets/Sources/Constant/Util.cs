using System;
using System.Collections.Generic;
using System.Linq;
using Pixeye.Actors;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// Get random items from list without repeating
    /// </summary>
    /// <param name="list"></param>
    /// <param name="count"></param>
    /// <param name="getWeight"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetRandomItems<T>(List<T> list, int count, Func<T, int> getWeight)
    {
        List<T> listCache = new List<T>(list);
        count = Mathf.Clamp(count, 0, listCache.Count);
        List<T> result = new List<T>();
        List<int> weights = listCache.Select(getWeight).ToList();
        for (int i = 0; i < count; i++)
        {
            int randomIndex = GetRandomWeightedIndex(weights);
            result.Add(listCache[randomIndex]);
            weights.RemoveAt(randomIndex);
            listCache.RemoveAt(randomIndex);
        }

        return result;
    }

    public static T GetRandomWeightedItem<T>(Dictionary<T, float> dict)
    {
        float totalWeight = dict.Values.Sum();
        float randomValue = UnityEngine.Random.Range(0, totalWeight);

        foreach (var pair in dict)
        {
            randomValue -= pair.Value;
            if (randomValue <= 0)
            {
                return pair.Key;
            }
        }

        // This should not happen if the total weight is correct
        throw new InvalidOperationException("Unable to retrieve a random key.");
    }
    
    public static int GetRandomWeightedIndex(List<int> list)
    {
        int totalWeight = list.Sum();
        int randomValue = Pixeye.Actors.Random.Range(0, totalWeight);

        for (int i = 0; i < list.Count; i++)
        {
            randomValue -= list[i];
            if (randomValue <= 0)
            {
                return i;
            }
        }

        // This should not happen if the total weight is correct
        throw new InvalidOperationException("Unable to retrieve a random key.");
    }
    
    public static void TryGetEntity(this GameObject obj, out ent entity)
    {
        obj.TryGetComponent<Actor>(out var actor);
        if (actor == null)
        {
            entity = default;
            return;
        }
        entity = actor.entity;
    }
}

public static class LayerUtil
{
    public static void IncludeLayer(this Rigidbody2D rb, int layer)
    {
        int layerMask = 1 << layer;
        rb.includeLayers |= layerMask;
        rb.excludeLayers &= ~(layerMask);
    }
        
    public static void ExcludeLayer(this Rigidbody2D rb, int layer)
    {
        int layerMask = 1 << layer;
        rb.excludeLayers |= layerMask;
        rb.includeLayers &= ~(layerMask);
    }
        
    public static void IncludeLayer(this Collider2D col, int layer)
    {
        int layerMask = 1 << layer;
        col.includeLayers |= layerMask;
        col.excludeLayers &= ~(layerMask);
    }
        
    public static void ExcludeLayer(this Collider2D col, int layer)
    {
        int layerMask = 1 << layer;
        col.excludeLayers |= layerMask;
        col.includeLayers &= ~(layerMask);
    }
}