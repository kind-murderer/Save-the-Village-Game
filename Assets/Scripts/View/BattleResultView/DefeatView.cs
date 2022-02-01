using UnityEngine;
using UnityEngine.UI;

public class DefeatView : MonoBehaviour
{
    public Button buttonToMenu, buttonRetry;

    [SerializeField]
    private Canvas defeatCanvas;
    [SerializeField]
    private Text defeatText;

    public void InformOfDefeat(int day)
    {
        defeatText.text = string.Format("On the {0} day so many dragons have came that you couldn't fight back. \n" +
            "Maybe you should hire more Dragon Slayers next time.", day);
    }
    public void OpenWindow()
    {
        defeatCanvas.gameObject.SetActive(true);
    }
}
