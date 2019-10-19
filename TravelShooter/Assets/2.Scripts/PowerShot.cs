using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerShot : MonoBehaviour
{
    [Range(20,50)]public float ShotSpeed = 20f;


    public GameObject Target;
    public float dragValue = 0.9f;

    private Transform projectile;
    private Rigidbody projRbdy;

    private bool IsHitTarget = false;

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
        projectile.GetComponent<Rigidbody>().useGravity = true;
        if (other.collider.tag == "PLANES")
        {
            IsHitTarget = true;
            Destroy(Target, 1f);
            Destroy(gameObject, 1f);
            
        }
        else
        {
            IsHitTarget = true;
            Destroy(Target, 1f);
            Destroy(gameObject, 3f);
           
        }
    }
    void DragObject()
    {
        projRbdy.velocity = projRbdy.velocity * dragValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null && Target!=null)
        {
            if (projectile.transform.position.z > Target.transform.position.z)
            {
                IsHitTarget = true;
                projectile.GetComponent<Rigidbody>().useGravity = true;
                
               
            }
            if (IsHitTarget)
                DragObject();

            if (projRbdy.velocity.y !=0)
                projRbdy.velocity.Set(projRbdy.velocity.x,0, projRbdy.velocity.z);
        }
    }
}
