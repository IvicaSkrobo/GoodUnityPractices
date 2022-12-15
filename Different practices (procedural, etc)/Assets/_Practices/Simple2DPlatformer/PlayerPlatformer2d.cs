using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPlatformer2d : MonoBehaviour
{
    [Header("Checks")]
    [SerializeField]
    Transform groundedTransform;
    [SerializeField] 
    Transform ceilingCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    float groundCheckRadius = 0.5f;
 

    [SerializeField]
    float speed = 20f;

    [Header("Jumps")]
    [SerializeField]
    float jumpForce = 10f;
    [SerializeField]
    int maxjumpCount = 2;
    [SerializeField]
    float jumpHeight;
    int jumpCount = 2;
  

    Rigidbody2D rb;

    Vector2 direction;
    bool isGrounded = false;
    bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        Jump();
    }

    private void FixedUpdate()
    {
        //grounded check
        isGrounded = Physics2D.OverlapCircle(groundedTransform.position, groundCheckRadius, groundLayer);
        if(isGrounded)
        {
            jumpCount = 0;
        }
        //physics movement
        Movement();

    }

    void Movement()
    {
        rb.velocity = new Vector2(direction.x*speed, rb.velocity.y);
        if (direction.x > 0 && !isFacingRight)
        {
            FlipCharacter();
        }
        else if(direction.x<0 && isFacingRight)
        {
            FlipCharacter();
        }
    }


    private void Jump()
    {
        if (jumpCount < maxjumpCount)
        {
            var jumpVel = Mathf.Sqrt(2 * -Physics.gravity.y* jumpHeight);


            rb.velocity = new Vector2(rb.velocity.x, jumpVel);

            jumpCount++;
        }
    }



    void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundedTransform.position, groundCheckRadius);
        Gizmos.color = Color.red;
    }

}
