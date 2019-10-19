using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public int isClear = 0;     //1이 클리어
    public int isFailed = 0;    //패배 상태체크;
    public int IsStart = 0;     //1이 시작
    public float Time = 10.0f;
    public GameObject[] Enemys;
    public int EnemyCount;
    private void Start()
    {
        StartCoroutine(this.TimeClear());
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        EnemyCount = Enemys.Length;
        //GameClearCheak();
    }

    public void GameClearCheak()    //AI.cs 에서 적이 죽을 때 마다 검사
    {
        if (EnemyCount == 0)           //적이 남아있지 않으면
        {
            isClear = 1;            //클리어
        }
    }

    IEnumerator TimeClear()
    {
        if (IsStart == 1)
        {
            yield return new WaitForSeconds(Time);     //Time만큼 시간이 지나면 클리어
            isClear = 1;
        }
    }
}
