using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class Inventory
{
    public static int axeLevel = 0;
    public static int gold = 0;
}

public static class Data
{
    public static int level = 1;

    public static bool playerMoved = false;
    public static bool playerAttacked = false;
    public static int tutorial = 0;
}

public static class LevelData
{
    public static int enemiesKilled = 0;
    public static int chestsBroken = 0;
    public static int goldCollected = 0;
}

public class Manager : MonoBehaviour
{
    [SerializeField]
    Text goldPlunderedText = null;

    public void changeScreen(string t_scene)
    {
        if (t_scene == "Lobby" && Data.tutorial == 0)
        {
            SceneManager.LoadSceneAsync("Level1");
        }
        else
        {
            SceneManager.LoadSceneAsync(t_scene);
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if(goldPlunderedText != null)
        {
            goldPlunderedText.text = "Gold Plundered: " + Inventory.gold.ToString();
        }
    }
}
