using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //private bool isPaused = false;

    public enum GameState
    { 
        ActiveGame,
        Raid, 
        Defeat, 
        Victory
    }
    public GameState CurrentState { get; private set; } = GameState.ActiveGame;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void ChangeGameState(GameState newState)
    {
        CurrentState = newState;
        switch (newState)
        {
            case GameState.ActiveGame:
                RunTime();
                break;
            case GameState.Raid:
                PauseTime();
                break;
            case GameState.Defeat:
                PauseTime();
                break;
            case GameState.Victory:
                PauseTime();
                break;
        }
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
    }
    public void RunTime()
    {
        Time.timeScale = 1;
    }
}