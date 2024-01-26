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
        Debug.Log("Fire Boomerang 2");
        rigidbody.AddForce(dir * 200);
    }

}