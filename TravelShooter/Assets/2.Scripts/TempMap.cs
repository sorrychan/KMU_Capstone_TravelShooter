using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMap : MonoBehaviour
{
    public int[] MapData = new int[63];

    private void Start()
    {
        for (int i = 0; i < 63; i++)
        {
            MapData[i] = 0;
        }
    }
}
