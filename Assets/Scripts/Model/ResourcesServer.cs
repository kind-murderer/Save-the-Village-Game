using System;
using UnityEngine;

public class ResourcesServer : MonoBehaviour
{
    [SerializeField]
    private int startWithNumberOfGems;
    [SerializeField]
    private int costOfDragonSlayer, costOfMiner, quantityOfProducedGems, salaryOfSlayer;
    public int CurrentGems { get; private set; } = 0;
    public int NumberOfMiners { get; private set; } = 0;
    public int NumberOfSlayers { get; private set; } = 0;

    public event Action<int> SlayersDiscontent;
    public bool CanHireMiner { get => CurrentGems >= costOfMiner ? true : false; } //for view
    public bool CanHireSlayer { get => CurrentGems >= costOfDragonSlayer ? true : false; } //for view
    public event Action MinerWasHired; //for timers 
    public event Action SlayerWasHired; //for timers 
    /// <summary>
    /// int gemsAddition, int minersAddition, int slayersAddition
    /// </summary>
    public event Action<int, int, int> ResourcesHasChanged; //for view
    public event Action OnTwoThousandGems;

    private void Start()
    {
        CurrentGems = startWithNumberOfGems;
        TimersServer timersManager = gameObject.GetComponent<TimersServer>();
        timersManager.timers["TimerPaySalary"].TimeIsOut += PaySalary;
        timersManager.timers["TimerFinishMining"].TimeIsOut += AddProducedGems;
        BattleServer battleServer = gameObject.GetComponent<BattleServer>();
        battleServer.OnBattleWin += ChangeAfterBattle;
    }

    private void PaySalary()
    {
        int previousGems = CurrentGems;
        int previousSlayers = NumberOfSlayers;

        int remains = CurrentGems - salaryOfSlayer * NumberOfSlayers;
        if(remains >= 0)
        {
            CurrentGems = remains;
            SlayersDiscontent?.Invoke(0);
        }
        else
        {
            int numberOfSlayersYouCanAffort = CurrentGems / salaryOfSlayer;
            CurrentGems -= numberOfSlayersYouCanAffort * salaryOfSlayer;
            SlayersDiscontent?.Invoke(NumberOfSlayers - numberOfSlayersYouCanAffort);
            NumberOfSlayers = numberOfSlayersYouCanAffort;
        }
        ResourcesHasChanged?.Invoke(CurrentGems - previousGems, 0, NumberOfSlayers - previousSlayers);
    }

    private void AddProducedGems()
    {
        CurrentGems += quantityOfProducedGems * NumberOfMiners;
        ResourcesHasChanged?.Invoke(quantityOfProducedGems * NumberOfMiners, 0, 0);
        if (CurrentGems >= 2000)
        {
            OnTwoThousandGems?.Invoke();
        }
    }

    public void HireMiner()
    {
        if (CurrentGems - costOfMiner >= 0)
        {
            CurrentGems -= costOfMiner;
            NumberOfMiners++;
            MinerWasHired?.Invoke();
            ResourcesHasChanged?.Invoke(-costOfMiner, 1, 0);
        }
    }
    public void HireSlayer()
    {
        if (CurrentGems - costOfDragonSlayer >= 0)
        {
            CurrentGems -= costOfDragonSlayer;
            NumberOfSlayers++;
            SlayerWasHired?.Invoke();
            ResourcesHasChanged?.Invoke(-costOfDragonSlayer, 0, 1);
        } 
    }

    public void ChangeAfterBattle(int numberOfDragons, int numberOfFallen, bool didDragonJoinYou)
    {
        NumberOfSlayers -= numberOfFallen;
        if (didDragonJoinYou)
        {
            NumberOfSlayers++;
            ResourcesHasChanged(0, 0, -numberOfFallen + 1);
        }
        else
        {
            ResourcesHasChanged(0, 0, -numberOfFallen);
        }
    }

    public void ResetResources()
    {
        CurrentGems = startWithNumberOfGems;
        NumberOfMiners = 0;
        NumberOfSlayers = 0;
    }
}
