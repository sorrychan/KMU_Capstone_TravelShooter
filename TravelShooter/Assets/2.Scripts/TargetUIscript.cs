using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetUIscript : MonoBehaviour
{
    [Range(15.0f, 75.0f)] public float FiringAngleSave = 15.0f;


    public GameObject AngleDisplay;

    // Start is called before the first frame update
    void Start()
    {
        AngleDisplay = GameObject.FindGameObjectWithTag("AngleDP");
    }

    // Update is called once per frame
    void Update()
    {
        AngleDisplay.GetComponent<Text>().text = "Angle : "+FiringAngleSave;

        if (FiringAngleSave > 75.0f)
            FiringAngleSave = 15.0f;
    }
}
