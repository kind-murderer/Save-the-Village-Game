using System;
using UnityEngine;

public class BattleServer : MonoBehaviour
{
    [SerializeField]
    private int enemyMultiplier;
    private TimersServer timersServer;
    private ResourcesServer resourcesServer;
    private int numberOfDragons;
    private int fallenSlayers;
    private System.Random rand = new System.Random();

    /// <summary>
    /// int numberOfDragons, int numberOfFallen, bool didDragonJoinYou
    /// </summary>
    public event Action<int, int, bool> OnBattleWin;
    public event Action OnBattleLose;

    public int Day { get; private set; } = 0;
    public int DefeatedDragons { get; private set; } = 0;
    public int DragonsOnOurSide { get; private set; } = 0;
    public bool NoLossInBattle { get; private set; } = false;
    public bool LoseWithSingleSlayer { get; private set; } = false;

    private void Start()
    {
        timersServer = gameObject.GetComponent<TimersServer>();
        resourcesServer = gameObject.GetComponent<ResourcesServer>();
        timersServer.Timers["TimerStartBattle"].TimeIsOut += ResultOfBattle;
    }

    private void ResultOfBattle()
    {
        Day++;
        if (Day >= 3)
        {
            if (resourcesServer.NumberOfSlayers > 0)
            {
                numberOfDragons = (Day - 1) * enemyMultiplier + rand.Next(-1, 1);
                fallenSlayers = 0;
                for (int i = 0; i < numberOfDragons; i++)
                {
                    fallenSlayers += (rand.Next(0, 2) == 0) ? 0 : 1; //(50% that slayer will be defeated)
                }
                if (fallenSlayers > resourcesServer.NumberOfSlayers)
                {
                    if(resourcesServer.NumberOfSlayers == 1)
                    {
                        LoseWithSingleSlayer = true;
                    }
                    OnBattleLose?.Invoke();
                }
                else
                {
                    if (fallenSlayers == 0)
                    {
                        NoLossInBattle = true; 
                    }
                    DefeatedDragons += numberOfDragons;
                    //will dragon join us or not (25%)
                    if (rand.Next(0, 4) == 1)
                    {
                        OnBattleWin?.Invoke(numberOfDragons, fallenSlayers, true);
                        DragonsOnOurSide++;
                    }
                    else
                    {
                        OnBattleWin?.Invoke(numberOfDragons, fallenSlayers, false);
                    }
                }
            } 
            else
            {
                OnBattleLose?.Invoke();
            }
        }
        else //if it's first or second day
        {
            OnBattleWin?.Invoke(0, 0, false);
        }
    }

    public void ResetBattles()
    {
        Day = 0;
        DefeatedDragons = 0;
        DragonsOnOurSide = 0;
        LoseWithSingleSlayer = false;
        NoLossInBattle = false;
    }
}
