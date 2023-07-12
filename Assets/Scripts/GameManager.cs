using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Transform fluidLevelObjectTransform;
    public FluidLevelHandler fluidLevelHandler;
    public ColorIncrementButton redColorButtonValues;
    public ColorIncrementButton greenColorButtonValues;
    public ColorIncrementButton blueColorButtonValues;
    public SpriteRenderer targetRGBDisplay;
    public UIManager uiManager;
    public ScoreManager scoreManager;
    public SoundManager soundManager;
    private Color targetRGBValue;
    private float minColorIncrementRate = 0.005f;
    private float maxColorIncrementRate = 0.05f;
    private int numberOfWarnings;
    private int endGameOnNumberOfWarnings = 3;


    private void Start()
    {
        SetNewTargetRGB();
    }
    void SetNewTargetRGB()
    {
        uiManager.UpdateRequestText();
        float randomRedValue = getNewRedTargetValue();
        float randomGreenValue = getNewGreenTargetValue();
        float randomBlueValue = getNewBlueTargetValue();

        targetRGBValue.r = randomRedValue;
        targetRGBValue.g = randomGreenValue;
        targetRGBValue.b = randomBlueValue;
        targetRGBValue.a = 1;

        targetRGBDisplay.color = targetRGBValue;
    }

    float getNewRedTargetValue()
    {
        float redMinValue = redColorButtonValues.GetColorIncrementRate0To255();
        return Random.Range(redMinValue, 1);
    }
    float getNewGreenTargetValue()
    {
        float greenMinValue = greenColorButtonValues.GetColorIncrementRate0To255();
        return Random.Range(greenMinValue, 1);
    }
    float getNewBlueTargetValue()
    {
        float blueMinValue = blueColorButtonValues.GetColorIncrementRate0To255();
        return Random.Range(blueMinValue, 1);
    }

    public void adjustFillRateForVariance(float currentIncrementRate)
    {
        float currentVariance = CalculateColorVariance();
        float clampedVariance = Mathf.Clamp(currentVariance, 0f, 1f);

        float adjustedIncrementRate = (currentIncrementRate * clampedVariance) + minColorIncrementRate;
        float clampedAdustedIncrementRate = Mathf.Clamp(adjustedIncrementRate, minColorIncrementRate, maxColorIncrementRate);

        redColorButtonValues.SetColorIncrementRate(clampedAdustedIncrementRate);
        greenColorButtonValues.SetColorIncrementRate(clampedAdustedIncrementRate);
        blueColorButtonValues.SetColorIncrementRate(clampedAdustedIncrementRate);
    }

    public void FillLineHit()
    {
        float finalAccuracy = CalculateAccuracyPercentage();
        Debug.Log("Accuracy:" + finalAccuracy);
        SetNewTargetRGB();
        fluidLevelHandler.ResetFluidLevelPosition();
        fluidLevelHandler.ResetFluidLevelColor();
        uiManager.showAccuracyFeedback(finalAccuracy);
        float scoreFromAccuracy = scoreManager.GetScoreFromAccuracy(finalAccuracy);
        scoreManager.AddToScore(scoreFromAccuracy);
        if (finalAccuracy <= 70f)
        {
            AddWarning();
        }
    }

    float CalculateColorVariance()
    {
        Color currentColor = fluidLevelHandler.GetCurrentColor();
        float colorVariance = Mathf.Sqrt(Mathf.Pow(currentColor.r - targetRGBValue.r, 2) + Mathf.Pow(currentColor.g - targetRGBValue.g, 2) + Mathf.Pow(currentColor.b - targetRGBValue.b, 2));

        return colorVariance;
    }

    float CalculateAccuracyPercentage()
    {
        // The maximum variance is root 3 since the max value for each color channel is 1
        float colorVariance = CalculateColorVariance();
        float variancePercentage = (colorVariance / Mathf.Sqrt(3)) * 100;
        float accuracyPercentage = 100f - variancePercentage;
        return accuracyPercentage;
    }

    void AddWarning()
    {
        ++numberOfWarnings;
        uiManager.DisplayWarningIcons(numberOfWarnings);

        if (numberOfWarnings == endGameOnNumberOfWarnings)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        scoreManager.AddEndGameScoreToPlayerPrefs();
        SceneManager.LoadScene(sceneName: "Leaderboard");
    }
}
