using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI endGameScoreText;
    // Start is called before the first frame update
    void Start()
    {
        int finalScore = GetEndGameScore();
        UpdateEndGameScoreText(finalScore);
    }

    int GetEndGameScore()
    {
        return PlayerPrefs.GetInt("endGameScore");
    }

    void UpdateEndGameScoreText(int endGameScore)
    {
        endGameScoreText.text = "FINAL SCORE:" + endGameScore;
    }

}
