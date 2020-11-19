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
    [SerializeField]
    TextMeshProUGUI myText;

    private Color myImageOgColor;
    private Color myTextOgColor;
    public void ButtonChangesState()
    {
        myImageOgColor = myImage.color;
        myTextOgColor = myText.color;
        Color tempColorImage = myImage.color;
        tempColorImage.a = 0.5F;
        Color tempColorText = myText.color;
        tempColorText.a = 0.5F;

        if (!myButton.IsInteractable())
        {
            myImage.color = tempColorImage;
            myText.color = tempColorText;
        }
        else
        {
            myImage.color = myImageOgColor;
            myText.color = myTextOgColor;
        }

    }
}
