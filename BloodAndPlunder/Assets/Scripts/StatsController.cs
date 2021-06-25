using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    [SerializeField]
    GameObject enemiesKilledText = null;

    [SerializeField]
    GameObject chestsBrokenText = null;

    [SerializeField]
    GameObject goldCollectedText = null;

    int currentChestsBroken = 0;
    int currentGoldCollected = 0;
    int currentEnemiesKilled = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StatsDisplay());
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Inventory.gold += LevelData.goldCollected;
                LoadNextScene();
            }
        }
    }

    IEnumerator StatsDisplay()
    {
        enemiesKilledText.SetActive(true);

        while(currentEnemiesKilled != LevelData.enemiesKilled)
        {
            currentEnemiesKilled++;
            enemiesKilledText.GetComponent<Text>().text = "Enemies Killed: " + currentEnemiesKilled;

            yield return new WaitForSeconds(0.5f);
        }

        chestsBrokenText.SetActive(true);

        while (currentChestsBroken != LevelData.chestsBroken)
        {
            currentChestsBroken++;
            chestsBrokenText.GetComponent<Text>().text = "Chests Broken: " + currentChestsBroken;

            yield return new WaitForSeconds(0.5f);
        }

        goldCollectedText.SetActive(true);

        while (currentGoldCollected != LevelData.goldCollected)
        {
            currentGoldCollected += 10;
            goldCollectedText.GetComponent<Text>().text = "Gold Collected: " + currentGoldCollected;

            yield return new WaitForSeconds(0.0001f);
        }
    }

    public void LoadNextScene()
    {
        Data.level++;

        if(Data.level == 5)
        {
            Data.level = 1;
            Data.tutorial = 0;
            Inventory.axeLevel = 0;
            Inventory.gold = 0;
            SceneManager.LoadSceneAsync("GameComplete");
        }
        else
        {
            SceneManager.LoadSceneAsync("Lobby");
        }
    }
}
