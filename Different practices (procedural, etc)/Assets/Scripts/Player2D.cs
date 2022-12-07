using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2D : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);

    }

    void OnMove(InputValue inputValue)
    {
        movement = inputValue.Get<Vector2>();

    }

}
