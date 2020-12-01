using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sc_LevelManager : MonoBehaviour
{
    public static Sc_LevelManager myInstance;
    public GameObject myLoadingScreen;

    private int myCurrentSceneIndex;
    
    
    private void Awake()
    {
        if (myInstance == null)
        {
            myInstance = this;
            SceneManager.LoadSceneAsync((int)Sc_SceneIndexes.eSceneIndexes.eMainMenu, LoadSceneMode.Additive);
            myCurrentSceneIndex = (int)Sc_SceneIndexes.eSceneIndexes.eMainMenu;
        }
    }

    
    public void LoadGame(int aSceneIndex)
    {
        myLoadingScreen.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync((int)myCurrentSceneIndex);
        myCurrentSceneIndex = aSceneIndex;
        //Invoke("DelaySpawn", 5.0f);
        StartCoroutine(CoRoutineLoad());
    }
    IEnumerator CoRoutineLoad()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(myCurrentSceneIndex, LoadSceneMode.Additive);
        while (!loadingOperation.isDone)
        {
            yield return null;
        }
        yield return new WaitForSecondsRealtime(1);
        myLoadingScreen.gameObject.SetActive(false);

    }
    public void QuitApplication()
    {
        Application.Quit();
    }
    
}
