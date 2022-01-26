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
            Debug.Log("Miner was hired");
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
            { "TimerStartBattle", new Timer(timeToStartBattle, imageToStartBattle, true, true)},
            { "TimerHireMiner", new Timer(timeToHireMiner, imageToHireMiner, false)},
            { "TimerHireSlayer", new Timer(timeToHireSlayer, imageToHireSlayer, false)},
            { "TimerFinishMining", new Timer(timeToFinishMining, imageToFinishMining, false)},
            { "TimerPaySalary", new Timer(timeToPaySalary, imageToPaySalary, false)}
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
            else if ((timer.Key == "TimerFinishMining" && resourcesServer.DoWeHaveMiners)
                || (timer.Key == "TimerPaySalary" && resourcesServer.DoWeHaveSlayers))
            {
                timer.Value.isRunning = true;
                timer.Value.TimePassed(Time.deltaTime);
            }
        }
    }

}