using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIScript : MonoBehaviour
{
    public Text enemyCountText, 
                strengthCountText;

    private RandomSpawnScript randomSpawnScript;
    private PlayerManagerScript playerManagerScript;
    private GameManagerScript gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        randomSpawnScript = FindObjectOfType<RandomSpawnScript>();
        playerManagerScript = FindObjectOfType<PlayerManagerScript>();
        gameManagerScript = FindObjectOfType<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCountText.text = randomSpawnScript.enemySpawnCount.ToString() + " / 10";

        if (playerManagerScript.playerStrength < 3)
        {
            strengthCountText.text = playerManagerScript.playerStrength.ToString() + " / 3";
        }
        else if (playerManagerScript.playerStrength == 3)
        {
            strengthCountText.text = "OVERPOWERED!";
        }
    }

    public void Retry()
    {
        gameManagerScript.RetryGame();
    }

    public void Menu()
    {
        gameManagerScript.GoToMenu();
    }
}
