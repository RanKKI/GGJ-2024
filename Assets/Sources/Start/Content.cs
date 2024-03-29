public enum CanvasAction
{
    none,
    ChangeToBackStage,

    ChangeToTute1,
    ChangeToTute2,
}

public class Content
{
    public PlayerType playerType = default;
    public string text = "";

    public SpotlightType spotlight = SpotlightType.Skip;
    public int spotlightCharacter = -2;

    public CanvasAction action = CanvasAction.none;

    public static Content[] contents =
    {
        new()
        {
            playerType = PlayerType.Ringmaster,
            text = "Ladies and gentlemen, boys and girls, welcome to the thrilling 'Happy Show'! ",
            spotlight = SpotlightType.MonsterOnly,
            spotlightCharacter = 7,
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "Tonight, we shall take you into a world filled with magic, surprises, and laughter. Here, dreams and reality converge, and every moment is enchanted."
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "Our clowns will showcase their incredible skills, aiming not only to win your applause but also to capture your hearts! "
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "So, please, sit back, relax, and let our performers amaze you with their bravery and talent on this unforgettable night!",
            spotlightCharacter = 8,
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "So, wait no more! Let the show begin! Prepare to be utterly captivated by our ‘Happy Show!'",
            spotlightCharacter = 7,
        },
        new() { action = CanvasAction.ChangeToBackStage },
        //   幕后

        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "Listen up, Mr. M and Mr. E, tonight's performance better not disappoint me! The audience needs laughter, and if you can't deliver, you'll deal with the consequences!",
            spotlight = SpotlightType.Monster,
            spotlightCharacter = 6,
        },
        new()
        {
            playerType = PlayerType.Player1,
            text = "Ringmaster, we'll do our best, but safety—",
            spotlight = SpotlightType.Joker,
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "Safety? You're clowns; making the audience happy is your only job! Don't give me excuses; I...",
            spotlight = SpotlightType.Monster,
            spotlightCharacter = 2,
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "Oh, wait, my dear children, today is an important day. I should reward you, shouldn't I? Let me think... maybe it's time for you to leave?",
            spotlightCharacter = 0,
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "Oh, don't be afraid, my children! Tonight's performance, I guarantee, is safe, and it will be your reward.",
            spotlightCharacter = 5,
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "As long as you can satisfy the audience, bring them unprecedented joy, you have a chance for freedom."
        },
        new()
        {
            playerType = PlayerType.Player1,
            text = "Really, Ringmaster? We can leave if we do well in this performance?",
            spotlight = SpotlightType.Joker,
            spotlightCharacter = 4,
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "Of course, you have my word.  But remember, this performance must be outstanding, not just safe but also stunning. Our audience expects an unforgettable show, and you will be their stars.",
            spotlight = SpotlightType.Monster,
            spotlightCharacter = 0,
        },
        new()
        {
            playerType = PlayerType.Player2,
            text = "we’ll give it our all.",
            spotlight = SpotlightType.Joker,
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text = "Very well, I have confidence in you. Tonight, let's create history together.",
            spotlight = SpotlightType.Monster,
        },
        new()
        {
            playerType = PlayerType.Ringmaster,
            text =
                "The opening ceremony concludes with dazzling fireworks, marking the beginning of the 'Happy Show' with enthusiastic applause and cheers from the audience……"
        },
        new() { action = CanvasAction.ChangeToTute1 },
        new() { action = CanvasAction.ChangeToTute2 }
    };
}
