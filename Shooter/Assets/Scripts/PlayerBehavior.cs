using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private float speed;
    public float horizontalInput;
    public float verticalInput;
    public float horizontalScreenLimit;
    public float verticalScreenLimit;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public int lives;

    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        horizontalScreenLimit = 9.5f;
        verticalScreenLimit = 3.5f;
        lives = 3;
        Debug.Log(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);

        if (transform.position.x > horizontalScreenLimit)

        //I am outside screen from right
        {
            transform.position = new Vector3(-horizontalScreenLimit, transform.position.y, 0);
        }
        else if (transform.position.x < -horizontalScreenLimit)
        //I am outside screen from left
        {
            transform.position = new Vector3(horizontalScreenLimit, transform.position.y, 0);
        }
        if (transform.position.y > 1)
        //I am outside of screen from top
        {
            transform.position = new Vector3(transform.position.x, 1, 0);

        }
        else if (transform.position.y < -verticalScreenLimit)
        //I am outside screen from bottom
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenLimit, 0);
        }
    }
    void Shooting()
    {
        //If I press Space, create bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Create a bullet
            Instantiate(bulletPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
        }
    }
    public void LoseLife()
    {
        lives --;
        GameObject.Find("GameManager").GetComponent<GameManager>().LoseLife(1);
        if (lives == 0) 
        {
            //GameOver
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
            Destroy(this.gameObject);
        }
    }
}
