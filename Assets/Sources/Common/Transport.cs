using System;
using System.Collections;
using DG.Tweening;
using Pixeye.Actors;
using UnityEngine;

public class Transport : Actor
{

    public GameObject Open;
    public GameObject Loading;
    public GameObject Closed;

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

        player.isActive = false;

        var openGatePos = openLocation + heightOffset;
        var closedGatePos = targetPosition + heightOffset;

        gameObject.transform.position = openLocation;

        yield return OpenGateAt(openGatePos);
        yield return HoldGateAt(openGatePos, 0.5f);
        yield return MovePlayerTo(gameObject, openGatePos);
        yield return CloseGateAt(openGatePos);
        gameObject.transform.position = closedGatePos;

        yield return OpenGateAt(closedGatePos);
        player.isActive = true;
        yield return HoldGateAt(closedGatePos, 0.5f);
        yield return CloseGateAt(closedGatePos);
    }

    private IEnumerator MovePlayerTo(ent player, Vector2 pos, bool reverse = false)
    {
        Log("MovePlayerTo");
        float speed = 0.43f;
        player.transform.DOMoveY(pos.y, speed)
            .SetEase(reverse ? Ease.OutQuint : Ease.InQuint);
        yield return new WaitForSeconds(speed - 0.2f);
    }

    private IEnumerator OpenGateAt(Vector2 pos)
    {
        Log("OpenGateAt");
        var obj = Play(Open, pos);
        yield return new WaitForSeconds(1f);
        RemoveObject(obj);
    }

    private IEnumerator HoldGateAt(Vector2 pos, float duration)
    {
        Log("HoldGateAt");
        var obj = Play(Loading, pos);
        yield return new WaitForSeconds(duration);
        RemoveObject(obj);
    }

    private IEnumerator CloseGateAt(Vector2 pos)
    {
        Log("CloseGateAt");
        var obj = Play(Closed, pos);
        yield return new WaitForSeconds(1.5f);
        RemoveObject(obj);
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