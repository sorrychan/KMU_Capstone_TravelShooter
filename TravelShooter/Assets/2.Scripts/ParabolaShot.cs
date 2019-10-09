using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaShot : MonoBehaviour
{
    public GameObject Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    private Transform Projectile;
    private Transform myTransform;

    void Awake()
    {
        myTransform = transform;
        Projectile = gameObject.transform;
       
    }

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("UI_Target");
        firingAngle = Target.GetComponent<TargetUIscript>().FiringAngleSave;
        StartCoroutine(SimulateProjectile());

    }

    void OnCollisionEnter(Collision other)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
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


    IEnumerator SimulateProjectile()
    {
        // 투사체 발사후 딜레이 설정
        yield return new WaitForSeconds(1.0f);

        // 투사체 던지는 시작점 설정, 뒤에 + 벡터는 수정 가능(오프셋)
        Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // 목표와의 거리 계산
        float target_Distance = Vector3.Distance(Projectile.position, Target.transform.position);

        // 목표까지 특정 각도로 쏠때 얼마의 속도가 필요한지 계산  (거리/ sin(2 x 사격각도 x Deg2Rad) / 중력)
        
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // 각 x,y의 속도를 구해서 적용
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // 날아가는 시간 계산
        float flightDuration = target_Distance / Vx;

        // 투사체가 목표지점을 정면으로 보도록 설정
        Projectile.rotation = Quaternion.LookRotation(Target.transform.position - Projectile.position);

        float elapse_time = 0;

        // 목표에도달 하기  전까지 지속적으로 움직임 
        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;
            yield return null;
        }
    }

}