using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public Image FeedbackPanelImage;
  public FeedbackSprites FeedBackSpritesGrades;
  public ParticleSystem FeedBackParticleSystem;
  public void showAccuracyFeedback(float accuracyPercentage)
  {
    if (accuracyPercentage > 90f)
    {
      FeedbackPanelImage.sprite = FeedBackSpritesGrades.AGradeSprite;
    }
    else if (accuracyPercentage > 85f)
    {
      FeedbackPanelImage.sprite = FeedBackSpritesGrades.BGradeSprite;
    }
    else if (accuracyPercentage > 80f)
    {
      FeedbackPanelImage.sprite = FeedBackSpritesGrades.CGradeSprite;
    }
    else if (accuracyPercentage > 75f)
    {
      FeedbackPanelImage.sprite = FeedBackSpritesGrades.DGradeSprite;
    }
    else if (accuracyPercentage > 70f)
    {
      FeedbackPanelImage.sprite = FeedBackSpritesGrades.EGradeSprite;
    }
    else
    {
      FeedbackPanelImage.sprite = FeedBackSpritesGrades.FGradeSprite;
    }
    ShowFeedbackIcon();
    PlayFeedbackParticles();
  }

  public void ShowFeedbackIcon()
  {
    FeedbackPanelImage.enabled = true;
    Invoke("HideFeedbackIcon", 1f);
  }

  void HideFeedbackIcon()
  {
    FeedbackPanelImage.enabled = false;
  }

  public void PlayFeedbackParticles()
  {
    FeedBackParticleSystem.Play();
  }
}


