using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDatabase : MonoBehaviour
{
    public string inputMapName;
    public string inputEditor;
    public string ImplodeMapData;

    public Text MapName;
    public Text Editor;

    public int[] MapData = new int[77];
    public GameObject Tile75;

    public GameObject TempMap;

    string SaveMapURL = "http://gn0317.dothome.co.kr/index.php";
    

    public void SaveMap()
    {
        WWWForm form = new WWWForm();

        ImplodeMapData = null;
        /*
        foreach (int element in MapData)
        {
            MapData[element] = Tile75.transform.GetChild(element).GetComponent<TileMap>().TileData;
            ImplodeMapData = ImplodeMapData + MapData[element] + "/";
        }*/


        for (int i = 0; i < 77; i++)
        {
            TempMap.GetComponent<TempMap>().MapData[i] = Tile75.transform.GetChild(i).GetComponent<TileMap>().TileData;
        }


        for (int i=0;i<77;i++)
        {
            MapData[i] = Tile75.transform.GetChild(i).GetComponent<TileMap>().TileData;
            ImplodeMapData = ImplodeMapData + MapData[i] + "/";
        }
        ImplodeMapData = ImplodeMapData.Substring(0, ImplodeMapData.Length - 1);

        inputMapName = MapName.text;
        inputEditor = Editor.text;

        form.AddField("MapNamePost", inputMapName);
        form.AddField("EditorPost", inputEditor);
        form.AddField("MapdataPost", ImplodeMapData);

        WWW www = new WWW(SaveMapURL, form);
    }


}
