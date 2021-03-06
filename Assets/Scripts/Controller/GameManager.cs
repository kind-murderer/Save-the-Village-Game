using UnityEngine;

// !!!! Edit > ProjectSettings > ScriptExecutionOrder. Make this script RUN BEFORE OTHER custom scripts, but after GameManager. !!!!

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Menu,
        Suspended,
        ActiveGame,
    }

    public static GameManager Instance;

    private ActiveGameController activeGameController;
    private MusicServer musicServer;
    public GameState CurrentState { get; private set; } = GameState.Menu;
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
