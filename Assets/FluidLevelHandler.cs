using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidLevelHandler : MonoBehaviour
{
    public GameManager gameManager;
    private Vector3 fluidLevelStartPosition;
    private SpriteRenderer FluidLevelSpriteRenderer;
    private bool isOpaque;

    // Start is called before the first frame update
    void Start()
    {
        fluidLevelStartPosition = gameObject.transform.position;
        FluidLevelSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResetFluidLevelPosition()
    {
        Vector3 newFluidLevelScale = gameObject.transform.localScale;
        newFluidLevelScale.y = 0;
        gameObject.transform.localScale = newFluidLevelScale;
        gameObject.transform.position = fluidLevelStartPosition;
    }

    public void ResetFluidLevelColor()
    {
        Color newFluidColor = FluidLevelSpriteRenderer.color;
        newFluidColor.r = 0;
        newFluidColor.g = 0;
        newFluidColor.b = 0;
        newFluidColor.a = 0;

        FluidLevelSpriteRenderer.color = newFluidColor;
        SetIsOpaque(false);
    }

    public Color GetCurrentColor()
    {
        return FluidLevelSpriteRenderer.color;
    }

    public bool GetIsOpaque()
    {
        return isOpaque;
    }
    public void SetIsOpaque(bool newValueBool)
    {
        isOpaque = newValueBool;
    }
    public void SetCurrentColor(Color newColor)
    {
        if (newColor.a >= 1f)
        {
            SetIsOpaque(true);
        }

        FluidLevelSpriteRenderer.color = newColor;
    }
}
