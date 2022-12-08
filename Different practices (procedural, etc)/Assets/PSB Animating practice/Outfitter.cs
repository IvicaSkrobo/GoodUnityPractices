using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Outfitter : MonoBehaviour
{
    [SerializeField]
    UnitType unitType;
    SpriteResolver[] allSpriteResolvers;
    private void Awake()
    {

        allSpriteResolvers = GetComponentsInChildren<SpriteResolver>();
    }


    private void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,50,50), "NextSprite"))
        {
            NextSprite();
        }
    }


    public void NextSprite()
    {
        foreach (var spriteResolver in allSpriteResolvers)
        {
            //check if the spritesolver has this next sprite, in case some sprites have hats
            //and others dont
            var hasSprite = spriteResolver.spriteLibrary.GetSprite(spriteResolver.GetCategory(), unitType.ToString());
            spriteResolver.gameObject.SetActive(hasSprite);

            /// change the sprite based of on category and label
            spriteResolver.SetCategoryAndLabel(spriteResolver.GetCategory(), unitType.ToString());
           
        }

        unitType += 1;

        if (((int)unitType) == System.Enum.GetValues(typeof(UnitType)).Length)
        {
            unitType = 0;
        }
    }
    
}


public enum UnitType
{
    Unit1=0,
    Unit2=1
}

