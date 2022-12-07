using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PongScoreManager : MonoBehaviour
{
    public static PongScoreManager Instance;

    
    public GameObject ball;

    public TextMeshProUGUI player1ScoreTxt;
    public TextMeshProUGUI player2ScoreTxt;

    private int player1Score;
    private int player2Score;
    Vector3  ballStartPos;

    private void Awake()
    {
        Instance = this;
        ballStartPos = ball.transform.position;
    }
    public void Player1Scored()
    {
        player1Score++;
        player1ScoreTxt.text = player1Score.ToString();
     
    
        if (player1Score == 5)
        {
            ResetGame();
        }
        ball.transform.position = ballStartPos;
        ball.GetComponent<PongBallLaunch>().Launch();
    }

    public void Player2Scored()
    {
        player2Score++;
        player2ScoreTxt.text = player2Score.ToString();
   
    
        if (player2Score == 5)
        {
            ResetGame();
        }
        ball.transform.position = ballStartPos;
        ball.GetComponent<PongBallLaunch>().Launch();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
