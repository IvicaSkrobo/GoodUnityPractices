using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMovement : MonoBehaviour
{
    Vector3 dir;
    [SerializeField]
    float speed;

    Rigidbody rb;
    [SerializeField]
    float jumpHeight = 5f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        var velocity = dir * speed * Time.deltaTime;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

  
    }


    public void OnMove(InputValue value)
    {


        var values = value.Get<Vector2>();
        dir.z = values.x;
        dir.x = -values.y;


    }

    public void OnFire()
    {
        rb.AddForce( jumpHeight * Vector2.up);
    }

}
