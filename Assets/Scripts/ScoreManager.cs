using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public float currentScore;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        UpdateScoreTextWithCurrentScore();
    }
    public void AddToScore(float scoreToAdd)
    {
        currentScore += scoreToAdd;
        UpdateScoreTextWithCurrentScore();
    }

    public float GetScoreFromAccuracy(float accuracyPercentage)
    {
        float scoreFromAccuracy = accuracyPercentage * 10;

        return scoreFromAccuracy;
    }

    void UpdateScoreTextWithCurrentScore()
    {
        int currentScoreInt = (int)currentScore;
        scoreText.text = "SCORE: " + currentScoreInt;
    }
}
