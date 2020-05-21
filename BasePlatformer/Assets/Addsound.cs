using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Addsound : MonoBehaviour
{
  public static AudioSource AddAudio(AudioClip clipsound, bool loop, float volume, GameObject that)
    {
        var audio = that.AddComponent<AudioSource>();
        audio.clip = clipsound;
        audio.loop = loop;
        audio.playOnAwake = false;
        audio.volume = volume;
        return audio;
    }
}

