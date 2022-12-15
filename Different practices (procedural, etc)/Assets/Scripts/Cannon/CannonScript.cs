using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonScript : MonoBehaviour
{
    Vector3 mousePos;



    [SerializeField]
    Transform shootFrom;

    [SerializeField]
    CannonBall cannonBallPrefab;


    // Update is called once per frame
    void Update()
    {
        mousePos = Mouse.current.position.ReadValue();
        mousePos.z =  -Helpers.Camera.transform.position.z;

        var mousePos2 = Helpers.Camera.ScreenToWorldPoint(mousePos);

        // works splendidly
        var transformPosWithoutZ = transform.position;
     
        transformPosWithoutZ.z = 0;


   

    }


    public void OnFire()
    {
      Instantiate(cannonBallPrefab, shootFrom.position, Quaternion.identity).Init(transform.up);
    }
    


}
