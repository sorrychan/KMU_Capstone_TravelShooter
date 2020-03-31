using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public GameObject Choose;
    public enum Tile { none ,enemy, tree, boom, bullet};

    public Tile tile = Tile.none;


    public void setNone()
    {
        tile = Tile.none;
    }

    public void setEnemy()
    {
        tile = Tile.enemy;
    }

    public void setTree()
    {
        tile = Tile.tree;
    }

    public void setBoom()
    {
        tile = Tile.boom;
    }

    public void setBullet()
    {
        tile = Tile.bullet;
    }

    public void OnClicked()
    {
        tile = Choose.gameObject.GetComponent<TileMap>().tile;
        /*Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
        foreach(UnityEngine.Transform obj in transforms)
        {
            obj.gameObject.SetActive(false);
        }*/
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
        switch (tile)
        {
            case Tile.none:
                break;

            case Tile.enemy:
                transform.GetChild(0).gameObject.SetActive(true);
                break;

            case Tile.tree:
                transform.GetChild(1).gameObject.SetActive(true);
                break;

            case Tile.boom:
                transform.GetChild(2).gameObject.SetActive(true);
                break;

            case Tile.bullet:
                transform.GetChild(3).gameObject.SetActive(true);
                break;
        }
    }
}
