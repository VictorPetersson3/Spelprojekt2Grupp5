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

   [SerializeField] private Scrollbar myScollbar;

   private bool myMusicStatus = true;
   public enum EAmbience { CHIMES, WIND };
   public enum EEffects { COIN, FOOTSTEPS, LOSS, PORTAL, PRESSUREPLATE, OPENDOOR, CLOSEDOOR, PLACE, REMOVE, WIN };
   public enum EMusic { SYNTH, FUJU };
   public enum EUI { UI1, UI2, UI3 };

   private List<AudioSource> myAudioSources = new List<AudioSource>();
   //private List<bool> myAudioWasPlaying = new List<bool>();
   //private float myLastTimeScale;

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
            //myAudioWasPlaying.Add(false);
         }
      }
      //myLastTimeScale = Time.timeScale;
   }
   public void MasterVolume()
   {
      float vol = myScollbar.value;
      myAudioMixer.SetFloat("MasterVolume", vol);
   }
   public void MusicVolume()
   {
      if (myMusicStatus)
      {
         myAudioMixer.SetFloat("MusicVolume", -80.0f);
         myMusicStatus = false; 
      }
      else
      {
         myAudioMixer.SetFloat("MusicVolume", 0);
         myMusicStatus = true;
      }
   }

   //private void CheckPause()
   //{

   //}
   private void Update()
   {
      MasterVolume();
   }

   //Play-metoder
   public void PlayAmbience(EAmbience anEnum)
   {
      myAmbience[(int)anEnum].Play();
   }
   public void PlayEffect(EEffects anEnum)
   {
      myEffects[(int)anEnum].Play();
   }
   public void PlayMusic(EMusic anEnum)
   {
      //switch(SceneManager.GetActiveScene().buildIndex)
      myMusic[(int)anEnum].Play();
   }
   public void PlayUI(EUI anEnum)
   {
      myUI[(int)anEnum].Play();
   }


}
