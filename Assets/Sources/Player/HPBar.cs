using Pixeye.Actors;
using UnityEngine;

class HPBar : MonoCached
{

    public Sprite fullHealthSprite;
    public Sprite halfHealthSprite;
    public Sprite emptyHealthSprite;

    public bool leftToRight = true;

    public int maxHealth = 10;

    public void SetHealth(float health)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            var idx = leftToRight ? i : maxHealth - i - 1;
            var child = transform.GetChild(idx);
            if (child == null)
            {
                Debug.LogError("child not found");
                continue;
            }
            var spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                continue;
            }


            if (idx < (int)health)
            {
                spriteRenderer.sprite = fullHealthSprite;
            }
            else if (idx < health)
            {
                spriteRenderer.sprite = halfHealthSprite;
            }
            else
            {
                spriteRenderer.sprite = emptyHealthSprite;
            }

        }
    }

}