﻿using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

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

    public class AllLevelScores
    {
        public List<LevelScore> LevelScores;
    }

    [System.Serializable]
    public class LevelScore
    {
        public string myLevelName;
        public int myMinScore;
        public int myMediumScore;
        public int myHighScore;
        public int myStartingMoney;
    }



    [System.Serializable]
    public class Level
    {
        public string myLevelName;
        public int myMinScore;
        public int myMediumScore;
        public int myHighScore;
        public int myStartingMoney;
        public int myAmountOfMoney;
        public int myFinishedScore;
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
                level.myLevelName = act + " Level " + levelNumber;
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


    public void SetFinishedLevel(int aScore)
    {
        for (int i = 0; i < (int)(Levels.Count - 1); i++)
        {
            if (i == SceneManager.GetActiveScene().buildIndex)
            {
                if (aScore > myLevelList[i].myFinishedScore)
                {
                    myLevelList[i].myFinishedScore = aScore;
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


    public void SaveScore()
    {
        string json =               "{\n" +
                                    "\"LevelScores\":[\n";

        for (int i = 0; i < myLevelList.Count; i++)
        {
            LevelScore saveobject = new LevelScore
            {
                myLevelName = myLevelList[i].myLevelName,
                myMinScore = myLevelList[i].myMinScore,
                myMediumScore = myLevelList[i].myMediumScore,
                myHighScore = myLevelList[i].myHighScore,
                myStartingMoney = myLevelList[i].myStartingMoney
            };



            json += JsonUtility.ToJson(saveobject);
            if (i != myLevelList.Count - 1)
            {
                json += ",\n";
            }
           
        }
        
        json += "\n]\n}";

        string path = Application.streamingAssetsPath + "/levelscores.txt";
        if (File.Exists(path))
        {
            for (int i = 1; i < 5000; i++)
            {
                string backupPath = Application.streamingAssetsPath + "/backupLevelscore" + i + ".txt";
                if (File.Exists(backupPath))
                {
                    continue;
                }
                else
                {
                    FileUtil.CopyFileOrDirectory(path, backupPath);
                    break;
                }
            }
        }

        File.WriteAllText(Application.streamingAssetsPath + "/levelscores.txt", json);
    }


    public void SaveFile(int aSaveIndex)
    {
        string path = "";

        if (SetSaveFile(aSaveIndex))
        {
            path = Application.streamingAssetsPath + "/" + mySaveSlot;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, myLevelList);
        stream.Close();
    }


    public void LoadFile(int aLoadedFileIndex)
    {
        string path = "";

        if (SetSaveFile(aLoadedFileIndex))
        {
            path = Application.streamingAssetsPath + "/" + mySaveSlot;
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

    public void LoadAllScores()
    {
        AllLevelScores levelListScore;
        string path = Application.streamingAssetsPath + "/levelscores.txt";
        if (File.Exists(path))
        {
            string contents = File.ReadAllText(path);
            levelListScore = JsonUtility.FromJson<AllLevelScores>(contents);

            if (levelListScore != null)
            {
                for (int i = 0; i < levelListScore.LevelScores.Count; i++)
                {
                    myLevelList[i].myLevelName = levelListScore.LevelScores[i].myLevelName;
                    myLevelList[i].myMinScore = levelListScore.LevelScores[i].myMinScore;
                    myLevelList[i].myMediumScore = levelListScore.LevelScores[i].myMediumScore;
                    myLevelList[i].myHighScore = levelListScore.LevelScores[i].myHighScore;
                    myLevelList[i].myStartingMoney = levelListScore.LevelScores[i].myStartingMoney;
                    myLevelList[i].myAmountOfMoney = myLevelList[i].myStartingMoney;
                }
            }
        }
        else
        {
            Debug.Log("NO FILE PATH DETECTED AT: "+ path);
        }



        //AllLevelScores levelListScore;
        //TextAsset file = Resources.Load("levelscores") as TextAsset;
        //string json = file.ToString();

        //levelListScore = JsonUtility.FromJson<AllLevelScores>(json);
        //for (int i = 0; i < levelListScore.LevelScores.Count; i++)
        //{
        //    myLevelList[i].myMinScore = levelListScore.LevelScores[i].myMinScore;
        //    myLevelList[i].myMediumScore = levelListScore.LevelScores[i].myMediumScore;
        //    myLevelList[i].myHighScore = levelListScore.LevelScores[i].myHighScore;
        //    myLevelList[i].myLevelName = levelListScore.LevelScores[i].myLevelName;
        //    myLevelList[i].myStartingMoney = levelListScore.LevelScores[i].myStartingMoney;
        //}

    }

}