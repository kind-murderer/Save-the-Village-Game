using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveGameView : MonoBehaviour
{
    // !!!! Edit > ProjectSettings > ScriptExecutionOrder. Make this script RUN AFTER all other custom scripts. !!!!

    public Button buttonHireMiner, buttonHireSlayer;
    [SerializeField]
    private Image minerImage, slayerImage;
    [SerializeField]
    private Sprite minerNormalSprite, slayerNormalSprite, minerHighlightedSprite, slayerHighlightedSprite;
    [SerializeField]
    private Text numberOfMinersText, numberOfSlayersText, numberOfGemsText, scrollText;
    StatusScroll statusScroll;

    ResourcesServer resourcesServer;
    TimersServer timersServer;

    private void Start()
    {
        timersServer = gameObject.GetComponent<TimersServer>();
        resourcesServer = gameObject.GetComponent<ResourcesServer>();
        resourcesServer.ResourcesHasChanged += ShowResources;
        resourcesServer.ResourcesHasChanged += UpdateHireButtons;
        statusScroll = new StatusScroll(scrollText);
        resourcesServer.SlayersDiscontent += (int numberLeft) =>
        {
            if(numberLeft == 0)
            {
                statusScroll.CleanMessage();
            }
            else
            {
                statusScroll.SomeSlayersLeftMessage(numberLeft);

            }
        };
        timersServer.timers["TimerHireMiner"].TimeIsOut += () => 
        {
            buttonHireMiner.image.fillAmount = 1; 
            UpdateHireButtons();
        };
        timersServer.timers["TimerHireSlayer"].TimeIsOut += () =>
        {
            buttonHireSlayer.image.fillAmount = 1;
            UpdateHireButtons();
        };
        ShowResources();
        UpdateHireButtons();
    }

    private void ShowResources()
    {
        numberOfMinersText.text = resourcesServer.NumberOfMiners.ToString();
        numberOfSlayersText.text = resourcesServer.NumberOfSlayers.ToString();
        numberOfGemsText.text = resourcesServer.CurrentGems.ToString();
    }

    private void UpdateHireButtons()
    {
        if(!timersServer.timers["TimerHireMiner"].isRunning && resourcesServer.CanHireMiner)
        {
            buttonHireMiner.interactable = true;
            minerImage.sprite = minerHighlightedSprite;
        }
        else
        {
            buttonHireMiner.interactable = false;
            minerImage.sprite = minerNormalSprite;
        }
        if(!timersServer.timers["TimerHireSlayer"].isRunning && resourcesServer.CanHireSlayer)
        {
            buttonHireSlayer.interactable = true;
            slayerImage.sprite = slayerHighlightedSprite;
        }
        else
        {
            buttonHireSlayer.interactable = false;
            slayerImage.sprite = slayerNormalSprite;
        }
    }
}
