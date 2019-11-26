using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public int isHit = 0;
    public GameObject particle;

    private void OnCollisionEnter(Collision collision)
    {
        if (isHit==0 && collision.gameObject.tag == "PLANES")
        {
            Instantiate(particle, this.gameObject.transform);
            isHit = 1;
        }
    }
}
