using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : BaseTile
{
    [SerializeField] Color originalColor;
    [SerializeField] Color offsetColor;


    public override void Init(int x, int y)
    {

        bool isOffset = (x + y) % 2 == 1;

        sprite = GetComponent<SpriteRenderer>();

        sprite.color = isOffset ? offsetColor : originalColor;
    }
}
