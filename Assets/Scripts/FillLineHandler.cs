using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillLineHandler : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collideWithTag = collision.tag;

        if (collideWithTag.Equals("FluidLevel"))
        {
            gameManager.FillLineHit();
        }
    }
}
