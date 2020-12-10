using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    GameObject myCanvasObject;

    GameManager myGameManager;
    PopUpText myPopUpText;

    private void Start()
    {
        gameObject.SetActive(true);
        LeanTween.rotateAround(gameObject, Vector3.up, 360, myRotatingSpeed).setLoopClamp();
        myGameManager = GameManager.globalInstance;

        if (GameObject.Find("P_PopUpText(Clone)") == false)
        {
            myCanvasObject = Instantiate(myParentedObject);
            SceneManager.MoveGameObjectToScene(myCanvasObject, SceneManager.GetSceneAt(1));
        }   

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
        GameObject foundGameObject = GameObject.Find("P_PopUpText(Clone)");
        Debug.Log(foundGameObject);
        GameObject textObject = Instantiate(myTextObject, foundGameObject.transform);
        Vector3 myNewPosition = gameObject.transform.position;
        myNewPosition.y = myNewPosition.y + 1f;
        textObject.transform.position = myNewPosition;
        myPopUpText = textObject.GetComponent<PopUpText>();
    }

}
