using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnScript : MonoBehaviour
{
    public float setSlimeSpawnTime = 3f;

    public int setEnemySpawnLimit = 5, 
               setNeutralSpawnLimit = 3;
    public int neutralSpawnLimit = 0, 
               countSpawnLimit;
 
    public Transform[] spawnPoints;
    public GameObject[] spawnNeutrals;
    public GameObject[] spawnEnemies;
    public GameObject player;

    private float slimeSpawnTime;

    private int enemySpawnLimit = 0;
    private int totalSpawnLimit;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        slimeSpawnTime = setSlimeSpawnTime;

        totalSpawnLimit = setEnemySpawnLimit + setNeutralSpawnLimit;
        countSpawnLimit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            slimeSpawnTime -= Time.deltaTime;

            if (countSpawnLimit < totalSpawnLimit)
            {
                if (slimeSpawnTime <= 0f)
                {
                    int randomSpawn = Random.Range(1, 3);

                    switch (randomSpawn)
                    {
                        case 1:
                            EnemySlimeSpawn();
                            break;

                        case 2:
                            NeutralSlimeSpawn();
                            break;
                    }

                    slimeSpawnTime = setSlimeSpawnTime;
                }
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

            countSpawnLimit++;
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

            countSpawnLimit++;
        }
    }
}
