using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_shopUIVertical : MonoBehaviour
{

    public LeanTweenType easeType;

    
    [SerializeField]
    GameObject myRemoveTiles;
    [SerializeField]
    GameObject myPlaceTiles;
    private bool myIsPlacingTiles = true;

    public bool GetIsPlacingTiles()
    {
        return myIsPlacingTiles;
    }

    public void SwitchIconOnPlaceTiles()
    {
        if(myIsPlacingTiles)
        {
            myRemoveTiles.SetActive(true);
            myPlaceTiles.SetActive(false);
            myIsPlacingTiles = false;
        }
        else
        {
            myRemoveTiles.SetActive(false);
            myPlaceTiles.SetActive(true);
            myIsPlacingTiles = true;
        }
    }

   
}
