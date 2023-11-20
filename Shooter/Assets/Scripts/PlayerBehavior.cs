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
    public AudioClip coinSound;
    public AudioClip healthSound;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public int lives;
    private bool betterWeapon;
    private bool shielded;
    public GameObject thruster;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        horizontalScreenLimit = 9.5f;
        verticalScreenLimit = 3.5f;
        lives = 3;
        Debug.Log(lives);
        betterWeapon = false;
        shielded = false;
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
        if (Input.GetKeyDown(KeyCode.Space) && betterWeapon == false)
        {
            //Create a bullet
            Instantiate(bulletPrefab, transform.position + new Vector3(0,1,0), Quaternion.identity);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && betterWeapon == true)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 1, 0), Quaternion.Euler(0,0,-45f));
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 1, 0), Quaternion.Euler(0, 0, 45f));
        }
    }
    public void LoseLife(int lifeToLose)
    {
        if (shielded == false)
        {
            lives = lives - lifeToLose;
            lives = Mathf.Clamp(lives, 0, 3);
            GameObject.Find("GameManager").GetComponent<GameManager>().LoseLife(lifeToLose);
            if (lives == 0)
            {
                //GameOver
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
                Destroy(this.gameObject);
            }
        }
        if (shielded == true)
        {
            shield.SetActive(false);
            shielded = false;
            GameObject.Find("GameManager").GetComponent<GameManager>().PowerUpChange("No Powerup");
        }
    }
    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        switch (whatIHit.name)
        {
            case "Coin(Clone)":
                AudioSource.PlayClipAtPoint(coinSound, transform.position);

                GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(10);
                Destroy(whatIHit.gameObject);
                break;
            case "Health(Clone)":
                AudioSource.PlayClipAtPoint(healthSound, transform.position);
                GameObject.Find("GameManager").GetComponent<GameManager>().LoseLife(-1);
                LoseLife(-1);
                Destroy(whatIHit.gameObject);
                break;
            case "PowerUp(Clone)":
                AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
                Destroy(whatIHit.gameObject);
                int tempInt;
                tempInt = Random.Range(1, 4);
                if (tempInt == 1) 
                {
                    //speed powerup
                    speed = 8f;
                    thruster.SetActive(true);
                    StartCoroutine("SpeedPowerDown");
                    GameObject.Find("GameManager").GetComponent<GameManager>().PowerUpChange("Speed");
                }
                else if (tempInt == 2) 
                {
                    ///weapon powerup
                    betterWeapon = true;
                    StartCoroutine("WeaponPowerDown");
                    GameObject.Find("GameManager").GetComponent<GameManager>().PowerUpChange("Weapon");
                }
                else if(tempInt == 3) 
                {
                    ///sheild powerup
                    shielded = true;
                    shield.SetActive(true);
                    StartCoroutine("ShieldPowerDown");
                    GameObject.Find("GameManager").GetComponent<GameManager>().PowerUpChange("Shield");
                }
                break;
        }

    }
    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(4f);
        speed = 4f;
        thruster.SetActive(false);
        GameObject.Find("GameManager").GetComponent<GameManager>().PowerUpChange("No Powerup");
        AudioSource.PlayClipAtPoint(powerDownSound, transform.position);
    }
    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(4f);
        betterWeapon = false;
        GameObject.Find("GameManager").GetComponent<GameManager>().PowerUpChange("No Powerup");
        AudioSource.PlayClipAtPoint(powerDownSound, transform.position);
    }
    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(6f);
        shield.SetActive(false);
        shielded = false;
        GameObject.Find("GameManager").GetComponent<GameManager>().PowerUpChange("No Powerup");
        AudioSource.PlayClipAtPoint(powerDownSound, transform.position);
    }
}
