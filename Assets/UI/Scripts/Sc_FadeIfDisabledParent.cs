using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_FadeIfDisabledParent : MonoBehaviour
{
    [SerializeField]
    Button myButton;
    [SerializeField]
    Image myImage;
    

    private Color myImageOgColor;

    private void OnValidate()
    {
        myImageOgColor = myImage.color;
    }
    public void ButtonChangesState()
    {
        if (!myButton.IsInteractable())
        {
            Color tempColorImage = myImage.color;
            tempColorImage.a = 0.5F;
            myImage.color = tempColorImage;
        }
        else
        {
            myImage.color = myImageOgColor;
        }

    }
}
