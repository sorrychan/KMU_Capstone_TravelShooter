using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//these are required for the script to work.
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class DragShot : MonoBehaviour
{

     private float currentDistance; //공이랑 마우스 위치
     private float goodSpace; // 0 - max distance 사이 공간
     private float shootPower; //쏘는 힘
     private Vector3 shootDirection; //쏠 방향
     private LineRenderer line; //라인생성

     private RaycastHit hitInfo; 
     private Vector3 currentMousePosition; 
     private Vector3 temp;

     //publics
     [Header("The Layer/layers the floors are on")]
     public LayerMask groundLayers;
     [Header("Max pull distance")]
     public float maxDistance = 3f;
     [Header("Power")]
     public float power;
    [Header("Fire GuideLine")]
    public GameObject guide;

     private NavMeshObstacle obstacle;
     private Rigidbody rbody;
    public float dragValue = 0.9f;
    public float shotposY = 0.5f;

    private bool IsHitTarget = false;
    private bool IsShotProjectile = false;

    private void Awake()
     {
         line = GetComponent<LineRenderer>(); 
         rbody = GetComponent<Rigidbody>();
        obstacle = gameObject.GetComponent<NavMeshObstacle>();
    }
     
     void DragObject()
     {
         obstacle.enabled = true;
        
         rbody.velocity = rbody.velocity * dragValue;
     }

     private void GravityOn()
     {
        rbody.useGravity = true;
     }

    private void OnMouseDown()
    {
        if (!IsShotProjectile)
        {
            line.enabled = true;
            //라인 시작점
            line.SetPosition(0, transform.position);
        }
    }

    private void OnMouseDrag()
    {
        if (!IsShotProjectile)
        {
            currentDistance = Vector3.Distance(currentMousePosition, transform.position); //현재 마우스 위치 갱신
                                                             
            if (currentDistance <= maxDistance)
            {
                temp = currentMousePosition; //드래그 최종 가능한 거리만큼 저장
                goodSpace = currentDistance;

            }
            else
            {
                temp = new Vector3(currentMousePosition.x, currentMousePosition.y, temp.z); 
                goodSpace = maxDistance;
            }
            //쏘는 힘 계산
            shootPower = Mathf.Abs(goodSpace) * power;

         
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, groundLayers))
            {
                currentMousePosition = new Vector3(hitInfo.point.x, transform.position.y-shotposY, hitInfo.point.z);
            }

            
            shootDirection = Vector3.Normalize(currentMousePosition - transform.position);
            
            line.SetPosition(1, temp);

            guide.GetComponent<PreviewArch>().Preview(gameObject.transform.position, shootDirection* shootPower/5 * -1);
        }
    }   

    private void OnMouseUp()
    {
        if (!IsShotProjectile)
        {
            guide.GetComponent<LineRenderer>().enabled = false;
            GravityOn();
            Vector3 push = shootDirection * shootPower * -1;
            //push.y = 0;
            GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
            line.enabled = false; //remove the line
            IsShotProjectile = true;
        }
    }


    private void Update()
    {
        if (IsHitTarget)
        {
            DragObject();

            if (rbody.velocity.z < 2.0f)
                gameObject.tag = "PLANES";
        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (other.collider.tag == "PLANES")
        {
            Destroy(gameObject, 4f);

        }
        else
        {
            IsHitTarget = true;
            Destroy(gameObject, 4f);

        }
    }
}
