using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMapButton : MonoBehaviour
{
    public GameObject Canvas;

    private void Start()
    {
        Canvas = GameObject.Find("Canvas");
    }

    public void OnLoadMap()
    {
        gameObject.transform.parent.SendMessage("LoadMap");
        Canvas.SetActive(false);
    }
}
