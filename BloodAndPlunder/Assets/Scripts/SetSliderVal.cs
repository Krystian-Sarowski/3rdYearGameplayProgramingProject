using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderVal : MonoBehaviour
{
    public GameObject object1;
    public Slider s;
    // Start is called before the first frame update
    void Start()
    {
        s.value = object1.GetComponent<SaveSliderVal>().GetVolume();
    }
}
