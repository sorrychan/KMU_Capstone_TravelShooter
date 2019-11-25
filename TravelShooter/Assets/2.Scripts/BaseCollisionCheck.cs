using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionCheck : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;


    public void OnTriggerEnter(UnityEngine.Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            //mainCamera.GetComponent<GameManagement>().isFailed = 1;
            Camera.main.GetComponent<GameManagement>().isFailed = 1;
        }
    }
}
