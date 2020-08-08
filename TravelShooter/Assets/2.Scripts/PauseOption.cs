using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshPro Gold;
    public Canvas ContinueCanvas;
    public TextMeshPro T1;
    public TextMeshPro T2;

    public GameObject Item1;
    public GameObject Item2;

    public GameObject Bullet;
    public GameObject ContinuePosition;

    //아이템 관리
    public int PreviewLineState = -1;

    public int MulitShotState = -1;

    public Text CountDown;
    public int TimeCost = 10;

    private IEnumerator CountDownC;
    private IEnumerator BulletCheckC;

    public GameObject[] Bullets;
    public GameObject[] Enemys;
    public bool IsContinued = false;
    //public Text ContinueText;

    // Start is called before the first frame update
    void Start()
    {
        CountDownC = CountDownCoroutine();
        BulletCheckC = BulletCheckCoroutine();

        Bullets = GameObject.FindGameObjectsWithTag("Bullet");
        StartCoroutine(BulletCheckC);

        //ContinueText.enabled = false;
        if (T2 != null)
            T2.gameObject.SetActive(false);
        Time.timeScale = 1;
        InfoCanvas.enabled = true;
        if (Gold != null)
            Gold.gameObject.SetActive(true);
        mainCanvas.enabled = true;
        stopCanvas.enabled = false;
        WinCanvas.enabled = false;
        LoseCanvas.enabled = false;
        ContinueCanvas.enabled = false;
        if (T1 != null)
            T1.gameObject.SetActive(false);
        //T2.gameObject.SetActive(false);

        PreviewLineState = -1;

        //Debug.Log(BGMClass.instance.name);
        if (BGMClass.instance != null)
        {
            if (BGMClass.instance.GetComponent<AudioSource>().isPlaying)
            {
                return;
            }
            else
                BGMClass.instance.GetComponent<AudioSource>().Play();
        }
        
    }

    private void OnEnable()
    {
        CountDownC = CountDownCoroutine();
        BulletCheckC = BulletCheckCoroutine();

        Bullets = GameObject.FindGameObjectsWithTag("Bullet");
        StartCoroutine(BulletCheckC);

        // ContinueText.enabled = false;
        if(T2!=null)
            T2.gameObject.SetActive(false);
        Time.timeScale = 1;
        InfoCanvas.enabled = true;
        if (Gold != null)
            Gold.gameObject.SetActive(true);
        mainCanvas.enabled = true;
        stopCanvas.enabled = false;
        WinCanvas.enabled = false;
        LoseCanvas.enabled = false;
        ContinueCanvas.enabled = false;
        if (T1 != null)
            T1.gameObject.SetActive(false);
        //T2.gameObject.SetActive(false);

        PreviewLineState = -1;

        //Debug.Log(BGMClass.instance.name);
        if (BGMClass.instance != null)
        {
            if (BGMClass.instance.GetComponent<AudioSource>().isPlaying)
            {
                return;
            }
            else
                BGMClass.instance.GetComponent<AudioSource>().Play();
        }
    }

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
            if (Gold != null)
                Gold.gameObject.SetActive(false);

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

    public void Continue()
    {
        Time.timeScale = 0;
        TimeCost = 10;
        StartCoroutine(CountDownC);
    }

    public void ContinueClicked()
    {
        if(gameObject.GetComponent<GoldManagement>().UseGoldForContinue())
        {
            Instantiate(Bullet,ContinuePosition.transform);
            Time.timeScale = 1;
            IsContinued = true;
            ContinueCanvas.enabled = false;
            if (T1 != null)
                T1.gameObject.SetActive(false);
            if (T2 != null)
                T2.gameObject.SetActive(false);
        }
        else
        {
            //ContinueText.enabled = true;
            if (T2 != null)
                T2.gameObject.SetActive(true);
        }
    }

    public void ExitContinueButton()
    {
        Time.timeScale = 1;
        ContinueCanvas.enabled = false;
        if (T1 != null)
            T1.gameObject.SetActive(false);
        if (T2 != null)
            T2.gameObject.SetActive(false);
        StopCoroutine(CountDownC);
    }

    public IEnumerator CountDownCoroutine()
    {
        while (true)
        {
            //Debug.Log("CountDownCoroutine()");
            IsContinued = true;
            if (TimeCost > 0)
            {
                TimeCost -= 1;
                CountDown.text = TimeCost.ToString();
            }
            else
            {
                Time.timeScale = 1;
                ContinueCanvas.enabled = false;
                if (T1 != null)
                    T1.gameObject.SetActive(false);
                if (T2 != null)
                    T2.gameObject.SetActive(false);
                StopCoroutine(CountDownC);
            }
            //yield return new WaitForSeconds(1);
            yield return new WaitForSecondsRealtime(1f);
        }
    }

    public IEnumerator BulletCheckCoroutine()
    {
        while (true)
        {
            //Debug.Log("BulletCheck()");
            Bullets = GameObject.FindGameObjectsWithTag("Bullet");
            Enemys = GameObject.FindGameObjectsWithTag("Enemy");
            if (Bullets.Length <= 0 && IsContinued == false&& Enemys.Length>0)
            {
                ContinueCanvas.enabled = true;
                if (T1 != null)
                    T1.gameObject.SetActive(true);
                //T2.gameObject.SetActive(true);
                Continue();
                StopCoroutine(BulletCheckC);
            }
            yield return new WaitForSecondsRealtime(1);
        }
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
