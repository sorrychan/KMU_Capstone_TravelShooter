using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_GiveDieInfo : MonoBehaviour
{
    public GameObject Parent;
    //찬솔 적 애니메이션 및 래그돌 테스트용으로 2019.10.19
    public GameObject Charobj;
    public GameObject Ragdobj;

    public AudioSource DieSound;

    public GameObject BombPos;


    RaycastHit hit;
    public float MaxDistance = 3f;

    [Header("파티클 선택용")]
    public GameObject particle;
    [Header("충돌속도가 이 값 이하면 충돌판정 x")]
    public float CollisionSpeed = 2.0f;

    [Header("폭발 범위 조절 값")]
    public float explosionRange = 15;


    private Vector3 rayposition;
    private bool IsOnceHit = false; //파티클 한번만

    // Start is called before the first frame update
    void Start()
    {
        BombPos = GameObject.FindGameObjectWithTag("Bomb");
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
        Vector3 newPos = transform.position;
        DieSound.Play();
        newPos.y += 2.0f;
        Instantiate(particle, newPos, Quaternion.identity);
        Parent.GetComponent<AI>().enemyState = AI.EnemyState.die;
        ChangeRagdoll();
    }


    private void HitByExplosion()
    {
        Vector3 newPos = transform.position;
        DieSound.Play();
        newPos.y += 2.0f;
        Instantiate(particle, newPos, Quaternion.identity);
        Parent.GetComponent<AI>().enemyState = AI.EnemyState.die;
        ChangeRagdoll();
        Ragdobj.GetComponentInChildren<Rigidbody>().AddExplosionForce(10000, BombPos.transform.position, explosionRange, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
       

        Parent.transform.position = gameObject.transform.position;
        Ragdobj.transform.position = gameObject.transform.position;
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Object" || collision.gameObject.tag == "Die")/*&&collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude> CollisionSpeed)*/      //태그가 불릿이나 오브젝트이고, 속도가 일정 이상이 되면
        {
            if (!IsOnceHit)
            {

                IsOnceHit = true;
            }
            HitByProjectile();

            //Debug.Log(" Die");
        }
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        Vector3 newPos = transform.position;

        Parent.transform.position = gameObject.transform.position;
        Ragdobj.transform.position = gameObject.transform.position;
        if ((other.gameObject.tag == "Bullet" || other.gameObject.tag == "Object" || other.gameObject.tag == "Die")&&other.GetComponent<Rigidbody>().velocity.magnitude > CollisionSpeed)      //태그가 불릿이나 오브젝트이고, 속도가 일정 이상이 되면
        {
            if (!IsOnceHit)
            {
                IsOnceHit = true;
            }
            HitByProjectile();

            //Debug.Log(" Die");
        }
    }
}