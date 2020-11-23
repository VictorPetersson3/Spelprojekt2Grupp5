using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField]
    PlayerController myPlayerController;

    [SerializeField]
    PathTile pathTilePrefab;

    PathTile myStartPathTile;
    PathTile myLastPlacedPathTile;

    PathTile[,] myPathTiles;

    List<PathTile> myPath;

    void Start()
    {
        myPath = new List<PathTile>();
        myStartPathTile = Instantiate(pathTilePrefab, new Vector3(Mathf.FloorToInt(myPlayerController.transform.position.x), 0, Mathf.FloorToInt(myPlayerController.transform.position.z)), Quaternion.identity);
        myStartPathTile.GetPathTilePosition = new Vector3(Mathf.FloorToInt(myPlayerController.transform.position.x), 0, Mathf.FloorToInt(myPlayerController.transform.position.z));
        myPathTiles = new PathTile[WorldController.Instance.GetWorldWidth, WorldController.Instance.GetWorldDepth];
        myLastPlacedPathTile = myStartPathTile;
        AddItemToMap(myStartPathTile);
    }
    public void AddItemToMap(PathTile aPathTileToAdd)
    {
        int x = Mathf.FloorToInt(aPathTileToAdd.GetPathTilePosition.x);
        int z = Mathf.FloorToInt(aPathTileToAdd.GetPathTilePosition.z);

        myPathTiles[x, z] = aPathTileToAdd;
        myPath.Add(aPathTileToAdd);
        myLastPlacedPathTile = aPathTileToAdd;
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
                Debug.Log("Return true");
                return true;
            }
        }
        if (x + 1 < WorldController.Instance.GetWorldWidth)
        { 
            if (myPathTiles[x + 1, z] == myLastPlacedPathTile)
            {
                Debug.Log("Return true");
                return true;
            }
        }
        if (z - 1 >= 0)
        {
            if (myPathTiles[x, z - 1] == myLastPlacedPathTile)
            {
                Debug.Log("Return true");
                return true;
            }
        }
        if (z + 1 < WorldController.Instance.GetWorldDepth)
        {
            if (myPathTiles[x, z + 1] == myLastPlacedPathTile)
            {
                Debug.Log("Return true");
                return true;
            }
        }
        Debug.Log("Return false");
        return false;

    }

}
