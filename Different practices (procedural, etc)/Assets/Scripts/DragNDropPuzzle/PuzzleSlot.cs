using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PuzzleSlot : MonoBehaviour
{
    SpriteRenderer puzzleSlotSprite;

    private void Awake()
    {
        puzzleSlotSprite = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite newSprite)
    {
        puzzleSlotSprite.sprite = newSprite;
    }
    public void Placed()
    {
      
    }
}
