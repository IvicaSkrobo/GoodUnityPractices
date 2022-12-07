using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    bool isPlayer1Goal = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            if (!isPlayer1Goal)
            {
                PongScoreManager.Instance.Player1Scored();
            }
            else
            {
                PongScoreManager.Instance.Player2Scored();

            }
        }
    }
}
