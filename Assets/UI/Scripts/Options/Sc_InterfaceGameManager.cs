using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_InterfaceGameManager : MonoBehaviour
{
    public void ShowOptionsMenu()
    {
        GameManager.globalInstance.ShowOptionsMenu();
    }
}
