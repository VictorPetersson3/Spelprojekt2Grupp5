using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_CreditsClose : MonoBehaviour
{

    [SerializeField]
    public GameObject myCredits;
    [SerializeField]
    private CanvasGroup myCanvasGroup;

    public void ShowCredits()
    {
        myCredits.SetActive(true);
        LeanTween.alphaCanvas(myCanvasGroup, 1, 0.50f);
    }
    public void CloseCredits()
    {
        LeanTween.alphaCanvas(myCanvasGroup, 0, 0.50f);
        Invoke("TurnOffMenu", 0.50f);

    }
    private void TurnOffMenu()
    {
        myCredits.SetActive(false);
    }

}
