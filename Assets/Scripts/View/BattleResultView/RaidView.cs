using UnityEngine;
using UnityEngine.UI;

public class RaidView : MonoBehaviour
{
    public Button continueButton;
    [SerializeField]
    Canvas raidCanvas;
    [SerializeField]
    Text resultText;

    public void InformOfBattleResult(int day, int numberOfDragons, int numberOfFallen, bool didDragonJoinYou)
    {
        if (day == 1)
        {
            resultText.text = "The first day of mining was a success. You celebrated the realization of your good idea after work. " +
                "You're eager to start a new day.";
        }
        else if (day == 2)
        {
            resultText.text = " Àfter the working day, miners were met by worried villagers. They said they saw dragons near the village. " +
                "It's seems like active mining attracted the attention of the dragons. \nIt was said that dragons just circled around and flew away. " +
                "But you have a feeling that they will come back.";
        }
        else
        {
            resultText.text = string.Format("On the {0} day your village have been attacked by {1} dragon(s)! Dragon slayers have protected the villagers. " +
                "{2} of warriors have fallen down. \n", day, numberOfDragons, numberOfFallen);
            if (didDragonJoinYou)
            {
                resultText.text += "One dragon has been impressed by the courage of warriors. When the battle ended, " +
                    "it had offered itself as a defender on your side.";
            }
        }
    }

    public void OpenWindow()
    {
        raidCanvas.gameObject.SetActive(true);
    }
}
