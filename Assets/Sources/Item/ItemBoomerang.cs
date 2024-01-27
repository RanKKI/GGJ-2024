using Pixeye.Actors;

using UnityEngine;

public class ItemBoomerang : Item
{

    public Rigidbody2D rigidbody;


    private Vector2 _velocity = Vector2.zero;

    public override bool Fire(Vector2 dir)
    {
        base.Fire(dir);
        GameObject parent = transform.parent.gameObject;
        if (parent == null) return false;
        transform.SetParent(null);
        transform.position = parent.transform.position + (Vector3)dir;
        _velocity = dir;
        rigidbody.AddForce(dir * 200);
        return true;
    }

    public override void Reset()
    {
        base.Reset();
        UpdateVelocity(Vector2.zero);
    }

    public override void OnOutOfScreen()
    {
        base.OnOutOfScreen();
        Vector2 reverseDir = rigidbody.velocity;
        reverseDir.Scale(new Vector2(-1, 0));
        UpdateVelocity(reverseDir);
    }

    private void UpdateVelocity(Vector2 vec)
    {
        _velocity = rigidbody.velocity.normalized;
        rigidbody.velocity = vec;
    }

    protected override void OnHitPlayer(ent targetPlayer)
    {
        var targetRigibody2D = targetPlayer.GetMono<Rigidbody2D>();
        if (targetRigibody2D != null)
        {
            targetRigibody2D.AddForce(_velocity * 100);
        }
        GameLayer.Send(new SignalDisposeItem
        {
            item = entity,
            obj = gameObject,
        });
    }

}