using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAutoDestroy : MonoBehaviour
{
    public float DeadCount = 5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "PLANES")
        {
            gameObject.layer = 0;
            Destroy(gameObject, DeadCount);
        }
    }
}
