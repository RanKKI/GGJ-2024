using Pixeye.Actors;
using UnityEngine;

sealed class Prefab
{
    public static GameObject Player1 = Box.Get<GameObject>("Prefabs/Player1");
    public static GameObject Player2 = Box.Get<GameObject>("Prefabs/Player2");
    public static GameObject Transport = Box.Get<GameObject>("Prefabs/Transport");

}