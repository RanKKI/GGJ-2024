using Unity.VisualScripting;
using UnityEngine;

public class ItemBoomerang : Item
{

    public Rigidbody2D rigidbody;

    public override void Fire(Vector2 dir)
    {
        base.Fire(dir);
        Debug.Log("Fire Boomerang");
        GameObject parent = transform.parent.gameObject;
        if (parent == null) return;
        transform.SetParent(null);
        transform.position = parent.transform.position + (Vector3)dir;
        Debug.Log("Fire Boomerang 2");
        rigidbody.AddForce(dir * 200);
    }

    public override void Reset()
    {
        base.Reset();
        rigidbody.velocity = Vector2.zero;
    }

    public override void OnOutOfScreen()
    {
        base.OnOutOfScreen();
        Debug.Log("OnOutOfScreen Boomerang");
        Vector2 reverseDir = rigidbody.velocity;
        reverseDir.Scale(new Vector2(-1, 0));
        rigidbody.velocity = reverseDir;
    }

}