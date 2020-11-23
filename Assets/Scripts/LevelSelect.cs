using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
   [SerializeField] GameObject myWorld;
   private GameObject mySelectedLevel;
   void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         mySelectedLevel = WhatDidIHit();
         Debug.Log(mySelectedLevel);
      }
   }
   public GameObject WhatDidIHit()
   {
      Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
      RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
      if (hit)
      {
         Debug.Log("Funkar");
         return hit.collider.gameObject;
      }
      else
      {
         Debug.Log("Funkar inte");
         return myWorld;
      }
   }
}
