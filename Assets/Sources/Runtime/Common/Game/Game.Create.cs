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
            var player1 = GameLayer.Actor.Create(Prefab.Player, new Vector3(1, 1));
            var player2 = GameLayer.Actor.Create(Prefab.Player, new Vector3(2, 1));

            player1.entity.ComponentPlayer().playerType = PlayerType.Player1;
            player2.entity.ComponentPlayer().playerType = PlayerType.Player2;

        }

    }
}