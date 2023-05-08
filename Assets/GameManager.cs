using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform fluidLevelObjectTransform;
    public SpriteRenderer fluidLevelObjectSpriteRenderer;
    public ColorIncrementButton redColorButtonValues;
    public ColorIncrementButton greenColorButtonValues;
    public ColorIncrementButton blueColorButtonValues;
    public SpriteRenderer targetRGBDisplay;
    private Color targetRGBValue;
    private float minColorIncrementRate = 0.005f;
    private float maxColorIncrementRate = 0.05f;


    private void Start()
    {
        SetNewTargetRGB();
    }
    void SetNewTargetRGB()
    {
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
        Color currentColor = fluidLevelObjectSpriteRenderer.color;
        // Value between 0-1
        float currentVariance = Mathf.Sqrt(Mathf.Pow(currentColor.r - targetRGBValue.r, 2) + Mathf.Pow(currentColor.g - targetRGBValue.g, 2) + Mathf.Pow(currentColor.b - targetRGBValue.b, 2));
        float clampedVariance = Mathf.Clamp(currentVariance, 0f, 1f);
        Debug.Log("current variance:" + clampedVariance);

        float adjustedIncrementRate = (currentIncrementRate * clampedVariance) + minColorIncrementRate;
        float clampedAdustedIncrementRate = Mathf.Clamp(adjustedIncrementRate, minColorIncrementRate, maxColorIncrementRate);

        Debug.Log("Adjusted FillRate:" + clampedAdustedIncrementRate);
        redColorButtonValues.SetColorIncrementRate(clampedAdustedIncrementRate);
        greenColorButtonValues.SetColorIncrementRate(clampedAdustedIncrementRate);
        blueColorButtonValues.SetColorIncrementRate(clampedAdustedIncrementRate);
    }
}
