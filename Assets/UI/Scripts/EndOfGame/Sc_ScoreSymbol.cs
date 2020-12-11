using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_ScoreSymbol : MonoBehaviour
{
    [SerializeField]
    GameObject myHasGottenScore;

    public void UnlockScore()
    {
        myHasGottenScore.SetActive(true);
    }

    public void LockScore()
    {
        myHasGottenScore.SetActive(false);
    }
}
