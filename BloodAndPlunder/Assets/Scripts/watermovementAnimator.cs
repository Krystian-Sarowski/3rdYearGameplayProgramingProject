using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watermovementAnimator : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.position += new Vector3(0.2f, 0.0f, 0.0f);
        if(transform.position.x > 100.0f)
        {
            transform.position = new Vector3(-100.0f, transform.position.y, transform.position.z);
        }
    }
}
