using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject Enemy1Prefab; 
    public GameObject Enemy2Prefab;
    public GameObject Cloud;
    public GameObject CoinPrefab;
    public int score;
    public int livesCounter;
    public int cloudsMove;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemy1", 1.0f, 3.0f);
        InvokeRepeating("CreateEnemy2", 1.0f, 4.0f);
        InvokeRepeating("CreateCoin", 1.0f, 15.0f);
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
        
    }
    void CreateEnemy1()
    {
        Instantiate(Enemy1Prefab, new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
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
        Instantiate(CoinPrefab, new Vector3(Random.Range(-8, 8), 7, 0), Quaternion.identity);
    }
    public void GameOver()
    {
        CancelInvoke();
        cloudsMove = 0;
    }
    public void EarnScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void LoseLife(int lifeToRemove)
    {
        livesCounter = livesCounter - lifeToRemove;
        livesText.text = "Lives: " + livesCounter;
    }
}