using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAIScript : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    public float setWaitTime = 3f;
    public float decreasePlayerMoveSpeed = 0.75f;

    public int neutralSizeID = 0;

    private Animator animator;

    private GameManagerScript gameManagerScript;
    private RandomSpawnScript randomSpawnScript;

    private bool canMove = false;

    private float distanceThreshold = 0.2f;
    private float moveTime, 
                  idleTime;
    private float waitTime;
    private float minMoveSpeed,
                  maxMoveSpeed,
                  randomSpeed;
    private float defaultMoveSpeed;

    private int randomPoint;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        gameManagerScript = FindObjectOfType<GameManagerScript>();
        randomSpawnScript = FindObjectOfType<RandomSpawnScript>();

        waitTime = setWaitTime;

        minMoveSpeed = moveSpeed - 0.5f;
        maxMoveSpeed = moveSpeed + 0.5f;
        randomSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        randomPoint = Random.Range(0, gameManagerScript.wanderingPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        if (!gameManagerScript.GetGameIsOver())
        {
            MoveTiming();
            Wandering();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            if (collision.GetComponent<EnemyAIScript>().enemySizeID == 0 && neutralSizeID == 0)
            {
                Destroy(gameObject);
            }
            else if (collision.GetComponent<EnemyAIScript>().enemySizeID > neutralSizeID)
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveTiming()
    {
        if (canMove)
        {
            idleTime = 0f;
            
            if (waitTime == setWaitTime)
            {
                moveTime += 1f * Time.fixedDeltaTime;
                animator.SetBool("isMoving", true);
            }
            else if (waitTime < setWaitTime) // If the slime reaches its destination...
            {
                moveTime = 0f;
                animator.SetBool("isMoving", false);
            }

            if (moveTime > 1f)
            {
                randomSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
                canMove = false;
            }

            defaultMoveSpeed = randomSpeed;
        }
        else
        {
            moveTime = 0f;

            if (waitTime == setWaitTime)
            {
                idleTime += 1f * Time.fixedDeltaTime;
            }
            else if (waitTime < setWaitTime) // If the slime reaches its destination...
            {
                idleTime = 0f;
            }

            animator.SetBool("isMoving", false);

            if (idleTime > 1f)
            {
                canMove = true;
            }

            defaultMoveSpeed = 0f;
        }
    }

    private void Wandering()
    {
        transform.position = Vector2.MoveTowards(transform.position, gameManagerScript.wanderingPoints[randomPoint].position,
                                                defaultMoveSpeed * Time.fixedDeltaTime);

        // If the slime reaches its destination...
        if (Vector2.Distance(transform.position, gameManagerScript.wanderingPoints[randomPoint].position) < distanceThreshold)
        {
            if (waitTime <= 0f)
            {
                randomPoint = Random.Range(0, gameManagerScript.wanderingPoints.Length);
                waitTime = setWaitTime;
            }
            else
            {
                waitTime -= Time.fixedDeltaTime;
            }
        }
    }
}
