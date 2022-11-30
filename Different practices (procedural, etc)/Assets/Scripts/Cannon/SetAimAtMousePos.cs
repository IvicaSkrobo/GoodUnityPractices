using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetAimAtMousePos : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {

        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = -Helpers.Camera.transform.position.z;
       this.transform.position = Helpers.Camera.ScreenToWorldPoint(mousePos);
    


    }
}
