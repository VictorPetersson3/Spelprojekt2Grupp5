using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
   [SerializeField] private BuildManager myBuildManager;
   private Vector3Int myInputCoordinates;
   private Tile myTile;


   private void Update()
   {
      //TOUCH INPUT - Oklart om det behövs
      if (Input.touchCount > 0)
      {
         myInputCoordinates = Vector3Int.FloorToInt(GetTouchCoordinates());
      }

      //LEFT CLICK INPUT
      if (Input.GetMouseButton(0))
      {
         //Floorar spelarens input till integers
         myInputCoordinates = Vector3Int.FloorToInt(GetClickCoordinates());

         myInputCoordinates.Clamp(new Vector3Int(0, 0, 0), new Vector3Int(10, 0, 10));

         //Kollar om en tile är upptagen
         if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.empty)
         {
            //Spawnar en tile
            myBuildManager.SpawnFromPool("Sphere", Quaternion.identity).transform.position = myInputCoordinates;

            //Sätter tilen till obstructed
            WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z,Tile.TileState.obstructed);
         }
      }

      //RIGHT CLICK INPUT
      if (Input.GetMouseButton(1))
      {
         myInputCoordinates = Vector3Int.FloorToInt(GetClickCoordinates());
         Debug.Log("");
         myInputCoordinates.Clamp(new Vector3Int(0, 0, 0), new Vector3Int(10, 0, 10));

         if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.empty)
         {
            //Spawnar en tile
            myBuildManager.SpawnFromPool("Cube", Quaternion.identity).transform.position = myInputCoordinates;

            //Sätter tilen till obstructed
            WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z, Tile.TileState.obstructed);
            
            
            //Sätter tilen till empty
            //WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z, Tile.TileState.empty);
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

      return input;
   }
}
