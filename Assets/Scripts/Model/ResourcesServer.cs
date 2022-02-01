using System;
using UnityEngine;

public class ResourcesServer : MonoBehaviour
{
    [SerializeField]
    private int _startWithNumberOfGems;
    [SerializeField]
    private int _costOfDragonSlayer, _costOfMiner, _quantityOfProducedGems, _salaryOfSlayer;

    public event Action<int> SlayersDiscontent;
    public event Action MinerWasHired; //for timers 
    public event Action SlayerWasHired; //for timers 
    /// <summary>
    /// int gemsAddition, int minersAddition, int slayersAddition
    /// </summary>
    public event Action<int, int, int> ResourcesHasChanged; //for view
    public event Action OnTwoThousandGems;

    public bool CanHireMiner { get => CurrentGems >= _costOfMiner ? true : false; } //for view
    public bool CanHireSlayer { get => CurrentGems >= _costOfDragonSlayer ? true : false; } //for view
    public int CurrentGems { get; private set; } = 0;
    public int NumberOfMiners { get; private set; } = 0;
    public int NumberOfSlayers { get; private set; } = 0;

    private void Start()
    {
        CurrentGems = _startWithNumberOfGems;
        TimersServer timersManager = gameObject.GetComponent<TimersServer>();
        timersManager.Timers["TimerPaySalary"].TimeIsOut += PaySalary;
        timersManager.Timers["TimerFinishMining"].TimeIsOut += AddProducedGems;
        BattleServer battleServer = gameObject.GetComponent<BattleServer>();
        battleServer.OnBattleWin += ChangeAfterBattle;
    }

    private void PaySalary()
    {
        int previousGems = CurrentGems;
        int previousSlayers = NumberOfSlayers;

        int remains = CurrentGems - _salaryOfSlayer * NumberOfSlayers;
        if(remains >= 0)
        {
            CurrentGems = remains;
            SlayersDiscontent?.Invoke(0);
        }
        else
        {
            int numberOfSlayersYouCanAffort = CurrentGems / _salaryOfSlayer;
            CurrentGems -= numberOfSlayersYouCanAffort * _salaryOfSlayer;
            SlayersDiscontent?.Invoke(NumberOfSlayers - numberOfSlayersYouCanAffort);
            NumberOfSlayers = numberOfSlayersYouCanAffort;
        }
        ResourcesHasChanged?.Invoke(CurrentGems - previousGems, 0, NumberOfSlayers - previousSlayers);
    }

    private void AddProducedGems()
    {
        CurrentGems += _quantityOfProducedGems * NumberOfMiners;
        ResourcesHasChanged?.Invoke(_quantityOfProducedGems * NumberOfMiners, 0, 0);
        if (CurrentGems >= 2000)
        {
            OnTwoThousandGems?.Invoke();
        }
    }

    public void HireMiner()
    {
        if (CurrentGems - _costOfMiner >= 0)
        {
            CurrentGems -= _costOfMiner;
            NumberOfMiners++;
            MinerWasHired?.Invoke();
            ResourcesHasChanged?.Invoke(-_costOfMiner, 1, 0);
        }
    }
    public void HireSlayer()
    {
        if (CurrentGems - _costOfDragonSlayer >= 0)
        {
            CurrentGems -= _costOfDragonSlayer;
            NumberOfSlayers++;
            SlayerWasHired?.Invoke();
            ResourcesHasChanged?.Invoke(-_costOfDragonSlayer, 0, 1);
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
        CurrentGems = _startWithNumberOfGems;
        NumberOfMiners = 0;
        NumberOfSlayers = 0;
    }
}
