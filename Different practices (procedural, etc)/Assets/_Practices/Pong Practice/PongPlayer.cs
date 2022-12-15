using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PongPlayer : MonoBehaviour
{
 
    [SerializeField]
    float speed = 15f;
    Rigidbody2D rb;
    Vector2 moveDir;

    public InputActionAsset inputActionsAsset;
    InputAction move;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        move = inputActionsAsset.FindAction("Move");


    }
    private void OnEnable()
    {
        inputActionsAsset.Enable();
        move.started += OnMove;
        move.canceled += OnMoveCanceled;

    }

    private void OnDisable()
    {
        move.started -= OnMove;
        move.canceled -= OnMoveCanceled;

        inputActionsAsset.Disable();

    }

    

    private void FixedUpdate()
    {
        rb.velocity = moveDir.y * Vector2.up * speed * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        moveDir.y = 0;
    }
}
