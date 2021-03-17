using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject victoryPanel, defeatPanel;
    public Transform[] wanderingPoints;

    public bool ghostShroud = false;

    public int defeatEnemiesGoal = 1;

    private PlayerManagerScript playerManagerScript;
    private RandomSpawnScript randomSpawnScript;

    private bool gameIsOver = false;

    public bool GetGameIsOver()
    {
        return gameIsOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerManagerScript = FindObjectOfType<PlayerManagerScript>();
        randomSpawnScript = FindObjectOfType<RandomSpawnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManagerScript.GetEnemiesDefeatCount() == defeatEnemiesGoal)
        {
            GameOver("Win");
        }
        else if (playerManagerScript.GetPlayerDefeated())
        {
            GameOver("Lose");
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

    public void BudgetCoroutine()
    {
        float time = 0f;
        time += Time.deltaTime;

        if (time > 1f)
        {
            GameOver("Lose");
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
