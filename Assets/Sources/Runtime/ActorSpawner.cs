using System.Collections.Generic;
using System.Linq;
using Pixeye.Actors;
using UnityEngine;

public class ActorSpawner : Actor
{
    public List<Item> prefabs;
    
    protected override void Setup()
    {
        base.Setup();
        // get prefabs from child
        prefabs = GetComponentsInChildren<Item>().ToList();
    }
}