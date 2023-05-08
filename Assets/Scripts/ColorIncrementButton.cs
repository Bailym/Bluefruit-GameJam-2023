using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorIncrementButton : MonoBehaviour
{
    public Transform fluidLevelObjectTransform;
    public FluidLevelHandler fluidLevelHandler;
    public bool incrementsRed = false;
    public bool incrementsGreen = false;
    public bool incrementsBlue = false;
    public GameManager gameManager;
    private float sizeIncrementRateUnits = 0.02f;
    private float colorIncrementRate = 0.1f;
    private float opacityIncrementRate = 0.5f;
    private float lastMouseDownTime = 0f;
    private float buttonDebouceDelay = 0.1f;
    

    // Start is called before the first frame update
    void Start()
    {
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
        if (!fluidLevelHandler.GetIsOpaque()) 
        {
            IncreaseOpacity();
        }

        gameManager.adjustFillRateForVariance(colorIncrementRate);

        Color newFluidColor = fluidLevelHandler.GetCurrentColor();

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

        fluidLevelHandler.SetCurrentColor(newFluidColor);
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
        Color newFluidColor = fluidLevelHandler.GetCurrentColor();
        newFluidColor.a += opacityIncrementRate;

        fluidLevelHandler.SetCurrentColor(newFluidColor);
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
