using Pixeye.Actors;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLayer : Layer<GameLayer>
{

    public GameObject UIRoot;
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
            OnFinished();
            return;
        }

        Content content = Content.contents[index];
        if (content.action != CanvasAction.none)
        {
            ExecuteAction(content.action);
            Next();
            return;
        }
        text.text = content.text;
        SpriteRenderer.sprite = content.playerType == PlayerType.Player1 ? Player1 : Player2;
    }


    private void ExecuteAction(CanvasAction action)
    {
        switch (action)
        {
            case CanvasAction.ChangeSceneTo1:
                SceneManager.LoadScene("Scene 1");
                break;
            case CanvasAction.ChangeSceneTo2:
                SceneManager.LoadScene("Scene 2");
                break;
        }
    }


    private void OnFinished()
    {
        SceneManager.LoadScene("Scene Default");
    }


    private void ShowUI()
    {
        UIRoot.SetActive(true);
    }

    private void HideUI()
    {
        UIRoot.SetActive(false);
    }
}
