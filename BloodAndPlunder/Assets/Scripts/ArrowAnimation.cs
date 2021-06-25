using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAnimation : MonoBehaviour
{
    [SerializeField]
    Vector3 startPos = Vector3.zero;       //The position in which the arrow starts.

    [SerializeField]
    Vector3 endPos = Vector3.zero;         //The end position to which the arrow can go to.

    Vector3 velVector = Vector3.zero;      //The velocity of the arrow.

    private void Start()
    {
        velVector = (endPos - startPos).normalized * 0.5f;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.localPosition.x - endPos.x) <= 0.5f)
        {
            transform.localPosition = startPos;
        }
        else
        {
            transform.localPosition += velVector;
        }

    }
}
