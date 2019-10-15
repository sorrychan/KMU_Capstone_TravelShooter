using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraMovement : MonoBehaviour
{
    public bool IsMoveOn = false;

    private Vector3 TargetPos;
    private Transform mytr;
    // Start is called before the first frame update
    void Start()
    {
        mytr = gameObject.transform;
        TargetPos = new Vector3(mytr.position.x, mytr.position.y, (mytr.position.z - 20));
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(IsMoveOn)
            mytr.position = Vector3.Lerp(mytr.position, TargetPos, Time.deltaTime * 2f);

        
    }
}
