using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CameraMoveControl : MonoBehaviour
{
    
    public Camera MenuCamera;

    public GameObject[] CameraPositions;

    //0 : 타이틀  1: 메뉴 2: 레벨 3:게임씬
    public Canvas[] canvas;
    

    private string Stages =  "Stage1_";
    

    // Start is called before the first frame update
    void Start()
    {
        //MenuCamera.enabled = true;
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
        string name = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(Stages + name);
    }



}
