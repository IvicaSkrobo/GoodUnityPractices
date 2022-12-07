
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawWithoutPhysics : MonoBehaviour
{
    [SerializeField]
    SimpleLine line;

    SimpleLine activeLine;


    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            activeLine = Instantiate(line);
           
        }

        if(Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = -Helpers.Camera.transform.position.z;
            Vector2 point = Helpers.Camera.ScreenToWorldPoint(mousePos);
            
            
            activeLine.UpdateLine(point); 
        }
        else
        {
            activeLine = null;
        }
        
    }


}
