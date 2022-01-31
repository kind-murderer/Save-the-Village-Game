using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicServer : MonoBehaviour
{
    [SerializeField] 
    private AudioClip menuSound, activeGameSound, victorySound, defeatSound, 
        clickSound, swordSound, stoneSound, gainGemsSound, giveGemsSound;
    private AudioSource audioSource;
    
    public enum SoundEffect 
    {
        DrawingSword, 
        StoneImpact,
        GainGems, 
        GiveGems
    }
    public enum SoundBackground
    { 
        Menu, 
        ActiveGame,
        Victory, 
        Defeat
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = menuSound;
        audioSource.Play();
    }

    public void ChangeBackgroundSound(SoundBackground sound)
    {
        switch (sound)
        {
            case SoundBackground.Menu:
                audioSource.clip = menuSound;
                audioSource.loop = true;
                break;
            case SoundBackground.ActiveGame:
                audioSource.clip = activeGameSound;
                audioSource.loop = true;
                break;
            case SoundBackground.Victory:
                audioSource.clip = victorySound;
                audioSource.loop = false;
                break;
            case SoundBackground.Defeat:
                audioSource.clip = defeatSound;
                audioSource.loop = false;
                break;
        }
        audioSource.Play();
    }

    public void PlaySoundEffect(SoundEffect sound)
    {
        switch (sound)
        {
            case SoundEffect.DrawingSword:
                audioSource.PlayOneShot(swordSound, 0.5f);
                break;
            case SoundEffect.StoneImpact:
                audioSource.PlayOneShot(stoneSound, 0.5f);
                break;
            case SoundEffect.GainGems:
                audioSource.PlayOneShot(gainGemsSound, 0.5f);
                break;
            case SoundEffect.GiveGems:
                audioSource.PlayOneShot(giveGemsSound, 0.5f);
                break;
        }
    }
    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(clickSound, 0.5f);
    }

    public void MuteUnmute()
    {
        audioSource.mute = !audioSource.mute;
    }
 
}