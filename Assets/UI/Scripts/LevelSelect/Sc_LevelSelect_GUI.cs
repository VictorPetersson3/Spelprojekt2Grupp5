using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_LevelSelect_GUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI myTotalScore;
    [SerializeField]
    GameManager myGameManager;
    private void Start()
    {
        myGameManager = GameManager.FindObjectOfType<GameManager>();
        SetScore();
    }
    public void SetScore()
    {
        myTotalScore.text = myGameManager.GetTotalStars().ToString();
    }
}
