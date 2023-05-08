using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorIncrementButton : MonoBehaviour
{
    public Transform fluidLevelObjectTransform;
    public SpriteRenderer fluidSpriteRenderer;
    public bool incrementsRed = false;
    public bool incrementsGreen = false;
    public bool incrementsBlue = false;
    public GameManager gameManager;
    private bool isOpaque;
    private float sizeIncrementRateUnits = 0.02f;
    private float colorIncrementRate = 0.1f;
    private float opacityIncreaseRate = 0.5f;
    private float lastMouseDownTime = 0f;
    private float buttonDebouceDelay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        isOpaque = false;
    }

    void OnMouseDrag()
    {
        if (hasDebouceExpired()) {
            IncrementColors();
            IncrementHeight();
        } 
    }

    bool hasDebouceExpired()
    {
        if (Time.time - lastMouseDownTime < buttonDebouceDelay)
        {
            return false;
        }
        lastMouseDownTime = Time.time;
        return true;
    }

    void IncrementColors()
    {
        if (!isOpaque) 
        {
            IncreaseOpacity();
        }

        gameManager.adjustFillRateForVariance(colorIncrementRate);

        Color newFluidColor = fluidSpriteRenderer.color;

        float incrementRateNormalised = colorIncrementRate;

        if (incrementsRed)
        {
            newFluidColor.r += incrementRateNormalised;
        }
        if (incrementsGreen)
        {
            newFluidColor.g += incrementRateNormalised;
        }
        if (incrementsBlue)
        {
            newFluidColor.b += incrementRateNormalised;
        }

        fluidSpriteRenderer.color = newFluidColor;
    }


    void IncrementHeight()
    {

        // Get the current scale of the sprite
        Vector3 currentScale = fluidLevelObjectTransform.localScale;
        currentScale.y += sizeIncrementRateUnits;
        
        Vector3 currentPosition = fluidLevelObjectTransform.position;
        currentPosition.y += sizeIncrementRateUnits / 2;

        fluidLevelObjectTransform.position = currentPosition;
        fluidLevelObjectTransform.localScale = currentScale;
    }

    void IncreaseOpacity()
    {
        Color targetSpriteColorOpaque = fluidSpriteRenderer.color;
        targetSpriteColorOpaque.a += opacityIncreaseRate;

        fluidSpriteRenderer.color = targetSpriteColorOpaque;

        if (fluidSpriteRenderer.color.a >= 1f)
        {
            isOpaque = true;
        }
    }

    public float GetColorIncrementRate0To255()
    {
        return colorIncrementRate;
    }

    public void SetColorIncrementRate(float newRate)
    {
        colorIncrementRate = newRate;
    }


}
