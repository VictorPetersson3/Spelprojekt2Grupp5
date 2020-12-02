using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_LevelSelect_GUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI myTotalScore;

    public void SetScore(int aScore)
    {
        myTotalScore.text = aScore.ToString();

    }
}
