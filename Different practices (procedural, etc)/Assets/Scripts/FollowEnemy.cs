using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;

    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<CannonMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        var dir = target.position - transform.position;

        transform.up = Vector3.MoveTowards(transform.up, dir, Time.deltaTime * speed);
      
        transform.position = Vector3.MoveTowards(transform.position, transform.position+transform.up, Time.deltaTime * speed);

    }
}
