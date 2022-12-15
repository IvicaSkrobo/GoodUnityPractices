using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithinRadius : MonoBehaviour
{
    [SerializeField]
    float radius;
    [SerializeField]
    float speed = 15f;

    public Rigidbody rb;

    Vector3 endpos;
    Vector3 startingPos;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
           startingPos = this.transform.position;
        CalculateNewEndPos();
    }

    private void Update()
    {
        Vector3 interpoLatedPos = Vector3.Lerp(rb.position, endpos, Time.deltaTime * speed);

        rb.MovePosition(interpoLatedPos);

        if (Vector3.Distance(rb.position, endpos) < 0.2f)
        {
            CalculateNewEndPos();
        }
    }

    void FixedUpdate()
    {
       
    }



    private void CalculateNewEndPos()
    {
        endpos = startingPos+Random.insideUnitSphere * radius;

    }
}
