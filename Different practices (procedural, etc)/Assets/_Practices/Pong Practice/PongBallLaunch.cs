using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBallLaunch : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float speed = 5f;

    private void Start()
    {
        Launch();
    }
    public void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }
}
