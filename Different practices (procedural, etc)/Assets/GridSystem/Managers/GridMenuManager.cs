using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridMenuManager : MonoBehaviour
{
    public static GridMenuManager Instance;

    [SerializeField]
    GameObject selectedHeroObject, tileObject, tileUnitObject; 

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowSelectedHero(HeroBase hero) 
    {
        if (hero == null)
        {
            selectedHeroObject.SetActive(false);
            return;

        }
        selectedHeroObject.GetComponentInChildren<TextMeshProUGUI>().text = hero.UnitName;
        selectedHeroObject.SetActive(true);
    }

    public void ShowTileInfo(BaseTile tile)
    {
        if (tile == null)
        {
            tileObject.SetActive(false);
            tileUnitObject.SetActive(false);
            return;
        }

        tileObject.GetComponentInChildren<TextMeshProUGUI>().text = tile.TileName;
        tileObject.SetActive(true);


        if(tile.occupiedUnit)
        {
            tileUnitObject.GetComponentInChildren<TextMeshProUGUI>().text = tile.occupiedUnit.UnitName;
            tileUnitObject.SetActive(true);
        }
    }

}
