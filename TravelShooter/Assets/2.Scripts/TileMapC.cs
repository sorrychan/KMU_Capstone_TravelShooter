using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TileMapC : MonoBehaviour
{
    public Camera camera;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null && hit.transform.gameObject.tag == "Tile")
            {
                hit.transform.gameObject.SendMessage("OnClicked");
            }
        }
    }

    public void MoveToMain()
    {
        SceneManager.LoadScene("Scene01");
    }
}
