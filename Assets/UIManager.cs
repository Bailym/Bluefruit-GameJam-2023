using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
  public Image FeedbackPanelImage;
  public FeedbackSprites FeedBackSpritesGrades;
  public ParticleSystem FeedBackParticleSystem;
  public TextMeshProUGUI requestText;
  private List<string> requestTextOptions;

  private void Start()
  {
    requestTextOptions = new List<string>();
    requestTextOptions.Add("Im sure this is the one we're looking for!");
    requestTextOptions.Add("This one's going to work!");
    requestTextOptions.Add("We're getting close! Try this!");
    requestTextOptions.Add("If my calculations are correct, this is what we need to make.");
    requestTextOptions.Add("Make an elixir this colour");
    requestTextOptions.Add("Im certain of this one...");
  }

  public void UpdateRequestText()
  {
    int numberOfOptions = requestTextOptions.Count - 1;
    int randomOptionIndex = Random.Range(0, numberOfOptions);

    requestText.text = requestTextOptions[randomOptionIndex].ToString();
  }
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


