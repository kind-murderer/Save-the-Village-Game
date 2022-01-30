using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    [SerializeField]
    Canvas menuCanvas;
    public Button buttonStartGame, buttonAchievements, buttonRules, buttonExit;

    public void OpenWindow()
    {
        menuCanvas.gameObject.SetActive(true);
    }
}
