using Pixeye.Actors;
using UnityEngine;
using UnityEngine.SceneManagement;

public static partial class Game
{
    public static void InitComponentObject(this ent entity)
    {
        var cObject = entity.ComponentObject();

        cObject.position = entity.transform.position;
        cObject.collider = entity.GetMono<Collider2D>();
        // cObject.renderer = entity.GetMono<SpriteRenderer>("view");
    }

    public static void ChangeHealth(in ent entity, float count)
    {
        GameLayer.Send(new SignalChangeHealth { target = entity, count = count });
    }

    public static void ChangeHappiness(in ent entity, int count)
    {
        GameLayer.Send(new SignalChangeHappiness { target = entity, count = count });
    }

    public static void OnGameFinished(PlayerType winner)
    {
        PlayerPrefs.SetInt("winner", winner == PlayerType.Player1 ? 1 : 2);
        SceneManager.LoadScene("GameEnd");
    }
}
