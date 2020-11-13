using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
   Vector3Int myInputCoordinates;

   Tile tile;


   private void Update()
   {
      //TOUCH INPUT
      if (Input.touchCount > 0)
      {
         myInputCoordinates = Vector3Int.FloorToInt(GetTouchCoordinates());
      }

      //LEFT CLICK INPUT
      //if (Input.GetMouseButtonDown(0))
      //{
      //   myInputCoordinates = Vector3Int.FloorToInt(GetClickCoordinates());
      //   Debug.Log(myInputCoordinates);
      //   if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.empty)
      //   {
      //      tile = WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z);
      //      tile.GetSetTileState = Tile.TileState.obstructed;
      //      WorldController.Instance.GetWorld.CopySetTile(tile);
      //   }
      //   Debug.Log(WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState);
      //}
      if (Input.GetMouseButton(0))
      {
         myInputCoordinates = Vector3Int.FloorToInt(GetClickCoordinates());
         Debug.Log(myInputCoordinates);
         if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.empty)
         {
            tile = WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z);
            tile.GetSetTileState = Tile.TileState.obstructed;
            WorldController.Instance.GetWorld.CopySetTile(tile);
         }
         Debug.Log(WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState);
      }

      //RIGHT CLICK INPUT
      //if (Input.GetMouseButtonDown(1))
      //{
      //   myInputCoordinates = Vector3Int.FloorToInt(GetClickCoordinates());
      //   Debug.Log(myInputCoordinates);
      //   if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.obstructed)
      //   {
      //      tile = WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z);
      //      tile.GetSetTileState = Tile.TileState.empty;
      //      WorldController.Instance.GetWorld.CopySetTile(tile);
      //   }
      //   Debug.Log(WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState);
      //}
      if (Input.GetMouseButton(1))
      {
         myInputCoordinates = Vector3Int.FloorToInt(GetClickCoordinates());
         Debug.Log(myInputCoordinates);
         if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.obstructed)
         {
            tile = WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z);
            tile.GetSetTileState = Tile.TileState.empty;
            WorldController.Instance.GetWorld.CopySetTile(tile);
         }
         Debug.Log(WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState);
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
