using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Patroling,
    Chasing,
    Attacking
}


public class EnemyController : MonoBehaviour
{
    EnemyState state;                   //The state in which the enemy currently is in.

    Vector3 velocity;                   //The velocity of the enemy.
    public Transform spearTranform;     //Tranfrom used for position in instaciating the spear prefab.
    public Transform swordTranform;     //Tranfrom used for position in instaciating the sword prefab.
    public GameObject bloodExplosion;   //Prefab of the blood explosion.
    public float speed = 100;                  //The speed at which the enemy moves.
    float stateTimeLeft = 0.0f;         //The time left before another action is under taken in the current state.
    float turnHandsTimeLeft = 0.0f;     //The remaning time for which the hands are going to be rotated for.
    float turnHandsTimeDelay = 0.005f;  //The time that needs to pass before the hands are going to be rotated.
    float patrolTimeMax = 10.0f;        //The maximum time the nemy can be patrolling in one direction.
    float chaseTimeMax = 4.0f;          //The max time the enemy can try to chase the player without seeing him.
    float detectRange = 5.0f;           //The radius for the detect range of the enemy.
    float handsTurnAmount = 2.0f;       //The amount by whoch the hands are rotated each time while walking.
    float handsTurnOffset = 0.0f;       //The offset of the hands during th walk animation.
    public float health = 100;                 //The health of the enemy. When it reaches 0 the enemy is destroyed.
    float attackRange = 0.0f;

    bool tookDamage = false;

    Transform playerTransform;          //Reference to the transform of the player.
    public Transform handsTransform;    //Reference to the transfrom of the hands object.
    PlayerController playerScript;      //Reference to the player controller script.
    public HealthBar healthBarScript;
    GameObject weapon;                  //The instace of the weapon the enemy is holding.
    public GameObject spearPrefab;      //Prefab of the spear object.
    public GameObject swordPrefab;      //Prefab of the sword object.
    public GameObject parent;
    public Sprite[] characters;
    public Rigidbody2D rb2d;

    [SerializeField]
    GameObject coinPrefab = null;   //Prefab of the coin object.

    void Start()
    {
        healthBarScript.SetMaxHealth(health);

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = FindObjectOfType<PlayerController>();
        handsTransform.parent = transform;

        switch (Random.Range(0,2))
        {
            case 0:
                weapon = Instantiate(spearPrefab, spearTranform.position, Quaternion.identity, handsTransform);
                break;
            case 1:
                weapon = Instantiate(swordPrefab, swordTranform.position, Quaternion.identity,  handsTransform);
                break;
            default:
                break;
        }

        GetComponent<SpriteRenderer>().sprite = characters[Random.Range(0, characters.Length)];

        attackRange = weapon.GetComponent<Weapon>().range;
    }

    
    void FixedUpdate()
    {
        switch (state)
        {
            case EnemyState.Patroling:
                Patroling();
                ChangePosition();
                break;
            case EnemyState.Chasing:
                Chasing();
                ChangePosition();
                break;
            case EnemyState.Attacking:
                Attacking();
                break;
            default:
                break;
        }

        UpdateHandsRotation();
        stateTimeLeft -= Time.fixedDeltaTime;

        if(tookDamage && !AxeController.axeInMotion)
        {
            tookDamage = false;
        }
    }

