using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    [SerializeField]
    int health = 3;             //The health of the chest. The chest can take max of 1 health at a time.

    [SerializeField]
    Sprite[] sprites = null;    //The list of sprites for the chest.

    [SerializeField]
    GameObject coinPrefab = null;       //Prefab of the coin object.

    [SerializeField]
    GameObject emeraldPrefab = null;    //Prefab of the emerald object.

    [SerializeField]
    GameObject diamondPrefab = null;    //Prefab of the diamond object.

    [SerializeField]
    int maxCoins = 0;               //The max number of coins that can spawn form a chest.

    bool tookDamage = false;

    void FixedUpdate()
    {
        switch (health)
        {
            case 1:
                GetComponent<SpriteRenderer>().sprite = sprites[2];
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                break;
            default:
                SpawnCoins();
                LevelData.chestsBroken++;
                Destroy(gameObject);
                break;
        }

        if(tookDamage && !AxeController.axeInMotion)
        {
            tookDamage = false;
        }
    }

    /// <summary>
    /// Takes away one point of health from the chest.
    /// </summary>
    public void TakeDamage()
    {
        if(tookDamage)
        {
            return;
        }

        tookDamage = true;
        health--;
        FindObjectOfType<AudioManager>().Play("WoodHit");
    }

    public int GetHealth()
    {
        return health;
    }
    /// <summary>
    /// Instantiates a number of coin prefabs at the position of the chest.
    /// </summary>
    void SpawnCoins()
    {
        for(int i =0; i < maxCoins; i++)
        {
            int spawnChance = Random.Range(1, 101);

            if(spawnChance < 50)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("CoinDrop");
            }

            else if (spawnChance < 80)
            {
                Instantiate(emeraldPrefab, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("CoinDrop");
            }

            else
            {
                Instantiate(diamondPrefab, transform.position, Quaternion.identity);
                FindObjectOfType<AudioManager>().Play("CoinDrop");
            }

        }
    }
}
