using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_EndGameOver : MonoBehaviour
{
    [SerializeField]
    Sc_LevelManager myLevelManager;
    [SerializeField]
    CanvasGroup myCanvas;
    [SerializeField]
    private GameObject myUIObject;

    public void FadeIn()
    {
        TurnOnMenu();
        LeanTween.alphaCanvas(myCanvas, 1, 1.0f);
    }
    public void FadeOut()
    {
        LeanTween.alphaCanvas(myCanvas, 0, 1.0f);
    }
    public void CloseGame()
    {
        FadeOut();
        myLevelManager.LoadMainMenu();
        Invoke("TurnOffMenu", 1.0f);
    }
    public void RetryGame()
    {
        FadeOut();
        myLevelManager.ReloadLevel();
        Invoke("TurnOffMenu", 1.0f);
    }
    private void TurnOffMenu()
    {
        myUIObject.SetActive(false);
    }
    private void TurnOnMenu()
    {
        myUIObject.SetActive(true);
    }
}
