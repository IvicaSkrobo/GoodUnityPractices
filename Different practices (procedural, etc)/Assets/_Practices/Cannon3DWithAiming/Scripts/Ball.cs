using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {


    [SerializeField]  Rigidbody rb;
    private bool isGhost;


    public void Init(Vector3 velocity, bool isGhost) {
        rb.AddForce(velocity,ForceMode.Impulse);
        this.isGhost = isGhost;

    }


    public void OnCollisionEnter(Collision col)
    {
        if (isGhost) return;
       // Instantiate(_poofPrefab, col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));
   
    }



}
