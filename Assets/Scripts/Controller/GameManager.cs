using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool ContinueBattle { get; set; } = true;

    private bool isPaused = false;
    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        isPaused = !isPaused;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
