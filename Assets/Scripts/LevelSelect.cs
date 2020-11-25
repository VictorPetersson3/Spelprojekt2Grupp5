using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
   private GameObject mySelectedLevel;
   void Update()
   {
      if (Input.GetMouseButtonDown(0) && Input.touchCount == 1)
      {
         mySelectedLevel = WhatDidIHit();
         Camera.main.gameObject.GetComponent<LevelSelectCamera>().SetFocus(mySelectedLevel);
      }
   }
   public GameObject WhatDidIHit()
   {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit))
      {
         Debug.Log(hit.collider.gameObject);
         return hit.collider.gameObject;
      }
      return null;
   }
}
