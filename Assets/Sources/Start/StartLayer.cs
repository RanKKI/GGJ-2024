using DG.Tweening;
using Pixeye.Actors;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLayer : Layer<GameLayer>
{

    public GameObject UIRoot;
    public TextMeshProUGUI text;
    public Image SpriteRenderer;
    public Image NameRenderer;

    public Image Overlay;


    public GameObject frontStage;
    public GameObject backStage;


    public SpriteRenderer spotlight;
    public SpriteRenderer spotlightCharacter;

    public Sprite Player1;
    public Sprite Player2;
    public Sprite Monster;

    public Sprite P1Name;
    public Sprite P2Name;
    public Sprite MonsterName;
    private int index = -1;

    protected override void Setup()
    {
        text.text = "";
        Add<ProcessorBackground>();
        Next();

        frontStage.SetActive(true);
        backStage.SetActive(false);
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
        }

        GameLayer.Send(new SignalChangeBack
        {
            layer = this,
            spotlight = content.spotlight,
            SpotlightCharacter = content.spotlightCharacter,
        });

        text.text = content.text;
        UpdateUI(content.playerType);
    }


    private void UpdateUI(PlayerType playerType)
    {
        if (playerType == PlayerType.Player1)
        {
            SpriteRenderer.sprite = Player1;
            NameRenderer.sprite = P1Name;
        }
        else if (playerType == PlayerType.Player2)
        {
            SpriteRenderer.sprite = Player2;
            NameRenderer.sprite = P2Name;
        }
        else
        {
            SpriteRenderer.sprite = Monster;
            NameRenderer.sprite = MonsterName;
        }
    }


    private void ExecuteAction(CanvasAction action)
    {
        switch (action)
        {
            case CanvasAction.ChangeToBackStage:
                Overlay.DOColor(Color.black, 1f).OnComplete(() =>
                {
                    frontStage.SetActive(false);
                    backStage.SetActive(true);
                    Next();
                    Overlay.DOColor(Color.clear, 1f);
                });
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
