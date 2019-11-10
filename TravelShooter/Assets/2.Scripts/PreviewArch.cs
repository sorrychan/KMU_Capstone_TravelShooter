using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PreviewArch : MonoBehaviour
{

    public float predictionSeconds = 4f;
    public int subdivisionCount = 5;

    LineRenderer _line;
    Vector3[] _points;

    void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.SetVertexCount(subdivisionCount);
        _points = new Vector3[subdivisionCount];
    }

    public void Preview(Vector3 startPosition, Vector3 initialVelocity)
    {
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
