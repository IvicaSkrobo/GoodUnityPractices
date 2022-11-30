using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonScript : MonoBehaviour
{
    Vector3 mousePos;

    [Range(0,100)]
    [SerializeField]
    float speedRotation = 3f;
    [SerializeField]
    float speedMovement = 3f;

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
        var transformPosOnlyY = transform.position;
        transformPosOnlyY.x = 0;
        transformPosOnlyY.z = 0;


       
   
      transform.up = Vector3.MoveTowards(transform.up, mousePos2- transformPosOnlyY, speedRotation * Time.deltaTime);


    }


    public void OnFire()
    {
      Instantiate(cannonBallPrefab, shootFrom.position, Quaternion.identity).Init(transform.up);
    }
    


}
