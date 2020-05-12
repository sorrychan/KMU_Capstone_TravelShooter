using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserMapPanel : MonoBehaviour
{
    public string[] items;
    public string[] Elements;
    public GameObject UserMap;
    public GameObject UM;

    IEnumerator Start()
    {
        WWW itemsData = new WWW("http://gn0317.dothome.co.kr/read.php");
        yield return itemsData;
        string itemDataString = itemsData.text;
        items = itemDataString.Split(';');

        for (int i = 0; i < items.Length-1; i++)
        {
            UM = Instantiate(UserMap);
            UM.transform.SetParent(this.transform, false);
            UM.GetComponent<RectTransform>().position = transform.GetComponent<RectTransform>().position - new Vector3(0,30 + 200*i,0);
            Elements = items[i].Split('|');

            UM.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Elements[0];
            UM.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = Elements[1];
            UM.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = Elements[2];
            UM.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = Elements[3];
            UM.GetComponent<UserMapMapData>().Map = Elements[4];
            //Map = Elements[4].Split('/');

            /*
            for (int a = 0; a < 35; i++)
            {
                UM.GetComponent<PanelData>().MapData[a] = int.Parse(Map[a]);
            }
            */
        }
    }
    

}
