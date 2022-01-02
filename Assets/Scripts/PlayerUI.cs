using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] allSprites;

    public void UpdateSprite(int newSpriteVal)
    {
        spriteRenderer.sprite = allSprites[newSpriteVal];
    }
}
