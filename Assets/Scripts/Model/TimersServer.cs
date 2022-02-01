using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimersServer : MonoBehaviour
{
    /// <summary>
    /// Container for Timers with keys "TimerStartBattle", "TimerHireMiner", "TimerHireSlayer", "TimerFinishMining", "TimerPaySalary"
    /// </summary>
    public Dictionary<string, Timer> Timers;

    [SerializeField]
    private int timeToStartBattle, timeToHireMiner, timeToHireSlayer, timeToFinishMining, timeToPaySalary;
    [SerializeField]
    private Image imageToStartBattle, imageToHireMiner, imageToHireSlayer, imageToFinishMining, imageToPaySalary;
    private ResourcesServer resourcesServer;

    private void Start()
    {
        resourcesServer = gameObject.GetComponent<ResourcesServer>();
        resourcesServer.MinerWasHired += () => { 
            Timers["TimerHireMiner"].IsRunning = true;
            Timers["TimerFinishMining"].IsRunning = true;
        };
        resourcesServer.SlayerWasHired += () => { 
            Timers["TimerHireSlayer"].IsRunning = true;
            Timers["TimerPaySalary"].IsRunning = true;
        };
    }

    public void InitializeTimersArray()
    {
        Timers = new Dictionary<string, Timer>()
        {
            { "TimerStartBattle", new Timer(timeToStartBattle, imageToStartBattle, 0, false, true)},
            { "TimerHireMiner", new Timer(timeToHireMiner, imageToHireMiner, 1, false)},
            { "TimerHireSlayer", new Timer(timeToHireSlayer, imageToHireSlayer, 1, false)},
            { "TimerFinishMining", new Timer(timeToFinishMining, imageToFinishMining, 0, false)},
            { "TimerPaySalary", new Timer(timeToPaySalary, imageToPaySalary, 0, false)}
        };
    }
    private void Update()
    {
        foreach(KeyValuePair<string, Timer> timer in Timers)
        {
            if (timer.Value.IsRunning)
            {
                timer.Value.TimePassed(Time.deltaTime);
            }
            else if ((timer.Key == "TimerFinishMining" && resourcesServer.NumberOfMiners > 0)
                || (timer.Key == "TimerPaySalary" && resourcesServer.NumberOfSlayers > 0))
            {
                timer.Value.IsRunning = true;
                timer.Value.TimePassed(Time.deltaTime);
            }
        }
    }
    public void StartBattlingTimer()
    {
        Timers["TimerStartBattle"].IsRunning = true;
    }
    public void ResetTimers()
    {
        foreach (KeyValuePair<string, Timer> timer in Timers)
        {
            timer.Value.Reset();
        }
    }
}