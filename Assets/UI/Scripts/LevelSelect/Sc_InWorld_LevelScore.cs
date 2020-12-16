using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_InWorld_LevelScore : MonoBehaviour
{
    [SerializeField]
    Sc_ScoreSymbol my1Score;
    [SerializeField]
    Sc_ScoreSymbol my2Score;
    [SerializeField]
    Sc_ScoreSymbol my3Score;

    public void SetScore(int aScore)
    {

        switch (aScore)
        {
            case 0:
                break;
            case 1:
                my1Score.UnlockScore();
                break;
            case 2:
                my1Score.UnlockScore();
                my2Score.UnlockScore();
                break;
            case 3:
                my1Score.UnlockScore();
                my2Score.UnlockScore();
                my3Score.UnlockScore();
                break;
        }

    }

}
