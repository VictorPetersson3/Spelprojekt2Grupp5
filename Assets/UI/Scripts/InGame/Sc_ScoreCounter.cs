using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Sc_ScoreCounter : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI myScore;
   

    private float myScoreValue = 10000;
    private void Update()
    {
        SetScoreText();
    }
    void SetScoreText()
    {
        myScore.text = myScoreValue.ToString("0");
    }

}
