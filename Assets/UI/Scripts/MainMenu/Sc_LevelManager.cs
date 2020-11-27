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
        Debug.Log("I have happened twice");
        if (myInstance == null)
        {
            myInstance = this;
            SceneManager.LoadSceneAsync((int)Sc_SceneIndexes.eSceneIndexes.eMainMenu, LoadSceneMode.Additive);
            myCurrentSceneIndex = (int)Sc_SceneIndexes.eSceneIndexes.eMainMenu;
            
            Debug.LogError(myCurrentSceneIndex);

        }
    }

    
    public void LoadGame(int aSceneIndex)
    {
        Debug.LogError(myCurrentSceneIndex);
        myLoadingScreen.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync((int)myCurrentSceneIndex);
        myCurrentSceneIndex = aSceneIndex;
        Debug.LogError(myCurrentSceneIndex);
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
        //while (myTimer < 20)
        //{
        //    myTimer += Time.deltaTime;
        //    yield return null;
        //}
        myLoadingScreen.gameObject.SetActive(false);

    }
    //void DelaySpawn()
    //{
    //    SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    //    myLoadingScreen.gameObject.SetActive(false);
    //}

    public void QuitApplication()
    {
        Application.Quit();
    }
}
