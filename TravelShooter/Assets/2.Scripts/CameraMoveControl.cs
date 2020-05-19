﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CameraMoveControl : MonoBehaviour
{
    public static int A = 0;

    public Camera MenuCamera;

    public GameObject[] CameraPositions;

    public GameObject Stage;

    //0 : 타이틀  1: 메뉴 2: 레벨 3:게임씬
    public Canvas[] canvas;


    private string Stages =  "Stage1_";


    public float CamTimer = 1.0f;

    private bool IsStageButtonClicked = false;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Screen.SetResolution(600, 960, true);
    }



    // Start is called before the first frame update
    void Start()
    {
        //MenuCamera.enabled = true;
        canvas[0].enabled = true;
        canvas[1].enabled = false;
        canvas[2].enabled = false;
        MenuCamera.transform.position = CameraPositions[0].transform.position;

        if(A==1)
        {
            MoveToStageSelect();
        }

        BGMClass.instance.GetComponent<AudioSource>().Stop();
    }
    private void Update()
    {
        if(IsStageButtonClicked)
        {
            CamTimer -= Time.deltaTime;
            if (CamTimer < 0)
            {
                string name = EventSystem.current.currentSelectedGameObject.name;
                switch (Stage.GetComponent<StageMove>().CurrentStage)
                {
                    case 0:
                        Stages = "Stage1_";
                        break;
                    case 1:
                        Stages = "Stage2_";
                        break;
                    case 2:
                        Stages = "Stage3_";
                        break;
                }
                SceneManager.LoadScene(Stages + name);
            }
        }

    }

    public void MoveToMain()
    {
        canvas[0].enabled = true;
        canvas[1].enabled = false;
        canvas[2].enabled = false;
        MenuCamera.transform.position = CameraPositions[0].transform.position;
    }
    public void MoveToMenu()
    {
      //  MenuCamera.enabled = true;
        MenuCamera.transform.position = CameraPositions[1].transform.position;
        canvas[0].enabled = false;
        canvas[1].enabled = true;
        canvas[2].enabled = false;
    }
    public void MoveToStageSelect()
    {
       // MenuCamera.enabled = true;
        MenuCamera.transform.position = CameraPositions[2].transform.position;
        canvas[2].enabled = true;
        canvas[1].enabled = false;
    }

    public void MoveToGame()
    {

       if (!gameObject.GetComponent<HeartRechargeManagement>().isHeartBelowZero)
      {
            IsStageButtonClicked = true;
            //string name = EventSystem.current.currentSelectedGameObject.name;
            //    SceneManager.LoadScene(Stages + name);
      }
        

    }

    public void MoveToUserMap()
    {
        SceneManager.LoadScene("UserMap");
    }

    public void QuitGmae()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
