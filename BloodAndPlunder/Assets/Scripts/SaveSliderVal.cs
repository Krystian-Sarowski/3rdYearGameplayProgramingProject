using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSliderVal : MonoBehaviour
{
    public static float volumeValue = 1;
    public void ChangeVolume(Slider vol)
    {
        volumeValue = vol.value;
    }
    public float GetVolume()
    {
        return volumeValue;
    }
}

