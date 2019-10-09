﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTarget : MonoBehaviour
{
    //타겟
    public GameObject Target;
    //투사체 발사 위치
    public Transform ProjSpawnPos;
    //투사체 지정
    public GameObject Projectile_1;

    public int ShotCount = 3;



    public Camera GameCamera;

    private bool IsMarkerSpawned = false;

    //임시 타겟 생성용
    private GameObject newTarget;

    void Start()
    {

    }


    void Update()
    {
        if (GameCamera.enabled )
        {
            if (Input.GetMouseButton(0)  )
            {
                Vector3 mos = Input.mousePosition;
                mos.z = GameCamera.farClipPlane;
                Ray ray = GameCamera.ScreenPointToRay(Input.mousePosition);

                Vector3 dir = GameCamera.ScreenToWorldPoint(mos);

                RaycastHit hit;
                
                if (Physics.Raycast(ray, out hit))
                {

                    //Debug.Log(hit.point);
                    if (IsMarkerSpawned == false)
                     {
                        Vector3 pos = new Vector3(0,0.1f,0);

                        newTarget = Instantiate(Target, pos + hit.point, Quaternion.Euler(90, 0, 0)) as GameObject;
                        newTarget.gameObject.tag = "UI_TempTarget";
                        IsMarkerSpawned = true;

                    }
                    newTarget.transform.position = new Vector3( hit.point.x,4.1f,hit.point.z);

                    newTarget.GetComponent<TargetUIscript>().FiringAngleSave += 1;
                }
                
            }
            else if (Input.GetMouseButtonUp(0) && IsMarkerSpawned && GameObject.FindGameObjectWithTag("UI_Target") == null)
            {
                newTarget.gameObject.tag = "UI_Target";
                Instantiate(Projectile_1, ProjSpawnPos.position, transform.rotation);
                ShotCount--;
                IsMarkerSpawned = false;
            }
        }
    }
}