using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    //public GameObject Choose;
    public enum Tile { none ,enemy, tree, boom, bullet, wall};    //추가 시 TileMap75, UserMapMapData 스크립트도 함께 수정
    public static int EnumTile;     //버튼에 선택된 오브젝트
    public int TileData=0;          //이 타일의 값

    public Tile tile = Tile.none;

    private void Awake()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void setNone()
    {
        //tile = Tile.none;
        EnumTile = 0;
    }

    public void setEnemy()
    {
       // tile = Tile.enemy;
        EnumTile = 1;
    }

    public void setTree()
    {
       // tile = Tile.tree;
        EnumTile = 2;
    }

    public void setBoom()
    {
        //tile = Tile.boom;
        EnumTile = 3;
    }

    public void setBullet()
    {
        //tile = Tile.bullet;
        EnumTile = 4;
    }

    public void setWall()
    {
        //tile = Tile.bullet;
        EnumTile = 5;
    }

    public void OnClicked()
    {
        //tile = Choose.gameObject.GetComponent<TileMap>().tile;
        /*Transform[] transforms = gameObject.GetComponentsInChildren<Transform>();
        foreach(UnityEngine.Transform obj in transforms)
        {
            obj.gameObject.SetActive(false);
        }*/
        for (int i = 0; i < 5; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
        TileData = EnumTile;
        switch (EnumTile)
        {
            case 0:
                break;

            case 1:
                transform.GetChild(0).gameObject.SetActive(true);
                break;

            case 2:
                transform.GetChild(1).gameObject.SetActive(true);
                break;

            case 3:
                transform.GetChild(2).gameObject.SetActive(true);
                break;

            case 4:
                transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 5:
                transform.GetChild(4).gameObject.SetActive(true);
                break;
        }
    }

    public void OnPlay()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        transform.Find("Cube").gameObject.SetActive(false);
    }
}
