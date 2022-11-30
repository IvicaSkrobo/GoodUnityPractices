using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CannonMovement : MonoBehaviour
{
       

    public float speed;
    public float rotationSpeed;
    Vector2 movement;
    DefaultInputActions playerInput;


    private void Awake()
    {
        playerInput = new DefaultInputActions();
    }
    private void OnEnable()
    {
        playerInput.Player.Enable();
    
    }


    private void OnDestroy()
    {
        playerInput.Player.Disable();
    }

    private void Update()
    {
        movement = playerInput.Player.Move.ReadValue<Vector2>().normalized;

        //  rb.velocity = speed * movement.y * Time.deltaTime *transform.up;

        transform.Translate(speed * movement.y * Time.deltaTime * Vector3.up);


        if (movement.x!=0)
        {
            transform.Rotate(-Vector3.forward*movement.x*Time.deltaTime* rotationSpeed);
        }


    }


}
