using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
   public static AudioManager ourInstance;

   [SerializeField] private AudioSource[] myMusic;
   [SerializeField] private AudioSource myTilePlacementSound;

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
   public void PlayMusic()
   {
      switch(SceneManager.GetActiveScene().buildIndex)
      {
         default:
            myMusic[0].Play();
            break;
      }
   }

}
