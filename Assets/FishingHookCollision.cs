using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FishingHookCollision : MonoBehaviour
{
    void Start()
    {
        CheckColliderSetup();
    }

    void Update()
    {
        // Check every frame to make sure collider still exists
        CheckColliderSetup();
    }

    void CheckColliderSetup()
    {
        // Check if this object has a collider
        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogError("FishingHook is missing a Collider2D component! Adding one now...");
            CircleCollider2D newCol = gameObject.AddComponent<CircleCollider2D>();
            newCol.isTrigger = true;
            newCol.radius = 0.3f;
        }
        else if (!col.isTrigger)
        {
            Debug.LogWarning("FishingHook collider is not set to Trigger! Fixing...");
            col.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger detected with: {other.gameObject.name}, Tag: {other.tag}");

        if (other.CompareTag("Fish"))
        {
            Debug.Log("✓ Fish tag confirmed! Attempting to catch fish...");

            // Kill all tweens on the fish
            DOTween.Kill(other.transform);
            Debug.Log("✓ DOTween killed");

            // Stop the fish's speed randomization coroutine
            Fish1_0 fishScript = other.GetComponent<Fish1_0>();
            if (fishScript != null)
            {
                fishScript.enabled = false;
                Debug.Log("✓ Fish script disabled");
            }
            else
            {
                Debug.LogWarning("Fish1_0 script not found on fish!");
            }

            // Make the fish a child of the hook
            other.transform.SetParent(transform);
            Debug.Log($"✓ Fish parented to hook. Parent is now: {other.transform.parent.name}");

            // Stop physics movement
            Rigidbody2D fishRb = other.GetComponent<Rigidbody2D>();
            if (fishRb != null)
            {
                fishRb.velocity = Vector2.zero;
                fishRb.isKinematic = true;
                Debug.Log("✓ Rigidbody2D made kinematic");
            }
            else
            {
                Debug.LogWarning("Rigidbody2D not found on fish!");
            }

            // Position the fish at the hook
            other.transform.localPosition = Vector3.zero;
            Debug.Log($"✓ Fish local position set to: {other.transform.localPosition}");

            // Disable the fish's collider
            other.enabled = false;
            Debug.Log("✓ Fish collider disabled");

            Debug.Log("=== FISH CAUGHT SUCCESSFULLY ===");
        }
        else
        {
            Debug.LogWarning($"Collided object does not have 'Fish' tag. Has tag: '{other.tag}'");
        }
    }

}