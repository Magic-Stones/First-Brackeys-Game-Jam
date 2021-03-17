using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    // public float distanceLimit;

    public int enemySizeID = 0;

    private Animator animator;
    private Transform playerTarget;

    private GameManagerScript gameManagerScript;

    private bool canMove = false;

    private float moveTime, 
                  idleTime;
    private float maxMoveSpeed, 
                  randomSpeed;
    private float defaultMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        gameManagerScript = FindObjectOfType<GameManagerScript>();
        
        maxMoveSpeed = moveSpeed + 0.5f;
        randomSpeed = Random.Range(moveSpeed, maxMoveSpeed);
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
            PlayerChasing();
        }
    }

    private void MoveTiming()
    {
        if (canMove)
        {
            idleTime = 0f;

            moveTime += 1f * Time.fixedDeltaTime;

            animator.SetBool("isMoving", true);
            
            if (moveTime > 1f)
            {
                randomSpeed = Random.Range(moveSpeed, maxMoveSpeed);
                canMove = false;
            }

            defaultMoveSpeed = randomSpeed;
        }
        else
        {
            moveTime = 0f;

            idleTime += 1f * Time.fixedDeltaTime;

            animator.SetBool("isMoving", false);

            if (idleTime > 1f)
            {
                canMove = true;
            }

            defaultMoveSpeed = 0f;
        }
    }

    private void PlayerChasing()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, defaultMoveSpeed * Time.fixedDeltaTime);

        /*
        if (Vector2.Distance(transform.position, playerTarget.position) > distanceLimit)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, defaultMoveSpeed * Time.fixedDeltaTime);
        }
        */
    }
}
