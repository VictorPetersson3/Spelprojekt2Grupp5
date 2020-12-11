using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sc_LevelElementData : MonoBehaviour
{

    [SerializeField]
    int myLevelIndex;
    [SerializeField]
    Texture2D myImage;
    [SerializeField]
    LevelSelect myLevelSelectScript;

    [Space(10, order = 0)]
    [Header("Do not touch these beneath here:", order = 1)]
    [Space(40, order = 2)]

    [SerializeField]
    Image myLevelImage;
    [SerializeField]
    TextMeshProUGUI myLevelNameTMP;
    [SerializeField]
    TextMeshProUGUI myLevelSpendGoalTMP;
    [SerializeField]
    GameObject myUIElement;
    [SerializeField]
    RectTransform myLevelMenuRect;
    [SerializeField]
    Sc_LevelSelect_WorldUI_Data myLevelSelect_WorldUI;

    [SerializeField]
    Sc_InterfaceLevelManager myLevelManagerInterface;

    [SerializeField]
    Sc_LevelSelect_GiveScore myLevelTotalScore;
   
    
    private bool myActive;
    private Sprite myConvertedImageSprite;
    private Vector3 myMin = new Vector3(-100.0f, 100.0f, 0.0f);


    private void Start()
    {
        myConvertedImageSprite = Sprite.Create(myImage, new Rect(0.0f, 0.0f, myImage.width, myImage.height), new Vector2(0.5f, 0.5f), 100.0f);
        myLevelImage.sprite = myConvertedImageSprite;
        myLevelSelect_WorldUI.SetMyLevelName(GameManager.globalInstance.GetName(myLevelIndex));
        myLevelSelect_WorldUI.SetMyLevelImage(myConvertedImageSprite);
        myLevelSelect_WorldUI.SetRequiredScore(
            myLevelSelectScript.GetStarRequirements()[myLevelIndex - 1],
            GameManager.globalInstance.GetTotalStars());

        myLevelNameTMP.text = GameManager.globalInstance.GetName(myLevelIndex);
        myLevelSpendGoalTMP.text = GameManager.globalInstance.GetHighestScore(myLevelIndex).ToString();
        myActive = false;
        myLevelTotalScore.GetLevelData(GameManager.globalInstance.GetLevelStars(myLevelIndex));
    } 
    private void OnValidate()
    {
        myLevelManagerInterface = FindObjectOfType<Sc_InterfaceLevelManager>();

        //myActive = false;
        //myConvertedImageSprite = Sprite.Create(myImage, new Rect(0.0f, 0.0f, myImage.width, myImage.height), new Vector2(0.5f, 0.5f), 100.0f);
        //myLevelImage.sprite = myConvertedImageSprite;
        //myInLevelImage.sprite = myConvertedImageSprite;
        //myLevelNameTMP.text = myLevelName;
        //myLevelSpendGoalTMP.text = mySpendGoal.ToString("0");
    }
    private void Update()
    {
        if(myUIElement.activeSelf && !myActive)
        {
            MoveMenuUp();
            myActive = true;
        }
        if(!myUIElement.activeSelf && myActive)
        {
            MoveMenuDown();
            myActive = false;
        }
    }
    void MoveMenuUp()
    {
        LeanTween.move(myLevelMenuRect, myMin, 1.0f).setEase(LeanTweenType.easeInCubic);
    }
    void MoveMenuDown()
    {
        LeanTween.move(myLevelMenuRect, new Vector3(-100, -800.0f, 0), 1.0f).setEase(LeanTweenType.easeInCubic);
    }
    public void StartLevel()
    {
        myLevelManagerInterface.LoadLevel(myLevelIndex);
    }
    public int GetLevelIndex()
    {
        return myLevelIndex;
    }

}
