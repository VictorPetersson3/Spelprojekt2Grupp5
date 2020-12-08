using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_Options_MusicButton : MonoBehaviour
{
    [SerializeField]
    GameObject myMusicOn;
    [SerializeField]
    GameObject myMusicOff;

    bool myMusicBoolOn = true;

    public void TurnOffMusic()
    {
        if(myMusicBoolOn)
        {
            myMusicOff.SetActive(true);
            myMusicOn.SetActive(false);
            myMusicBoolOn = false;
        }
        else
        {
            myMusicOff.SetActive(false);
            myMusicOn.SetActive(true);
            myMusicBoolOn = true;
        }
    }
}
