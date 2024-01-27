using System;
using System.Collections;
using DG.Tweening;
using Pixeye.Actors;
using UnityEngine;

public class Transport : Actor
{

    public GameObject Open;

    protected override void Setup()
    {

    }

    public void TransportObjectTo(
        ent gameObject,
        Vector2 position
    )
    {
        StartCoroutine(_TransportObjectTo(gameObject, position));
    }


    private IEnumerator _TransportObjectTo(
        ent gameObject,
        Vector2 targetPosition
    )
    {
        var openLocation = (Vector2)gameObject.transform.position;
        var heightOffset = new Vector2(0, 2f);
        var player = gameObject.ComponentPlayer();
        var playerObj = gameObject.GetMono<ActorPlayer>().gameObject;

        player.isActive = false;

        var openGatePos = openLocation + heightOffset;
        var closedGatePos = targetPosition + heightOffset;

        gameObject.transform.position = openLocation;

        var obj = Play(Open, openGatePos);
        GameLayer.Send(new SignalPlaySound
        {
            name = "teleportation",
            volume = 0.8f,
            pos = gameObject.transform.position,
        });
        yield return new WaitForSeconds(1f);
        yield return MovePlayerTo(gameObject, openGatePos);
        playerObj.SetActive(false);
        yield return new WaitForSeconds(3f);
        RemoveObject(obj);
        var obj2 = Play(Open, closedGatePos);
        GameLayer.Send(new SignalPlaySound
        {
            name = "teleportation",
            volume = 0.8f,
            pos = gameObject.transform.position,
        });
        yield return new WaitForSeconds(1f);
        playerObj.SetActive(true);
        player.isActive = true;
        gameObject.transform.position = closedGatePos;
        yield return new WaitForSeconds(3f);
        RemoveObject(obj2);
    }

    private IEnumerator MovePlayerTo(ent player, Vector2 pos, bool reverse = false)
    {
        float speed = 0.43f;
        player.transform.DOMoveY(pos.y, speed)
            .SetEase(reverse ? Ease.OutQuint : Ease.InQuint);
        yield return new WaitForSeconds(speed);
    }



    private void RemoveObject(GameObject gameObject)
    {
        gameObject.transform.SetParent(null);
        Destroy(gameObject);
    }

    private GameObject Play(GameObject prefab, Vector2 pos)
    {
        var gameObject = Instantiate(prefab);
        gameObject.transform.SetParent(transform);
        gameObject.transform.SetPositionAndRotation(pos, Quaternion.Euler(0, 0, 90));
        var animator = gameObject.GetComponent<Animator>();
        animator.enabled = true;
        return gameObject;
    }

    private void Log(string message)
    {
        Debug.Log(message);
    }

}