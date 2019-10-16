﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Objects : MonoBehaviour
{
    public int isActivation = 0;           //활성화 되었는가
    public Material capMaterial;
    public int ExpForce = 700;

    public enum Kinds
    {
        Fall,       //일정한 방향으로 쓰러지는 물체
        Break,      //부서지는 물체
        Exp,        //폭발하는 물체
        Effect,     //투사체에 영향을 주는 물체
        Cut         //투사체를 자르는 물체
    }

    public Kinds kind;

    Rigidbody rb;

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.tag == "Bullet" && isActivation == 0)      //총알의 태그 = "Bullet", 활성화 되지 않았을 경우
        {
            switch (kind)
            {
                case Kinds.Fall:
                    transform.GetChild(0).gameObject.AddComponent<Rigidbody>();     //쓰러지는 물체 = (0)번쨰 자식 오브젝트
                    rb = transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
                    rb.velocity = new Vector3(10, 0, 0);

                    isActivation = 1;       //상호작용 후 물체는 활성화
                    break;

                case Kinds.Break:
                    Transform[] BreakObjectList = gameObject.GetComponentsInChildren<Transform>();
                    foreach(Transform obj in BreakObjectList)
                    {
                        obj.gameObject.AddComponent<Rigidbody>();
                    }

                    isActivation = 1;       //상호작용 후 물체는 활성화
                    break;

                case Kinds.Exp:
                    Transform[] ExpObjectList = gameObject.GetComponentsInChildren<Transform>();
                    //Collider[] ExpObjectLists = Physics.OverlapSphere(transform.position, 10.0f);     //원점을 중심으로 반경 안에 있는 오브젝트 객체 추출, 폭발을 다른 오브젝트나 적들에게도 영향이 가게 하려면 이것을 사용

                    foreach (Transform obj in ExpObjectList)
                    {
                        obj.gameObject.AddComponent<Rigidbody>();
                        rb = obj.GetComponent<Rigidbody>();
                        rb.AddExplosionForce(700, transform.position, 3, 1);       //힘, 위치, 반경, 위로 튀는 힘
                    }

                    isActivation = 1;       //상호작용 후 물체는 활성화
                    break;

                case Kinds.Effect:
                    //other.SendMessage("함수 이름");    //투사체에서 일어나는 효과는 투사체 안에 함수로 설정

                    //isActivation = 1;       //상호작용 후 물체는 활성화(일회용이라면)
                    break;

                case Kinds.Cut:
                    GameObject[] gameObjects = MeshCut.Cut(other.gameObject, other.gameObject.transform.position, Vector3.right, capMaterial);
                    
                    gameObjects[1].AddComponent<BoxCollider>();
                    gameObjects[1].AddComponent<Rigidbody>();

                    rb = gameObjects[1].gameObject.GetComponent<Rigidbody>();
                    rb.velocity = gameObjects[0].gameObject.GetComponent<Rigidbody>().velocity;
                    rb.mass = gameObjects[0].gameObject.GetComponent<Rigidbody>().mass;
                    gameObject.tag = "Bullet";
                   // EditorUtility.CopySerialized(gameObjects[0], gameObjects[1]);
                    
                    //isActivation = 1;       //상호작용 후 물체는 활성화(일회용이라면)
                    break;
            }
        }
    }
}