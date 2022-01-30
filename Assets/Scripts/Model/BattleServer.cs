using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleServer : MonoBehaviour
{
    private TimersServer timersServer;
    private ResourcesServer resourcesServer;

    /// <summary>
    /// int numberOfDragons, int numberOfFallen, bool didDragonJoinYou
    /// </summary>
    public event Action<int, int, bool> OnBattleWin;
    public event Action OnBattleLose;

    //
    public int Day { get; private set; } = 0;
    public int DefeatedDragons { get; private set; } = 0;
    //
    private int numberOfDragons;
    private int fallenSlayers;
    [SerializeField]
    private int enemyMultiplier;

    System.Random rand = new System.Random();

    private void Start()
    {
        timersServer = gameObject.GetComponent<TimersServer>();
        resourcesServer = gameObject.GetComponent<ResourcesServer>();
        timersServer.timers["TimerStartBattle"].TimeIsOut += ResultOfBattle;
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
                    OnBattleLose?.Invoke();
                }
                else
                {
                    DefeatedDragons += numberOfDragons;
                    //will dragon join us or not (25%)
                    if (rand.Next(0, 4) == 1)
                    {
                        OnBattleWin?.Invoke(numberOfDragons, fallenSlayers, true);
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
    }
}
