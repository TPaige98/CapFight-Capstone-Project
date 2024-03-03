using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    private static readonly string effectsVolume = "EffectsVolume";
    private static readonly string musicVolume = "MusicVolume";

    private float effectsFloat;
    private float musicFloat;

    public AudioSource[] musicAudio;
    public AudioSource[] effectsAudio;

    void Awake()
    {
        SettingsFollow();
    }

    private void SettingsFollow()
    {
        effectsFloat = PlayerPrefs.GetFloat(effectsVolume);
        musicFloat = PlayerPrefs.GetFloat(musicVolume);
       
        for (int i = 0; i < effectsAudio.Length; i++)
        {
            effectsAudio[i].volume = effectsFloat;
        }

        for (int i = 0; i < musicAudio.Length; i++)
        {
            musicAudio[i].volume = musicFloat;
        }
    }
}
