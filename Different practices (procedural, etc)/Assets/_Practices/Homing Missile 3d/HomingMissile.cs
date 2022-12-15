using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [Header("Rocket attributes")]
    [SerializeField]
    float speed = 50f;
    [SerializeField]
    float rotSpeed = 50f;

    [Header("References")]
    [SerializeField]
    MoveWithinRadius target;

    Rigidbody rb;

    [Header("PREDICTION")]
    [SerializeField] private float maxDistancePredict = 100;
    [SerializeField] private float minDistancePredict = 5;
    [SerializeField] private float maxTimePrediction = 5;
    private Vector3 standardPrediction, deviatedPrediction;

    [SerializeField] private float deviationAmount = 50;
    [SerializeField] private float deviationSpeed = 2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;

        var leadTimePercentage = Mathf.InverseLerp(minDistancePredict,maxDistancePredict, Vector3.Distance(transform.position, target.transform.position));

        PredictTargetMove(leadTimePercentage);
        AddDeviation(leadTimePercentage);
        RotateTowardsTarget();
    }

    //movement prediction
    private void PredictTargetMove(float leadTimePercentage)
    {
        var predictionTime = Mathf.Lerp(0, maxTimePrediction, leadTimePercentage);

        standardPrediction = target.transform.position + target.rb.velocity * predictionTime;
    }

    // just to get variability
    private void AddDeviation(float leadTimePercentage)
    {
        var deviation = new Vector3(Mathf.Cos(Time.time * deviationSpeed), 0, 0);

        var predictionOffset = transform.TransformDirection(deviation) * deviationAmount * leadTimePercentage;

        deviatedPrediction = standardPrediction + predictionOffset;
    }

    private void RotateTowardsTarget()
    {
        var dir = deviatedPrediction - transform.position;

        var rot = Quaternion.LookRotation(dir);

        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rot, rotSpeed * Time.deltaTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, standardPrediction);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(standardPrediction, deviatedPrediction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Helpers.Camera.transform.parent = null;
        Destroy(this.gameObject);
    }
}
