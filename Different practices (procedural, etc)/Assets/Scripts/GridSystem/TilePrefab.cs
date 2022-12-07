using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TilePrefab : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{

    [SerializeField] Color originalColor;
    [SerializeField] Color offsetColor;
    [SerializeField]
    GameObject highlight;

    SpriteRenderer sprite;

 

    public void Init(bool offset)
    {
        sprite = GetComponent<SpriteRenderer>();

        sprite.color = offset ? offsetColor : originalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.gameObject.SetActive(false);
    }

}
