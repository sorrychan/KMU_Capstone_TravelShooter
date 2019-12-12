using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseOption : MonoBehaviour
{
    [Header("승리 및 패배시 사운드")]
    public AudioSource WinSound_ohh;
    public AudioSource LoseSound_Bhoo;
    public AudioSource LoseSound_Harp;


    public Camera mainCamera;
    
    public GameObject Campos;


    public Canvas mainCanvas;
    public Canvas stopCanvas;
    public Canvas WinCanvas;
    public Canvas LoseCanvas;
    public Canvas InfoCanvas;

    public GameObject Item1;
    public GameObject Item2;

    //아이템 관리
    public int PreviewLineState = -1;

    public int MulitShotState = -1;

    //public Easing.Type easing;


    // Start is called before the first frame update
    void Start()
    {
        //FadeInOut(true);
        Time.timeScale = 1;
        InfoCanvas.enabled = true;
        mainCanvas.enabled = true;
        stopCanvas.enabled = false;
        WinCanvas.enabled = false;
        LoseCanvas.enabled = false;


        PreviewLineState = -1;

        //Debug.Log(BGMClass.instance.name);
        if (BGMClass.instance.GetComponent<AudioSource>().isPlaying)
        {
            return;
        }
        else
            BGMClass.instance.GetComponent<AudioSource>().Play();

    }

    //public void FadeInOut(bool FadeIn)
    //{
    //    if (FadeIn)
    //        Camera.main.FadeIn(1f, easing);
    //    else
    //    {
    //        Camera.main.FadeOut(1f, easing);
    //    }

    //}

    //아이템1 체크 관리
    public void CheckPreviewLineState()
    {
        if (gameObject.GetComponent<GoldManagement>().IsGoldMoreThan300())
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

    //아이템2 체크 관리
    public void CheckMultiPleShotState()
    {
        if (gameObject.GetComponent<GoldManagement>().IsGoldMoreThan1000())
        {
            //Item1 = GameObject.Find("CheckItem1");
            if (!Item2.gameObject.activeSelf)
            {
                Item2.SetActive(true);
            }
            else
                Item2.SetActive(false);
            MulitShotState *= -1;
        }
    }


    private void Update()
    {
        if(mainCamera.GetComponent<GameManagement>().isClear == 1)
        {
            Time.timeScale = 0;
            WinCanvas.enabled = true;
            WinSound_ohh.Play();
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
            LoseSound_Bhoo.Play();
            LoseSound_Harp.Play();
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

            if (Item1 != null && Item1.activeSelf)
            {
                Debug.Log("골드 소모 체크");
                gameObject.GetComponent<GoldManagement>().UseGoldForGuideLine();
            }
            if (Item2 != null && Item2.activeSelf)
            {
                gameObject.GetComponent<GoldManagement>().UseGoldForMultiShot();
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
