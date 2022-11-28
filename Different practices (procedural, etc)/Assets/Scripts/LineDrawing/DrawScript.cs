using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawScript : MonoBehaviour
{
    public static DrawScript instance;


    [SerializeField]
    Line prefab;
    Line currentLine;
    Camera _cam;
  
    public const float RESOLUTION = 0.1f;

    private void Awake()
    {
        if(instance==null)
        {instance = this; }
        _cam = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
    

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {

            currentLine = Instantiate(prefab, Vector3.zero, Quaternion.identity);  // important to start at zero, because you instantiate it then at the mouse position 
        }

        if(Mouse.current.leftButton.IsPressed())
        {
            Vector3 pos = Mouse.current.position.ReadValue();
            pos.z = _cam.transform.position.z;   // if you set this to opposite you get a cool effect 
            Vector2 position = -_cam.ScreenToWorldPoint(pos);

            currentLine.SetPosition(position);
        }


    }
}
