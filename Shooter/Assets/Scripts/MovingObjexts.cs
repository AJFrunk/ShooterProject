using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjexts : MonoBehaviour
{
    public int objectType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objectType == 0)
        {
            //You are a bullet
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * 8f);
        }
        else if (objectType == 1)
        {
            //You are enemy 1
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 3f);
        }
        else if (objectType == 2)
        {
            //You are enemy 2
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * 2f);
        }

        if (transform.position.y > 12f || transform.position.y < -12f || transform.position.x > 12f || transform.position.x < -12f)
        {
            Destroy(this.gameObject);
        }
    }
}
