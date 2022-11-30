using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 30;


    public Vector3 rotateAxis;


    // Update is called once per frame
    void Update()
    {
     
            transform.Rotate(rotateAxis* speed * Time.deltaTime);
      
    }
}
