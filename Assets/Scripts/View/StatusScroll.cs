using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusScroll
{
    private Text scroll;
    private Color greenColor = new Color(0.3138977f, 0.5377358f, 0.2358935f);
    private Color redColor = new Color(0.6431373f, 0.2039216f, 0.1254902f);

    public StatusScroll(Text scrollText)
    {
        this.scroll = scrollText;
    }

    public void SomeSlayersLeftMessage(int numberLeft)
    {
        scroll.text = "You couldn't affort to pay all of the SLAYERS. " + numberLeft + " of them LEFT.";
        scroll.color = redColor;
    }

    public void MinerWasHiredMessage()
    {
        scroll.text = "You've hired a miner!";
        scroll.fontStyle = FontStyle.Normal;
        scroll.color = greenColor;
    }
    public void SlayerWasHiredMessage()
    {
        scroll.text = "You've hired a dragon slayer! But remember they don't work for free.";
        scroll.fontStyle = FontStyle.Normal;
        scroll.color = greenColor;
    }

    public void CleanMessage()
    {
        scroll.text = "";
    }

}
