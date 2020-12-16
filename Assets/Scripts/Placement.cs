using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
   bool isPlaceingByPortal = false;
   bool myIsEnded = false;
   List<bool> myValidPlacement = new List<bool>();

   List<bool> myBooleansPortal = new List<bool>();

   [SerializeField]
   PathManager myPathManager;
   [SerializeField] private BuildManager myBuildManager;
   private Vector3Int myInputCoordinates;
   private Tile myTile;
   [SerializeField]
   PathTile temp;
   [SerializeField]
   Sc_shopUIVertical myShopOption;
   public bool GetSetIsEnded { get { return myIsEnded; } set { myIsEnded = value; } }
   private void Start()
   {
      for (int i = 0; i < myPathManager.GetPortals.Count; i++)
      {
         myBooleansPortal.Add(isPlaceingByPortal);
         myValidPlacement.Add(isPlaceingByPortal);
      }
   }
   private void OnValidate()
   {
      myShopOption = FindObjectOfType<Sc_shopUIVertical>();
   }
   private void Update()
   {
      if (!myIsEnded)
      {
         if (Input.GetMouseButton(0))
         {

            //Avrundar spelarens input till integers
            myInputCoordinates.x = Mathf.RoundToInt(GetClickCoordinates().x);
            myInputCoordinates.z = Mathf.RoundToInt(GetClickCoordinates().z);

            myInputCoordinates.Clamp(new Vector3Int(0, 0, 0), new Vector3Int(WorldController.Instance.GetWorldWidth - 1, 0, WorldController.Instance.GetWorldDepth - 1));

            //Kollar om en tile är upptagen
            if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.empty)
            {
               PlacementLogic();
            }
         }
         //else
         //{
         //    if (Input.GetMouseButton(0))
         //    {
         //        //Avrundar spelarens input till integers
         //        myInputCoordinates.x = Mathf.RoundToInt(GetClickCoordinates().x);
         //        myInputCoordinates.z = Mathf.RoundToInt(GetClickCoordinates().z);

         //        myInputCoordinates.Clamp(new Vector3Int(0, 0, 0), new Vector3Int(10, 0, 10));

         //        //Kollar om en tile är upptagen
         //        //if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.road)
         //        //{

         //        //    AudioManager.ourInstance.PlayEffect(AudioManager.EEffects.REMOVE);
         //        //    myBuildManager.ResetTilesPositionRemoval();
         //        //    myPathManager.DeleteTile(myInputCoordinates);
         //        //    //Sätter tilen till empty

         //        //    //Reset:ar tile-objektet

         //        //}
         //    }
         //}
      }
   }
   bool CheckForPortal()
   {
      for (int i = 0; i < myPathManager.GetPortals.Count; i++)
      {
         if (myPathManager.CheckPlacement(myInputCoordinates, myPathManager.GetPortals[i].myStartTile))
         {
            return true;
         }
      }
      return false;
   }
   void PlacementLogic()
   {
      for (int i = 0; i < myBooleansPortal.Count; i++)
      {
         if (myBooleansPortal[i] == false)
         {
            AddToPortalListLogic();
         }
      }


      if (myPathManager.CheckPlacement(myInputCoordinates, myPathManager.GetLastPlacedTile))
      {
         PathTile path = myBuildManager.SpawnFromPool(1, Quaternion.identity, myInputCoordinates);
         path.GetPathTilePosition = myInputCoordinates;

         if (myBooleansPortal.Count > 0)
         {
            for (int i = 0; i < myBooleansPortal.Count; i++)
            {
               if (myBooleansPortal[i] == true)
               {
                  myPathManager.AddItemToPortalMap(path, i);
                  path.CheckNeighbors();
               }
               else
               {
                  myPathManager.AddItemToMap(path);
               }
               path.CheckNeighbors();

               WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z, Tile.TileState.road);
            }
         }
         else
         {
            myPathManager.AddItemToMap(path);
         }
         path.CheckNeighbors();

         WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z, Tile.TileState.road);



      }
   }
   void AddToPortalListLogic()
   {
      for (int i = 0; i < myPathManager.GetPortals.Count; i++)
      {

         if (myPathManager.CheckPlacement(myInputCoordinates, myPathManager.GetPortals[i].myStartTile))
         {
            if (myValidPlacement[i] == true)
            {
               PathTile path = myBuildManager.SpawnFromPool(1, Quaternion.identity, myInputCoordinates);
               path.GetPathTilePosition = myInputCoordinates;
               //myPathManager.GetPathTileMap[myInputCoordinates.x, myInputCoordinates.z] = path;
               myPathManager.AddItemToPortalMap(path, i);
               path.CheckNeighbors();
               Debug.Log("Add item to portal list");
               WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z, Tile.TileState.road);

               if (myBooleansPortal[i] == false)
               {
                  myBooleansPortal[i] = true;
               }
            }


            //isPlaceingByPortal = true;
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

   public void SetValidPlacement(int index)
   {
      myValidPlacement[index] = true;
   }

}
