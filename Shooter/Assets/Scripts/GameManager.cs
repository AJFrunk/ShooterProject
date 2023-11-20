using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Enemy1Prefab;
    public GameObject Enemy2Prefab;
    public GameObject Cloud;
    public GameObject CoinPrefab;
    public GameObject LifePrefab;
    public int score;
    public int livesCounter;
    public int cloudsMove;
    public GameObject[] thingsThatSpawn;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI PowerUpText;
    public GameObject gameOverSet;
    private bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        InvokeRepeating("CreateEnemy1", 1.0f, 3.0f);
        InvokeRepeating("CreateEnemy2", 1.0f, 4.0f);
        ///InvokeRepeating("CreateCoin", 1.0f, 15.0f);
        ///InvokeRepeating("CreateLife", 30f, 30f);
        InvokeRepeating("SpawnSomething", 2f, 3f);
        CreateSky();
        score = 0;
        livesCounter = 3;
        cloudsMove = 1;
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + livesCounter;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            SceneManager.LoadScene("Game");
        }
    }
    void CreateEnemy1()
    {
        Instantiate(Enemy1Prefab, new Vector3(Random.Range(-7, 7), 7, 0), Quaternion.identity);
    }
    void CreateEnemy2()
    {
        Instantiate(Enemy2Prefab, new Vector3(-10, Random.Range(3, 6), 0), Quaternion.Euler(0, 0, 90));
    }
    void CreateSky()
    {
        for (int i = 0; i < 50; i++)
        {
            Instantiate(Cloud, new Vector3(Random.Range(-11f, 11f), Random.Range(-12f, 12f), 0), Quaternion.identity);
        }
    }
    void CreateCoin()
    {
        Instantiate(CoinPrefab, new Vector3(Random.Range(-7, 7), 7, 0), Quaternion.identity);
    }
    void CreateLife()
    {
        Instantiate(LifePrefab, new Vector3(-8,-2, 0), Quaternion.identity);
    }

    void SpawnSomething()
    {
        int tempInt;
        tempInt = Random.Range(0,3);
        Instantiate(thingsThatSpawn[tempInt], new Vector3(Random.Range(-7f, 7f), Random.Range(-3.5f, 0f), 0), Quaternion.identity);
    }
    public void GameOver()
    {
        CancelInvoke();
        cloudsMove = 0;
        GetComponent<AudioSource>().Stop();
        gameOverSet.SetActive(true);
        isGameOver = true;
    }
    public void EarnScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void LoseLife(int lifeToRemove)
    {
        livesCounter = livesCounter - lifeToRemove;
        livesCounter = Mathf.Clamp(livesCounter, 0, 3);
        livesText.text = "Lives: " + livesCounter;
    }
    public void PowerUpChange(string whatPowerUp) 
    {
        PowerUpText.text = whatPowerUp;
    }
}