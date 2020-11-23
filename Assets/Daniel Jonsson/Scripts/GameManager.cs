using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
    public List<Level> myLevelList;


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

    

    [System.Serializable]
    public class Level
    {
        public string myLevelName;
        public int myMinScore;
        public int myMediumScore;
        public int myHighScore;
        public int myFinishedScore;
        public bool myFinishedLevel;
    }


    void Start()
    {

        PlayerPrefs.SetInt("level1", 50);

        if (myLoadedLevel == false)
        {
            for (int i = 1; i <= (int)(Levels.Count - 1); i++)
            {
                Level level = new Level();
                level.myLevelName = "Level " + i;
                level.myFinishedScore = 0;
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
        IsPlayerAlive();

        if (Input.GetKeyDown(KeyCode.R))
        {
            BuildManager.globalInstance.ResetTiles();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

}
