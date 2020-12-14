using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_OptionsMenu : MonoBehaviour
{
    [SerializeField]
    CanvasGroup myOptionsCanvasGroup;
    [SerializeField]
    RectTransform myOptionsTransform;
    [SerializeField]
    GameObject myOptionsGameObject;
    [SerializeField]
    Scrollbar myScrollbar;
    public void ShowOptionsMenu()
    {
        myOptionsGameObject.SetActive(true);
        LeanTween.alphaCanvas(myOptionsCanvasGroup, 1.0f, 0.50f).setEase(LeanTweenType.easeInExpo);
        LeanTween.scale(myOptionsTransform, new Vector3(1.0f, 1.0f, 1.0f), 0.30f).setEase(LeanTweenType.easeInExpo);
    }
    public void HideOptionsMenu()
    {
        LeanTween.alphaCanvas(myOptionsCanvasGroup, 0.0f, 0.50f).setEase(LeanTweenType.easeOutExpo);
        LeanTween.scale(myOptionsTransform, new Vector3(0.0f, 0.0f, 0.0f), 0.30f).setEase(LeanTweenType.easeOutExpo);
        Invoke("DeactivateObject", 0.50f);
    }
    private void DeactivateObject()
    {
        myOptionsGameObject.SetActive(false);
    }
    public Scrollbar GetScrollbar()
    {
        return myScrollbar;
    }
}
