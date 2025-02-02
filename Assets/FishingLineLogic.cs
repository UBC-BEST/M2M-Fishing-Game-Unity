using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLineLogic : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 topPoint;
    private Vector3 bottomPoint;

    public float dropSpeed = 0.5f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        // !!! find better way to get top and bottom instead of hardcoding
        topPoint = new Vector3(0, 2, 0); // top of fishing rod
        bottomPoint = new Vector3(0, 1, 0); // botttom of rod

        lineRenderer.SetPosition(0, topPoint);
        lineRenderer.SetPosition(1, bottomPoint);
    }

    void Update()
    {
        // include controls here to bring it up and down
        bottomPoint.y -= dropSpeed * Time.deltaTime;

        lineRenderer.SetPosition(0, topPoint);
        lineRenderer.SetPosition(1, bottomPoint);
    }
}
