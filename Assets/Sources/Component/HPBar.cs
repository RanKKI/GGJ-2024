using Pixeye.Actors;
using UnityEngine;

class HPBar : MonoCached
{

    public Sprite fullHealthSprite;
    public Sprite emptyHealthSprite;

    public bool leftToRight = true;

    public void SetHealth(float health)
    {
        health = Mathf.Abs(health);
        Debug.Log("HPBar: " + health);
        for (int i = 0; i < health; i++)
        {
            var child = transform.GetChild(leftToRight ? i : transform.childCount - i - 1);
            if (child == null)
            {
                Debug.LogError("child not found");
                continue;
            }
            var spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = emptyHealthSprite;
            }
        }
    }

}