using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// !!!! Edit > ProjectSettings > ScriptExecutionOrder. Make this script RUN BEFORE OTHER custom scripts. !!!!
public class ActiveGameController : MonoBehaviour
{
    TimersServer timersServer;
    ActiveGameView activeGameView;
    ResourcesServer resourcesServer;

    private void Start()
    {
        timersServer = gameObject.GetComponent<TimersServer>();
        timersServer.InitializeTimersArray();
        resourcesServer = gameObject.GetComponent<ResourcesServer>();
        activeGameView = gameObject.GetComponent<ActiveGameView>();
        activeGameView.buttonHireMiner.onClick.AddListener(resourcesServer.HireMiner);
        activeGameView.buttonHireSlayer.onClick.AddListener(resourcesServer.HireSlayer);
    }
}
