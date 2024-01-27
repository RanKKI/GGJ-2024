public class Content
{
    public PlayerType playerType;
    public string text;


    public static Content[] contents = {
       new Content { playerType = PlayerType.Ringmaster, text = "Ladies and gentlemen, children, welcome to the thrilling 'Happy Show'! " },
        new Content { playerType = PlayerType.Ringmaster, text = "Tonight, we will take you into a world full of fantasy, surprises, and laughter. Here, dreams and reality collide, and every moment is filled with magic." },
        new Content { playerType = PlayerType.Ringmaster, text = "Our clowns will showcase their incredible skills, aiming not only to win your applause but also to win your hearts! " },
        new Content { playerType = PlayerType.Ringmaster, text = "So, please, sit back, relax, and let our performers create an unforgettable night with their courage and talent." },
        new Content { playerType = PlayerType.Ringmaster, text = "So, don't wait any longer! Let our show, the 'Happy Show,' completely captivate you! Get ready to be conquered by us!" },
        new Content { playerType = PlayerType.Ringmaster, text = "Listen up, Mr. M and Mr. E, tonight's performance better not disappoint me! The audience needs laughter, and if you can't deliver, you'll deal with the consequences!" },
        new Content { playerType = PlayerType.Player1, text = "Ringmaster, we'll do our best, but safety—" },

        new Content { playerType = PlayerType.Ringmaster, text = "Safety? You're clowns; making the audience happy is your only job! Don't give me excuses; I..." },
        new Content { playerType = PlayerType.Ringmaster, text = "Oh, wait, my dear children, today is an important day. I should reward you, shouldn't I? Let me think... maybe it's time for you to leave?" },
        new Content { playerType = PlayerType.Ringmaster, text = "Oh, don't be afraid, children! Tonight's performance, I guarantee, is safe, and it will be your reward." },
        new Content { playerType = PlayerType.Ringmaster, text = "As long as you can satisfy the audience, bring them unprecedented joy, you have a chance for freedom." },
        new Content { playerType = PlayerType.Player1, text = "Really, Ringmaster? We can leave if we do well in this performance?" },
        new Content { playerType = PlayerType.Ringmaster, text = "Certainly, I give you my word. But remember, this performance must be outstanding, not just safe but also stunning. Our audience expects an unforgettable show, and you will be their stars." },
        new Content { playerType = PlayerType.Player2, text = "We will do our utmost best." },
        new Content { playerType = PlayerType.Ringmaster, text = "Very well, I have confidence in you. Tonight, let's create history together." },
        new Content { playerType = PlayerType.Ringmaster, text = "The opening ceremony concludes with dazzling fireworks, marking the beginning of the 'Happy Show' with enthusiastic applause and cheers from the audience." }
     };
}