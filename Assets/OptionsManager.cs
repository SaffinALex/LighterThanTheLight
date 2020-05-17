using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public Slider sliderVolume;

    void OnEnable()
    {
        sliderVolume.value = App.sfx.volume;
    }

    public void apply()
    {
        App.sfx.volume = sliderVolume.value;
    }
}
