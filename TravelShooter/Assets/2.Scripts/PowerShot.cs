using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PowerShot : MonoBehaviour
{
    [Range(20,50)]public float ShotSpeed = 20f;


    public GameObject Target;
    public float dragValue = 0.9f;

    private Transform projectile;
    private Rigidbody projRbdy;
    private NavMeshObstacle obstacle;

    private bool IsHitTarget = false;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("UI_Target");
        ShotSpeed = Target.GetComponent<TargetUIscript>().FiringPowerSave;
        projectile = gameObject.GetComponent<Transform>();
        projRbdy = gameObject.GetComponent<Rigidbody>();
        obstacle = gameObject.GetComponent<NavMeshObstacle>();

        Vector3 newTargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y, Target.transform.position.z) ;
       // Debug.Log("ntp: " + newTargetPos);
        Vector3 shoot = (newTargetPos - projectile.position).normalized;

        //Debug.Log("Shoot: " + shoot);
        Vector3 direction = new Vector3(shoot.x,0, shoot.z);
        projRbdy.AddRelativeForce(direction* (int)ShotSpeed*750f);
    }

    private void OnCollisionEnter(Collision other)
    {
        GravityOn();
        //projectile.GetComponent<Rigidbody>().useGravity = true;
        if (other.collider.tag == "PLANES")
        {
            //IsHitTarget = true;
            Destroy(Target, 1f);
            Destroy(gameObject, 4f);
           
        }
        else
        {
            IsHitTarget = true;
            

            Destroy(Target, 1f);
            Destroy(gameObject, 4f);
           
        }
    }
    void DragObject()
    {
        obstacle.enabled = true;
        projRbdy.velocity = projRbdy.velocity * dragValue;
    }
    private void GravityOn()
    {
        projectile.GetComponent<Rigidbody>().useGravity = true;
    }

    // Update is called once per frame
    private void ShotProjectile()
    {
        if (gameObject != null && Target != null)
        {
            if (projectile.transform.position.z > Target.transform.position.z)
            {
                IsHitTarget = true;
                //GravityOn();
                //projectile.GetComponent<Rigidbody>().useGravity = true;


            }
            if (IsHitTarget)
            {
                DragObject();
                //transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            //if (projRbdy.velocity.y != 0)
            //projRbdy.velocity.Set(projRbdy.velocity.x, 0, projRbdy.velocity.z);
            if (projRbdy.velocity.z < 2.0f)
                gameObject.tag = "PLANES";
        }
    }
    void Update()
    {
        ShotProjectile();
    }
}
