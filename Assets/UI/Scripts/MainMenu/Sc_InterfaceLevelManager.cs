using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sc_InterfaceLevelManager : MonoBehaviour
{
        public void LoadLevel(int aSceneIndex)
    {
        GameManager.globalInstance.SetLevel(aSceneIndex);
        Sc_LevelManager.myInstance.LoadGame(aSceneIndex);
    }
    public void QuitApplication()
    {
        Sc_LevelManager.myInstance.QuitApplication();
    }

    public void RestartLevel()
    {
        Sc_LevelManager.myInstance.ReloadLevel();
    }

}
