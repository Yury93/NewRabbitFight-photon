using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAudio : MonoBehaviour
{
    [SerializeField] private AudioSource walk;
    public void WalkAudioPlay()
    {
        walk.Play();
    }
}
