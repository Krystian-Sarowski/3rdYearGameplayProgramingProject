using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public GameObject triggerLobby;
    //tutorial 1
    public Image arrow1;
    public Image arrow2;
    public Image square;
    public Text timeAndGoldText;
    //tutorial 2
    public Text walkOffShip;
    public GameObject triggerShip;
    public Button attackButton;
    public Text attackButtonText;
    public Image attackButtonSquare1;
    public Image attackButtonSquare2;

    //tutorial 6
    public Text walkToHouseText;

    //tutorial 7 
    public GameObject triggerHouse;
    public Text chestText;
    public GameObject chestSquare1;
    public GameObject chestSquare2;
    public GameObject chestSquare3;
    public GameObject chestSquare4;

    //tutorial 8
    public Text walkToShipText;

    //tutorial 9
    public Text lobbyText;
    public GameObject lobbySquare1;
    public GameObject lobbySquare2;
    public GameObject lobbySquare3;
    public GameObject lobbySquare4;

    private Color myColour = new Color(0.0f, 0.0f, 0.0f, 195f);
    public bool tutorial = true;
    public GameObject m_enemy;
    public GameObject m_player;
    public GameObject m_chest;

    //Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                if (Data.tutorial == 0)
                {
                    Destroy(arrow1);
                    Destroy(arrow2);
                    Destroy(square);
                    Destroy(timeAndGoldText);
                    Data.tutorial = 1;
                }
                if (Data.tutorial == 2)
                {
                    Destroy(attackButtonText); //deletes text that tells you about the attack button
                    Destroy(attackButtonSquare1); //deletes the black squares around the attack button
                    Destroy(attackButtonSquare2); //deletes the black squares around the attack button
                    Data.tutorial = 3;
                }
                if (Data.tutorial == 7)
                {
                    Destroy(chestSquare1); //destroys the square around chest
                    Destroy(chestSquare2);
                    Destroy(chestSquare3);
                    Destroy(chestSquare4);
                    Destroy(chestText);
                    Data.tutorial = 8;
                }
                if(Data.tutorial == 9)
                {
                    Destroy(lobbySquare1);
                    Destroy(lobbySquare2);
                    Destroy(lobbySquare3);
                    Destroy(lobbySquare4);
                    Destroy(lobbyText);
                    Destroy(triggerLobby);
                    Data.tutorial = 10;
                }
            }
        }
        if (Data.tutorial == 1)
        {
            walkOffShip.gameObject.SetActive(true); //displays text to walk off ship
        }

        if (Data.tutorial == 2)
        {
            Destroy(triggerShip);
            Destroy(walkOffShip); //displays text to walk off ship
            attackButton.gameObject.SetActive(true); //makes attack button active
            attackButtonText.gameObject.SetActive(true); //displays text that tells you about the attack button
            attackButtonSquare1.gameObject.SetActive(true); //displays the black squares around the attack button
            attackButtonSquare2.gameObject.SetActive(true); //displays the black squares around the attack button
        }
        if (Data.tutorial == 3)
        {
            m_enemy.GetComponent<EnemyController>().speed = 50;
        }

        if (Data.tutorial == 6)
        {
            walkToHouseText.gameObject.SetActive(true); //displays text to walk to the house
        }
        if(Data.tutorial == 7)
        {
            Destroy(walkToHouseText); //destroys the text that tells player to walk to the house
            chestText.gameObject.SetActive(true); //displays a text that tells player to hit chests for coins
            chestSquare1.gameObject.SetActive(true); //displays a black square around the chest
            chestSquare2.gameObject.SetActive(true);
            chestSquare3.gameObject.SetActive(true);
            chestSquare4.gameObject.SetActive(true);
        }
        if(Data.tutorial == 8)
        {
            Destroy(triggerHouse);
            if(LevelData.goldCollected > 100)
            {
                walkToShipText.gameObject.SetActive(true);//display text to walk back to ship 
                triggerLobby.gameObject.SetActive(true); //creates trigger for lobby button
            }
        }
        if(Data.tutorial == 9)
        {
            Destroy(walkToShipText); //destory text to walk back to ship
            lobbyText.gameObject.SetActive(true); //displays a text that tells player to walk on the lobby square
            lobbySquare1.gameObject.SetActive(true); //displays a black square around the lobby button
            lobbySquare2.gameObject.SetActive(true);
            lobbySquare3.gameObject.SetActive(true);
            lobbySquare4.gameObject.SetActive(true);
        }
    }
}
