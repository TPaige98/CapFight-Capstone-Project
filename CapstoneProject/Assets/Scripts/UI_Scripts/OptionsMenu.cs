using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public void qualitySelector(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
