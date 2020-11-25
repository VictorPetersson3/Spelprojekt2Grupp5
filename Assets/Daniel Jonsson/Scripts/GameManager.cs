using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum SaveSlot
    {
        Save1 = 1,
        Save2 = 2,
        Save3 = 3
    }

    private SaveSlot mySaveSlot;
    private bool myLoadedLevel;

    public static GameManager globalInstance;
    [SerializeField] Text myMoneyText;
    [SerializeField] Text myLevelText;

    [SerializeField] short myFirstActTransition, mySecondActTransition;

    [SerializeField]
    public List<Level> myLevelList = new List<Level>();


    [System.Serializable]
    public class Level
    {
        public string myLevelName;
        public int myMinScore;
        public int myMediumScore;
        public int myHighScore;
        public int myFinishedScore;
        public int myStartingMoney;
        public int myAmountOfMoney;
        public bool myFinishedLevel;
    }


    private void Awake()
    {

        if (globalInstance == null)
        {
            globalInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (globalInstance != this)
        {
            Destroy(gameObject);
        }
    }

   

    void Start()
    {
        int actNumber = 1;
        int levelNumber = 1;
        string act = "Act " + actNumber + ": ";

        

        if (myLoadedLevel == false)
        {
            for (int i = 1; i <= (int)(Levels.Count - 1); i++)
            {
                if (i == myFirstActTransition)
                {
                    actNumber += 1;
                    act = "Act " + actNumber + ": ";
                    levelNumber = 1;
                }
                else if (i == mySecondActTransition)
                {
                    actNumber += 1;
                    act = "Act " + actNumber + ": ";
                    levelNumber = 1;
                }
                
                Level level = new Level();
                level.myLevelName = act +  " Level " + levelNumber;
                levelNumber++;
                level.myFinishedScore = 0;
                level.myStartingMoney = 0;
                level.myAmountOfMoney = 0;
                level.myMinScore = 0;
                level.myMediumScore = 0;
                level.myHighScore = 0;
                level.myFinishedLevel = false;
                myLevelList.Add(level);
            }
        }
    }


    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            CheckTextStatus();
        }
    }

    public void CheckTextStatus()
    {
        myMoneyText.text = "Money: " + myLevelList[SceneManager.GetActiveScene().buildIndex - 1].myAmountOfMoney;
        myLevelText.text = myLevelList[SceneManager.GetActiveScene().buildIndex - 1].myLevelName;
    }

    public void ResetLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResetAllLevels();
    }

    public void NextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index + 1);
        ResetAmountOfMoney(index - 1);
    }

    public void BackLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index - 1);
        ResetAmountOfMoney(index - 1);
    }

    public void ResetAmountOfMoney(int aBuildIndex)
    {
        myLevelList[aBuildIndex].myAmountOfMoney = myLevelList[aBuildIndex].myStartingMoney;
        //BuildManager.globalInstance.ResetTiles();
    }

    public void ResetAllLevels()
    {
        for (int i = 0; i < myLevelList.Count; i++)
        {
            myLevelList[i].myAmountOfMoney = myLevelList[i].myStartingMoney;
        }
    }

    public void SetLevelScore(int aLowScore, int aMediumScore, int aHighScore)
    {
        for (int i = 0; i < (int)(Levels.Count - 1); i++)
        {
            if (i == SceneManager.GetActiveScene().buildIndex)
            {
                myLevelList[i].myMinScore = aLowScore;
                myLevelList[i].myMediumScore = aMediumScore;
                myLevelList[i].myHighScore = aHighScore;
            }
        }
    }

    public void SetFinishedLevel(int aScore)
    {
        for (int i = 0; i < (int)(Levels.Count - 1); i++)
        {
            if (i == SceneManager.GetActiveScene().buildIndex)
            {
                if (aScore > myLevelList[i].myFinishedScore)
                {
                    myLevelList[i].myFinishedScore = aScore;
                    SaveFile(1);
                }

                
                //UI.SetScore(aScore)

                if (aScore > myLevelList[i].myHighScore)
                {
                    myLevelList[i].myFinishedLevel = false;
                    //UI.ShowFailScreen(aScore, 1);
                }
                else if (aScore >= myLevelList[i].myMediumScore && aScore <= myLevelList[i].myHighScore)
                {
                    myLevelList[i].myFinishedLevel = true;
                    //UI.ShowWinScreen(aScore, 2);
                }
                else if (aScore < myLevelList[i].myMinScore && myLevelList[i].myFinishedScore >= myLevelList[i].myMinScore && myLevelList[i].myFinishedScore < myLevelList[i].myMediumScore)
                {
                    myLevelList[i].myFinishedLevel = true;
                    //UI.ShowWinScreen(aScore, 3);
                }
            }
        }
    }

    public void IsPlayerAlive()
    {
        //if (Player.IsPlayerDead() == true)
        //{
        //    UI.ShowLoseScreen();
        //}
    }

    public void RestartLevel()
    {
        //BuildManager.globalInstance.ResetTiles();
        //Player.ResetPlayer();
    }


    public void SaveFile(int aSaveFileIndex)
    {
        string path = "";

        if (SetSaveFile(aSaveFileIndex))
        {
            path = Application.persistentDataPath + "/" + mySaveSlot;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        
        Debug.Log(Application.persistentDataPath);
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, myLevelList);
        stream.Close();
    }

    public void LoadFile(int aLoadedFileIndex)
    {
        string path = "";

        if (SetSaveFile(aLoadedFileIndex))
        {
            path = Application.persistentDataPath + "/" + mySaveSlot;
        }

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            List<Level> loadLevels = (List<Level>)formatter.Deserialize(stream);
            stream.Close();

            myLevelList = loadLevels;

            myLoadedLevel = true;
            ResetAllLevels();
        }
        else 
        {
            Debug.Log("NO EXISTED PATH FOR LOADED FILE");
            myLoadedLevel = false;
            //UI.ErrorSave();
        }
    }

    public List<bool> CheckAllFinishedLevels()
    {
        List<bool> listOfFinishedLevels = new List<bool>();
        for (int i = 0; i < myLevelList.Count; i++)
        {
            listOfFinishedLevels.Add(myLevelList[i].myFinishedLevel);
        }

        return listOfFinishedLevels;
    }


    bool SetSaveFile(int aSaveIndex)
    {
        switch (aSaveIndex)
        {
            case (int)SaveSlot.Save1:
                {
                    mySaveSlot = SaveSlot.Save1;
                    return true;
                }
            case (int)SaveSlot.Save2:
                {
                    mySaveSlot = SaveSlot.Save2;
                    return true;
                }
            case (int)SaveSlot.Save3:
                {
                    mySaveSlot = SaveSlot.Save3;
                    return true;
                }
            default:
                {
                    Debug.Log("NO VALID PATH");
                    return false;
                }
        }
    }

    public void ChangeMoney(int someMoney)
    {
        int myTotalMoney = myLevelList[SceneManager.GetActiveScene().buildIndex - 1].myAmountOfMoney;
        myTotalMoney += someMoney;

        myLevelList[SceneManager.GetActiveScene().buildIndex - 1].myAmountOfMoney = myTotalMoney;
    }


}
