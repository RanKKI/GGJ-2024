using System.Collections;
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

    public GameObject opening;

    public Canvas canvas;

    public GameObject dialog;

    public SpriteRenderer spotlight;
    public SpriteRenderer spotlightCharacter;

    public Sprite Player1;
    public Sprite Player2;
    public Sprite Monster;

    public Sprite P1Name;
    public Sprite P2Name;
    public Sprite MonsterName;
    private int index = -1;

    public Button NextButton;

    public GameObject Tutorial1;
    public GameObject Tutorial2;

    protected override void Setup()
    {
        text.text = "";
        Add<ProcessorBackground>();
        HideUI();
        StartCoroutine(SetupAsync());
    }

    private IEnumerator SetupAsync()
    {
        frontStage.SetActive(false);
        backStage.SetActive(false);
        opening.SetActive(true);
        var animator = opening.GetComponent<Animator>();
        animator.enabled = true;
        var duration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);
        ShowUI();
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
        }

        GameLayer.Send(
            new SignalChangeBack
            {
                layer = this,
                spotlight = content.spotlight,
                SpotlightCharacter = content.spotlightCharacter,
            }
        );

        text.text = content.text;
        dialog.SetActive(content.text != "");
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
        SpriteRenderer stageRenderer = backStage.GetComponentInChildren<SpriteRenderer>();
        switch (action)
        {
            case CanvasAction.ChangeToBackStage:
                NextButton.interactable = false;
                Overlay
                    .DOColor(Color.black, 1f)
                    .OnComplete(() =>
                    {
                        frontStage.SetActive(false);
                        backStage.SetActive(true);
                        Next();
                        Overlay.DOColor(Color.clear, 1f).onComplete += () =>
                            NextButton.interactable = true;
                    });
                break;
            case CanvasAction.ChangeToTute1:
                HideAllElements();
                Tutorial1.SetActive(true);
                break;
            case CanvasAction.ChangeToTute2:
                HideAllElements();
                Tutorial2.SetActive(true);
                break;
        }
    }

    private void HideAllElements()
    {
        spotlight.enabled = false;
        spotlightCharacter.enabled = false;
        SpriteRenderer.enabled = false;
        NameRenderer.enabled = false;
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(false);
    }

    private void OnFinished()
    {
        canvas.enabled = false;
        HideAllElements();
        SceneManager.LoadScene("Scene Default");
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
        spotlight.enabled = active;
        spotlightCharacter.enabled = active;
        SpriteRenderer.enabled = active;
        NameRenderer.enabled = active;
        canvas.enabled = active;
    }
}
