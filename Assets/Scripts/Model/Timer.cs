using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer
{
    private float maxTime;
    private float currentTime;
    private Image timerImage;
    private int defaultImageFillAmount;
    private bool isColorChangeable;
    private Color colorImageStart = new Color(0.3201512f, 1f, 0.1843137f, 1f); //50FF2F
    private Color colorImageMiddle = new Color(1f, 1f, 0.1843137f, 1f); //FFFF2F
    private Color colorImageFinish = new Color(1f, 0.1981609f, 0.1843137f, 1f); //FF332F

    public event Action TimeIsOut;
    public bool IsRunning {get; set;}

    public Timer(float maxTime, Image timerImage, int defaultImageFillAmount, bool isRunning, bool isColorChangeable = false)
    {
        this.maxTime = maxTime;
        currentTime = 0;
        this.timerImage = timerImage;
        this.defaultImageFillAmount = defaultImageFillAmount;
        this.isColorChangeable = isColorChangeable;
        this.IsRunning = isRunning;
    }

    public void TimePassed(float deltaTime)
    {
        currentTime += deltaTime;
        ChangeImage(Math.Min(currentTime / maxTime, 1));
        if (currentTime >= maxTime)
        {
            currentTime = 0;
            ChangeImage(defaultImageFillAmount);
            IsRunning = false;
            TimeIsOut?.Invoke();
        }
    }
    private void ChangeImage(float fullness)
    {
        timerImage.fillAmount = fullness;
        if (isColorChangeable)
        {
            if(fullness < 0.5) //approximately
            {
                timerImage.color = Color.LerpUnclamped(colorImageStart, colorImageMiddle, fullness*2);
            }
            else
            {
                timerImage.color = Color.LerpUnclamped(colorImageMiddle, colorImageFinish, (fullness - 0.5f)*2);
            }   
        }
    }
    public void Reset()
    {
        currentTime = 0;
        IsRunning = false;
        ChangeImage(defaultImageFillAmount);
    }
}
