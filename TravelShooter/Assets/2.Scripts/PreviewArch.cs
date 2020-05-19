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
    [Tooltip("The percent of the line that is consumed by the arrowhead")]
    [Range(0, 1)]
    public float PercentHead = 0.4f;
    public Vector3 ArrowOrigin;
    public Vector3 ArrowTarget;

    LineRenderer _line;
    Vector3[] _points;

    public Material LineMaterial;

    void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = subdivisionCount;
        //_line.SetVertexCount(subdivisionCount);
        _points = new Vector3[subdivisionCount];


    }

    void Start()
    {

       // UpdateArrow();
    }
    private void OnValidate()
    {

          // UpdateArrow();
    }
    [ContextMenu("UpdateArrow")]
    void UpdateArrow()
    {

            if (_line == null)
                _line = this.GetComponent<LineRenderer>();
            _line.widthCurve = new AnimationCurve(
                new Keyframe(0, 0.4f)
                , new Keyframe(0.999f - PercentHead, 0.4f)  // neck of arrow
                , new Keyframe(1 - PercentHead, 1f)  // max width of arrow head
                , new Keyframe(1, 0f));  // tip of arrow
            _line.SetPositions(new Vector3[] {
              ArrowOrigin
              , Vector3.Lerp(ArrowOrigin, ArrowTarget, 0.999f - PercentHead)
              , Vector3.Lerp(ArrowOrigin, ArrowTarget, 1 - PercentHead)
              , ArrowTarget });
        
    }


    public void Preview(Vector3 startPosition, Vector3 initialVelocity)
    {
        state = pauseOptionScript.GetComponent<PauseOption>().PreviewLineState;
        //Debug.Log("State : " + state);
        switch (state)
        {
            case -1:
                predictionSeconds = 0.2f;
                _line.startWidth = 1.0f;
                _line.textureMode = LineTextureMode.Stretch;
                UpdateArrow();
                break;
            case 1:
                predictionSeconds = 0.8f;
                _line.material = LineMaterial;
                _line.textureMode = LineTextureMode.Tile;
                _line.startWidth = 1.0f;
                //IsGuideLineActive = true;
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
