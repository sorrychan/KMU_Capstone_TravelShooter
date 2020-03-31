using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapC : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                hit.transform.gameObject.SendMessage("OnClicked");
            }
        }
    }
}
