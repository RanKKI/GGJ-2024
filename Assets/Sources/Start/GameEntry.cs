using System.Collections;
using DG.Tweening;
using Pixeye.Actors;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryLayer : Layer<GameLayer>
{
    public SpriteRenderer LogoOBJ;
    public GameObject canvas;
    public Animator OpeningAnimator;

    protected override void Setup() { }

    public void OnClickStart()
    {
        Debug.Log("OnClickStart");
        canvas.SetActive(false);
        // canvas
        LogoOBJ.DOColor(Color.clear, 1f).onComplete += () =>
        {
            SceneManager.LoadScene("StartScene");
        };
    }
}
