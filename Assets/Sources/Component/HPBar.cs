using Pixeye.Actors;
using UnityEngine;

class HPBar : MonoCached
{

    public Sprite fullHealthSprite;
    public Sprite emptyHealthSprite;

    public bool leftToRight = true;

    public int maxHealth = 10;

    public void SetHealth(float health)
    {
        Debug.Log("Set Health to" + health);
        health = Mathf.Abs(health);
        health = Mathf.Clamp(health, 0, maxHealth); // bound the health
        for (int i = 0; i < health; i++)
        {
            var idx = leftToRight ? i : transform.childCount - i - 1;
            var child = transform.GetChild(idx);
            if (child == null)
            {
                Debug.LogError("child not found");
                continue;
            }
            var spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = idx < health ? fullHealthSprite : emptyHealthSprite;
            }
        }
    }

}