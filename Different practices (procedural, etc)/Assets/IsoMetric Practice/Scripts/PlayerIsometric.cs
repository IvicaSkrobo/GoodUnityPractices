using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIsometric : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotationSpeed = 360f;
    


    Vector3 inputDir;
    Rigidbody rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        Look();
    }
    private void FixedUpdate()
    {
        Move();
    }

 
    public void OnMove(InputValue value)
    {
        inputDir = value.Get<Vector2>();
        inputDir.z = inputDir.y;
        inputDir.y = 0;
        
    }

    void Look()
    {
        if(inputDir != Vector3.zero)
        {

            // to move up in isometric fashion correctly, we skew the coordinate system in sense


            var skewedInput = Helpers.ToIsometric(inputDir);

            // look at the direction we are inputing
            //and rotate around up axis towards our inputDir

            var rot = Quaternion.LookRotation(skewedInput, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotationSpeed*Time.deltaTime);
           

        }

    }

    void Move()
    {
        rb.MovePosition(transform.position + transform.forward*inputDir.magnitude*speed*Time.deltaTime);

    }

}
