using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    private BoxCollider2D coll;
    private Rigidbody2D rb;


    private float width;
    [SerializeField]
    private float scrollSpeed = -2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();


        width = coll.size.x * transform.localScale.x;
        
        coll.enabled = false;
        rb.velocity = new Vector2(scrollSpeed, 0);
    }
    private void Update()
    {
        if(transform.position.x<-width)  // when it passes the widht, gets out of the screen
        {
            Vector3 resetPosition = new Vector3(width * 2f, 0,0); 
            transform.position = transform.position + resetPosition;
        }
    }
}
