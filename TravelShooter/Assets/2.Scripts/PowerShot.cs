﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShot : MonoBehaviour
{
    [Range(20,50)]public float ShotSpeed = 20f;


    public GameObject Target;
    public float dragValue = 0.9f;

    private Transform projectile;
    private Rigidbody projRbdy;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("UI_Target");
        ShotSpeed = Target.GetComponent<TargetUIscript>().FiringPowerSave;
        projectile = gameObject.GetComponent<Transform>();
        projRbdy = gameObject.GetComponent<Rigidbody>();

        Vector3 newTargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z) ;
       // Debug.Log("ntp: " + newTargetPos);
        Vector3 shoot = (newTargetPos - projectile.position).normalized;

        //Debug.Log("Shoot: " + shoot);
        Vector3 direction = new Vector3(shoot.x,0, shoot.z);
        projRbdy.AddForce(direction* (int)ShotSpeed*50f);
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
        if (gameObject != null && Target!=null)
        {
            if (projectile.transform.position.z > Target.transform.position.z)
            {
                projRbdy.velocity = projRbdy.velocity * dragValue;
               
            }
        }
    }
}
