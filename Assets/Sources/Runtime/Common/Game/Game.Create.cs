using UnityEngine;

public partial class Game
{

    public static class Create
    {

        public static void Board()
        {
            var entity = GameLayer.Actor.Create(Prefab.Player, new Vector3(1, 1)).entity;

            Debug.Log("Created Player entity");
        }

    }
}