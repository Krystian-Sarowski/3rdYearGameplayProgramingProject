using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldButton : MonoBehaviour
{
    [SerializeField]
    Color inactiveColour = Color.clear;

    [SerializeField]
    SpriteRenderer buttonSprite = null;

    GameObject player;

    [SerializeField]
    GameObject progressBar = null;

    [SerializeField]
    BoxCollider2D buttonCollider = null;

    [SerializeField]
    float maxScaleX = 0.0f;

    float progressSpeed = 0.5f;

    [SerializeField]
    bool isActive = false;

    [SerializeField]
    string sceneName = "";

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        SetIsActive(isActive);
    }

    // Update is called once per frame
    void Update()
    {
       if(isActive)
       {
            UpdateProgressBar();
       }
    }

    void UpdateProgressBar()
    {
        if (buttonCollider.bounds.Contains(player.transform.position) && progressBar.transform.localScale.x < maxScaleX)
        {
            progressBar.transform.localScale += new Vector3(progressSpeed, 0.0f, 0.0f);
        }

        else if (progressBar.transform.localScale.x > 0.0f)
        {
            progressBar.transform.localScale -= new Vector3(progressSpeed, 0.0f, 0.0f);
        }

        if (progressBar.transform.localScale.x >= maxScaleX)
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        if(sceneName == "Level")
        {
            SceneManager.LoadSceneAsync(sceneName + Data.level.ToString());
        }

        else
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
    }

    public void SetIsActive(bool t_isActive)
    {
        isActive = t_isActive;

        if(isActive)
        {
            buttonSprite.color = Color.white;
        }
        else
        {
            buttonSprite.color = inactiveColour;
        }

    }
}
