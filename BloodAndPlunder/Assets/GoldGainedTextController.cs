using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldGainedTextController : MonoBehaviour
{
    [SerializeField]
    Text textScript = null;

    [SerializeField]
    RectTransform textRect = null;

    Vector3 worldPos = Vector3.zero;

    void Awake()
    {
        worldPos = textRect.position;

        textRect.position = Camera.main.WorldToScreenPoint(worldPos);
    }


    void FixedUpdate()
    {
        textScript.color = textScript.color - new Color(0.0f, 0.0f, 0.0f, 0.015f);
        worldPos += Vector3.up * 0.01f;

        textRect.position = Camera.main.WorldToScreenPoint(worldPos);

        if (textScript.color.a <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
