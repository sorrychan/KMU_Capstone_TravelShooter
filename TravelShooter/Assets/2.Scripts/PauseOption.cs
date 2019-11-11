using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseOption : MonoBehaviour
{
    
    public Camera mainCamera;
    
    public GameObject Campos;


    public Canvas mainCanvas;
    public Canvas stopCanvas;
    public Canvas WinCanvas;
    public Canvas LoseCanvas;
    public Canvas InfoCanvas;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        InfoCanvas.enabled = true;
        mainCanvas.enabled = false;
        stopCanvas.enabled = false;
        WinCanvas.enabled = false;
        LoseCanvas.enabled = false;
       
        

    }
    private void Update()
    {
        if(mainCamera.GetComponent<GameManagement>().isClear == 1)
        {
            WinCanvas.enabled = true;
            mainCanvas.enabled = false;
            mainCamera.GetComponent<GameManagement>().isClear = 0;
        }
        else if (mainCamera.GetComponent<GameManagement>().isFailed== 1)
        {
            LoseCanvas.enabled = true;
            mainCanvas.enabled = false;
            mainCamera.GetComponent<GameManagement>().isFailed = 0;
        }

        if (Campos.transform.position.z < -112 && Campos.GetComponent<GameCameraMovement>().IsMoveOn)
        {
            Campos.GetComponent<GameCameraMovement>().IsMoveOn = false;
            Campos.GetComponent<GameCameraMovement>().IsSizeWiden = true;

            Campos.transform.position = new Vector3(Campos.transform.position.x, Campos.transform.position.y, Campos.transform.position.z + 10);
        }
        if(mainCamera.orthographicSize >24 && Campos.GetComponent<GameCameraMovement>().IsSizeWiden)
        {
            Campos.GetComponent<GameCameraMovement>().IsSizeWiden = false;
            mainCanvas.enabled = true;
            mainCamera.GetComponent<GameManagement>().IsStart = 1;
            
        }

    }

    public void StageInfo()
    {
        Campos.GetComponent<GameCameraMovement>().IsMoveOn = true;
        InfoCanvas.enabled = false;
    }
    public void NextStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void PauseButtonClicked()
    {
        Time.timeScale = 0;
        mainCanvas.enabled = false;
        stopCanvas.enabled = true;

    }
    public void ResumeGame()
    {
        mainCanvas.enabled = true;
        stopCanvas.enabled = false;
        Time.timeScale = 1;
    }
    public void RetryStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToStageSelect()
    {
        CameraMoveControl.A = 1;
        SceneManager.LoadScene("Scene01");
    }
}
