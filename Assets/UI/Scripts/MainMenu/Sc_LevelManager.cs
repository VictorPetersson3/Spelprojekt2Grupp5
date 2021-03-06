﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_LevelManager : MonoBehaviour
{

    public static Sc_LevelManager myInstance;
    public GameObject myLoadingScreen;
    public GameManager myGameManager;

    private int myCurrentSceneIndex;

    private void Awake()
    {
        if (myInstance == null)
        {
            myInstance = this;
            SceneManager.LoadScene(16, LoadSceneMode.Additive);
            myCurrentSceneIndex = 16;
        }
    }

    public void LoadGame(int aSceneIndex)
    {
        myLoadingScreen.gameObject.SetActive(true);
        myGameManager.ResetGameManager();
        myGameManager.ResetAmountOfMoney();
        SceneManager.UnloadSceneAsync(myCurrentSceneIndex);
        myCurrentSceneIndex = aSceneIndex;
        SceneManager.LoadScene(myCurrentSceneIndex, LoadSceneMode.Additive);
        Invoke("LoadAfterXTime", 0.80f);
        //StartCoroutine(CoRoutineLoad());
    }
    void LoadAfterXTime()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(myCurrentSceneIndex));
        myLoadingScreen.gameObject.SetActive(false);
    }
    public void ReloadLevel()
    {
        myGameManager.ResetAmountOfMoney();
        LoadGame(myCurrentSceneIndex);
    }
    public void LoadMainMenu()
    {
        LoadGame(16);
    }
    IEnumerator CoRoutineLoad()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(myCurrentSceneIndex, LoadSceneMode.Additive);
        while (!loadingOperation.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(myCurrentSceneIndex));

        yield return new WaitForSecondsRealtime(1);
        myLoadingScreen.gameObject.SetActive(false);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }

}
