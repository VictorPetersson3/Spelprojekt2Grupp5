using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sc_GameManager : MonoBehaviour
{

    public static Sc_GameManager myInstance;
    public GameObject myLoadingScreen;

    private void Awake()
    {
        myInstance = this;

        SceneManager.LoadSceneAsync((int)Sc_SceneIndexes.eSceneIndexes.eMainMenu, LoadSceneMode.Additive);
    }

    public void LoadGame()
    {
        myLoadingScreen.gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync((int)Sc_SceneIndexes.eSceneIndexes.eMainMenu);
        Invoke("DelaySpawn", 5.0f);
    }
    void DelaySpawn()
    {
        SceneManager.LoadSceneAsync((int)Sc_SceneIndexes.eSceneIndexes.eLevel01, LoadSceneMode.Additive);
    }

}
