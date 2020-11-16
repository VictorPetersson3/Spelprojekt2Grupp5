using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
   BuildManager myBuildManager;
   private Vector3Int myInputCoordinates;
   private Tile myTile;


   private void Update()
   {
      //TOUCH INPUT
      if (Input.touchCount > 0)
      {
         myInputCoordinates = Vector3Int.FloorToInt(GetTouchCoordinates());
      }

      //LEFT CLICK INPUT
      if (Input.GetMouseButton(0))
      {
         myInputCoordinates = Vector3Int.FloorToInt(GetClickCoordinates());
         Debug.Log(myInputCoordinates);
         if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.empty)
         {
            //Spawnar en tile

            //Sätter tilen till obstructed
            myTile = WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z);
            myTile.GetSetTileState = Tile.TileState.obstructed;
            WorldController.Instance.GetWorld.CopySetTile(myTile);
         }
      }

      //RIGHT CLICK INPUT
      if (Input.GetMouseButton(1))
      {
         myInputCoordinates = Vector3Int.FloorToInt(GetClickCoordinates());
         Debug.Log(myInputCoordinates);
         if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.obstructed)
         {
            myTile = WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z);
            myTile.GetSetTileState = Tile.TileState.empty;
            WorldController.Instance.GetWorld.CopySetTile(myTile);
         }
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
