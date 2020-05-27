using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMapMapData : MonoBehaviour
{
    public string Map;
    public string[] MapString;
    public int[] MapInt;
    public GameObject camera;
    public GameObject TileMap75;
    
    private void Start()
    {
        camera = GameObject.Find("Camera");
        TileMap75 = GameObject.Find("TileMap75");
        MapString = Map.Split('/');
        for (int i=0;i<MapString.Length;i++)
        {
            MapInt[i] =int.Parse(MapString[i]);
        }
    }
    
    public void LoadMap()
    {
        TileMap[] Tile = TileMap75.GetComponentsInChildren<TileMap>();
        foreach (TileMap obj in Tile)
        {
            obj.SendMessage("OnPlay");
        }

        camera.gameObject.SetActive(false);
        Camera.main.gameObject.SetActive(true);
        for (int a = 0; a < 35; a++)
        {
            for (int i = 0; i < 4; i++)
            {
                TileMap75.transform.GetChild(a).GetChild(i).gameObject.SetActive(false);
            }
            TileMap75.transform.GetChild(a).GetComponent<TileMap>().TileData = MapInt[a];
            switch (MapInt[a])
            {
                case 0:
                    break;

                case 1:
                    TileMap75.transform.GetChild(a).GetChild(0).gameObject.SetActive(true);
                    break;

                case 2:
                    TileMap75.transform.GetChild(a).GetChild(1).gameObject.SetActive(true);
                    break;

                case 3:
                    TileMap75.transform.GetChild(a).GetChild(2).gameObject.SetActive(true);
                    break;

                case 4:
                    TileMap75.transform.GetChild(a).GetChild(3).gameObject.SetActive(true);
                    break;
            }
        }
    }
}
