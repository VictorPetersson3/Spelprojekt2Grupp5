using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sc_InterfaceLevelManager : MonoBehaviour
{

   
    // Start is called before the first frame update
    public void LoadLevel(int aSceneIndex)
    {
        Sc_LevelManager.myInstance.LoadGame(aSceneIndex);
    }
    public void QuitApplication()
    {
        Sc_LevelManager.myInstance.QuitApplication();
    }
}
