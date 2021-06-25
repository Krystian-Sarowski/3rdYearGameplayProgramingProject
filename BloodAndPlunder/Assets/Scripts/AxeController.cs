using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    [SerializeField]
    List<Vector3> axeSizes = new List<Vector3>();

    [SerializeField]
    List<int> axeDamages = new List<int>();

    int axeDamage = 0;

    static public bool axeInMotion = false;    //Bool for if the axe is moving.

    // Start is called before the first frame update
    void Awake()
    {
        SetAxeStats();
    }

    public void SetAxeStats()
    {
        transform.localScale = axeSizes[Inventory.axeLevel];

        axeDamage = axeDamages[Inventory.axeLevel];
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (axeInMotion)
        {
            if (collision.tag == "Enemy")
            {
                collision.GetComponent<EnemyController>().takeDamage(axeDamage);
            }

            if (collision.tag == "Chest")
            {
                collision.GetComponent<ChestController>().TakeDamage();
            }
        }
    }
}
