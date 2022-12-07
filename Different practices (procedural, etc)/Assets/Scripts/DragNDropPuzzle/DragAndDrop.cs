
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public float speed;

    Vector3 dragOffset;
    Camera cam;
    Vector3 mousePos;

    bool isDraging = false;
    Vector2 startPos;

    PuzzleSlot thisPieceSlot;
    bool isPiecePlaced;
    // Start is called before the first frame update
    void Start()
    {
        cam = Helpers.Camera;
        startPos = this.transform.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
      /*  if (isPiecePlaced)
        { return; }
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);*/
    }

    void Update()
    {
        if (isPiecePlaced)
        { return; }
        if (!isDraging)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPiecePlaced)
        { return; }
        isDraging = true;

        dragOffset = transform.position - GetMousePos();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (thisPieceSlot)
        {
            if (Vector2.Distance(this.transform.position, thisPieceSlot.transform.position)<1f)
            {
                this.transform.position = thisPieceSlot.transform.position;
                isPiecePlaced = true;
            }
            else
            {
                this.transform.position = startPos;
            }
        }
        else
        {
            this.transform.position = startPos;

        }
        isDraging = false;

    }


    private Vector3 GetMousePos()
    {
        //to be perfectly aligned camera has to be in orthographic mode
        mousePos = Mouse.current.position.ReadValue();
        mousePos.z = -cam.transform.position.z;
      
     

        mousePos = cam.ScreenToWorldPoint(mousePos);
        return mousePos;
    }
    public void SetSprite(Sprite newSprite, PuzzleSlot slot)
    {
        //using get component here not for speed, but for modularity of the script
        GetComponent<SpriteRenderer>().sprite = newSprite;
        thisPieceSlot = slot;
        thisPieceSlot.SetSprite(newSprite);

    }


}
