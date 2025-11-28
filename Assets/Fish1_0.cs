using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Fish1_0 : MonoBehaviour
{
    [Header("Movement Bounds (World X)")]
    public float leftLimitX = -5f;
    public float rightLimitX = 5f;

    [Header("Movement Settings")]
    [Tooltip("Seconds to travel from one bound to the other at full speed (timeScale = 1)")]
    public float moveDuration = 2f;
    public Ease easeType = Ease.Linear;

    [Header("Speed Randomization")]
    [Tooltip("Minimum timeScale (slower). 1 = full speed, minTimeScale = slowest.")]
    [Range(0.1f, 1f)]
    public float minTimeScale = 0.5f;
    [Tooltip("How often (seconds) to pick a new random speed")]
    public float speedChangeInterval = 2f;

    [Header("Depth & Scale")]
    public float depth = 0f;
    public Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);

    private bool movingRight = true;
    private Vector3 originalScale;
    private Tweener moveTween;
    private Coroutine speedRoutine;

    void Start()
    {
        // apply scale & record it for flipping
        transform.localScale = scale;
        originalScale = scale;

        // Use the current Y position as depth (set by FishManager)
        depth = transform.position.y;

        // lock in depth
        Vector3 p = transform.position;
        transform.position = new Vector3(p.x, depth, p.z);

        // Randomly decide initial direction
        movingRight = Random.value > 0.5f;

        // face initial direction
        Flip();

        // start swimming
        SwimToNextBoundary();

        // start randomizing speed
        speedRoutine = StartCoroutine(RandomizeSpeedRoutine());
    }

    void SwimToNextBoundary()
    {
        float targetX = movingRight ? rightLimitX : leftLimitX;

        // kill previous tween just in case
        if (moveTween != null) moveTween.Kill();

        moveTween = transform
            .DOMoveX(targetX, moveDuration)
            .SetEase(easeType)
            .SetAutoKill(false) // keep the tween alive so we can change timeScale
            .OnComplete(() =>
            {
                movingRight = !movingRight;
                Flip();
                SwimToNextBoundary();
            });
    }

    IEnumerator RandomizeSpeedRoutine()
    {
        while (true)
        {
            // pick a timeScale between min and 1
            float ts = Random.Range(minTimeScale, 1f);
            if (moveTween != null)
                moveTween.timeScale = ts;
            yield return new WaitForSeconds(speedChangeInterval);
        }
    }

    void Flip()
    {
        Vector3 s = originalScale;
        s.x = movingRight
            ? Mathf.Abs(originalScale.x)
            : -Mathf.Abs(originalScale.x);
        transform.localScale = s;
    }

    void OnDestroy()
    {
        // cleanup
        if (moveTween != null) moveTween.Kill();
        if (speedRoutine != null) StopCoroutine(speedRoutine);
    }
}