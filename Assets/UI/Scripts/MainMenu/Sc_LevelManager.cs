using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_LevelManager : MonoBehaviour
{
    public static Sc_LevelManager myInstance;
    public GameObject myLoadingScreen;
    public GameManager myGameManager;

    private int myCurrentSceneIndex;
    private PathManager myPathManager;
    private bool myHasLoaded = false;
    private void Awake()
    {
        myPathManager = null;
        if (myInstance == null)
        {
            myInstance = this;
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
            myCurrentSceneIndex = 3;
        }
    }

    
    
    public void LoadGame(int aSceneIndex)
    {
        myLoadingScreen.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync((int)myCurrentSceneIndex);
        if (myPathManager != null)
        {
            myPathManager = null;
        }
        myCurrentSceneIndex = aSceneIndex;
        StartCoroutine(CoRoutineLoad());
        Invoke("InitGame", 1.40f);


    }

    IEnumerator CoRoutineLoad()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(myCurrentSceneIndex, LoadSceneMode.Additive);
        while (!loadingOperation.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(myCurrentSceneIndex));
        Debug.LogError("We have set an active Scene.");
        if (myPathManager == null)
        {
            myPathManager = FindObjectOfType<PathManager>();
        }
        yield return new WaitForSecondsRealtime(1);
        myLoadingScreen.gameObject.SetActive(false);
    }
    public void QuitApplication()
    {
        Application.Quit();
    }
    public void ReloadLevel()
    {
        LoadGame(myCurrentSceneIndex);
    }
    public void LoadMainMenu()
    {
        LoadGame(3);
    }
    void InitGame()
    {
        if (myPathManager != null)
        {
            Debug.LogError("We have Inited myPathManager.");
            myPathManager.init();
            myLoadingScreen.gameObject.SetActive(false);
        }
    }
}
