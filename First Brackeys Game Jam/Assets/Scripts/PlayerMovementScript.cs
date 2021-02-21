using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    
    private bool canMove;

    private bool moveRight = false,
                 moveLeft = false,
                 moveUp = false, 
                 moveDown = false;

    [SerializeField] private float moveTime, idleTime;
    private float toggleSpeed;

    private Rigidbody2D rigidBody2D;
    private Vector2 movement;

    private GameManagerScript gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        gameManagerScript = FindObjectOfType<GameManagerScript>();

        //RandomStartDirection();
    }

    // Update is called once per frame
    void Update()
    {
        //MovementInputs();

        ProcessInputs();
    }

    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        if (!gameManagerScript.gameIsOver)
        {
            MoveTiming();

            MovePosition();
        }

        //MoveDirection();
    }

    private void RandomStartDirection()
    {
        int randomDirection = Random.Range(1, 5);
        switch (randomDirection)
        {
            case 1:
                moveRight = true;
                break;

            case 2:
                moveLeft = true;
                break;

            case 3:
                moveUp = true;
                break;

            case 4:
                moveDown = true;
                break;
        }

        moveTime = 0f;
        canMove = true;
    }

    private void MovementInputs()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (moveTime == 0 && !moveRight)
            {
                moveRight = true;
                moveLeft = false;
                moveUp = false;
                moveDown = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (moveTime == 0 && !moveLeft)
            {
                moveLeft = true;
                moveRight = false;
                moveUp = false;
                moveDown = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (moveTime == 0 && !moveUp)
            {
                moveUp = true;
                moveRight = false;
                moveLeft = false;
                moveDown = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (moveTime == 0 && !moveDown)
            {
                moveDown = true;
                moveRight = false;
                moveLeft = false;
                moveUp = false;
            }
        }
    }
    
    private void ProcessInputs()
    {
        float xMove = Input.GetAxisRaw("Horizontal"), 
              yMove = Input.GetAxisRaw("Vertical");

        movement = new Vector2(xMove, yMove).normalized;
    }

    private void MoveTiming()
    {
        if (canMove)
        {
            idleTime = 0f;

            moveTime += 1f * Time.fixedDeltaTime;

            if (moveTime > 1f)
            {
                canMove = false;
            }

            toggleSpeed = moveSpeed;
        }
        else
        {
            moveTime = 0f;

            idleTime += 1f * Time.fixedDeltaTime;

            if (idleTime > 1f)
            {
                canMove = true;
            }

            toggleSpeed = 0f;
        }
    }

    private void MoveDirection()
    {
        if (moveRight)
        {
            rigidBody2D.velocity = new Vector2(toggleSpeed, 0);
        }
        else if (moveLeft)
        {
            rigidBody2D.velocity = new Vector2(-toggleSpeed, 0);
        }
        else if (moveUp)
        {
            rigidBody2D.velocity = new Vector2(0, toggleSpeed);
        }
        else if (moveDown)
        {
            rigidBody2D.velocity = new Vector2(0, -toggleSpeed);
        }
    }

    private void MovePosition()
    {
        rigidBody2D.velocity = new Vector2(movement.x * toggleSpeed, movement.y * toggleSpeed);
    }

}
