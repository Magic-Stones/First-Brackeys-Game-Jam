using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIScript : MonoBehaviour
{
    public int enemySizeID = 0;

    public float moveSpeed = 0.1f;
    public float distanceLimit;
    public float minimumLimit, excludedLimit;

    private bool canMove = false;

    [SerializeField] private float moveTime, idleTime;
    private float toggleSpeed;
    private float moveTimeLimit, idleTimeLimit;

    private Transform playerTarget;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        idleTimeLimit = Random.Range(minimumLimit, excludedLimit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        if (playerTarget != null)
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
            
            if (moveTime > moveTimeLimit)
            {
                idleTimeLimit = Random.Range(minimumLimit, excludedLimit);
                canMove = false;
            }

            toggleSpeed = moveSpeed;
        }
        else
        {
            moveTime = 0f;

            idleTime += 1f * Time.fixedDeltaTime;

            if (idleTime > idleTimeLimit)
            {
                moveTimeLimit = Random.Range(minimumLimit, excludedLimit);
                canMove = true;
            }

            toggleSpeed = 0f;
        }
    }

    private void PlayerChasing()
    {
        if (Vector2.Distance(transform.position, playerTarget.position) > distanceLimit)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, toggleSpeed * Time.fixedDeltaTime);
        }
    }
}
