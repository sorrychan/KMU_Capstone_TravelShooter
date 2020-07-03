using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDatabase : MonoBehaviour
{
    public string inputMapName;
    public string inputEditor;
    public string ImplodeMapData;
    public int[] MapData = new int[63];
    public GameObject Tile75;
    
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

        for(int i=0;i<63;i++)
        {
            MapData[i] = Tile75.transform.GetChild(i).GetComponent<TileMap>().TileData;
            ImplodeMapData = ImplodeMapData + MapData[i] + "/";
        }
        ImplodeMapData = ImplodeMapData.Substring(0, ImplodeMapData.Length - 1);
        
        form.AddField("MapNamePost", inputMapName);
        form.AddField("EditorPost", inputEditor);
        form.AddField("MapdataPost", ImplodeMapData);

        WWW www = new WWW(SaveMapURL, form);
    }


}
