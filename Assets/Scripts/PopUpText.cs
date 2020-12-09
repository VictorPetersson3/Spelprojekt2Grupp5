using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{

    [SerializeField]
    [Header("In Seconds: ")]
    float myTextFadingOut;
    [SerializeField]
    [Header("In Position Y from Origin Point: ")]
    float myTextPosition;

    Transform myTextMeshTransform;
    bool myRegularView = true;

    void Start()
    {
        myTextMeshTransform = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.z = cameraPos.z - 10f;
        myTextMeshTransform.rotation = Quaternion.LookRotation(myTextMeshTransform.position - cameraPos);
    }

    public void StartPopUp(int aMoneyValue, Vector3 aPosition)
    {

        gameObject.transform.GetComponent<TextMeshProUGUI>().text = aMoneyValue.ToString();
        gameObject.SetActive(true);

        LeanTween.value(gameObject, 1f, 0f, myTextFadingOut).setOnUpdate((float value) =>
        {
            gameObject.transform.GetComponent<TextMeshProUGUI>().alpha = value;
        });

        if (myRegularView == true)
        {
            LeanTween.moveY(gameObject, aPosition.y + myTextPosition, myTextFadingOut);
        }
        else
        {
            LeanTween.moveZ(gameObject, aPosition.z + 0.2f, myTextFadingOut);
        }

        Invoke("ClosePopUp", myTextFadingOut);
    }

    public void ClosePopUp()
    {
        gameObject.SetActive(false);
    }

    public void SetCameraView()
    {
        myRegularView = !myRegularView;
    }


}
