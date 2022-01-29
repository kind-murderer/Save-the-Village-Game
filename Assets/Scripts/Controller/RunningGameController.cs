using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// !!!! Edit > ProjectSettings > ScriptExecutionOrder. Make this script RUN BEFORE OTHER custom scripts. !!!!
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

        battleServer = gameObject.GetComponent<BattleServer>();
        battleServer.OnBattleWin += HandleBattleWin;
        battleServer.OnBattleLose += HandleDefeat;

        raidView = gameObject.GetComponent<RaidView>();
        raidView.continueButton.onClick.AddListener(ContinueBattling);

        victoryView = gameObject.GetComponent<VictoryView>();
        defeatView = gameObject.GetComponent<DefeatView>();
    }
    
    private void HandleBattleWin(int numberOfDragons, int numberOfFallen, bool didDragonJoinYou)
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Raid);
        raidView.ChangeActiveCanvas(true);
        raidView.InformOfBattleResult(battleServer.Day, numberOfDragons, numberOfFallen, didDragonJoinYou);
    }
    private void ContinueBattling()
    {
        raidView.ChangeActiveCanvas(false);
        GameManager.Instance.ChangeGameState(GameManager.GameState.ActiveGame);
        timersServer.ContinueBattleTimer();
    }
    private void HandleDefeat()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Defeat);
        defeatView.InformOfDefeat(battleServer.Day);
        defeatView.ChangeActiveCanvas(true);
    }
    private void HandleGameVictory()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Victory);
        victoryView.InformOfVictoryAndSummarize(resourcesServer.NumberOfMiners, resourcesServer.NumberOfSlayers, 
            battleServer.DefeatedDragons, battleServer.Day);
        victoryView.ChangeActiveCanvas(true);
    }
}
