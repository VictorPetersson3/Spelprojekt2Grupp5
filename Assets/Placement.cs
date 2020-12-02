using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    [SerializeField]
    PathManager myPathManager;
    [SerializeField] private BuildManager myBuildManager;
    private Vector3Int myInputCoordinates;
    private Tile myTile;
    [SerializeField]
    PathTile temp;

    public bool isPlaceablePortal = false;
    private void Update()
    {
        //LEFT CLICK INPUT
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

        //RIGHT CLICK INPUT
        if (Input.GetMouseButton(1))
        {
            //Avrundar spelarens input till integers
            myInputCoordinates.x = Mathf.RoundToInt(GetClickCoordinates().x);
            myInputCoordinates.z = Mathf.RoundToInt(GetClickCoordinates().z);

            myInputCoordinates.Clamp(new Vector3Int(0, 0, 0), new Vector3Int(10, 0, 10));

            //Kollar om en tile är upptagen
            if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.obstructed)
            {
                GameObject temp = WhatDidIHit(myInputCoordinates);

                if (temp.tag == "Cube" || temp.tag == "Sphere")
                {
                    //Sätter tilen till empty
                    WorldController.Instance.GetWorld.SetTileState((int)temp.transform.position.x, (int)temp.transform.position.z, Tile.TileState.empty);

                    //Reset:ar tile-objektet
                    myBuildManager.ReturnToPool(temp);
                }
            }
        }
    }
    void PlacementLogic()
    {
        AddToPortalListLogic();

        //Spawnar en tile
        //myBuildManager.SpawnFromPool("Cube", Quaternion.identity).transform.position = myInputCoordinates;
        if (myPathManager.CheckPlacement(myInputCoordinates, myPathManager.GetLastPlacedTile))
        {
            if (isPlaceablePortal)
            {
                //PathTile path = myBuildManager.SpawnFromPool(2, Quaternion.identity, myInputCoordinates);

                //path.GetPathTilePosition = myInputCoordinates;
                //myPathManager.AddItemToMap(path);
                //path.CheckNeighbors();
                //WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z, Tile.TileState.obstructed);

                PathTile path = myBuildManager.SpawnFromPool(2, Quaternion.identity, myInputCoordinates);

                path.GetPathTilePosition = myInputCoordinates;
                myPathManager.AddItemToMap(path);
                path.CheckNeighbors();
                WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z, Tile.TileState.obstructed);
            }
            else
            {
                PathTile path = myBuildManager.SpawnFromPool(1, Quaternion.identity, myInputCoordinates);

                path.GetPathTilePosition = myInputCoordinates;
                myPathManager.AddItemToMap(path);
                path.CheckNeighbors();
                WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z, Tile.TileState.obstructed);
            }
            
        }
        //Sätter tilen till obstructed
    }


    private void AddToPortalListLogic()
    {
        for (int i = 0; i < myPathManager.GetPortals.Count; i++)
        {
            if (myPathManager.CheckPlacement(myInputCoordinates, myPathManager.GetPortals[i].GetSetLastPathTile) && myPathManager.GetPortals[i].GetSetLastPathTile != null)
            {
                if (isPlaceablePortal)
                {
                    print(myPathManager.GetPortals[i].GetSetLastPathTile);
                    //PathTile path = Instantiate(temp, myInputCoordinates, transform.rotation);
                    PathTile path = myBuildManager.SpawnFromPool(2, Quaternion.identity, myInputCoordinates);
                    path.GetPathTilePosition = myInputCoordinates;
                    myPathManager.GetPathTileMap[myInputCoordinates.x, myInputCoordinates.z] = path;
                    myPathManager.AddItemToOneTilePortalMap(path, i);
                    myPathManager.GetOneTilePortals[i].GetSetLastPathTile = path;
                }
                else
                {
                    print(myPathManager.GetPortals[i].GetSetLastPathTile);
                    //PathTile path = Instantiate(temp, myInputCoordinates, transform.rotation);
                    PathTile path = myBuildManager.SpawnFromPool(1, Quaternion.identity, myInputCoordinates);
                    path.GetPathTilePosition = myInputCoordinates;
                    myPathManager.GetPathTileMap[myInputCoordinates.x, myInputCoordinates.z] = path;
                    myPathManager.AddItemToPortalMap(path, i);
                    myPathManager.GetPortals[i].GetSetLastPathTile = path;
                }
                

                WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z, Tile.TileState.obstructed);
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
