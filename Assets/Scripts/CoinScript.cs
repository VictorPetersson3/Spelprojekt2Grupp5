using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField]
    int myMoneyValue;
    [SerializeField]
    private float myRotatingSpeed;

    GameManager myGameManager;

    private void Start()
    {
        gameObject.SetActive(true);
        LeanTween.rotateAround(gameObject, Vector3.up, 360, myRotatingSpeed).setLoopClamp();
        myGameManager = GameManager.globalInstance;
    }


    public void AddingMoney()
    {
        gameObject.SetActive(false);
        myGameManager.ChangeMoney(myMoneyValue);
    }

}
