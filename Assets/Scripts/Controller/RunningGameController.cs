using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// !!!! Edit > ProjectSettings > ScriptExecutionOrder. Make this script RUN BEFORE OTHER custom scripts, but after GameManager. !!!!
public class RunningGameController : MonoBehaviour
{
    TimersServer timersServer;
    ActiveGameView activeGameView;
    RaidView raidView;
    VictoryView victoryView;
    DefeatView defeatView;
    ResourcesServer resourcesServer;
    BattleServer battleServer;

    private void Start()
    {
        timersServer = gameObject.GetComponent<TimersServer>();
        timersServer.InitializeTimersArray();

        resourcesServer = gameObject.GetComponent<ResourcesServer>();
        resourcesServer.OnTwoThousandGems += HandleGameVictory;
        activeGameView = gameObject.GetComponent<ActiveGameView>();
        activeGameView.buttonHireMiner.onClick.AddListener(resourcesServer.HireMiner);
        activeGameView.buttonHireSlayer.onClick.AddListener(resourcesServer.HireSlayer);
        activeGameView.buttonPauseMenu.onClick.AddListener(() => GameManager.Instance.ChangeGameState(GameManager.GameState.Suspended));

        battleServer = gameObject.GetComponent<BattleServer>();
        battleServer.OnBattleWin += HandleBattleWin;
        battleServer.OnBattleLose += HandleDefeat;

        raidView = gameObject.GetComponent<RaidView>();
        raidView.continueButton.onClick.AddListener(ContinueBattling);

        victoryView = gameObject.GetComponent<VictoryView>();
        victoryView.buttonToMenu.onClick.AddListener(ReturnToMenu);
        defeatView = gameObject.GetComponent<DefeatView>();
        defeatView.buttonToMenu.onClick.AddListener(ReturnToMenu);
        defeatView.buttonRetry.onClick.AddListener(() => GameManager.Instance.ChangeGameState(GameManager.GameState.ActiveGame, true));
    }

    public void StartOverGame()
    {
        activeGameView.UpdateResourcesAndButtons();
        timersServer.StartBattlingTimer();
        activeGameView.ChangeActiveCanvas(true);
    }
    public void ResetGame()
    {
        battleServer.ResetBattles();
        resourcesServer.ResetResources();
        timersServer.ResetTimers();
        activeGameView.ResetView();
    }
    private void HandleBattleWin(int numberOfDragons, int numberOfFallen, bool didDragonJoinYou)
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Suspended);
        raidView.OpenWindow();
        raidView.InformOfBattleResult(battleServer.Day, numberOfDragons, numberOfFallen, didDragonJoinYou);
    }
    private void ContinueBattling()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.ActiveGame);
        timersServer.StartBattlingTimer();
    }
    private void HandleDefeat()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Suspended);
        defeatView.InformOfDefeat(battleServer.Day);
        defeatView.OpenWindow();
    }
    private void HandleGameVictory()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Victory);
        victoryView.InformOfVictoryAndSummarize(resourcesServer.NumberOfMiners, resourcesServer.NumberOfSlayers, 
            battleServer.DefeatedDragons, battleServer.Day);
        victoryView.OpenWindow();
    }
    private void ReturnToMenu()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Menu);
    }
}
