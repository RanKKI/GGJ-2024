using DG.Tweening;
using Pixeye.Actors;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndLayer : Layer<GameLayer>
{
    public GameObject UIRoot;
    public TextMeshProUGUI text;
    public Image SpriteRenderer;
    public Image NameRenderer;

    public Image Overlay;

    public Canvas canvas;
    public GameObject canvasObj;

    public SpriteRenderer spotlightCharacter;

    public Sprite Player1;
    public Sprite Player2;
    public Sprite Monster;

    public Sprite P1Name;
    public Sprite P2Name;
    public Sprite MonsterName;
    private int index = -1;

    public Button NextButton;

    public Sprite JokerStand1;
    public Sprite JokerStand2;

    public Sprite JokerDead1;
    public Sprite JokerDead2;

    public GameObject theEnd;

    public PlayerType winner = PlayerType.Player1;

    public AudioSource gunShot;

    protected override void Setup()
    {
        text.text = "";
        Add<ProcessorEndBackground>();
        theEnd.SetActive(false);
        ShowUI();
        Next();
    }

    public void Next()
    {
        index++;
        if (index >= EndContent.contents.Length)
        {
            OnFinished();
            return;
        }

        EndContent content = EndContent.contents[index];
        if (content.action != EndCanvasAction.none)
        {
            ExecuteAction(content.action);
        }

        GameLayer.Send(new SignalEndChangeBack { layer = this, });

        text.text = content.text;
        canvasObj.SetActive(content.text != "");
        UpdateUI(content.playerType);
    }

    private void UpdateUI(PlayerType playerType)
    {
        SpriteRenderer.enabled = true;
        NameRenderer.enabled = true;
        if (
            playerType == PlayerType.Player1
            || (playerType == PlayerType.Winner && winner == PlayerType.Player1)
        )
        {
            SpriteRenderer.sprite = Player1;
            NameRenderer.sprite = P1Name;
        }
        else if (
            playerType == PlayerType.Player2
            || (playerType == PlayerType.Winner && winner == PlayerType.Player2)
        )
        {
            SpriteRenderer.sprite = Player2;
            NameRenderer.sprite = P2Name;
        }
        else if (playerType == PlayerType.Ringmaster)
        {
            SpriteRenderer.sprite = Monster;
            NameRenderer.sprite = MonsterName;
        }
        else if (playerType == PlayerType.None)
        {
            SpriteRenderer.enabled = false;
            NameRenderer.enabled = false;
        }
    }

    private void ExecuteAction(EndCanvasAction action)
    {
        spotlightCharacter.enabled = false;
        switch (action)
        {
            case EndCanvasAction.ChangeToBackStage:
                NextButton.interactable = false;
                Overlay
                    .DOColor(Color.black, 1f)
                    .OnComplete(() =>
                    {
                        Next();
                        Overlay.DOColor(Color.clear, 1f).onComplete += () =>
                            NextButton.interactable = true;
                    });
                break;
            case EndCanvasAction.ToBlack:
                Overlay.color = Color.black;
                break;
            case EndCanvasAction.Gunshot:
                gunShot.Play();
                Debug.Log("Gunshot");
                break;
            case EndCanvasAction.MasterLine1:
                Debug.Log("MasterLine1");
                break;
            case EndCanvasAction.MasterLine2:
                Debug.Log("MasterLine2");
                break;
            case EndCanvasAction.ShowDeadJoker:
                Overlay.color = Color.clear;
                spotlightCharacter.enabled = true;
                spotlightCharacter.sprite = winner == PlayerType.Player1 ? JokerDead2 : JokerDead1;
                break;
            case EndCanvasAction.ShowWinJoker:
                Overlay.color = Color.clear;
                spotlightCharacter.enabled = true;
                spotlightCharacter.sprite =
                    winner == PlayerType.Player1 ? JokerStand1 : JokerStand2;
                break;
            case EndCanvasAction.ShowTheEnd:
                NextButton.interactable = false;
                var sprite = theEnd.GetComponent<SpriteRenderer>();
                theEnd.SetActive(true);
                sprite.color = Color.clear;
                sprite.DOColor(Color.white, 1f).onComplete += () =>
                {
                    NextButton.interactable = true;
                };
                break;
        }
    }

    private void OnFinished()
    {
        spotlightCharacter.enabled = false;
        SpriteRenderer.enabled = false;
        NameRenderer.enabled = false;
        canvas.enabled = false;
    }

    private void ShowUI()
    {
        SetUI(true);
    }

    private void HideUI()
    {
        SetUI(false);
    }

    private void SetUI(bool active)
    {
        spotlightCharacter.enabled = active;
        SpriteRenderer.enabled = active;
        NameRenderer.enabled = active;
        canvas.enabled = active;
    }
}
