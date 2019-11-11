using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetUIscript : MonoBehaviour
{
    [Range(15.0f, 75.0f)] public float FiringAngleSave = 15.0f;
    [Range(30f, 50f)] public float FiringPowerSave = 30f;

    //public GameObject AngleDisplay;
    public GameObject PowerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        PowerDisplay = GameObject.FindGameObjectWithTag("AngleDP");
    }

    // Update is called once per frame
    void Update()
    {
        PowerDisplay.GetComponent<Text>().text = Mathf.CeilToInt(FiringPowerSave).ToString();
       


        if (FiringPowerSave > 50f)
            FiringPowerSave = 30f;
    }
}
