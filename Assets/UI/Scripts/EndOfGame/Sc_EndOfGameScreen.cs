using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_EndOfGameScreen : MonoBehaviour
{
    [SerializeField]
    Sc_LevelManager myLevelManager;
    [SerializeField]
    RectTransform myEndMenuRect;

    [SerializeField]
    TextMeshProUGUI myAmountOfMoneySpent_TMP;
    [SerializeField]
    TextMeshProUGUI myAmountOfMoneyRequired_TMP;
    [SerializeField]
    Sc_ScoreSymbol my1Star;
    [SerializeField]
    Sc_ScoreSymbol my2Star;
    [SerializeField]
    Sc_ScoreSymbol my3Star;

    private Vector3 myStartPosition = new Vector3(0.0f, -1000.0f, 0.0f);
    private Vector3 myEndPosition = new Vector3(0.0f, 0.0f, 0.0f);

    public void GetLevelData(int aAmountOfMoney, int aAmountOfStars)
    {
        myAmountOfMoneySpent_TMP.text = aAmountOfMoney.ToString();
        switch (aAmountOfStars)
        {
            case 0:
                break;
            case 1:
                my1Star.UnlockScore();
                break;
            case 2:
                my1Star.UnlockScore();
                my2Star.UnlockScore();
                break;
            case 3:
                my1Star.UnlockScore();
                my2Star.UnlockScore();
                my3Star.UnlockScore();
                break;
        }

    }
    public void MoveUp()
    {
        LeanTween.move(myEndMenuRect, myEndPosition, 1.0f).setEase(LeanTweenType.easeInCubic);
    }
    public void MoveDown()
    {
        LeanTween.move(myEndMenuRect, myStartPosition, 0.50f).setEase(LeanTweenType.easeInCubic);
    }
    public void CloseGame()
    {
        MoveDown();
        myLevelManager.LoadMainMenu();
    }
    public void RetryGame()
    {
        MoveDown();
        myLevelManager.ReloadLevel();
    }
}
