using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubeMovement : MonoBehaviour
{

    [SerializeField]
    float rotSpeed = 3f;
    bool isMoving = false;

    Vector2 move;


    private void Update()
    {
        if (isMoving) return;

        if (move.x > 0)
        {
            Assemble(Vector3.right);
        }
        else if (move.x < 0)
        {
            Assemble(Vector3.left);

        }
        else if (move.y > 0)
        {
            Assemble(Vector3.forward);

        }
        else if (move.y < 0)
        {
            Assemble(Vector3.back);

        }

        void Assemble(Vector3 dir)
        {
            //half a unit right and half a unit down for anchor point
            var anchor = transform.position + (Vector3.down + dir) * 0.5f;
            // axis is the perpendicular vector for up and right
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(RollCube(anchor, axis));
        }
    }

    void OnMove(InputValue movementValue)
    {

        move = movementValue.Get<Vector2>();
       
    }

    private IEnumerator RollCube(Vector3 anchor, Vector3 axis)
    {
        isMoving = true;

        for(int i=0; i<(90/rotSpeed); i++)
        {
            transform.RotateAround(anchor, axis, rotSpeed);
            yield return Helpers.GetWait(0.01f);

        }

        isMoving = false;
    }
}
