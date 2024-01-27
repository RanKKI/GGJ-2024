using System.Numerics;
using Pixeye.Actors;
using UnityEngine;
using Random = Pixeye.Actors.Random;


public class ProcessorSpawner : Processor, ITick
{
    readonly Group<ComponentSpawner> objects;

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
        if (entity == null || entity == default || !entity.transform) return;
        var spawner = entity.ComponentSpawner();
        float fdt = UnityEngine.Time.deltaTime;
        spawner.timeSinceLastSpawn += fdt;
        if (spawner.timeSinceLastSpawn < spawner.spawnInterval) return;
        
        // generate spawn point from a random point in spawn area
        UnityEngine.Vector3 spawnPoint = spawner.spawnPointFunc();
        
        // get a random prefab from prefabs
        var prefabs = spawner.prefabDict;
        var prefab = Util.GetRandomWeightedItem(prefabs);
        
        // spawn the prefab
        var item = Actor.Create(prefab.gameObject, spawnPoint);
        // Debug.Log("Spawned " + item.name + " at " + spawnPoint);
        spawner.timeSinceLastSpawn = 0;

        item.entity.Get<ComponentLife>().liveTime = 0;
        item.entity.Get<ComponentLife>().lifeTime = spawner.lifeTime;
    }
}