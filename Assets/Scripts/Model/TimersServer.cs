using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimersServer : MonoBehaviour
{
    /// <summary>
    /// Container for Timers with keys "TimerStartBattle", "TimerHireMiner", "TimerHireSlayer", "TimerFinishMining", "TimerPaySalary"
    /// </summary>
    public Dictionary<string, Timer> timers;
    [SerializeField]
    private int timeToStartBattle, timeToHireMiner, timeToHireSlayer, timeToFinishMining, timeToPaySalary;
    [SerializeField]
    private Image imageToStartBattle, imageToHireMiner, imageToHireSlayer, imageToFinishMining, imageToPaySalary;

    ResourcesServer resourcesServer;

    void Start()
    {
        resourcesServer = gameObject.GetComponent<ResourcesServer>();
        resourcesServer.MinerWasHired += () => { 
            timers["TimerHireMiner"].isRunning = true;
            timers["TimerFinishMining"].isRunning = true;
        };
        resourcesServer.SlayerWasHired += () => { 
            timers["TimerHireSlayer"].isRunning = true;
            timers["TimerPaySalary"].isRunning = true;
        };
    }

    public void InitializeTimersArray()
    {
        timers = new Dictionary<string, Timer>()
        {
            { "TimerStartBattle", new Timer(timeToStartBattle, imageToStartBattle, 0, false, true)},
            { "TimerHireMiner", new Timer(timeToHireMiner, imageToHireMiner, 1, false)},
            { "TimerHireSlayer", new Timer(timeToHireSlayer, imageToHireSlayer, 1, false)},
            { "TimerFinishMining", new Timer(timeToFinishMining, imageToFinishMining, 0, false)},
            { "TimerPaySalary", new Timer(timeToPaySalary, imageToPaySalary, 0, false)}
        };
    }
    void Update()
    {
        foreach(KeyValuePair<string, Timer> timer in timers)
        {
            if (timer.Value.isRunning)
            {
                timer.Value.TimePassed(Time.deltaTime);
            }
            else if ((timer.Key == "TimerFinishMining" && resourcesServer.NumberOfMiners > 0)
                || (timer.Key == "TimerPaySalary" && resourcesServer.NumberOfSlayers > 0))
            {
                timer.Value.isRunning = true;
                timer.Value.TimePassed(Time.deltaTime);
            }
        }
    }
    public void StartBattlingTimer()
    {
        timers["TimerStartBattle"].isRunning = true;
    }
    public void ResetTimers()
    {
        foreach (KeyValuePair<string, Timer> timer in timers)
        {
            timer.Value.Reset();
        }
    }
}