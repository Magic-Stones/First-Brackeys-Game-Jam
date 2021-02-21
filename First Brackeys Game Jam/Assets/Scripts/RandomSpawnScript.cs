using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnScript : MonoBehaviour
{
    public float setSlimeSpawnTime = 3f;

    public int setEnemySpawnLimit = 5, 
               setNeutralSpawnLimit = 3;
    public int neutralSpawnLimit = 0, 
               enemySpawnCount = 0;
 
    public Transform[] spawnPoints;
    public GameObject[] spawnNeutrals;
    public GameObject[] spawnEnemies;
    public GameObject player;

    private GameManagerScript gameManagerScript;

    private float slimeSpawnTime;

    private int enemySpawnLimit = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManagerScript = FindObjectOfType<GameManagerScript>();

        slimeSpawnTime = setSlimeSpawnTime;

        EnemySlimeSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManagerScript.gameIsOver)
        {
            slimeSpawnTime -= Time.deltaTime;

            if (slimeSpawnTime <= 0f)
            {
                EnemySlimeSpawn();

                NeutralSlimeSpawn();

                slimeSpawnTime = setSlimeSpawnTime;
            }
        }
    }

    private void EnemySlimeSpawn()
    {
        if (enemySpawnLimit < setEnemySpawnLimit)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            int randomEnemy = Random.Range(0, spawnEnemies.Length);

            Instantiate(spawnEnemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            enemySpawnLimit++;
            enemySpawnCount++;
        }
    }

    private void NeutralSlimeSpawn()
    {
        if (neutralSpawnLimit < setNeutralSpawnLimit)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);

            switch (player.GetComponent<PlayerManagerScript>().playerStrength)
            {
                case 0:
                    Instantiate(spawnNeutrals[0], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                    break;

                case 1:
                    Instantiate(spawnNeutrals[1], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                    break;

                case 2:
                    Instantiate(spawnNeutrals[2], spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                    break;
            }

            neutralSpawnLimit++;
        }
    }
}
