using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Enemy1Prefab; 
    public GameObject Enemy2Prefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemy1", 1.0f, 3.0f);
        InvokeRepeating("CreateEnemy2", 1.0f, 5.0f);
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
        Instantiate(Enemy2Prefab, new Vector3(-10, Random.Range(3,6), 0), Quaternion.Euler(0,0,90));
    }
}
