using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    static int coinValue = 50;      //The value of the coin.
    static int impulseSpeed = 1;    //The impulse speed that is applied to the coin when it is created.

    Rigidbody2D rb2d;               //The rigidbody of the sprite.

    GameController gameController;  //Reference to the game controller script.

    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        GenerateVelocity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameController.UpdateGold(coinValue);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Generates a velocity vector for the coin that is then applied to it as an impulse force.
    /// </summary>
    void GenerateVelocity()
    {
        //The impulse velocity of the coin.
        Vector3 impulseVel = Vector3.zero;

        impulseVel.x = Random.Range(-1.0f, 1.0f);
        impulseVel.y = Random.Range(-1.0f, 1.0f);

        impulseVel = impulseVel.normalized * impulseSpeed;

        rb2d.AddForce(impulseVel, ForceMode2D.Impulse);
    }
}
