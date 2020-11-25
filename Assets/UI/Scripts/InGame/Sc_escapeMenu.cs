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
    private Vector3 myMin = new Vector3(0.0f, 240.0f, 0.0f);

    public void EscapeMenuUp(GameObject aEscapeMenuContainer)
    {
        if (!LeanTween.isTweening(myEscapeMenuRect))
        {
            Invoke("DeActivateUpArrow", 0.5f);
            Invoke("MoveMenuUp", 0.1f);
        }
    }
    public void EscapeMenuDown(GameObject aEscapeMenuContainer)
    {
        if (!LeanTween.isTweening(myEscapeMenuRect))
        {
            Invoke("ActivateUpArrow", 0.5f);
            Invoke("MoveMenuDown", 0.1f);
        }
    }
    void ActivateUpArrow()
    {
        myEscapeMenuUpArrow.SetActive(true);
        myEscapeMenuDownArrow.SetActive(false);
    }
    void DeActivateUpArrow()
    {
        myEscapeMenuDownArrow.SetActive(true);
        myEscapeMenuUpArrow.SetActive(false);
    }
    void MoveMenuUp()
    {
        LeanTween.move(myEscapeMenuRect, myMin, 1.0f).setEase(easeType);
    }
    void MoveMenuDown()
    {
        LeanTween.move(myEscapeMenuRect, new Vector3(0, -520.0f, 0), 1.0f).setEase(easeType);
    }
}
