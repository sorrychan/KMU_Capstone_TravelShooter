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

    public GameObject Item1;

    //아이템 관리
    public int PreviewLineState = -1;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        InfoCanvas.enabled = true;
        mainCanvas.enabled = true;
        stopCanvas.enabled = false;
        WinCanvas.enabled = false;
        LoseCanvas.enabled = false;


        PreviewLineState = -1;
    }

    //아이템1 체크 관리
    public void CheckPreviewLineState()
    {
        if (!gameObject.GetComponent<GoldManagement>().isGoldBelowZero)
        {
            //Item1 = GameObject.Find("CheckItem1");
            if (!Item1.gameObject.activeSelf)
            {
                Item1.SetActive(true);
            }
            else
                Item1.SetActive(false);
            PreviewLineState *= -1;
        }
    }


    private void Update()
    {
        if(mainCamera.GetComponent<GameManagement>().isClear == 1)
        {
            Time.timeScale = 0;
            WinCanvas.enabled = true;
            //스테이지 클리어 골드 증가
            gameObject.GetComponent<GoldManagement>().StageClearRewardGold();
            //스테이지 클리어시 다음 스테이지 언락
            gameObject.GetComponent<StageLock>().AddClearStageInfo();

            mainCanvas.enabled = false;
            mainCamera.GetComponent<GameManagement>().isClear = 0;
        }
        else if (mainCamera.GetComponent<GameManagement>().isFailed== 1)
        {
            Time.timeScale = 0;
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
        if(Item1!=null &&Item1.activeSelf)
        {
            Debug.Log("골드 소모 체크");
            gameObject.GetComponent<GoldManagement>().UseGoldForGuideLine();
        }
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
