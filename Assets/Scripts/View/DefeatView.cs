using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatView : MonoBehaviour
{
    [SerializeField]
    Canvas defeatCanvas;
    [SerializeField]
    Text defeatText;

    public void InformOfDefeat(int day)
    {
        defeatText.text = string.Format("On the {0} day so many dragons have came that you couldn't fight back. \n" +
            "Maybe you should hire more Dragon Slayers next time.", day);
    }
    public void ChangeActiveCanvas(bool setActive)
    {
        defeatCanvas.gameObject.SetActive(setActive);
    }
}
