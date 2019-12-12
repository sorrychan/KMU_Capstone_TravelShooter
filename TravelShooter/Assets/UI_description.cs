using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_description : MonoBehaviour
{
    
    [Header("1. 발사체 자체 dragshot의 pauseoption 에 Gamemanager 오브젝트 설정")]
    public bool Projectile_pauseOption;
    [Header("2. 발사체 child 의 guideline의 pauseoption에도 Gamemanager 오브젝트 설정")]
    public bool GuideLine_pauseOption;
    [Header("2. 적들 AI 안의 GameManager 항목에 GamePlayCamera")]
    public bool noValue;
    [Header("3. DefenseLine BaseCollisionCheck에 GamePlayCamera할당")]
    public bool noval;
    [Header("4. Gamemanager 안에 CurrentStageNum을 스테이지에 맞게 설정")]
    public bool thisisimportant;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
