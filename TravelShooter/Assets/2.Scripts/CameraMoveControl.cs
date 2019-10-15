using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveControl : MonoBehaviour
{
    
    public Camera MenuCamera;
    public Camera GameCamera;

    public GameObject[] CameraPositions;

    //0 : 타이틀  1: 메뉴 2: 레벨 3:게임씬
    public Canvas[] canvas;


    

    // Start is called before the first frame update
    void Start()
    {
        MenuCamera.enabled = true;
        GameCamera.enabled = false;
        canvas[0].enabled = true;
        canvas[1].enabled = false;
        canvas[2].enabled = false;
        MenuCamera.transform.position = CameraPositions[0].transform.position;

       
    }


    public void MoveToMenu()
    {
        MenuCamera.enabled = true;
        MenuCamera.transform.position = CameraPositions[1].transform.position;
        canvas[0].enabled = false;
        canvas[1].enabled = true;
        canvas[2].enabled = false;
        GameCamera.enabled = false;
    }
    public void MoveToStageSelect()
    {
        MenuCamera.enabled = true;
        MenuCamera.transform.position = CameraPositions[2].transform.position;
        canvas[2].enabled = true;
        canvas[1].enabled = false;
        GameCamera.enabled = false;
        GameCamera.GetComponent<GameManagement>().IsStart = 0;
    }

    public void MoveToGame()
    {
        MenuCamera.enabled = false;
        GameCamera.enabled = true;
        GameCamera.transform.position = CameraPositions[3].transform.position;
        canvas[3].enabled = true;
        canvas[2].enabled = false;
        GameCamera.GetComponent<GameManagement>().IsStart = 1;
    }



}
