using DG.Tweening;
using Pixeye.Actors;
using Unity.VisualScripting;
using UnityEngine;

public class ItemBoomerang : Item
{

    public Rigidbody2D rigidbody;

    public GameObject renderer;

    private bool spin = false;
    private int rotation = 0;

    private Vector2 _velocity = Vector2.zero;

    public override bool Fire(Vector2 dir)
    {
        base.Fire(dir);
        spin = true;
        GameObject parent = transform.parent.gameObject;
        if (parent == null) return false;
        transform.SetParent(null);
        transform.position = parent.transform.position + (Vector3)dir;
        _velocity = dir;
        rigidbody.AddForce(dir * 200);
        return true;
    }

    public override void OnPickUp()
    {
        base.OnPickUp();
        spin = false;
        UpdateVelocity(Vector2.zero);
    }

    void Update()
    {
        if (spin)
        {
            renderer.transform.rotation = Quaternion.Euler(0, 0, rotation);
            rotation += 1;
        }
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
        var cItem = entity.ComponentItem();
        if (cItem.holder == targetPlayer) return;
        var targetRigibody2D = targetPlayer.GetMono<Rigidbody2D>();
        if (targetRigibody2D != null)
        {
            targetRigibody2D.AddForce(_velocity * 100);
        }
        Dispose();
    }

}