using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLineLogic : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 topPoint;
    private Vector3 bottomPoint;

    public float moveSpeed = 2f;
    public float maxDepth = 5f;

    public Transform fishingHook;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        topPoint = transform.position;
        bottomPoint = topPoint;

        UpdateLine();

        fishingHook.position = bottomPoint;
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        bottomPoint += new Vector3(0, verticalInput * moveSpeed * Time.deltaTime, 0);
        bottomPoint.y = Mathf.Clamp(bottomPoint.y, topPoint.y - maxDepth, topPoint.y);

        UpdateLine();

        fishingHook.position = bottomPoint;
    }

    private void UpdateLine()
    {
        lineRenderer.SetPosition(0, topPoint);
        lineRenderer.SetPosition(1, bottomPoint);
    }
}