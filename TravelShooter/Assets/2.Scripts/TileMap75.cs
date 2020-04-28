using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap75 : MonoBehaviour
{
    private void Awake()
    {
    }

    public void OnPlay()
    {
        TileMap[] Tile = gameObject.GetComponentsInChildren<TileMap>();
        foreach (TileMap obj in Tile)
        {
            obj.SendMessage("OnPlay");
        }
    }
}
