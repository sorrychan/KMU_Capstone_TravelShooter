using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TileMapEditorMove : MonoBehaviour
{
    public GameObject TempMap;
    public GameObject Tile75;

    public void MoveToMain()
    {
        for (int i = 0; i < 77; i++)
        {
            TempMap.GetComponent<TempMap>().MapData[i] = Tile75.transform.GetChild(i).GetComponent<TileMap>().TileData;
        }

        SceneManager.LoadScene("Scene01");

    }

    public void MoveToUserMap()
    {
        for (int i = 0; i < 77; i++)
        {
            TempMap.GetComponent<TempMap>().MapData[i] = Tile75.transform.GetChild(i).GetComponent<TileMap>().TileData;
        }
        SceneManager.LoadScene("UserMap");
    }
}
