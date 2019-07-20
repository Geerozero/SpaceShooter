using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public int score;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public bool restart;
    public bool gameOver;

    private void Start() {
        
        restartText.text = "";
        gameOverText.text = "";
        restart = false;
        gameOver = false;
        
        StartCoroutine(SpawnWaves());

        score = 0;
    }

    private void Update() 
    {
        if(Input.GetKey("escape"))
            {
                Application.Quit();
            }

        if(restart)
        {
            if(Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    //instantiate hazards
    IEnumerator SpawnWaves()
    {   
        yield return new WaitForSeconds(spawnWait);

        while(true)
        {
            for(int i = 0; i < hazardCount; i++)
            {
                float x = Random.Range(-6.0f, 6.0f);
                Vector3 spawnPosition = new Vector3(x, spawnValues.y,spawnValues.z);
                
                //this means no rotation
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                restartText.text = "Press R to restart.";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
    score += newScoreValue;
    UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}


