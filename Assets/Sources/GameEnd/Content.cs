public enum EndCanvasAction
{
    none,
    ChangeToBackStage,

    ToBlack,

    Gunshot,

    MasterLine1,
    MasterLine2,

    ShowDeadJoker,
    ShowWinJoker,
    ShowTheEnd,
}

public class EndContent
{
    public PlayerType playerType = default;
    public string text = "";

    public EndCanvasAction action = EndCanvasAction.none;

    public static EndContent[] contents =
    {
        new()
        {
            action = EndCanvasAction.ToBlack,
            playerType = PlayerType.None,
            text = "....",
        },
        new()
        {
            action = EndCanvasAction.Gunshot,
            playerType = PlayerType.None,
            text = "",
        },
        new() { playerType = PlayerType.None, text = "....", },
        new() { playerType = PlayerType.None, text = "Gunshots whizzing by..", },
        new()
        {
            action = EndCanvasAction.MasterLine1,
            playerType = PlayerType.Ringmaster,
            text =
                "Distinguished guests, allow the fall of this unfortunate soul to serve as the magnificent closing act of our splendid banquet today!",
        },
        new()
        {
            action = EndCanvasAction.MasterLine1,
            playerType = PlayerType.Ringmaster,
            text =
                "Let our cheers resonate as we celebrate the conclusion of an evening filled with unparalleled spectacle and drama!"
        },
        new()
        {
            action = EndCanvasAction.ShowDeadJoker,
            playerType = PlayerType.Winner,
            text = "..."
        },
        new()
        {
            action = EndCanvasAction.ShowWinJoker,
            playerType = PlayerType.Winner,
            text = "..."
        },
        new()
        {
            action = EndCanvasAction.ShowDeadJoker,
            playerType = PlayerType.Winner,
            text = "..."
        },
        new()
        {
            action = EndCanvasAction.MasterLine2,
            playerType = PlayerType.Winner,
            text = "..."
        },
        new()
        {
            action = EndCanvasAction.ShowWinJoker,
            playerType = PlayerType.Winner,
            text = "..."
        },
        new()
        {
            action = EndCanvasAction.ShowDeadJoker,
            playerType = PlayerType.Winner,
            text = "..."
        },
        new()
        {
            action = EndCanvasAction.ShowWinJoker,
            playerType = PlayerType.Winner,
            text = "..."
        },
        new()
        {
            action = EndCanvasAction.ShowDeadJoker,
            playerType = PlayerType.Winner,
            text = "..."
        },
        new()
        {
            action = EndCanvasAction.ShowTheEnd,
            playerType = PlayerType.None,
        },
    };
}
