using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public float range;
    public float rotation;
    public float attackSpeed;
    public Sprite sprite;

    void Start()
    {
        transform.localRotation = Quaternion.AngleAxis(rotation, Vector3.forward);
    }
}
