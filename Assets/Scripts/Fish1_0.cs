using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fish1_0 : MonoBehaviour
{
    private Tweener moveTween; 
    private bool movingRight = true;
    private Vector3 originalScale;
    public float moveDuration = 2f; // Changes speed

    public float depth = 0f; // Changes height

    public float leftRange = 2f; // Changes how far left travel is

    public float rightRange = 5f; // Changes how far right travel is
    
    public Vector3 scale = new Vector3(2f,2f,2f);

    void Start()
    {
        transform.localScale = scale;
        originalScale = transform.localScale; 
        transform.position = new Vector3(0f, depth, 0f);
        
        MoveAndFlip();
    }

 void MoveAndFlip()
    {
        float moveDistance = Random.Range(leftRange, rightRange); 
        Vector3 rightPoint = new Vector3(moveDistance, depth, 0f);
        Vector3 leftPoint = new Vector3(-moveDistance, depth, 0f); 

        moveTween = transform.DOMove(rightPoint, moveDuration) 
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .OnStepComplete(() => {
                movingRight = !movingRight;
                Flip();

                moveDistance = Random.Range(leftRange, rightRange); 
                rightPoint = new Vector3(moveDistance, depth, 0f);
                leftPoint = new Vector3(-moveDistance, depth, 0f);

                moveTween.ChangeEndValue(movingRight ? rightPoint : leftPoint, true); 
            });
    }
    void Flip()
    {
        transform.localScale = new Vector3(movingRight ? originalScale.x : -originalScale.x, originalScale.y, originalScale.z);
    }
}
