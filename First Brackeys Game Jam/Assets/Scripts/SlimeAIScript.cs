using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAIScript : MonoBehaviour
{
    public int neutralSizeID = 0;

    public float moveSpeed = 0.1f;
    public float startWaitTime = 3f;
    public float stoppingDistance = 0.2f;
    public float minimumLimit, excludedLimit;

    private WanderingPointsScript wanderingPointsScript;
    public Transform[] wanderingPoints;
    private int randomPoint;

    private RandomSpawnScript randomSpawnScript;

    /*
    private Transform movePoint;
    public float minX, maxX, minY, maxY;
    */

    private bool canMove;

    [SerializeField] private float moveTime, idleTime;
    private float waitTime;
    private float moveTimeLimit, idleTimeLimit;
    private float toggleSpeed;

    private GameObject player;
    private GameManagerScript gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wanderingPointsScript = FindObjectOfType<WanderingPointsScript>();
        gameManagerScript = FindObjectOfType<GameManagerScript>();

        randomSpawnScript = FindObjectOfType<RandomSpawnScript>();

        waitTime = startWaitTime;

        idleTimeLimit = Random.Range(minimumLimit, excludedLimit);

        randomPoint = Random.Range(0, wanderingPointsScript.wanderPoints.Length);

        /*
        movePoint = GameObject.FindGameObjectWithTag("Wandering Point").GetComponent<Transform>();
        movePoint.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        */
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        if (!gameManagerScript.gameIsOver)
        {
            Wandering();
        }
    }

    private void MoveTiming()
    {
        if (canMove)
        {
            idleTime = 0f;
            
            if (waitTime == startWaitTime)
            {
                moveTime += 1f * Time.fixedDeltaTime;
            }
            else if (waitTime < startWaitTime)
            {
                moveTime = 0f;
            }

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

            if (waitTime == startWaitTime)
            {
                idleTime += 1f * Time.fixedDeltaTime;
            }
            else if (waitTime < startWaitTime)
            {
                idleTime = 0f;
            }

            if (idleTime > idleTimeLimit)
            {
                moveTimeLimit = Random.Range(minimumLimit, excludedLimit);
                canMove = true;
            }

            toggleSpeed = 0f;
        }
    }

    private void Wandering()
    {
        MoveTiming();

        transform.position = Vector2.MoveTowards(transform.position, wanderingPointsScript.wanderPoints[randomPoint].position, 
                                                toggleSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, wanderingPointsScript.wanderPoints[randomPoint].position) < stoppingDistance)
        {
            if (waitTime <= 0f)
            {
                randomPoint = Random.Range(0, wanderingPointsScript.wanderPoints.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.fixedDeltaTime;
            }
        }

        /*
        transform.position = Vector2.MoveTowards(transform.position, movePoint.position, toggleSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, movePoint.position) < stoppingDistance)
        {
            if (waitTime <= 0f)
            {
                movePoint.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.fixedDeltaTime;
            }
        }
        */
    }
}
