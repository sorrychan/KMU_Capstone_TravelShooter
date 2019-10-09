﻿//적의 AI에 사용되는 스크립트

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
	public enum EnemyState {idle, trace, pattern, die};  //적의 상태 열거형
	public EnemyState enemyState;       //적의 상태
    public float HP;                    //체력
    public GameObject Target;           //타겟의 정보를 받아오기 위해 선언
    public Vector3 TargetPos;           //타겟의 위치
	private Transform EnemyTr;          //적의 좌표
	private NavMeshAgent nvAgent;       //네비메쉬를 사용하기 위해 선언
    public float Speed = 10.0f;
	//Animator animator;                //애니메이션 제어를 위해 선언
    public GameObject Camera;
    public enum EnemyPattern { pattern1,pattern2,pattern3 };  //패턴 종류
    public EnemyPattern enemypattern;

    void OnEnable()     //SetActive(true) 상태가 될때 초기화
	{
		EnemyTr = GetComponent<Transform>();        //자신의 좌표를 받아옴
		//animator = GetComponent<Animator>();      //애니메이터 연결
		nvAgent = GetComponent<NavMeshAgent>();     //네비메쉬 연결
		StartCoroutine(this.CheckMonsterState());   //상태체크 코루틴 시작
		StartCoroutine(this.MonsterAction());       //행동 코루틴 시작
		enemyState = EnemyState.idle;               //적의 상태 초기화
        nvAgent.speed= Speed;                       //적의 속도 설정
        gameObject.tag = "Enemy";                   //적의 테그 설정
        TargetPos= Target.transform.position;       //타겟 위치
        TargetPos.x = EnemyTr.position.x;           //x 좌표는 고정 (직진)
    }

	IEnumerator CheckMonsterState()     //적의 상태 체크
	{
		while (true)
		{
			yield return new WaitForSeconds(0.2f);      //0.2초마다 코루틴 반복

            if (Camera.GetComponent<GameManagement>().IsStart == 0)     //스테이지 시작 전
            {
                enemyState = EnemyState.idle;
            }

            else if (HP<0)       //죽었는가?
            {
                enemyState = EnemyState.die;
            }

            /*
            else if()           //패턴
            {
                 enemyState = EnemyState.pattern;
            }
            */

            else
            {
                enemyState = EnemyState.trace;
            }
		}
	}

	IEnumerator MonsterAction()     //적의 상태에 따른 행동
    {
		while (true)
		{
			switch (enemyState)
			{
                case EnemyState.die:
                    //animator.SetBool("Walk", false);
                    //animator.SetBool("Die", true);
                    nvAgent.isStopped = true;
                    gameObject.tag = "Die";
                    yield return new WaitForSeconds(1.9f);
                    break;

                case EnemyState.pattern:
                    nvAgent.isStopped = true;
                    pattern();
                    break;

			    case EnemyState.idle:     //기본상태
                    nvAgent.isStopped = true;       //정지
                    //animator.SetBool("Walk", false);
                    break;

			    case EnemyState.trace:    //추격상태
                    nvAgent.isStopped = false;      //이동
                    //animator.SetBool("Walk", true);
                    nvAgent.destination = TargetPos;        //타겟 위치로 이동
				    break;
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
    
    void Damage(float D)
    {
        HP -= D;
    }

    void pattern()
    {
        switch(enemypattern)
        {
            case EnemyPattern.pattern1:
                break;

            case EnemyPattern.pattern2:
                break;

            case EnemyPattern.pattern3:
                break;
        }
    }

}