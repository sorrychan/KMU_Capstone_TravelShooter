using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Function : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject ClusterBullet;
    public GameObject GamePlayCamera;
    public GameObject GameManager;

    public Text FunctionText;

    public static int F = 0;


    public GameObject EnemyText;
    public GameObject BulletText;
    public GameObject ClusterBulletText;
    public GameObject RespawnBulletText;
    public GameObject TreeText;
    public GameObject WallText;
    public GameObject BoomText;
    public GameObject FireText;

    public GameObject EnemyF;
    public GameObject BulletF;
    public GameObject ClusterBulletF;
    public GameObject RespawnBulletF;
    public GameObject TreeF;
    public GameObject WallF;
    public GameObject Boomf;
    public GameObject Firef;

    public void Awake()
    {
        switch (F)
        {
            case 0:
                EnemyF.SetActive(true);
                GamePlayCamera.GetComponent<GameManagement>().IsStart = 1;
                Enemy.GetComponent<AI>().enemyState = AI.EnemyState.trace;
                FunctionText.text = "적";
                break;

            case 1:
                GameManager.GetComponent<PauseOption>().MulitShotState = -1;
                BulletF.SetActive(true);
                FunctionText.text = "총알";
                break;

            case 2:
                ClusterBulletF.SetActive(true);
                GameManager.GetComponent<PauseOption>().MulitShotState = 1;
                FunctionText.text = "확산탄";
                break;

            case 3:
                RespawnBulletF.SetActive(true);
                GameManager.GetComponent<PauseOption>().MulitShotState = -1;
                FunctionText.text = "리스폰 총알";
                break;

            case 4:
                TreeF.SetActive(true);
                FunctionText.text = "나무";
                break;

            case 5:
                WallF.SetActive(true);
                FunctionText.text = "벽";
                break;

            case 6:
                Boomf.SetActive(true);
                FunctionText.text = "폭탄";
                break;

            case 7:
                Firef.SetActive(true);
                FunctionText.text = "불";
                break;
        }
    }

    public void OnClicked()
    {
        switch(F)
        {
            case 0:
                EnemyFunction();
                break;

            case 1:
                BulletFunction();
                break;

            case 2:
                ClusterBulletFunction();
                break;

            case 3:
                RespawnBulletFunction();
                break;

            case 4:
                TreeFunction();
                break;

            case 5:
                WallFunction();
                break;

            case 6:
                BoomFunction();
                break;

            case 7:
                FireFunction();
                break;
        }
    }

    public void OnClickedNext()
    {
        if (F < 7)
        {
            F++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void OnClickedPrevious()
    {
        if (F > 0)
        {
            F--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void EnemyFunction()
    {
        if(EnemyText.gameObject.activeSelf)
            EnemyText.gameObject.SetActive(false);
        else
            EnemyText.gameObject.SetActive(true);
    }

    void BulletFunction()
    {
        if (BulletText.gameObject.activeSelf)
            BulletText.gameObject.SetActive(false);
        else
            BulletText.gameObject.SetActive(true);
    }

    void ClusterBulletFunction()
    {
        if (ClusterBulletText.gameObject.activeSelf)
            ClusterBulletText.gameObject.SetActive(false);
        else
            ClusterBulletText.gameObject.SetActive(true);
    }

    void RespawnBulletFunction()
    {
        if (RespawnBulletText.gameObject.activeSelf)
            RespawnBulletText.gameObject.SetActive(false);
        else
            RespawnBulletText.gameObject.SetActive(true);
    }

    void TreeFunction()
    {
        if (TreeText.gameObject.activeSelf)
            TreeText.gameObject.SetActive(false);
        else
            TreeText.gameObject.SetActive(true);
    }

    void WallFunction()
    {
        if (WallText.gameObject.activeSelf)
            WallText.gameObject.SetActive(false);
        else
            WallText.gameObject.SetActive(true);
    }

    void BoomFunction()
    {
        if (BoomText.gameObject.activeSelf)
            BoomText.gameObject.SetActive(false);
        else
            BoomText.gameObject.SetActive(true);
    }

    void FireFunction()
    {
        if (FireText.gameObject.activeSelf)
            FireText.gameObject.SetActive(false);
        else
            FireText.gameObject.SetActive(true);
    }
}
