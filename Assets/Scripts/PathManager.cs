using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField]
    PlayerController myPlayerController;

    [SerializeField]
    PathTile myPathTilePrefab; 

    PathTile myStartPathTile;
    PathTile myLastPlacedPathTile;
    [SerializeField]
    PathTile myEndTile;
    [SerializeField]
    List<Portals> myPortals = new List<Portals>();

    public List<Portals> GetPortalTiles()
    {
        return myPortals;
    }

    PathTile[,] myPathTiles;

    List<PathTile> myPath;

    void OnValidate()
    {
        myPlayerController = FindObjectOfType<PlayerController>();
        PathTile[] objects = FindObjectsOfType<PathTile>();
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i].IsEndTile)
            {
                myEndTile = objects[i];
                break;
            }     
        }
    }
    void Start()
    {
        
        myPath = new List<PathTile>();
        myStartPathTile = Instantiate(myPathTilePrefab, new Vector3(Mathf.FloorToInt(myPlayerController.transform.position.x), 0, Mathf.FloorToInt(myPlayerController.transform.position.z)), Quaternion.identity);
        myStartPathTile.GetPathTilePosition = new Vector3(Mathf.FloorToInt(myPlayerController.transform.position.x), 0, Mathf.FloorToInt(myPlayerController.transform.position.z));
        myPathTiles = new PathTile[WorldController.Instance.GetWorldWidth, WorldController.Instance.GetWorldDepth];
        myLastPlacedPathTile = myStartPathTile;
        AddItemToMap(myStartPathTile);

        AddItemToPortalMap(Instantiate(myPathTilePrefab, myPortals[0].GetExit() + myPortals[0].transform.position, Quaternion.identity));
        
        myPathTiles[(int)myEndTile.GetPathTilePosition.x, (int)myEndTile.GetPathTilePosition.z] = myEndTile;
    }

    public void AddItemToPortalMap(PathTile aPathTileToAdd)
    {
        myPortals[0].AddVectorToMovementList(aPathTileToAdd);
    }
    public void AddItemToMap(PathTile aPathTileToAdd)
    {
        int x = Mathf.FloorToInt(aPathTileToAdd.GetPathTilePosition.x);
        int z = Mathf.FloorToInt(aPathTileToAdd.GetPathTilePosition.z);

        if (myPathTiles[x, z] != myEndTile)
        {
            myPathTiles[x, z] = aPathTileToAdd;
            myPath.Add(aPathTileToAdd);
            myLastPlacedPathTile = aPathTileToAdd;

            Debug.Log("Add noraml tile");
        }
        else
        {
            Debug.Log("Add end tile");
            myPath.Add(myEndTile);
            myPathTiles[x, z] = myEndTile;
        }
        
        myPlayerController.myMovementList = myPath;


    }
    public bool CheckPlacement(Vector3 aPosition)
    {
        int x = Mathf.FloorToInt(aPosition.x);
        int z = Mathf.FloorToInt(aPosition.z);
        if (x - 1 >= 0)
        {
            if (myPathTiles[x - 1, z] == myLastPlacedPathTile)
            {
                return true;
            }
        }
        if (x + 1 < WorldController.Instance.GetWorldWidth)
        { 
            if (myPathTiles[x + 1, z] == myLastPlacedPathTile)
            {
                return true;
            }
        }
        if (z - 1 >= 0)
        {
            if (myPathTiles[x, z - 1] == myLastPlacedPathTile)
            {
                return true;
            }
        }
        if (z + 1 < WorldController.Instance.GetWorldDepth)
        {
            if (myPathTiles[x, z + 1] == myLastPlacedPathTile)
            {
                return true;
            }
        }
        
        
        

        return false;

    }

}
