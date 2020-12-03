using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    GameManager myGameManager;

    private void Start()
    {
        myGameManager = GameManager.globalInstance;
    }


    public void AddingMoney()
    {
        Debug.Log("Finished");
        myGameManager.SetFinishedLevel();
    }

}
