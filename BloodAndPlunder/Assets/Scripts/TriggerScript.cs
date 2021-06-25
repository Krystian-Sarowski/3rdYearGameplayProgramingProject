using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Data.tutorial == 1)
        {
            Data.tutorial = 2;
        }
        if (collision.tag == "Player" && Data.tutorial == 6)
        {
            Data.tutorial = 7;
        }
        if (collision.tag == "Player" && Data.tutorial == 8)
        {
            Data.tutorial = 9;
        }
    }
}

