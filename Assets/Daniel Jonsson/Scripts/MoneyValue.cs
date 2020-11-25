using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyValue : MonoBehaviour
{
    [SerializeField]
    int myMoneyValue;

    GameManager myGameManager;

    private void Start()
    {
        myGameManager = GameManager.globalInstance;
    }


    public void AddingMoney()
    {
        gameObject.SetActive(false);
        myGameManager.ChangeMoney(myMoneyValue);
    }


}
