using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    private float randomSpeed;
    private GameObject gM;
    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.Find("GameManager");
        float tempValue = Random.Range(0.5f, 1f);
        randomSpeed = Random.Range(1f, 4f);
        transform.localScale = new Vector3(1, 1, 1) * tempValue;
        tempValue = Random.Range(0.1f, 0.3f);
        GetComponent<SpriteRenderer>().color =new Color(1,1,1,tempValue);
    }

    // Update is called once per frame
    void Update()
    {
        int shouldIMove = gM.GetComponent <GameManager>().cloudsMove;
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * randomSpeed * shouldIMove);
        if (transform.position.y < -12)
        {
            transform.position = new Vector3(transform.position.x, 12, 0);
        }
    }
}
