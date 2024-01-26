using Pixeye.Actors;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBoomerang : Item
{

    public Rigidbody2D rigidbody;


    private Vector2 _velocity = Vector2.zero;

    public override bool Fire(Vector2 dir)
    {
        base.Fire(dir);
        Debug.Log("Fire Boomerang");
        GameObject parent = transform.parent.gameObject;
        if (parent == null) return false;
        transform.SetParent(null);
        transform.position = parent.transform.position + (Vector3)dir;
        Debug.Log("Fire Boomerang 2");
        rigidbody.AddForce(dir * 200);
        return true;
    }

    public override void Reset()
    {
        base.Reset();
        updateVelocity(Vector2.zero);
    }

    public override void OnOutOfScreen()
    {
        base.OnOutOfScreen();
        Debug.Log("OnOutOfScreen Boomerang");
        Vector2 reverseDir = rigidbody.velocity;
        reverseDir.Scale(new Vector2(-1, 0));
        updateVelocity(reverseDir);
    }

    private void updateVelocity(Vector2 vec)
    {
        _velocity = rigidbody.velocity.normalized;
        rigidbody.velocity = vec;
    }

    protected override void OnHit(ref ent entity)
    {
        base.OnHit(ref entity);
        var targetRigibody2D = entity.GetMono<Rigidbody2D>();
        if (targetRigibody2D != null)
        {
            targetRigibody2D.AddForce(_velocity * 100);
        }
    }

}