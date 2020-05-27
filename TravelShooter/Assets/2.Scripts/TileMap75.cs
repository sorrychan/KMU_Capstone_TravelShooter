using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap75 : MonoBehaviour
{
    public GameObject TempMap;

    private void Start()
    {
        LoadTemp();
    }

    public void OnPlay()
    {
        TileMap[] Tile = gameObject.GetComponentsInChildren<TileMap>();
        foreach (TileMap obj in Tile)
        {
            obj.SendMessage("OnPlay");
        }

        for (int i = 0; i < 35; i++)
        {
            TempMap.GetComponent<TempMap>().MapData[i] = transform.GetChild(i).GetComponent<TileMap>().TileData;
        }
    }


    public void LoadTemp()
    {
        for (int a = 0; a < 35; a++)
        {
            for (int i = 0; i < 4; i++)
            {
                transform.GetChild(a).GetChild(i).gameObject.SetActive(false);
            }
            transform.GetChild(a).GetComponent<TileMap>().TileData = TempMap.GetComponent<TempMap>().MapData[a];
            switch (TempMap.GetComponent<TempMap>().MapData[a])
            {
                case 0:
                    break;

                case 1:
                    transform.GetChild(a).GetChild(0).gameObject.SetActive(true);
                    break;

                case 2:
                    transform.GetChild(a).GetChild(1).gameObject.SetActive(true);
                    break;

                case 3:
                    transform.GetChild(a).GetChild(2).gameObject.SetActive(true);
                    break;

                case 4:
                    transform.GetChild(a).GetChild(3).gameObject.SetActive(true);
                    break;
            }
        }
    }
}
