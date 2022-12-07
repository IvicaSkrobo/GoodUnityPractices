using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{


    [SerializeField] private float triggerForce = 2;
    [SerializeField] private float explosionForce = 100;
    [SerializeField] private float explosionRadius = 5;


    private void OnCollisionEnter(Collision collision)
    {


        if (collision.relativeVelocity.magnitude >= triggerForce)
        {

            var surroundingObjects = Physics.OverlapSphere(transform.position, explosionRadius);


            foreach (var collObj in surroundingObjects)
            {
                var rb = collObj.GetComponent<Rigidbody>();
                if (rb == null) continue;

                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            }
            Destroy(gameObject);

        }
    }
}