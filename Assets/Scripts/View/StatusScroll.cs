using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusScroll
{
    
    private Text scroll;

    public StatusScroll(Text scrollText)
    {
        this.scroll = scrollText;
    }

    public void SomeSlayersLeftMessage(int numberLeft)
    {
        scroll.text = "You couldn't affort to pay all of the SLAYERS. " + numberLeft + " of them LEFT.";
    }

    public void CleanMessage()
    {
        scroll.text = "";
    }

}
