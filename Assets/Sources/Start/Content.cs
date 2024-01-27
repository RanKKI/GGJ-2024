public class Content
{
    public PlayerType playerType;
    public string text;


    public static Content[] contents = {
        new() { playerType = PlayerType.Player1, text = "Player 1" },
        new() { playerType = PlayerType.Player2, text = "bakljfdlkjafkldajfkldjakl" },
        new() { playerType = PlayerType.Player1, text = "Hello" },
        new() { playerType = PlayerType.Player2, text = "World" },
     };
}