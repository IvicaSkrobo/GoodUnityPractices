using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToSpawn : MonoBehaviour
{
    Action<ObjectToSpawn> killAction;


    internal void Init(Action<ObjectToSpawn> killShape)
    {
        killAction = killShape;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        { killAction.Invoke(this); }
     
    }
}
