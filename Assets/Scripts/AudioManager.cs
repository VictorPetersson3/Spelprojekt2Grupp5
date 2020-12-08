using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

   public enum EAmbience { CHIMES, WIND };
   public enum EEffects { COIN, FOOTSTEPS, LOSS, PORTAL, PRESSUREPLATE, DOOR, PLACE, REMOVE, WIN };
   public enum EMusic { SYNTH, FUJU };
   public enum EUI { UI1, UI2, UI3 };

   private List<AudioSource> myAudioSources = new List<AudioSource>();
   private List<bool> myAudioWasPlaying = new List<bool>();
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
            myAudioWasPlaying.Add(false);
         }
      }
      //myLastTimeScale = Time.timeScale;
   }
   public void VolumeSettings(float aVolume)
   {
      AudioListener.volume = aVolume;
   }
   //private void CheckPause()
   //{

   //}
   private void Update()
   {
      //CheckPause();   
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
