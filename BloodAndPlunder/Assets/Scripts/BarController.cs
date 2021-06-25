using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    [SerializeField]
    Transform targetToFollow = null;

    Vector3 offset;

    // Start is called before the first frame update
    void Awake()
    {
        offset = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = targetToFollow.localPosition + offset;
    }
}
