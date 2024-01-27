﻿using System.Collections.Generic;
using System.Linq;
using Pixeye.Actors;
using UnityEngine;

public class ActorSpawner : Actor
{
    [SerializeField]
    public SerializableDictionary<GameObject, float> prefabs;
    public BoxCollider2D spawnArea;
    public float spawnInterval = 1f;
    public float lifeTime = 10f;
    
    protected override void Setup()
    {
        base.Setup();
        // get prefabs from child
        spawnArea = GetComponent<BoxCollider2D>();
        var spawner = entity.Set<ComponentSpawner>();
        spawner.prefabDict = prefabs.ToDictionary(x => x.Key, x => x.Value);
        spawner.spawnArea = spawnArea;
        spawner.spawnInterval = spawnInterval;
        spawner.lifeTime = lifeTime;
    }
}