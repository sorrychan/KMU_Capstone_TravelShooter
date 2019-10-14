using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShot : MonoBehaviour
{
    [Range(20,50)]public float ShotSpeed = 20f;


    public GameObject Target;
    public float dragValue = 0.9f;

    private GameObject projectile;
    private Rigidbody projRbdy;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("UI_Target");
        ShotSpeed = Target.GetComponent<TargetUIscript>().FiringPowerSave;
        projectile = gameObject;
        projRbdy = gameObject.GetComponent<Rigidbody>();

        Vector3 shoot = (Target.transform.position - projectile.transform.position).normalized;

      
        projRbdy.AddForce(shoot * ShotSpeed * 30.0f);
    }

    void OnCollisionEnter(Collision other)
    {
        
        if (other.collider.tag == "PLANES")
        {
            Destroy(Target, 1f);
            Destroy(gameObject, 1f);
            
        }
        else
        {

            Destroy(Target, 1f);
            Destroy(gameObject, 3f);
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile.transform.position.z > Target.transform.position.z)
        {
            projRbdy.velocity = projRbdy.velocity * dragValue;
            projRbdy.useGravity = true;
        }
    }
}
