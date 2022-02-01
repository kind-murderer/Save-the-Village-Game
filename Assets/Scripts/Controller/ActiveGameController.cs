using UnityEngine;

public class ActiveGameController : MonoBehaviour
{
    TimersServer timersServer;
    ActiveGameView activeGameView;
    ResourcesServer resourcesServer;
    BattleServer battleServer;
    MusicServer musicServer;

    private void Start()
    {
        musicServer = gameObject.GetComponent<MusicServer>();
        timersServer = gameObject.GetComponent<TimersServer>();
        battleServer = gameObject.GetComponent<BattleServer>();
        resourcesServer = gameObject.GetComponent<ResourcesServer>();
        activeGameView = gameObject.GetComponent<ActiveGameView>();

        timersServer.timers["TimerFinishMining"].TimeIsOut += () => musicServer.PlaySoundEffect(MusicServer.SoundEffect.GainGems);
        timersServer.timers["TimerPaySalary"].TimeIsOut += () => musicServer.PlaySoundEffect(MusicServer.SoundEffect.GiveGems);

        activeGameView.buttonHireMiner.onClick.AddListener(() =>
        {
            resourcesServer.HireMiner();
            musicServer.PlaySoundEffect(MusicServer.SoundEffect.StoneImpact);
            
        });
        activeGameView.buttonHireSlayer.onClick.AddListener(() =>
        {
            resourcesServer.HireSlayer();
            musicServer.PlaySoundEffect(MusicServer.SoundEffect.DrawingSword);
        });
        activeGameView.buttonPauseMenu.onClick.AddListener(() => GameManager.Instance.ChangeGameState(GameManager.GameState.Suspended));
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
}
