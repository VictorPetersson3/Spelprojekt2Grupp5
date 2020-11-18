using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_escapeMenu : MonoBehaviour
{
    public LeanTweenType easeType;

    [SerializeField]
    GameObject myEscapeMenuDownArrow;
    [SerializeField]
    GameObject myEscapeMenuUpArrow;
    [SerializeField]
    RectTransform myEscapeMenuRect;
    private Vector3 myMin = new Vector3(0.0f, 0.0f, 0.0f);

    public void EscapeMenuUp(GameObject aEscapeMenuContainer)
    {
        myEscapeMenuDownArrow.SetActive(true);
        myEscapeMenuUpArrow.SetActive(false);
        LeanTween.move(myEscapeMenuRect, myMin, 1.0f).setEase(easeType);
    }
    public void EscapeMenuDown(GameObject aEscapeMenuContainer)
    {
        LeanTween.move(myEscapeMenuRect, new Vector3(0, -620.0f, 0), 1.0f).setEase(easeType);
        myEscapeMenuUpArrow.SetActive(true);
        myEscapeMenuDownArrow.SetActive(false);
    }

}
