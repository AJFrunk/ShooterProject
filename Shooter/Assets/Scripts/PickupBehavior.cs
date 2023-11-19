using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehavior : MonoBehaviour
{
    public int pickupType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupType == 0)
        {
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 4f);
        }   
    }
    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if (whatIHit.tag == "Player")
        {
            if (pickupType == 0)
            {

                GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(10);
                Destroy(this.gameObject);
            }
        }
    }
}
