﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_GiveDieInfo : MonoBehaviour
{
    public GameObject Parent;
    //찬솔 적 애니메이션 및 래그돌 테스트용으로 2019.10.19
    public GameObject Charobj;
    public GameObject Ragdobj;
    RaycastHit hit;
    public float MaxDistance = 3f;


    private Vector3 rayposition;


    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void ChangeRagdoll()
    {
        
        CopyAnimCharacterTransformToRagdoll(Charobj.transform, Ragdobj.transform);
        Charobj.gameObject.SetActive(false);
        Ragdobj.gameObject.SetActive(true);

    }

    private void Update()
    {
        rayposition = new Vector3(transform.position.x, transform.position.y+1.8f, transform.position.z);

        //Parent.transform.position = gameObject.transform.position;
        //Ragdobj.transform.position = gameObject.transform.position;

        //Debug.DrawRay(rayposition, transform.forward * MaxDistance, Color.blue, 0.3f);
        //if (Physics.Raycast(rayposition, transform.forward, out hit, MaxDistance))
        //{
        //    if(hit.collider.gameObject.tag=="Bullet")
        //        HitByProjectile();
        //}
         

  
    }



    private void CopyAnimCharacterTransformToRagdoll(Transform origin, Transform rag)
    {
        
        for (int i = 0; i < origin.transform.childCount; i++)
        {
            if (origin.transform.childCount != 0)
            {
                CopyAnimCharacterTransformToRagdoll(origin.transform.GetChild(i), rag.transform.GetChild(i));
            }

            rag.transform.GetChild(i).localPosition = origin.transform.GetChild(i).localPosition;
            rag.transform.GetChild(i).localRotation = origin.transform.GetChild(i).localRotation;

        }
    }
    private void HitByProjectile()
    {
        Parent.GetComponent<AI>().enemyState = AI.EnemyState.die;
        ChangeRagdoll();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Parent.transform.position = gameObject.transform.position;
        Ragdobj.transform.position = gameObject.transform.position;
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Object" || collision.gameObject.tag == "Die")/*&&collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude> CollisionSpeed)*/      //태그가 불릿이나 오브젝트이고, 속도가 일정 이상이 되면
        {

            HitByProjectile();

            //Debug.Log(" Die");
        }
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        Parent.transform.position = gameObject.transform.position;
        Ragdobj.transform.position = gameObject.transform.position;
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Object" || other.gameObject.tag == "Die")/*&&collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude> CollisionSpeed)*/      //태그가 불릿이나 오브젝트이고, 속도가 일정 이상이 되면
        {

            HitByProjectile();

            //Debug.Log(" Die");
        }
    }
}