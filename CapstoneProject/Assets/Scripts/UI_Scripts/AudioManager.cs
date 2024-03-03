using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string initialStart = "InitialStart";
    private static readonly string effectsVolume = "EffectsVolume";
    private static readonly string musicVolume = "MusicVolume";

    private int initialStartInt;
    private float effectsFloat;
    private float musicFloat;

    public Slider effectsSlider;
    public Slider musicSlider;

    public AudioSource[] musicAudio;
    public AudioSource[] effectsAudio;

    // Start is called before the first frame update
    void Start()
    {
        initialStartInt = PlayerPrefs.GetInt(initialStart);

        if (initialStartInt == 0)
        {
            effectsFloat = 1.0f;
            musicFloat = 1.0f;

            effectsSlider.value = effectsFloat;
            musicSlider.value = musicFloat;

            PlayerPrefs.SetFloat(effectsVolume, effectsFloat);
            PlayerPrefs.SetFloat(musicVolume, musicFloat);
            PlayerPrefs.SetInt(initialStart, -1);
        }
        else
        {
            effectsFloat = PlayerPrefs.GetFloat(effectsVolume);
            musicFloat = PlayerPrefs.GetFloat(musicVolume);

            effectsSlider.value = effectsFloat;
            musicSlider.value = musicFloat;
        }
    }

    public void KeepAudioSettings()
    {
        PlayerPrefs.SetFloat(effectsVolume, effectsSlider.value);
        PlayerPrefs.SetFloat(musicVolume, musicSlider.value);
    }

    void OnApplicationFocus(bool focus)
    {
        if(!focus)
        {
            KeepAudioSettings();
        }
    }

    public void UpdateAudio()
    {
        for (int i = 0; i < effectsAudio.Length; i++)
        {
            effectsAudio[i].volume = effectsSlider.value;
        }

        for (int i = 0; i < musicAudio.Length; i++)
        {
            musicAudio[i].volume = musicSlider.value;
        }
    }
}
