using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float lifeTime;      //The time for which the object will be alive for.
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        Destroy(gameObject, lifeTime);
    }
}
