using System;
using System.Collections;
using Pixeye.Actors;
using UnityEngine;
using UnityEngine.Animations;

public delegate void AnimationCallback(); // declare delegate type


public class Item : Actor
{
    public float lifeTime = 10f;

    protected override void Setup()
    {
        base.Setup();
        Debug.Log("Item: Setup");
        var obj = entity.Set<ComponentObject>();
        obj.name = "Item";
        entity.InitComponentObject();
        entity.Set<ComponentItem>();
        SetTag();

        var cItem = entity.Get<ComponentItem>();
        cItem.name = GetName();
    }

    protected virtual string GetName()
    {
        return "";
    }

    protected virtual void SetTag()
    {
        entity.Set(Tag.Item);
    }

    public virtual bool Fire(Vector2 dir)
    {
        return false;
    }

    public virtual void OnOutOfScreen()
    {
        entity.Get<ComponentItem>().onOutOfScreen?.Invoke();
    }

    public void SetConstraint(GameObject parent)
    {
        var constraint = GetComponent<ParentConstraint>();
        if (constraint == null)
        {
            constraint = gameObject.AddComponent<ParentConstraint>();
        }
        if (constraint.sourceCount > 0)
        {
            constraint.RemoveSource(0);
        }
        constraint.AddSource(new ConstraintSource
        {
            sourceTransform = parent.transform,
            weight = 1
        });
        constraint.constraintActive = true;
    }

    public void RemoveConstraint()
    {
        var constraint = GetComponent<ParentConstraint>();
        if (constraint == null) return;
        constraint.RemoveSource(0);
        constraint.constraintActive = false;
        Destroy(constraint);
    }

    public virtual void OnPickUp()
    {
        entity.Get<ComponentItem>().onPickUp?.Invoke();
        entity.Remove<ComponentLife>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        var cItem = entity.Get<ComponentItem>();
        if (!cItem.isActive)
        {
            return;
        }
        // if (cItem.holder != default) return;

        collision.gameObject.TryGetEntity(out var otherEntity);
        if (otherEntity.Has(Tag.Player))
        {
            OnHitPlayer(otherEntity);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("GameBoard"))
        {
            OnHitGround();
        }
    }

    protected virtual void OnHitGround()
    {

    }

    protected virtual void OnHitPlayer(ent player)
    {

    }



    protected void Dispose()
    {
        Debug.Log("Dispose" + entity.ComponentItem().name);
        GameLayer.Send(new SignalDisposeItem
        {
            item = entity,
            obj = gameObject,
        });
    }


    protected AnimationCallback animationCallback; // to store the function

    protected void PlayAnimator(float duration, AnimationCallback animationCallback)
    {
        this.animationCallback = animationCallback;
        var animator = GetComponentInChildren<Animator>();
        animator.enabled = true;
        StartCoroutine(WaitFor(duration));
    }

    private IEnumerator WaitFor(float duration)
    {
        yield return new WaitForSeconds(duration);
        animationCallback();
    }

}