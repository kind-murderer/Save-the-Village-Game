using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    ActiveGameController activeGameController;
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
        TimersServer timersServer = GetComponent<TimersServer>();
        timersServer.InitializeTimersArray();
        PauseTime();
        activeGameController = gameObject.GetComponent<ActiveGameController>();
        musicServer = gameObject.GetComponent<MusicServer>();
    }
    public void ChangeGameState(GameState newState, bool needRestart = false)
    {
        CurrentState = newState;
        switch (newState)
        {
            case GameState.Menu:
                activeGameController.ResetGame();
                musicServer.ChangeBackgroundSound(MusicServer.SoundBackground.Menu);
                break;
            case GameState.ActiveGame:
                if (needRestart)
                {
                    activeGameController.ResetGame();
                    activeGameController.StartOverGame(); 
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
    public void ExitApplication()
    {
        Application.Quit();
    }
}
