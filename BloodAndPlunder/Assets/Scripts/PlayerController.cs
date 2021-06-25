using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector3 targetVel = Vector3.zero;
    Vector3 currentVel = Vector3.zero;
    Vector3 accel = Vector3.zero;
    public Vector3 spawnPos;

    [HideInInspector]
    public float health = 100;
    float attackTimeLeft = 0.0f;
    float attackTimeDelay = 0.5f;
    float turnHandsTimeLeft = 0.0f;
    float turnHandsTimeDelay = 0.01f;
    float regenStartTimeLeft = 0.0f;
    float regenStartTimeDelay = 5.0f;
    float handsTurnAmount = 2.0f;
    float handsTurnOffset = 0.0f;
    float axeRotationSpeed = 10.0f;

    const float AXE_SLOW_DOWN = 1.5f;           //Decrease in swing speed due to axe increase in size.
    const float AXE_DELAY_SLOW_DOWN = 0.1f;     //Increase in axe swing delay due to axe's increase in size.

    public float accelTime;
    public float speedConversion;

    public Rigidbody2D rb2d;
    public Transform background;
    public Transform handsTransform;
    public GameObject bloodExplosion;
    public AxeController axeScript;
    bool isAlive = false;
    bool isTakingDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        InitialSpawn();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(isAlive)
        {
            if (targetVel != currentVel)
            {
                this.currentVel += accel * Time.fixedDeltaTime;

                if (Mathf.Abs(Vector3.Distance(this.currentVel, this.targetVel)) < 0.2)
                {
                    currentVel = targetVel;
                }
            }
            if(!(Data.tutorial == 0 || Data.tutorial == 1 || Data.tutorial == 3 || Data.tutorial == 6 || Data.tutorial == 8 || Data.tutorial == 10 ))
            {
                currentVel = Vector3.zero;
            }
            if (currentVel != Vector3.zero)
            {
                float rotationAngle = Mathf.Atan2(currentVel.y, currentVel.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(rotationAngle - 90, Vector3.forward);
            }

            if (attackTimeLeft <= 0.0f)
            {
                AxeController.axeInMotion = false;
            }

            rb2d.velocity = currentVel;
            UpdateHandsRotation();
            RegenHealth();
        }
    }

    /// <summary>
    /// Regenerates 1 point of health every update when the player's health
    /// is below 100. If the regen time is greater than 0 the player does not regen any health.
    /// </summary>
    void RegenHealth()
    {
        if(100 > health)
        {
            if(regenStartTimeLeft <= 0.0f)
            {
                health++;
            }
        }

        regenStartTimeLeft -= Time.deltaTime;
    }

    /// <summary>
    /// Rotates the hands around the player.
    /// When the player is attacking the hands are rotated to the left and back again.
    /// When the player is walking the hands are rotated left and right.
    /// </summary>
    void UpdateHandsRotation()
    {
        if(attackTimeLeft > 0.0f)
        {
            if (attackTimeLeft > (attackTimeDelay + AXE_DELAY_SLOW_DOWN * Inventory.axeLevel) / 2)
            {
                handsTurnOffset += (axeRotationSpeed - AXE_SLOW_DOWN * Inventory.axeLevel);
            }

            else
            {
                handsTurnOffset -= (axeRotationSpeed - AXE_SLOW_DOWN * Inventory.axeLevel);
            }

            attackTimeLeft -= Time.deltaTime;
        }

        else if (currentVel != Vector3.zero)
        {
            turnHandsTimeLeft -= Time.deltaTime;

            if (turnHandsTimeLeft <= 0.0f)
            {
                turnHandsTimeLeft = turnHandsTimeDelay;

                handsTurnOffset += handsTurnAmount;

                if (Mathf.Abs(handsTurnOffset) > 25)
                {
                    handsTurnAmount = -handsTurnAmount;
                }
            }
        }

        else
        {
            handsTurnOffset = 0.0f;
        }

        handsTransform.localRotation = Quaternion.AngleAxis(handsTurnOffset, Vector3.forward);
    }

    /// <summary>
    /// Makes the player disapear for a short period of time
    /// and then reappear in the original starting position with staring health. 
    /// </summary>
    /// <returns>Time the function will wait before the player will appear in original position</returns>
    IEnumerator Spawn()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;
        yield return new WaitForSeconds(1.0f);
        GetComponent<SpriteRenderer>().color = Color.white;
        isAlive = true;
        health = 100;
        transform.position = spawnPos;

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    /// <summary>
    /// Makes the player appear in the starting level position when the scene first starts.
    /// </summary>
    void InitialSpawn()
    {
        isAlive = true;
        health = 100;
        transform.position = spawnPos;

        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    /// <summary>
    /// Reduces the health of the player by the passed in amount.
    /// When the health is taken away the player makes a sound and calls a function to make him
    /// go red for a short period of time.
    /// When the remaning health goes to 0 the Spawn function is called.
    /// </summary>
    /// <param name="t_incomingDamage"></param>
    public void TakeDamage(float t_incomingDamage)
    {
        if(!isAlive)
        {
            return;
        }

        health -= t_incomingDamage;
       
        if(health <= 0)
        {
            Instantiate(bloodExplosion, transform.position, Quaternion.identity);
            isAlive = false;
            StartCoroutine(Spawn());
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("Hurt");
            if (!isTakingDamage)
            {
                StartCoroutine(takeDamageAnim());
            }
            regenStartTimeLeft = regenStartTimeDelay;
        }
    }

    /// <summary>
    /// Sets the target velocity of the player to the new target velocity.
    /// </summary>
    /// <param name="newTargetVel">The new target velocity of the player</param>
    public void setTargetVel(Vector3 newTargetVel)
    {
        if(isAlive)
        {
            targetVel = newTargetVel / speedConversion;
            accel = (targetVel - currentVel) / accelTime;
        }
    }

    /// <summary>
    /// Animation for the player taking damage by making the player flash red
    /// for a short period of time.
    /// </summary>
    /// <returns></returns>
    IEnumerator takeDamageAnim()
    {
        isTakingDamage = true;
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.4f);

        GetComponent<SpriteRenderer>().color = Color.white;
        isTakingDamage = false;
    }

    /// <summary>
    /// Makes the player attack by starting the axe swing animation.
    /// </summary>
    public void attack()
    {
        if(attackTimeLeft <= 0.0f)
        {
            float pitch = 1.0f - 0.1f * Inventory.axeLevel;
            FindObjectOfType<AudioManager>().AlterPitch("Swing", pitch);
            FindObjectOfType<AudioManager>().Play("Swing");
            AxeController.axeInMotion = true;
            handsTurnOffset = 0.0f;
            attackTimeLeft = attackTimeDelay + AXE_DELAY_SLOW_DOWN * Inventory.axeLevel;
        }
    }
}
