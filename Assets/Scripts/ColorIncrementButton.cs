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
  private Vector3 originalButtonScale;
  private float buttonScaleIncreaseAmount = 0.5f;

  private void Start()
  {
    originalButtonScale = gameObject.transform.localScale;
  }


  void OnMouseDrag()
  {
    if (hasDebouceExpired())
    {
      IncreaseButtonSize(buttonScaleIncreaseAmount);
      IncrementColors();
      IncrementHeight();
    }
  }

  void OnMouseUp()
  {
    ResetButtonSize();
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

    Color newFluidColorClamped = ClampColorValues0to1(newFluidColor); 
    fluidLevelHandler.SetCurrentColor(newFluidColorClamped);
  }

  Color ClampColorValues0to1(Color colorToClamp)
  {
    Color clampedColor = colorToClamp;
    clampedColor.r = Mathf.Clamp(colorToClamp.r, 0f, 1f);
    clampedColor.g = Mathf.Clamp(colorToClamp.g, 0f, 1f);
    clampedColor.b = Mathf.Clamp(colorToClamp.b, 0f, 1f);
    clampedColor.a = Mathf.Clamp(colorToClamp.a, 0f, 1f);

    return clampedColor;
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

  void IncreaseButtonSize(float scaleAmount)
  {
    Vector3 newScale = originalButtonScale;
    newScale.x += scaleAmount;
    newScale.y += scaleAmount;

    gameObject.transform.localScale = newScale;
  }

  void ResetButtonSize()
  {
    Vector3 newScale = gameObject.transform.localScale;
    newScale.x = originalButtonScale.x;
    newScale.y = originalButtonScale.y;

    gameObject.transform.localScale = newScale;
  }


}
