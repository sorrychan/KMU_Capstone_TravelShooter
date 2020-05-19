using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageMove : MonoBehaviour
{
    public GameObject Stage;
    public int CurrentStage = 0;
    public Text Name;

    public void NextStage()
    {
        for(int i=0; i< Stage.transform.childCount;i++)
        {
            Stage.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (CurrentStage < Stage.transform.childCount-1)
        {
            CurrentStage++;
        }
        Stage.transform.GetChild(CurrentStage).gameObject.SetActive(true);
        StageName();
    }

    public void PreviousStage()
    {
        for (int i = 0; i < Stage.transform.childCount; i++)
        {
            Stage.transform.GetChild(i).gameObject.SetActive(false);
        }
        if (CurrentStage >0)
        {
            CurrentStage--;
        }
        Stage.transform.GetChild(CurrentStage).gameObject.SetActive(true);
        StageName();
    }

    public void StageName()
    {
        switch (CurrentStage)
        {
            case 0:
                Name.text = "STAGE 1 : Monsters";
                break;
            case 1:
                Name.text = "STAGE 2 : Monsters";
                break;
            case 2:
                Name.text = "STAGE 3 : Monsters";
                break;
        }
    }
}
