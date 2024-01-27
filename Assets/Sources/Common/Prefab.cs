using Pixeye.Actors;
using UnityEngine;

sealed class Prefab
{
    public static GameObject Player = Box.Get<GameObject>("Prefabs/Player");
    public static GameObject Transport = Box.Get<GameObject>("Prefabs/Transport");

}