    /// <summary>
    /// Changes the state that the enemy is currently in to the new state.
    /// </summary>
    /// <param name="t_state">The new state of the enemy</param>
    void ChangeState(EnemyState t_state)
    {
        state = t_state;

        switch (state)
        {
            case EnemyState.Patroling:
                stateTimeLeft = 0.0f;
                handsTurnOffset = 0.0f;
                break;
            case EnemyState.Chasing:
                stateTimeLeft = chaseTimeMax;
                handsTurnOffset = 0.0f;
                break;
            case EnemyState.Attacking:
                velocity = Vector3.zero;
                rb2d.velocity = velocity;
                stateTimeLeft = 0.0f;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Updates the rotation of the hands relative to the body.
    /// If the time left before the hands can be rotated is 0 the hands
    /// are rotated. If the rotation of the offset is greater than the absolute
    /// value of the turn hands limits the hads are ortated the other way.
    /// If the enemy is not moving the offset is set to 0.
    /// </summary>
    void UpdateHandsRotation()
    {
        if (stateTimeLeft > 0.0f && state == EnemyState.Attacking)
        {
            if (stateTimeLeft > weapon.GetComponent<Weapon>().attackSpeed / 2)
            {
                handsTurnOffset += 8;
            }

            else
            {
                handsTurnOffset -= 8;
            }
        }

        else if (velocity != Vector3.zero)
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
    /// Called when the enemy is in patrol state.
    /// The enemy travels in a certain direction until the time is up
    /// when that happens a new velocity is generated.
    /// If the Enemy comes accross the player he switches state to chasing.
    /// </summary>
    void Patroling()
    {
        if (stateTimeLeft <= 0.0f)
        {
            GenerateVelocity();
        }

        if (Mathf.Abs(Vector3.Distance(playerTransform.position, transform.position)) < detectRange)
        {
            ChangeState(EnemyState.Chasing);
        }
    }

    /// <summary>
    /// Called when the enemy is in a chasing state.
    /// The enemy generates a new velocity to the player every time
    /// the player is within detection range. If the player is not within the
    /// detection range for a certain amount of time the enemy switches state
    /// to patroling.
    /// </summary>
    void Chasing()
    {
        if(Mathf.Abs(Vector3.Distance(playerTransform.position, transform.position)) <= attackRange)
        {
            ChangeState(EnemyState.Attacking);
        }

        else if(Mathf.Abs(Vector3.Distance(playerTransform.position, transform.position)) < detectRange)
        {
            velocity = (playerTransform.position - transform.position).normalized * speed;
            stateTimeLeft = chaseTimeMax;
        }

        else if(stateTimeLeft <= 0.0f)
        {
            ChangeState(EnemyState.Patroling);
        }
    }

    /// <summary>
    /// Called when the enemy is in a attacking state.
    /// When the delay between attacks is 0 the enemy deals damage to the player.
    /// </summary>
    void Attacking()
    {
        if(stateTimeLeft <= 0.0f)
        {
            if (Mathf.Abs(Vector3.Distance(playerTransform.position, transform.position)) > attackRange)
            {
                ChangeState(EnemyState.Chasing);
            }
            else
            {
                playerScript.TakeDamage(weapon.GetComponent<Weapon>().damage);
                stateTimeLeft = weapon.GetComponent<Weapon>().attackSpeed;
            }
        }
    }

    /// <summary>
    /// Creates a new velocity vecotr in random direction and resets the state time.
    /// </summary>
    void GenerateVelocity()
    {
        velocity = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0).normalized * speed;
        stateTimeLeft = patrolTimeMax;
    }

    /// <summary>
    /// Changes the position and rotation of the enemy using the velocity vecotor.
    /// </summary>
    void ChangePosition()
    {
        if(velocity != Vector3.zero)
        {
            float rotationAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(rotationAngle - 90, Vector3.forward);
        }
        rb2d.velocity = velocity * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Takes away an amount of health away from the current health of the enemy.
    /// If the health reaches 0 the enemy is destroyed.
    /// </summary>
    /// <param name="t_damage">the amount of health that will be taken away from the current health of enemy</param>
    public void takeDamage(float t_damage)
    {
        if(tookDamage)
        {
            return;
        }

        tookDamage = true;
        FindObjectOfType<AudioManager>().Play("Hit");
        health -= t_damage;
        healthBarScript.SetCurrentHealth(health);

        StartCoroutine(pushBack(((transform.position - playerTransform.position).normalized) * 0.1f));
        StartCoroutine(takeDamageAnim());

        if(health <= 0)
        {

            Instantiate(bloodExplosion, transform.position,Quaternion.identity);
            SpawnCoins();
            if(Data.tutorial == 3)
            {
                Data.tutorial = 6;
            }

            LevelData.enemiesKilled++;
            Destroy(parent);
        }
    }

    /// <summary>
    /// Moves the player in the direction of the incoming velocity.
    /// </summary>
    /// <param name="pushBackVel">The velocity that will be applied to the position of the enemy</param>
    /// <returns>time the function needs to wait before it can continue</returns>
    IEnumerator pushBack(Vector3 pushBackVel)
    {
        float maxTravelDist = 3.0f;

        while(maxTravelDist > 0.0f)
        {
            yield return new WaitForSeconds(0.01f);
            transform.position += pushBackVel;
            maxTravelDist -= pushBackVel.magnitude;
        }
    }

    /// <summary>
    /// Makes the sprite of the enemy change colour to red and after wait some time,
    /// change back to white again.
    /// </summary>
    /// <returns>time the function needs to wait before it can continue</returns>
    IEnumerator takeDamageAnim()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.4f);

        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (state == EnemyState.Patroling)
        {
            GenerateVelocity();
        }
    }

    public float GetHealth()
    {
        return health;
    }
    /// <summary>
    /// Instantiates a number of coin prefabs at the position of the enemy
    /// </summary>
    void SpawnCoins()
    {
        //The number of coin prefabs to be instaciated.
        int numOfCoins = Random.Range(0, 3);

        for (int i = 0; i < numOfCoins; i++)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
            FindObjectOfType<AudioManager>().Play("CoinDrop");
        }
    }
}
