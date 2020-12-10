using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinScript : MonoBehaviour
{
    [SerializeField]
    int myMoneyValue;
    [SerializeField]
    GameObject myParentedObject;
    [SerializeField]
    GameObject myTextObject;
    [SerializeField]
    private float myRotatingSpeed;

    GameManager myGameManager;
    PopUpText myPopUpText;

    private void Start()
    {
        gameObject.SetActive(true);
        LeanTween.rotateAround(gameObject, Vector3.up, 360, myRotatingSpeed).setLoopClamp();
        myGameManager = GameManager.globalInstance;
        SetPopUp();
    }


    public void AddingMoney()
    {
        myPopUpText.StartPopUp(myMoneyValue, gameObject.transform.position);
        gameObject.SetActive(false);
        myGameManager.ChangeMoney(myMoneyValue);
    }

    public int GetMoneyValue()
    {
        return myMoneyValue;
    }

    public void SetPopUp()
    {
        GameObject canvasObject = Instantiate(myParentedObject);
        GameObject textObject = Instantiate(myTextObject, canvasObject.transform);
        Vector3 myNewPosition = gameObject.transform.position;
        myNewPosition.y = myNewPosition.y + 1f;
        textObject.transform.position = myNewPosition;
        myPopUpText = textObject.GetComponent<PopUpText>();
    }

}
