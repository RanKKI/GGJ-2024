using System;
using System.Collections.Generic;
using System.Linq;
using Pixeye.Actors;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActorSpawner : Actor
{
    [SerializeField]
    public SerializableDictionary<GameObject, float> prefabs;
    public float spawnInterval = 1f;
    public float lifeTime = 10f;
    
    protected override void Setup()
    {
        base.Setup();
        // get prefabs from child
        var spawner = entity.Set<ComponentSpawner>();
        spawner.prefabDict = prefabs.ToDictionary(x => x.Key, x => x.Value);
        spawner.spawnInterval = spawnInterval;
        spawner.lifeTime = lifeTime;
        
        var spawnArea = GetComponent<BoxCollider2D>();
        if (spawnArea)
        {
            spawner.spawnPointFunc = () =>
            {
                var spawnPoint = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                    Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y), 0);
                return spawnPoint;
            };
        }
        else
        {
            // get a random point from all children
            var children = GetComponentsInChildren<Transform>();
            // exclude self
            children = children.Where(x => x != transform).ToArray();
            spawner.spawnPointFunc = () =>
            {
                var spawnPoint = children[Random.Range(0, children.Length)].position;
                return spawnPoint;
            };
        }
        
    }
}