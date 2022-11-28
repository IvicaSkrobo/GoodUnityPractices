
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public float speed;

    Vector3 dragOffset;
    Camera cam;
    Vector3 mousePos;

    bool isDraging = false;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = transform.position - GetMousePos();
    }



    private Vector3 GetMousePos()
    {
        mousePos = Mouse.current.position.ReadValue();
        mousePos.z = -cam.transform.position.z;

        mousePos = cam.ScreenToWorldPoint(mousePos);

        return mousePos;
    }

}
