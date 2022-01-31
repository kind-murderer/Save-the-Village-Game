using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleResultController : MonoBehaviour
{
    TimersServer timersServer;
    RaidView raidView;
    VictoryView victoryView;
    DefeatView defeatView;
    ResourcesServer resourcesServer;
    BattleServer battleServer;
    MusicServer musicServer;

    void Start()
    {
        musicServer = gameObject.GetComponent<MusicServer>();
        timersServer = gameObject.GetComponent<TimersServer>();
        resourcesServer = gameObject.GetComponent<ResourcesServer>();
        battleServer = gameObject.GetComponent<BattleServer>();
        raidView = gameObject.GetComponent<RaidView>();
        victoryView = gameObject.GetComponent<VictoryView>();
        defeatView = gameObject.GetComponent<DefeatView>();

        resourcesServer.OnTwoThousandGems += HandleGameVictory;
        
        battleServer.OnBattleWin += HandleBattleWin;
        battleServer.OnBattleLose += HandleDefeat;

        raidView.continueButton.onClick.AddListener(ContinueBattling);
        victoryView.buttonToMenu.onClick.AddListener(ReturnToMenu);
        defeatView.buttonToMenu.onClick.AddListener(ReturnToMenu);
        defeatView.buttonRetry.onClick.AddListener(() => GameManager.Instance.ChangeGameState(GameManager.GameState.ActiveGame, true));
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
        musicServer.ChangeBackgroundSound(MusicServer.SoundBackground.Defeat);
    }
    private void HandleGameVictory()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Victory);
        victoryView.InformOfVictoryAndSummarize(resourcesServer.NumberOfMiners, resourcesServer.NumberOfSlayers,
            battleServer.DefeatedDragons, battleServer.Day);
        victoryView.OpenWindow();
        musicServer.ChangeBackgroundSound(MusicServer.SoundBackground.Victory);
    }
    private void ReturnToMenu()
    {
        GameManager.Instance.ChangeGameState(GameManager.GameState.Menu);
    }
}
