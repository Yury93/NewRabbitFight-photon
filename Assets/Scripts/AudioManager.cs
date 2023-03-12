using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonBase<AudioManager>
{
    [SerializeField] private AudioSource audioKick, audioDamage, audioBanan,kickAir;
    public void AudioPlay(string nameAudio)
    {
        switch(nameAudio)
        {
            case "kick": audioKick.Play();
                break;
            case "damage": audioDamage.Play();
                break;
            case "banan":audioBanan.Play();
                break;
            case "kickAir":
                kickAir.Play();
                break;
            default: print("не одно имя не подошло!");
                break; 
        }
    }
    public void AudioStop(string nameAudio)
    {
        switch (nameAudio)
        {
            case "kick":
                audioKick.Stop();
                break;
            case "damage":
                audioDamage.Stop();
                break;
            case "banan":
                audioBanan.Stop();
                break;
            case "kickAir":
                kickAir.Stop();
                break;
            default:
                print("не одно имя не подошло!");
                break;
        }
    }
}
