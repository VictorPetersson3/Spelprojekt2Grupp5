using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
   public static AudioManager ourInstance;

   [SerializeField] private AudioMixer myAudioMixer;

   [SerializeField] private AudioSource[] myAmbience;
   [SerializeField] private AudioSource[] myEffects;
   [SerializeField] private AudioSource[] myMusic;
   [SerializeField] private AudioSource[] myUI;


   private bool myMusicStatus = true;
   public enum EAmbience { CHIMES, WIND, NONE };
   public enum EEffects { COIN, FOOTSTEPS, LOSS, PORTAL, PRESSUREPLATE, OPENDOOR, CLOSEDOOR, PLACE, REMOVE, WIN };
   public enum EMusic { SYNTH, FUJU, NONE };
   public enum EUI { UI1, UI2, UI3 };

   [SerializeField] private EMusic myPlayingMusic;
   [SerializeField] private EAmbience myPlayingAmbience;

   private float myVolume = 1;
   private List<AudioSource> myAudioSources = new List<AudioSource>();

   private void Awake()
   {
      if (ourInstance == null)
      {
         ourInstance = this;
      }
      else
      {
         Destroy(this);
      }
      foreach (Transform child in transform)
      {
         if (child != transform)
         {
            AudioSource audioSource = child.GetComponent<AudioSource>();
            myAudioSources.Add(audioSource);
         }
      }
   }
   private void Start()
   {
      PlayMusic(myPlayingMusic);
      PlayAmbience(myPlayingAmbience);
   }
   public void MasterVolume()
   {
        myVolume = 40.0f * myVolume  - 40.0f;
      myAudioMixer.SetFloat("MasterVolume", myVolume);
   }
   public void MusicVolume()
   {
      if (myMusicStatus)
      {
         myAudioMixer.SetFloat("MusicVolume", -80.0f);
      }
      else
      {
         myAudioMixer.SetFloat("MusicVolume", 0);
      }
   }
   private void Update()
   {
      GetAudioStats();
      MasterVolume();
      MusicVolume();
    }

    //Play-metoder
   public void PlayAmbience(EAmbience anEnum)
   {
      if (anEnum == EAmbience.NONE) return;
      myAmbience[(int)anEnum].Play();
   }
   public void PlayEffect(EEffects anEnum)
   {
      myEffects[(int)anEnum].Play();
   }
   public void PlayMusic(EMusic anEnum)
   {
      if (anEnum == EMusic.NONE) return;
      myMusic[(int)anEnum].Play();
   }
   public void PlayUI(EUI anEnum)
   {
      myUI[(int)anEnum].Play();
   }

    public void StopWalkingEffect()
    {
        myEffects[(int)EEffects.FOOTSTEPS].Stop();
    }

    private void GetAudioStats()
    {
        myVolume = GameManager.globalInstance.GetAudioVolume();
        myMusicStatus = GameManager.globalInstance.GetPlayMusic();
    }
}
