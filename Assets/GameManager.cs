using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager globalInstance;

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


    public List<Level> myLevelList;

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
                myLevelList[i].myFinishedScore = aScore;
                //UI.SetScore(myLevelList[i].myFinishedScore)

                if (myLevelList[i].myFinishedScore > myLevelList[i].myHighScore)
                {
                    myLevelList[i].myFinishedLevel = false;
                    //UI.ShowFailScreen();
                }
                else if (myLevelList[i].myFinishedScore >= myLevelList[i].myMediumScore && aScore <= myLevelList[i].myHighScore)
                {
                    myLevelList[i].myFinishedLevel = true;
                    //UI.ShowWinScreen();
                }
                else if (myLevelList[i].myFinishedScore < myLevelList[i].myMinScore && myLevelList[i].myFinishedScore >= myLevelList[i].myMinScore && myLevelList[i].myFinishedScore < myLevelList[i].myMediumScore)
                {
                    myLevelList[i].myFinishedLevel = true;
                    //UI.ShowWinScreen();
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
}
