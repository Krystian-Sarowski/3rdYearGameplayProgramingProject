using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    SpriteRenderer roofSprite;

    void Start()
    {
        roofSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            roofSprite.color = new Color(1, 1, 1, 0.3f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            roofSprite.color = Color.white;
        }
    }
}
