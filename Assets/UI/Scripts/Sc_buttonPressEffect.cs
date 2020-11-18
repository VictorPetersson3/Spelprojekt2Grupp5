using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_buttonPressEffect : MonoBehaviour
{

    public void PressButtonEffect(RectTransform aRectTransf)
    {
        LeanTween.scale(aRectTransf, new Vector3(1.1f, 1.1f, 1), 0.50f).setEase(LeanTweenType.punch);
    }
}
