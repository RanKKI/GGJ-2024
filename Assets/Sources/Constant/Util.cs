using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Pixeye.Actors;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Sequence = Unity.VisualScripting.Sequence;

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
    
    /// <summary>
    /// flash every 0.5s
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="flashColor"></param>
    /// <param name="flashDuration"></param>
    public static void FlashSprite(this SpriteRenderer spriteRenderer, Color flashColor, float flashDuration)
    {
        // Create a sequence of tweens to flash the sprite
        DG.Tweening.Sequence flashSequence = DOTween.Sequence();
        const float flashInterval = 0.5f;
            
        int loopCount = Mathf.CeilToInt(flashDuration / flashInterval * 2);
        int intervalCount = 2 * loopCount + 1;
        float remainder = flashDuration % flashInterval;
        float divisor = flashDuration - remainder;
            
        // wait for a short time before starting the flash
        flashSequence.AppendInterval(remainder);
            
        for (int i = 0; i < loopCount; i++)
        {
            flashSequence.Append(spriteRenderer.DOColor(flashColor, divisor / intervalCount));
            // exclude the last loop
            if (i < loopCount - 1)
            {
                flashSequence.Append(spriteRenderer.DOColor(Color.white, divisor /intervalCount));
            }
            else
            {
                flashSequence.Append(spriteRenderer.DOColor(Color.white, 0));
            }
        }
            
        flashSequence.Play();
    }
    
}

public class ColliderEventHandler : MonoBehaviour
{
    public Action<Collider2D> OnTriggerEnterEvent;
    public Action<Collider2D> OnTriggerExitEvent;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }

    private void OnDisable()
    {
        OnTriggerEnterEvent = null;
        OnTriggerExitEvent = null;
    }
}

public static class ColliderEventExtension
{
    public static void OnTriggerExit2DEvent<T>(this T self, Action<Collider2D> onCollisionExit)
        where T : UnityEngine.Component
    {
        self.GetOrAddComponent<ColliderEventHandler>().OnTriggerExitEvent += onCollisionExit;
    }
        
    public static void OnTriggerExit2DEvent(this GameObject self, Action<Collider2D> onCollisionExit)
    {
        self.GetOrAddComponent<ColliderEventHandler>().OnTriggerExitEvent += onCollisionExit;
    }
    
    public static void OnTriggerEnter2DEvent<T>(this T self, Action<Collider2D> onCollisionEnter)
        where T : UnityEngine.Component
    {
        self.GetOrAddComponent<ColliderEventHandler>().OnTriggerEnterEvent += onCollisionEnter;
    }
    
    public static void OnTriggerEnter2DEvent(this GameObject self, Action<Collider2D> onCollisionEnter)
    {
        self.GetOrAddComponent<ColliderEventHandler>().OnTriggerEnterEvent += onCollisionEnter;
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