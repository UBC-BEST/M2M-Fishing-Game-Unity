using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FishingHookCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            print("WORKING");

            // not sure if killing like this will cause problems?
            DOTween.Kill(other.transform);

            other.transform.SetParent(transform);

            Rigidbody2D fishRb = other.GetComponent<Rigidbody2D>();
            if (fishRb != null)
            {
                fishRb.velocity = Vector2.zero;
                fishRb.isKinematic = true;
            }

            // aligns the fish with fishing hook
            other.transform.localPosition = Vector3.zero;
        }
    }
}
