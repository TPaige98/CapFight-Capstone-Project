using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{










    public void qualitySelector(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    //Function and variables for volume slider in options menu
    public AudioMixer mixer;
    public void volumeSlider(float volume)
    {
        mixer.SetFloat("Volume", volume);
    }
}
