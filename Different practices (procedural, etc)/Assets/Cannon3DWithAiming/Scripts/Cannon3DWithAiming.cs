using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon3DWithAiming : MonoBehaviour
{
    [SerializeField]
    Projection projection;
    [SerializeField]
    Ball ball;
    [SerializeField]
    float force = 20;
    [SerializeField]
    private Transform ballSpawn;
    [SerializeField]
    private Transform barrelPivot;
    [SerializeField]
    private float rotateSpeed = 30;
    [SerializeField] private Transform leftWheel, rightWheel;

    Vector2 moveDir;

    private void Update()
    {
        if (moveDir.x < 0)
        {
            transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
           
            leftWheel.Rotate(Vector3.back * rotateSpeed * 1.5f * Time.deltaTime);
            rightWheel.Rotate(Vector3.forward * rotateSpeed * 1.5f * Time.deltaTime);

        }
        else if (moveDir.x > 0)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            leftWheel.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
            rightWheel.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);
        }

        if (moveDir.y < 0)
        {
            barrelPivot.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);

        }
        else if (moveDir.y > 0)
        {
            barrelPivot.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

        }

        projection.SimulateTrajectory(ball, ballSpawn.position, ballSpawn.transform.forward * force);

    }

    public void OnMove(InputValue inputMoveValue)
    {
        moveDir = inputMoveValue.Get<Vector2>();
    }

    public void OnFire()
    {
        var ballLocal = Instantiate(ball, ballSpawn.position, ballSpawn.rotation);
        ballLocal.Init(ballSpawn.transform.forward* force,false);
    }
}
