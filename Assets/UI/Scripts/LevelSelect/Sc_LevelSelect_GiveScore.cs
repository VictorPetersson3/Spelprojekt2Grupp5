using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LevelSelect_GiveScore : MonoBehaviour
{
    [SerializeField]
    Sc_ScoreSymbol my1Star;
    [SerializeField]
    Sc_ScoreSymbol my2Star;
    [SerializeField]
    Sc_ScoreSymbol my3Star;
    public void GetLevelData(int aAmountOfStars)
    {
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
}
