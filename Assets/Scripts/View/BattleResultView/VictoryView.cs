using UnityEngine;
using UnityEngine.UI;

public class VictoryView : MonoBehaviour
{
    public Button buttonToMenu; 

    [SerializeField]
    private Canvas victoryCanvas;
    [SerializeField]
    private Text victoryText;

    public void InformOfVictoryAndSummarize(int numberOfMiners, int numberOfSlayers, int defeatedDragons, int day)
    {
        victoryText.text = string.Format("You save up a lot of gems! For this money, you, {0} miners and {1} dragon slayers have improved the protection of the village, " +
            "and the infrastructure. For all that time {2} of dragons were defeated. Fewer and fewer dragons were comming out. " +
            "They decided that it was more profitable for them to have you as an ally than an enemy. On the {3} day you agreed to make peace. \n" +
            "\nThings were looking up, and soon your village became famous outside the mointain. ", numberOfMiners, numberOfSlayers, defeatedDragons, day);
    }
    public void OpenWindow()
    {
        victoryCanvas.gameObject.SetActive(true);
    }
}
