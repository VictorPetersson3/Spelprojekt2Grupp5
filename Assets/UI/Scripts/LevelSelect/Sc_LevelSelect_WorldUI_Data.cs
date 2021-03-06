﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_LevelSelect_WorldUI_Data : MonoBehaviour
{
    [SerializeField]
    Image myInLevelImage;
    [SerializeField]
    TextMeshProUGUI myLevelSceneNameTMP;
    [SerializeField]
    CanvasGroup myScoreCanvasGroup;
    [SerializeField]
    TextMeshProUGUI myAmountToUnlock;
    [SerializeField]
    RectTransform myScoreTransform;
    [SerializeField]
    RectTransform myLevelTransform;
    [SerializeField]
    GameObject myGrayOverlay;
    public void SetMyLevelName(string aName)
    {
        myLevelSceneNameTMP.text = aName;
    }
    public void SetMyLevelImage(Sprite aImage)
    {
        myInLevelImage.sprite = aImage;
    }
    public void SetRequiredScore(int aScore, int aCurrentScore)
    {
        myAmountToUnlock.text = aScore.ToString();
        if(aCurrentScore >= aScore)
        {
            HideRequiredScore();
        }
    }
    public void HideRequiredScore()
    {
        myGrayOverlay.SetActive(false);
        LeanTween.scale(myScoreTransform, new Vector3(0.06f, 0.06f, 0.06f), 1.2f).setEase(LeanTweenType.punch);
        LeanTween.alphaCanvas(myScoreCanvasGroup, 0.0f, 1.0f).setEase(LeanTweenType.easeInExpo);
        LeanTween.scale(myScoreTransform, new Vector3(0,0,0), 2.0f).setEase(LeanTweenType.easeInExpo);
    }
}
