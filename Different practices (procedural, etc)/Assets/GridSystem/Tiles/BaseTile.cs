using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseTile : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
{

    public string TileName;
    [SerializeField]
    GameObject highlight;

    [SerializeField]
    protected SpriteRenderer sprite;
    [SerializeField]
    bool isWalkable;

    public UnitBase occupiedUnit;

    public bool WalkableCheck => isWalkable && occupiedUnit == null;



    public virtual void Init(int x, int y)
    {
      
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.gameObject.SetActive(true);
        GridMenuManager.Instance.ShowTileInfo(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.gameObject.SetActive(false);
        GridMenuManager.Instance.ShowTileInfo(null);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GridSystemGameManager.Instance.GameState != GameState.HeroesTurn) { return; }

        if (occupiedUnit != null )
        {
            if (occupiedUnit.faction == Faction.Hero) { UnitManager.instance.SetSelectedHero((HeroBase)occupiedUnit); }
            else
            {
                if (UnitManager.instance.SelectedHero != null)
                {
                    var enemy = (EnemyBase)occupiedUnit;
                    UnitManager.instance.SetSelectedHero(null);
                    Destroy(occupiedUnit.gameObject);


                }
            }
        }
        else
        {
            if (UnitManager.instance.SelectedHero != null && WalkableCheck)
            {
                SetUnit(UnitManager.instance.SelectedHero);
                UnitManager.instance.SetSelectedHero(null);
            }
        }
    }



    public void SetUnit(UnitBase unit)
    {
        if(unit.occupiedTile!=null)
        {
            unit.occupiedTile.occupiedUnit = null;
        }    

        unit.transform.position = transform.position;
        occupiedUnit = unit;
        unit.occupiedTile = this;
    }
}
