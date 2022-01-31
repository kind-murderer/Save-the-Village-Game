using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
    MenuView menuView;
    PauseMenuView pauseMenuView;

    void Start()
    {
        menuView = gameObject.GetComponent<MenuView>();
        pauseMenuView = gameObject.GetComponent<PauseMenuView>();
        menuView.buttonStartGame.onClick.AddListener(() => GameManager.Instance.ChangeGameState(GameManager.GameState.ActiveGame, true));
        pauseMenuView.buttonResume.onClick.AddListener(() => GameManager.Instance.ChangeGameState(GameManager.GameState.ActiveGame));
        pauseMenuView.buttonRestart.onClick.AddListener(() => GameManager.Instance.ChangeGameState(GameManager.GameState.ActiveGame, true));
        pauseMenuView.buttonToMenu.onClick.AddListener(() => GameManager.Instance.ChangeGameState(GameManager.GameState.Menu));

    }

}
