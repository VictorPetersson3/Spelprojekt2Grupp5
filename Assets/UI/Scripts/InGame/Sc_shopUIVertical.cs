using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_shopUIVertical : MonoBehaviour
{

    public LeanTweenType easeType;

    [SerializeField]
    RectTransform myStoreColumn2;
    [SerializeField]
    RectTransform myStoreColumn3;
    [SerializeField]
    RectTransform myRectPlay;
    [SerializeField]
    RectTransform myRectCamera;
    [SerializeField]
    RectTransform myRectBuild;
    [SerializeField]
    Button myBuildButton;
    [SerializeField]
    Button myCameraButton;
    [SerializeField]
    Button myPlayButton;

    [SerializeField]
    Sc_FadeIfDisabledParent[] mySc_FadeIfDisabledParents;
    private Vector3 myMin = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector2 myMax = new Vector2(1.0f, 1.0f);

    private GameObject myColumn2;
    private GameObject myColumn3;
    private void OnValidate()
    {
        mySc_FadeIfDisabledParents = FindObjectsOfType<Sc_FadeIfDisabledParent>();
    }
    
    private bool myBuildHasBeenPressed = false;
    public void ShowBuildMenuColumn2(GameObject aSecondColumn)
    {
        myColumn2 = aSecondColumn;
        if (!LeanTween.isTweening(aSecondColumn))
        {
            LeanTween.move(myStoreColumn2, myMin, 1.0f).setEase(easeType);
            aSecondColumn.SetActive(true);
            DeActivateMainButtons();
        }
    }
  
    public void ShowBuildMenuColumn3(GameObject aThirdColumn)
    {
        myColumn3 = aThirdColumn;
        if (!LeanTween.isTweening(aThirdColumn))
        {
            aThirdColumn.SetActive(true);
            LeanTween.move(myStoreColumn3, myMin, 1.0f).setEase(easeType);
        Invoke("ScaleMainButtonsDown", 0.20f);
            myBuildHasBeenPressed = true;
        }
    }
    public void HideBuildMenuColumn2()
    {
        Invoke("DeactivateButtonsAfterAnimation",1.0f);
        LeanTween.move(myStoreColumn2, new Vector3(-257.0f, 0, 0), 1.0f).setEase(easeType);
        ActivateMainButtons();
    }
    public void HideBuildMenuColumn3()
    {
        LeanTween.move(myStoreColumn3, new Vector3(-518.0f, 0, 0), 1.0f).setEase(easeType);
        ScaleMainButtonsUp();
        myBuildHasBeenPressed = false;
    }
    void ScaleMainButtonsUp()
    {
        LeanTween.scale(myRectPlay, new Vector3(1.0f, 1.0f, 1.0f), 1.0f).setEase(easeType);
        LeanTween.scale(myRectCamera, new Vector3(1.0f, 1.0f, 1.0f), 1.0f).setEase(easeType);
        LeanTween.scale(myRectBuild, new Vector3(1.0f, 1.0f, 1.0f), 1.0f).setEase(easeType);
    }
    void ScaleMainButtonsDown()
    {
        LeanTween.scale(myRectPlay, new Vector3(0.8f, 0.8f, 0.8f), 1.0f).setEase(easeType);
        LeanTween.scale(myRectCamera, new Vector3(0.8f, 0.8f, 0.8f), 1.0f).setEase(easeType);
        LeanTween.scale(myRectBuild, new Vector3(0.8f, 0.8f, 0.8f), 1.0f).setEase(easeType);
    }
    void ActivateMainButtons()
    {
        myBuildButton.interactable = !myBuildButton.interactable;
        myCameraButton.interactable = !myCameraButton.interactable;
        myPlayButton.interactable = !myPlayButton.interactable;
        for (int i = 0; i < mySc_FadeIfDisabledParents.Length; i++)
        {
            mySc_FadeIfDisabledParents[i].ButtonChangesState();
        }
    }
    void DeActivateMainButtons()
    {
        myBuildButton.interactable = !myBuildButton.interactable;
        myCameraButton.interactable = !myCameraButton.interactable;
        myPlayButton.interactable = !myPlayButton.interactable;
        for (int i = 0; i < mySc_FadeIfDisabledParents.Length; i++)
        {
            mySc_FadeIfDisabledParents[i].ButtonChangesState();
        }
    }

    private void DeactivateButtonsAfterAnimation()
    {
        myColumn2.SetActive(false);
        myColumn3.SetActive(false);
    }
}
