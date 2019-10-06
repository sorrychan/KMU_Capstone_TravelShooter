using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnMousePos : MonoBehaviour
{
    public float speed;
    public bool ball = false;
    public GameObject S;
    public float fAniTime = 3.0f;
    float fTimeCalc = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            return;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ball == false)
        {
            fTimeCalc = Time.time + fAniTime;

            Vector3 mos = Input.mousePosition;
            mos.z = Camera.main.farClipPlane;

            Vector3 dir = Camera.main.ScreenToWorldPoint(mos);

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, mos.z))
            {
                Debug.Log(hit.point);
                GetComponent<Rigidbody>().AddForce(new Vector3(hit.point.x - transform.position.x, 0, hit.point.z - transform.position.z) * speed);
            }
            ball = true;
        }

        if (Time.time > fTimeCalc && ball == true)
        {
            Destroy(gameObject);
            Instantiate(S, new Vector3(1, 12, -14), transform.rotation);
            //fTimeCalc = Time.time + fAniTime;
            ball = false;
            //Debug.Log("destroy");
        }
    }

}
