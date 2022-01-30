using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveGameView : MonoBehaviour
{
    // !!!! Edit > ProjectSettings > ScriptExecutionOrder. Make this script RUN AFTER all other custom scripts. !!!!

    public Button buttonHireMiner, buttonHireSlayer, buttonPauseMenu;
    [SerializeField]
    Canvas activeGameCanvas;
    [SerializeField]
    private Image minerImage, slayerImage;
    [SerializeField]
    private Sprite minerNormalSprite, slayerNormalSprite, minerHighlightedSprite, slayerHighlightedSprite;
    [SerializeField]
    private Text numberOfMinersText, numberOfSlayersText, numberOfGemsText, scrollText, 
        gemsAdditionText, minersAdditionText, slayersAdditionText;
    StatusScroll statusScroll;

    private ResourcesServer resourcesServer;
    private TimersServer timersServer;

    private IEnumerator gemsFadeCoroutine, minersFadeCoroutine, slayersFadeCoroutine;
    private bool isGemsFadeRunning = false;
    private bool isMinersFadeRunning = false;
    private bool isSlayersFadeRunning = false;

    private void Start()
    {

        timersServer = gameObject.GetComponent<TimersServer>();
        resourcesServer = gameObject.GetComponent<ResourcesServer>();
        resourcesServer.ResourcesHasChanged += (int gemsAddition, int minersAddition, int slayersAddition) =>
        {
            UpdateResourcesAndButtons();
            ShowAddition(gemsAddition, minersAddition, slayersAddition);
        };
        statusScroll = new StatusScroll(scrollText);
        resourcesServer.SlayersDiscontent += ShowSlayersDiscontent;
        timersServer.timers["TimerHireMiner"].TimeIsOut += UpdateHireButtons;
        timersServer.timers["TimerHireSlayer"].TimeIsOut += UpdateHireButtons;
        buttonHireMiner.onClick.AddListener(statusScroll.MinerWasHiredMessage);
        buttonHireSlayer.onClick.AddListener(statusScroll.SlayerWasHiredMessage);
        UpdateResourcesAndButtons();
    }

    public void UpdateResourcesAndButtons()
    {
        numberOfMinersText.text = resourcesServer.NumberOfMiners.ToString();
        numberOfSlayersText.text = resourcesServer.NumberOfSlayers.ToString();
        numberOfGemsText.text = resourcesServer.CurrentGems.ToString();
        UpdateHireButtons();
    }
    
    private void ShowAddition(int gemsAddition, int minersAddition, int slayersAddition)
    {
        if (gemsAddition != 0)
        {
            //stop prev if it's still running 
            if (isGemsFadeRunning)
            {
                StopCoroutine(gemsFadeCoroutine);
            }
            gemsFadeCoroutine = DisplayAndFade(gemsAdditionText, gemsAddition);
            StartCoroutine(gemsFadeCoroutine);
        }
        if (minersAddition != 0)
        {
            if (isMinersFadeRunning)
            {
                StopCoroutine(minersFadeCoroutine);
            }
            minersFadeCoroutine = DisplayAndFade(minersAdditionText, minersAddition);
            StartCoroutine(minersFadeCoroutine);
        }
        if (slayersAddition != 0)
        {
            if (isSlayersFadeRunning)
            {
                StopCoroutine(slayersFadeCoroutine);
            }
            slayersFadeCoroutine = DisplayAndFade(slayersAdditionText, slayersAddition);
            StartCoroutine(slayersFadeCoroutine);
        }
    }
    IEnumerator DisplayAndFade(Text additionText, int addition)
    {
        isGemsFadeRunning = true;
        //insert plus character before positive number
        additionText.text = (addition > 0) ? "+" + addition.ToString() : addition.ToString();
        Color c = additionText.color;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.01f)
        {
            c.a = alpha;
            additionText.color = c;
            yield return null;
        }
        c.a = 0;
        additionText.color = c;
        isGemsFadeRunning = false;
    }

    private void ShowSlayersDiscontent(int numberLeft)
    {
        if (numberLeft == 0)
        {
            statusScroll.CleanMessage();
        }
        else
        {
            statusScroll.SomeSlayersLeftMessage(numberLeft);
        }
    }

    private void UpdateHireButtons()
    {
        if (!timersServer.timers["TimerHireMiner"].isRunning && resourcesServer.CanHireMiner)
        {
            buttonHireMiner.interactable = true;
            minerImage.sprite = minerHighlightedSprite;
        }
        else
        {
            buttonHireMiner.interactable = false;
            minerImage.sprite = minerNormalSprite;
        }
        if (!timersServer.timers["TimerHireSlayer"].isRunning && resourcesServer.CanHireSlayer)
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

    public void ChangeActiveCanvas(bool setActive)
    {
        activeGameCanvas.gameObject.SetActive(setActive);
    }
    public void ResetView()
    {
        numberOfMinersText.text = "0";
        numberOfSlayersText.text = "0";
        numberOfGemsText.text = "0";
        buttonHireMiner.interactable = false;
        minerImage.sprite = minerNormalSprite;
        buttonHireSlayer.interactable = false;
        slayerImage.sprite = slayerNormalSprite;
        if (isSlayersFadeRunning)
        {
            StopCoroutine(slayersFadeCoroutine);
        }
        if (isMinersFadeRunning)
        {
            StopCoroutine(minersFadeCoroutine);
        }
        if (isGemsFadeRunning)
        {
            StopCoroutine(gemsFadeCoroutine);
        }
        ShowAddition(0, 0, 0);
        statusScroll.SetInitialMessage();
    }
}
