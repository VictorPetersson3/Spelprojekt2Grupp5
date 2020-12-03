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
    PathTile[,] myPathTiles;
    [SerializeField]
    PlacementIndicator myPlacementEffects;
   
    [SerializeField]
    BuildManager myBuildManager;
    
    List<PathTile> myPathList = new List<PathTile>(); 
    public PathTile[,] GetPathTileMap { get { return myPathTiles; } }
    public PathTile GetLastPlacedTile { get { return myLastPlacedPathTile; } set { myLastPlacedPathTile = value; } }
    

    public List<Portals> GetPortals { get { return myPortals; } }




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
   
        myPlayerController.PlayerMoveList = myPathList;
        myStartPathTile = Instantiate(myPathTilePrefab, new Vector3(Mathf.FloorToInt(myPlayerController.transform.position.x), 0, Mathf.FloorToInt(myPlayerController.transform.position.z)), Quaternion.identity);
       
        myStartPathTile.GetPathTilePosition = new Vector3(Mathf.FloorToInt(myPlayerController.transform.position.x), 0, Mathf.FloorToInt(myPlayerController.transform.position.z));

        myPathTiles = new PathTile[WorldController.Instance.GetWorldWidth, WorldController.Instance.GetWorldDepth];

        myLastPlacedPathTile = myStartPathTile;

        InstantiateFirstPortalExitTile();

        AddItemToMap(myStartPathTile);

        myPathTiles[(int)myEndTile.GetPathTilePosition.x, (int)myEndTile.GetPathTilePosition.z] = myEndTile;
    }
    private void Update()
    {
     
    }
    private void InstantiateFirstPortalExitTile()
    {
        for (int i = 0; i < myPortals.Count; i++)
        {
            PathTile temp = Instantiate(myPathTilePrefab, myPortals[i].GetExit() + myPortals[i].transform.position, Quaternion.identity);
            temp.SetPathManager = this;
            temp.GetPathTilePosition = myPortals[i].GetExit() + myPortals[i].transform.position;
            GetPathTileMap[Mathf.FloorToInt(myPortals[i].GetExit().x + myPortals[i].transform.position.x), Mathf.FloorToInt(myPortals[i].GetExit().z + myPortals[i].transform.position.z)] = temp;
            AddItemToPortalMap(temp, i);
            GetPortals[i].GetSetLastPathTile = temp;
            WorldController.Instance.GetWorld.SetTileState(Mathf.FloorToInt(myPortals[i].GetExit().x + myPortals[i].transform.position.x), Mathf.FloorToInt(myPortals[i].GetExit().z + myPortals[i].transform.position.z), Tile.TileState.obstructed);
        }
    }

    public void AddItemToPortalMap(PathTile aPathTileToAdd, int index)
    {
        myPortals[index].AddVectorToMovementList(aPathTileToAdd);    
    }

    public void AddItemToMap(PathTile aPathTileToAdd)
    {
        int x = Mathf.FloorToInt(aPathTileToAdd.GetPathTilePosition.x);
        int z = Mathf.FloorToInt(aPathTileToAdd.GetPathTilePosition.z);

        if (myPathTiles[x, z] != myEndTile)
        {

            myPlacementEffects.transform.position = aPathTileToAdd.transform.position;
            myPlacementEffects.CheckPlacementIndicators();
            myPathTiles[x, z] = aPathTileToAdd;
            myPathList.Add(aPathTileToAdd);
        }
        else
        {
            myPathList.Add(myEndTile);
            myPathTiles[x, z] = myEndTile;
        }
    }
    public void DeleteTile(Vector3 aPosition)
    {
        int x = Mathf.FloorToInt(aPosition.x);
        int z = Mathf.FloorToInt(aPosition.z);
        
        if (myPathTiles[x, z] == myLastPlacedPathTile)
        {
            myLastPlacedPathTile.ResetMe();
            myBuildManager.ReturnToPool(myLastPlacedPathTile);
            myPathList.Remove(myLastPlacedPathTile);
            myLastPlacedPathTile = myPathList[myPathList.Count - 1];
        }
    }
    public bool CheckPlacement(Vector3 aPosition, PathTile aLastPlacedTile)
    {
        int x = Mathf.FloorToInt(aPosition.x);
        int z = Mathf.FloorToInt(aPosition.z);
        if (x - 1 >= 0)
        {
            if (myPathTiles[x - 1, z] == aLastPlacedTile)
            {
                return true;
            }
        }
        if (x + 1 < WorldController.Instance.GetWorldWidth)
        {
            if (myPathTiles[x + 1, z] == aLastPlacedTile)
            {
                return true;
            }
        }
        if (z - 1 >= 0)
        {
            if (myPathTiles[x, z - 1] == aLastPlacedTile)
            {
                return true;
            }
        }
        if (z + 1 < WorldController.Instance.GetWorldDepth)
        {
            if (myPathTiles[x, z + 1] == aLastPlacedTile)
            {
                return true;
            }
        }
        return false;

    }

}
