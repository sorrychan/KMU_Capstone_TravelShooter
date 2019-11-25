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


    //0 : 타이틀  1: 메뉴 2: 레벨 3:게임씬
    public Canvas[] canvas;
    

    private string Stages =  "Stage1_";

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
            string name = EventSystem.current.currentSelectedGameObject.name;
            SceneManager.LoadScene(Stages + name);
        }
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
