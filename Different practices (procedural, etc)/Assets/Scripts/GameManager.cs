using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public static GameManager Instance;
    public static event Action<GameState> OnGameStateChanged;

    public GameState currentGameState;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        currentGameState = GameState.MenuState;
    }

    //seems legit
    void UpdateGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MenuState:
                break;
            case GameState.PauseState:
                HandlePause();
                break;
            case GameState.StartState:
                break;
            case GameState.WinState:
                break;
            case GameState.LoseState:
                break;
            default: throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
               
        }


    }

    private void HandlePause()
    {
        OnGameStateChanged?.Invoke(GameState.PauseState);
    }

    public enum GameState
    {
    MenuState,
    PauseState,
    StartState,
    WinState,
    LoseState


    }
}