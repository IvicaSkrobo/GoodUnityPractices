using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cannon : MonoBehaviour {
    [SerializeField] private Ball _ballPrefab;

    [SerializeField] private Transform ballspawn;


    [SerializeField] private float force = 10;

 


    void OnFire()
    {
        var ball = Instantiate(_ballPrefab, ballspawn.position, ballspawn.rotation);
        ball.Init(ballspawn.transform.forward * force,false);
    
    }    
}
