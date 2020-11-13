using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{


   Vector3 myInputCoordinates;

   private void Update()
   {
      if (Input.touchCount > 0)
      {
         myInputCoordinates = GetTouchCoordinates();
      }
      if (Input.GetMouseButtonDown(0))
      {
         myInputCoordinates = GetClickCoordinates();
      }
   }
   public GameObject WhatDidIHit(Vector3 anInputType)
   {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit))
      {
         return hit.collider.gameObject;
      }
      return null;
   }
   public Vector3 WhereDidIHit(Vector3 anInputType)
   {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit))
      {
         return hit.point;
      }
      return new Vector3(0, 0, 0);
   }
   private Vector3 GetTouchCoordinates()
   {
      Vector3 input;
      Touch touch = Input.GetTouch(0); ;

      input = WhereDidIHit(touch.position);
      input.y = 0f;

      Debug.Log(input);

      return input;
   }
   private Vector3 GetClickCoordinates()
   {
      Vector3 input;

      input = WhereDidIHit(Input.mousePosition);
      input.y = 0f;

      Debug.Log(input);

      return input;
   }
}
