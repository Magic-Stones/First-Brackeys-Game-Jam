using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject victoryPanel, defeatPanel;
    private RandomSpawnScript randomSpawnScript;

    public bool gameIsOver = false;

    // Start is called before the first frame update
    void Start()
    {
        randomSpawnScript = FindObjectOfType<RandomSpawnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (randomSpawnScript.enemySpawnCount == 0)
        {
            GameOver("Win");
            randomSpawnScript.enemySpawnCount++;
        }
    }

    public void GameOver(string condition)
    {
        if (condition.Equals("Win"))
        {
            victoryPanel.SetActive(true);
            gameIsOver = true;

            //Debug.Log("Winner!");
        }
        else if (condition.Equals("Lose"))
        {
            defeatPanel.SetActive(true);
            gameIsOver = true;

            //Debug.Log("Game Over");
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuState");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
