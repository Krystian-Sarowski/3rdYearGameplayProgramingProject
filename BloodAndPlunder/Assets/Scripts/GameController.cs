using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    float timeLeft = 0;

    [SerializeField]
    float timeAtStart = 10.0f;

    int goldCollected;

    [SerializeField]
    int goldNeeded = 0;

    Color defualtTextColour;

    [SerializeField]
    Text timeLeftText = null;

    [SerializeField]
    Text goldCollectedText = null;

    [SerializeField]
    GameObject endScreen = null;

    [SerializeField]
    GameObject exitButton = null;

    //Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        timeLeft = timeAtStart;

        defualtTextColour = goldCollectedText.color;

        UpdateTimeText();
        UpdateGoldText();

        LevelData.chestsBroken = 0;
        LevelData.enemiesKilled = 0;
        LevelData.goldCollected = 0;
    }

    //Update is called once per frame
    void Update()
    {
        if ((Data.tutorial == 1 || Data.tutorial == 3 || Data.tutorial == 6 || Data.tutorial == 8 || Data.tutorial == 10))
        {
            timeLeft -= Time.deltaTime;
            UpdateTimeText();

            if (timeLeft <= 0.0f)
            {
                Time.timeScale = 0;
                endScreen.SetActive(true);
            }
        }
    }

    void UpdateTimeText()
    {
        int seconds = (int)(timeLeft % 60);
        int minutes = (int)(timeLeft / 60) % 60;

        timeLeftText.text = "Time Left: " + minutes + ":";

        if (seconds < 10)
        {
            timeLeftText.text += "0";
        }

        timeLeftText.text += seconds;
    }

    public void UpdateGold(int t_goldGained)
    {
        goldCollected += t_goldGained;

        UpdateGoldText();

        if (goldCollected >= goldNeeded)
        {
            exitButton.GetComponent<WorldButton>().SetIsActive(true);
        }

        LevelData.goldCollected = goldCollected;
    }

    void UpdateGoldText()
    {
        goldCollectedText.text = "Gold: " + goldCollected + " / " + goldNeeded;

        if(goldCollected >= goldNeeded)
        {
            goldCollectedText.color = Color.green;
        }
        else
        {
            goldCollectedText.color = defualtTextColour;
        }
    }

    /// <summary>
    /// Reloads the Gameplay scene, called on button press.
    /// </summary>
    public void Restart()
    {
        if (Data.level == 1)
        {
            SceneManager.LoadSceneAsync("Level1");
        }
        if (Data.level == 2)
        {
            SceneManager.LoadSceneAsync("Level2");
        }
        if (Data.level == 3)
        {
            SceneManager.LoadSceneAsync("Level3");
        }
        if (Data.level == 4)
        {
            SceneManager.LoadSceneAsync("Level4");
        }
    }

    /// <summary>
    /// Loads the Menu scene, called on button press.
    /// </summary>
    public void MainMenu()
    {
        SceneManager.LoadScene("Lobby");

        if (goldCollected >= goldNeeded)
        {
            if (Data.level == 1)
            {
                Data.level = 2;
            }
            else if(Data.level == 2)
            {
                Data.level = 3;
            }
            else
            {
                Data.level = 4;
            }
        }
       
    }

}
