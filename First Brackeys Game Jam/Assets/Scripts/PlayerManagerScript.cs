using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour
{
    public Vector3 offsetCameraPosition,
                   offsetLocalScale;

    private Animator animator;
    private Camera cameraProjection;
    private Rigidbody2D rigidBody2D;
    private Transform cameraPosition;
    private Transform playerPosition,
                      playerSpawn;
    private Vector2 movementAxis;

    private GameManagerScript gameManagerScript;
    private RandomSpawnScript randomSpawnScript;

    public float moveSpeed = 0.1f;

    public int playerStrength = 0;

    private bool playerCanMove = false;
    private bool playerDefeated = false;

    private float idleTime, 
                  moveTime;
    private float defaultMoveSpeed;

    private int enemiesDefeatCount = 0;

    public bool GetPlayerDefeated()
    {
        return playerDefeated;
    }
    public int GetEnemiesDefeatCount()
    {
        return enemiesDefeatCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rigidBody2D = GetComponent<Rigidbody2D>();

        cameraProjection = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        cameraPosition = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        playerPosition = GetComponent<Transform>();
        playerSpawn = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();

        gameManagerScript = FindObjectOfType<GameManagerScript>();
        randomSpawnScript = FindObjectOfType<RandomSpawnScript>();

        defaultMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition.position = transform.position + offsetCameraPosition;

        InputProcess();
    }

    void FixedUpdate()
    {
        if (!gameManagerScript.GetGameIsOver())
        {
            MoveTiming();
            PlayerMovement(true);
        }
        else
        {
            PlayerMovement(false);
        }
    }

    #region OnTriggerEnter2D
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameManagerScript.ghostShroud)
        {
            if (collision.tag.Contains("Enemy"))
            {
                if (collision.GetComponent<EnemyAIScript>().enemySizeID == 0 && playerStrength == 0)
                {
                    playerDefeated = true;
                    Destroy(gameObject);
                }
                else
                {
                    if (collision.GetComponent<EnemyAIScript>().enemySizeID > playerStrength)
                    {
                        playerDefeated = true;
                        Destroy(gameObject);
                    }
                    else if (collision.GetComponent<EnemyAIScript>().enemySizeID == playerStrength)
                    {
                        transform.localScale = transform.localScale - offsetLocalScale;
                        playerStrength--;
                        cameraProjection.orthographicSize--;

                        enemiesDefeatCount++;
                        Destroy(collision.gameObject);
                    }
                    else if (collision.GetComponent<EnemyAIScript>().enemySizeID < playerStrength)
                    {
                        enemiesDefeatCount++;
                        Destroy(collision.gameObject);
                    }
                }
            }
        }

        if (collision.tag.Contains("Neutral"))
        {
            if (collision.GetComponent<SlimeAIScript>().neutralSizeID == 0 && playerStrength == 0)
            {
                transform.localScale = transform.localScale + offsetLocalScale;
                playerStrength++;
                cameraProjection.orthographicSize++;

                randomSpawnScript.GatherNeutralSlime();
                Destroy(collision.gameObject);
            }
            else
            {
                if (collision.GetComponent<SlimeAIScript>().neutralSizeID == playerStrength)
                {
                    transform.localScale = transform.localScale + offsetLocalScale;
                    playerStrength++;
                    cameraProjection.orthographicSize++;

                    randomSpawnScript.GatherNeutralSlime();
                    Destroy(collision.gameObject);
                }
            }
        }

        if (collision.tag.Contains("Border"))
        {
            playerPosition.position = playerSpawn.position;
        }
    }
    */
    #endregion

    #region OnCollisionEnter2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameManagerScript.ghostShroud)
        {
            if (collision.collider.tag.Contains("Enemy"))
            {
                if (collision.collider.GetComponent<EnemyAIScript>().enemySizeID == 0 && playerStrength == 0)
                {
                    playerDefeated = true;
                    Destroy(gameObject);
                }
                else
                {
                    if (collision.collider.GetComponent<EnemyAIScript>().enemySizeID > playerStrength)
                    {
                        playerDefeated = true;
                        Destroy(gameObject);
                    }
                    else if (collision.collider.GetComponent<EnemyAIScript>().enemySizeID == playerStrength)
                    {
                        transform.localScale = transform.localScale - offsetLocalScale;
                        moveSpeed += 0.25f;
                        playerStrength--;
                        cameraProjection.orthographicSize--;

                        enemiesDefeatCount++;
                        Destroy(collision.collider.gameObject);
                    }
                    else if (collision.collider.GetComponent<EnemyAIScript>().enemySizeID < playerStrength)
                    {
                        enemiesDefeatCount++;
                        Destroy(collision.collider.gameObject);
                    }
                }
            }
        }

        if (collision.collider.tag.Contains("Neutral"))
        {
            if (collision.collider.GetComponent<SlimeAIScript>().neutralSizeID == 0 && playerStrength == 0)
            {
                transform.localScale = transform.localScale + offsetLocalScale;
                moveSpeed -= 0.25f;
                playerStrength++;
                cameraProjection.orthographicSize++;

                randomSpawnScript.GatherNeutralSlime();
                Destroy(collision.collider.gameObject);
            }
            else
            {
                if (collision.collider.GetComponent<SlimeAIScript>().neutralSizeID == playerStrength)
                {
                    transform.localScale = transform.localScale + offsetLocalScale;
                    moveSpeed -= 0.25f;
                    playerStrength++;
                    cameraProjection.orthographicSize++;

                    randomSpawnScript.GatherNeutralSlime();
                    Destroy(collision.collider.gameObject);
                }
            }
        }

        if (collision.collider.tag.Contains("Border"))
        {
            playerPosition.position = playerSpawn.position;
        }
    }
    #endregion

    private void InputProcess()
    {
        float xMove = Input.GetAxisRaw("Horizontal"), 
              yMove = Input.GetAxisRaw("Vertical");

        movementAxis = new Vector2(xMove, yMove).normalized;
    }

    private void MoveTiming()
    {
        float idleTimeLimit = 0.5f, 
              moveTimeLimit = 1f;

        if (playerCanMove)
        {
            idleTime = 0f;

            moveTime += Time.fixedDeltaTime;

            animator.SetBool("isMoving", true);

            if (moveTime > moveTimeLimit)
            {
                playerCanMove = false;
            }

            defaultMoveSpeed = moveSpeed;
        }
        else
        {
            moveTime = 0f;

            idleTime += Time.fixedDeltaTime;

            animator.SetBool("isMoving", false);

            if (idleTime > idleTimeLimit)
            {
                playerCanMove = true;
            }

            defaultMoveSpeed = 0f;
        }

        /*
        if (!playerCanMove)
        {
            moveTime = 0f;

            idleTime += Time.fixedDeltaTime;

            if (idleTime > idleTimeLimit)
            {
                playerCanMove = true;
            }

            defaultMoveSpeed = 0f;
        }
        else
        {
            idleTime = 0f;

            moveTime += Time.fixedDeltaTime;

            if (moveTime > moveTimeLimit)
            {
                playerCanMove = false;
            }

            defaultMoveSpeed = moveSpeed;
        }
        */
    }

    private void PlayerMovement(bool gameOnGoing)
    {
        if (gameOnGoing)
        {
            rigidBody2D.velocity = new Vector2(movementAxis.x * defaultMoveSpeed, movementAxis.y * defaultMoveSpeed);
        }
        else
        {
            rigidBody2D.velocity = new Vector2(0f, 0f);
        }
    }
}
