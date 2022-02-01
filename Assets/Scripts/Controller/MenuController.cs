using UnityEngine;

public class MenuController : MonoBehaviour
{
    
    public void HandleStartButton()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.ActiveGame, true);
    }
    public void HandleResumeButton()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.ActiveGame);
    }
    public void HandleRestartButton()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.ActiveGame, true);
    }
    public void HandleToMenuButton()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Menu);
    }
    public void HabdleExitButton()
    {
        GameManager.Instance.ExitApplication();
    }
}
