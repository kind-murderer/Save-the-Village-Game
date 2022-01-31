using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    RunningGameController runningGameController;
    MenuController menuController;
    MusicServer musicServer;
    public enum GameState
    { 
        Menu,
        Suspended,
        ActiveGame,
        Victory
    }
    public GameState CurrentState { get; private set; } = GameState.Menu;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        PauseTime();
        runningGameController = gameObject.GetComponent<RunningGameController>();
        menuController = gameObject.GetComponent<MenuController>();
        musicServer = gameObject.GetComponent<MusicServer>();
    }
    public void ChangeGameState(GameState newState, bool needRestart = false)
    {
        CurrentState = newState;
        switch (newState)
        {
            case GameState.Menu:
                runningGameController.ResetGame();
                musicServer.ChangeBackgroundSound(MusicServer.SoundBackground.Menu);
                break;
            case GameState.ActiveGame:
                if (needRestart)
                {
                    runningGameController.ResetGame();
                    runningGameController.StartOverGame(); 
                    musicServer.ChangeBackgroundSound(MusicServer.SoundBackground.ActiveGame);
                }
                RunTime();
                break;
            case GameState.Suspended:
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
