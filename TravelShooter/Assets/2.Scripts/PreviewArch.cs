using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PreviewArch : MonoBehaviour
{

    private int state = -1;

    [SerializeField]
    private GameObject pauseOptionScript;

    public float predictionSeconds = 4f;
    public int subdivisionCount = 5;

   // int state = -1;

    LineRenderer _line;
    Vector3[] _points;

    void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = subdivisionCount;
        //_line.SetVertexCount(subdivisionCount);
        _points = new Vector3[subdivisionCount];

        pauseOptionScript = GameObject.Find("GameManager");
        
    }

     public void Preview(Vector3 startPosition, Vector3 initialVelocity)
    {
        state = pauseOptionScript.GetComponent<PauseOption>().PreviewLineState;
        switch (state)
        {
            case -1:
                predictionSeconds = 0.1f;
                break;
            case 1:
                predictionSeconds = 1f;
                break;
        }


        float timeStep = predictionSeconds / _points.Length;
        for (int i = 0; i < _points.Length; i++)
        {
            float t = i * timeStep;

            // Standard ballistic motion:
            Vector3 point = startPosition
                          + t * initialVelocity
                          + 0.5f * t * t * (Vector3)(Physics.gravity);

            _points[i] = point;
        }

        _line.SetPositions(_points);
    }
}
