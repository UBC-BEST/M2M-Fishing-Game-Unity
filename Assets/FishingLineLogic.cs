using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

        // check if hook reached top
        if (fishingHook.position.y >= topPoint.y)
        {
            // go through sub-components of FishingHook
            for (int i = fishingHook.childCount - 1; i >= 0; i--)
            {
                Transform child = fishingHook.GetChild(i);
                // check if fish is attached to hook
                if (child.CompareTag("Fish"))
                {
                    // Determine points based on fish name
                    int points = GetFishValue(child.name);

                    Destroy(child.gameObject);
                    Debug.Log($"Fish REACHED TOP! Worth {points} points!");

                    // ADD EVENT TO UPDATE SCORE
                    if (ScoreManager.Instance != null)
                    {
                        ScoreManager.Instance.AddPoints(points);
                    }
                }
            }
        }
    }

    private void UpdateLine()
    {
        lineRenderer.SetPosition(0, topPoint);
        lineRenderer.SetPosition(1, bottomPoint);
    }

    // Determine fish value based on name
    // Fish 1 = most valuable, Fish 4 = least valuable
    private int GetFishValue(string fishName)
    {
        // Remove "(Clone)" from the name if it exists
        fishName = fishName.Replace("(Clone)", "").Trim();

        if (fishName.Contains("Fish 1") || fishName.Contains("fish 1"))
        {
            return 50; // Fish 1 = 50 points (most valuable)
        }
        else if (fishName.Contains("Fish 2") || fishName.Contains("fish 2"))
        {
            return 30; // Fish 2 = 30 points
        }
        else if (fishName.Contains("Fish 3") || fishName.Contains("fish 3"))
        {
            return 20; // Fish 3 = 20 points
        }
        else if (fishName.Contains("Fish 4") || fishName.Contains("fish 4"))
        {
            return 10; // Fish 4 = 10 points (least valuable)
        }
        else
        {
            // Default value if fish name doesn't match
            Debug.LogWarning($"Unknown fish name: {fishName}. Giving 10 points.");
            return 10;
        }
    }
}