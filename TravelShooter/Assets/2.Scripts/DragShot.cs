using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//these are required for the script to work.
[RequireComponent(typeof(Rigidbody))]

//[RequireComponent(typeof(LineRenderer))]
public class DragShot : MonoBehaviour
{
    public enum Kinds
    {
        defult,         //기본탄
        cluster        //확산탄
    }
    public Kinds kind;

    private float currentDistance; //공이랑 마우스 위치
     private float goodSpace; // 0 - max distance 사이 공간
     private float shootPower; //쏘는 힘
     private Vector3 shootDirection; //쏠 방향
    // private LineRenderer line; //라인생성

     private RaycastHit hitInfo; 
     private Vector3 currentMousePosition; 
     private Vector3 temp;

    private bool IsOnceTouchGround = false;

     //publics
     [Header("The Layer/layers the floors are on")]
     public LayerMask groundLayers;
     [Header("Max pull distance")]
     public float maxDistance = 3f;
     [Header("Power")]
     public float power;
    [Header("Fire GuideLine")]
    public GameObject guide;
    [Header("Particle Select")]
    public GameObject Particle;

     private NavMeshObstacle obstacle;
     private Rigidbody rbody;
    public float dragValue = 0.9f;
    public float shotposY = 0.5f;

    private bool IsHitTarget = false;
    private bool IsShotProjectile = false;

    [SerializeField]
    private GameObject pauseOptionScript;
    private int state = -1;

    private void Awake()
     {
        // line = GetComponent<LineRenderer>(); 
        rbody = GetComponent<Rigidbody>();
        obstacle = gameObject.GetComponent<NavMeshObstacle>();
        pauseOptionScript = GameObject.Find("GameManager");
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

    //private void OnMouseDown()
    //{
    //    if (!IsShotProjectile)
    //    {
    //        line.enabled = true;
    //        //라인 시작점
    //        line.SetPosition(0, transform.position);
    //    }
    //}

    //모바일용 터치 제어용 함수
    private void CalculateDragPower()
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
                currentMousePosition = new Vector3(hitInfo.point.x, transform.position.y - shotposY, hitInfo.point.z);
            }


            shootDirection = Vector3.Normalize(currentMousePosition - transform.position);

            //    line.SetPosition(1, temp);

            guide.GetComponent<PreviewArch>().Preview(gameObject.transform.position, shootDirection * shootPower / 5 * -1);
        }
    }
    private void ShotProjectile()
    {
        if (!IsShotProjectile)
        {
            guide.GetComponent<LineRenderer>().enabled = false;
            GravityOn();
            Vector3 push = shootDirection * shootPower * -1;
            //push.y = 0;
            GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
            //   line.enabled = false; //remove the line
            IsShotProjectile = true;
        }
    }

    //PC용 마우스 입력
    private void OnMouseDrag()
    {
        state = pauseOptionScript.GetComponent<PauseOption>().MulitShotState;
        // <<<< 멀티샷 구매 <<<<
        switch (state)
        {
            case -1:
                kind = Kinds.defult;
                break;
            case 1:
                kind = Kinds.cluster;
                break;
        }

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
                currentMousePosition = new Vector3(hitInfo.point.x, transform.position.y - shotposY, hitInfo.point.z);
            }


            shootDirection = Vector3.Normalize(currentMousePosition - transform.position);

            //    line.SetPosition(1, temp);

            guide.GetComponent<PreviewArch>().Preview(gameObject.transform.position, shootDirection * shootPower / 5 * -1);
        }
    }

    private void OnMouseUp()
    {
        if (!IsShotProjectile)
        {
            guide.GetComponent<LineRenderer>().enabled = false;
            GravityOn();
            Vector3 push = shootDirection * shootPower * -1;
            
            GetComponent<Rigidbody>().AddForce(push, ForceMode.Impulse);
            
            IsShotProjectile = true;
        }
    }


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                CalculateDragPower();
            }

            if (Input.touchCount == 1)
            {
                touch = Input.GetTouch(1);

                //if (touch.phase == TouchPhase.Began)
                //{
                //    // Halve the size of the cube.
                //    transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                //}

                if (touch.phase == TouchPhase.Ended)
                {
                    ShotProjectile();
                }
            }
            else if(Input.touchCount >1)
            {
                if (!IsShotProjectile)
                    GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }

        }

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
            if (!IsOnceTouchGround)
            {
                Vector3 collisionPos = transform.position;
                Instantiate(Particle, collisionPos,Quaternion.identity);
                IsOnceTouchGround = true;
            }
            Destroy(gameObject, 4f);
            
        }
        else
        {
            IsHitTarget = true;
            Destroy(gameObject, 4f);
        }

        if (kind == Kinds.cluster)
        {
            for (int i = 1; i < 4; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                //transform.GetChild(i).gameObject.SendMessage("ShotProjectile");
                transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Rigidbody>().velocity + new Vector3((i-2)*5,0,0);
            }
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
