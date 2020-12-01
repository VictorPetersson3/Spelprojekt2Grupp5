using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Sc_ScoreCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI myScore;

    GameManager myGameManager;

    private void Update()
    {
        SetScoreText();
    }
    void SetScoreText()
    {
        myScore.text = GameManager.globalInstance.GetUpdatedScore().ToString();
    }

}
