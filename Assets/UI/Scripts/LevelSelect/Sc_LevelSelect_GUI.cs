using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_LevelSelect_GUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI myTotalScore;

    private void OnValidate()
    {
        SetScore();
    }

    public void SetScore()
    {
        myTotalScore.text = GameManager.globalInstance.GetTotalStars().ToString();
    }
}
