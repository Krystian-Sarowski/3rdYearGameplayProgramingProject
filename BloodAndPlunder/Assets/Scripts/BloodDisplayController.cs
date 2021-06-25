using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodDisplayController : MonoBehaviour
{
    PlayerController playerScript;
    Image bloodImage;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        bloodImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bloodImage.color = new Color(1, 1, 1, (100.0f - playerScript.health) / 100.0f);
    }
}
