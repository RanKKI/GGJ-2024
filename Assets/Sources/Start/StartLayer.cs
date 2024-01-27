using Pixeye.Actors;
using UnityEngine;
using UnityEngine.UI;

public class StartLayer : Layer<GameLayer>
{

    public Text text;
    public Image SpriteRenderer;
    public Sprite Player1;
    public Sprite Player2;
    private int index = -1;

    protected override void Setup()
    {
        text.text = "";
        Next();
    }

    public void Next()
    {
        index++;
        if (index >= Content.contents.Length)
        {
            onFinished();
            return;
        }

        Content content = Content.contents[index];
        text.text = content.text;
        SpriteRenderer.sprite = content.playerType == PlayerType.Player1 ? Player1 : Player2;
    }


    private void onFinished()
    {

    }
}
