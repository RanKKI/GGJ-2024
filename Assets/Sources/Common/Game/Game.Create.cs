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

            player1.gameObject.name = "Player1";
            player2.gameObject.name = "Player2";

            var cPlayer1 = player1.entity.ComponentPlayer();
            var cPlayer2 = player2.entity.ComponentPlayer();

            cPlayer1.name = "Player1";
            cPlayer1.playerType = PlayerType.Player1;

            cPlayer2.name = "Player2";
            cPlayer2.playerType = PlayerType.Player2;

        }

        public static Transport Transport()
        {
            var transport = GameLayer.Actor.Create(Prefab.Transport, new Vector3(0, 0));
            return transport.GetComponent<Transport>();
        }

    }
}