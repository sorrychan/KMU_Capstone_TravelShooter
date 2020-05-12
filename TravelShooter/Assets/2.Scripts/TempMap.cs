using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMap : MonoBehaviour
{
    public int[] MapData = new int[35];

    private void Start()
    {
        for (int i = 0; i < 35; i++)
        {
            MapData[i] = 0;
        }
    }
}
