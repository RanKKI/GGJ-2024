using UnityEngine;

public partial class Game
{

    public static class Create
    {

        public static void Board(int columns = 32, int rows = 18)
        {
            Create.Player();
            Debug.Log("Created Player entity");
        }

        public static void Player()
        {
            GameLayer.Actor.Create(Prefab.Player, new Vector3(1, 1));
        }

    }
}