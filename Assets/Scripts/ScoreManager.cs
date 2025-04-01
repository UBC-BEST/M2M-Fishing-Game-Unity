using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance
    public TextMeshProUGUI scoreText; // The UI text element
    private int score = 0; // The score variable



    void Start()
    {
        scoreText.text = "Points: " + score;
    }

    void Awake()
    {
        // Ensure there is only one instance of ScoreManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(int points)
    {
        score += points; // Increase the score
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Points: " + score;
            Debug.Log("fish has been captured");
        }
    }
}
