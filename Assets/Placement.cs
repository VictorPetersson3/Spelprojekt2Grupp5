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

    int a = 1;
    private void Update()
    {
        //LEFT CLICK INPUT
        if (Input.GetMouseButton(0))
        {
            //Avrundar spelarens input till integers
            myInputCoordinates.x = Mathf.RoundToInt(GetClickCoordinates().x);
            myInputCoordinates.z = Mathf.RoundToInt(GetClickCoordinates().z);

            myInputCoordinates.Clamp(new Vector3Int(0, 0, 0), new Vector3Int(WorldController.Instance.GetWorldWidth, 0, WorldController.Instance.GetWorldDepth));

            //Kollar om en tile är upptagen
            if (WorldController.Instance.GetTileAtPosition(myInputCoordinates.x, myInputCoordinates.z).GetSetTileState == Tile.TileState.empty)
            {
                //Spawnar en tile
                //myBuildManager.SpawnFromPool("Cube", Quaternion.identity).transform.position = myInputCoordinates;

                if (myPathManager.CheckPlacement(myInputCoordinates))
                {
                    
                    PathTile path = Instantiate(temp, myInputCoordinates, transform.rotation);
                    path.name = "Path Tile " + a;
                    a++;
                    path.GetPathTilePosition = myInputCoordinates;
                    myPathManager.AddItemToMap(path);

                    //Sätter tilen till obstructed
                    WorldController.Instance.GetWorld.SetTileState(myInputCoordinates.x, myInputCoordinates.z, Tile.TileState.obstructed);
                }
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
