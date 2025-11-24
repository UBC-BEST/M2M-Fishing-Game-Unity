using UnityEngine;

public class FloatingGarbage : MonoBehaviour
{
    [Tooltip("Vertical bob range (units).")]
    public float amplitude = 0.5f;
    [Tooltip("Oscillations per second.")]
    public float frequency = 0.5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float yOffset = Mathf.Sin(Time.time * frequency * 2f * Mathf.PI) * amplitude;
        transform.position = startPos + Vector3.up * yOffset;
    }
}
