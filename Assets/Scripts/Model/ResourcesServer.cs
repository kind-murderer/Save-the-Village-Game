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
    public bool DoWeHaveMiners { get => NumberOfMiners > 0; } //for timers
    public bool DoWeHaveSlayers { get => NumberOfSlayers > 0; } //for timmers
    public event Action MinerWasHired; //for timers 
    public event Action SlayerWasHired; //for timers 
    public event Action ResourcesHasChanged; //for view
    private void Start()
    {
        CurrentGems = startWithNumberOfGems;
        TimersServer timersManager = gameObject.GetComponent<TimersServer>();
        timersManager.timers["TimerPaySalary"].TimeIsOut += PaySalary;
        timersManager.timers["TimerFinishMining"].TimeIsOut += AddProducedGems;
    }

    private void PaySalary()
    {
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
        ResourcesHasChanged?.Invoke();
    }

    private void AddProducedGems()
    {
        CurrentGems += quantityOfProducedGems * NumberOfMiners;
        ResourcesHasChanged?.Invoke();
    }

    public void HireMiner()
    {
        if (CurrentGems - costOfMiner >= 0)
        {
            CurrentGems -= costOfMiner;
            NumberOfMiners++;
            MinerWasHired?.Invoke();
            ResourcesHasChanged?.Invoke();
        }
    }
    public void HireSlayer()
    {
        if (CurrentGems - costOfDragonSlayer >= 0)
        {
            CurrentGems -= costOfDragonSlayer;
            NumberOfSlayers++;
            SlayerWasHired?.Invoke();
            ResourcesHasChanged?.Invoke();
        } 
    }
}
