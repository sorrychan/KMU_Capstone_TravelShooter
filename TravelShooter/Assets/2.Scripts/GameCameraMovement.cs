using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraMovement : MonoBehaviour
{
    public bool IsMoveOn = false;
    public bool IsSizeWiden = false;

    public float duration = 2.0f;

    private Vector3 TargetPos;
    private Transform mytr;

    private float firstSize = 16f;
    private float SecondSize = 25f;

    private float elapsed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        mytr = gameObject.transform;
        TargetPos = new Vector3(mytr.position.x, mytr.position.y, (mytr.position.z - 20));
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(IsMoveOn)
            mytr.position = Vector3.Lerp(mytr.position, TargetPos, Time.deltaTime);
        if (IsSizeWiden)
        {
            elapsed += Time.deltaTime/ duration;
            
            Camera.main.orthographicSize = Mathf.Lerp(firstSize, SecondSize, elapsed);
        }
    }

}
