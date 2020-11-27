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
    List<PathTileIntersection> myPathIntersectionlist;
    [SerializeField]
    List<Portals> myPortals = new List<Portals>();
    PathTile[,] myPathTiles;

    public List<PathTileIntersection> PathTileIntersectionList { get { return myPathIntersectionlist; } set { myPathIntersectionlist = value; } }
    public PathTile[,] GetPathTileMap { get { return myPathTiles; } }
    public PathTile GetLastPlacedTile { get { return myLastPlacedPathTile; } set { myLastPlacedPathTile = value; } }
    public List<Vector3> GetPathFromStart { get { return myPathList; } }

    public List<Portals> GetPortals { get { return myPortals; } }

    List<Vector3> myPathList; 



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
        myPathIntersectionlist = new List<PathTileIntersection>();
        myPathList = new List<Vector3>();
        myPlayerController.PlayerMoveList = myPathList;
        myStartPathTile = Instantiate(myPathTilePrefab, new Vector3(Mathf.FloorToInt(myPlayerController.transform.position.x), 0, Mathf.FloorToInt(myPlayerController.transform.position.z)), Quaternion.identity);
        myStartPathTile.GetPathTilePosition = new Vector3(Mathf.FloorToInt(myPlayerController.transform.position.x), 0, Mathf.FloorToInt(myPlayerController.transform.position.z));

        myPathTiles = new PathTile[WorldController.Instance.GetWorldWidth, WorldController.Instance.GetWorldDepth];

        myLastPlacedPathTile = myStartPathTile;

        InstantiateFirstPortalExitTile();

        AddItemToMap(myStartPathTile, myPathList, null);

        myPathTiles[(int)myEndTile.GetPathTilePosition.x, (int)myEndTile.GetPathTilePosition.z] = myEndTile;
    }

    private void InstantiateFirstPortalExitTile()
    {
        PathTile temp = Instantiate(myPathTilePrefab, myPortals[0].GetExit() + myPortals[0].transform.position, Quaternion.identity);
        temp.GetPathTilePosition = myPortals[0].GetExit() + myPortals[0].transform.position;
        GetPathTileMap[Mathf.FloorToInt(myPortals[0].GetExit().x + myPortals[0].transform.position.x), Mathf.FloorToInt(myPortals[0].GetExit().z + myPortals[0].transform.position.z)] = temp;
        AddItemToPortalMap(temp);
        GetPortals[0].GetSetLastPathTile = temp;
        WorldController.Instance.GetWorld.SetTileState(Mathf.FloorToInt(myPortals[0].GetExit().x + myPortals[0].transform.position.x), Mathf.FloorToInt(myPortals[0].GetExit().z + myPortals[0].transform.position.z), Tile.TileState.obstructed);
    }

    public void AddItemToPortalMap(PathTile aPathTileToAdd)
    {
        print(aPathTileToAdd.transform.position);
        myPortals[0].AddVectorToMovementList(aPathTileToAdd);
        
    }

    public void AddItemToMap(PathTile aPathTileToAdd, List<Vector3> aList, PathTileIntersection pathTileIdeifier)
    {
        int x = Mathf.FloorToInt(aPathTileToAdd.GetPathTilePosition.x);
        int z = Mathf.FloorToInt(aPathTileToAdd.GetPathTilePosition.z);

        if (myPathTiles[x, z] != myEndTile)
        {
            myPathTiles[x, z] = aPathTileToAdd;
            aList.Add(aPathTileToAdd.transform.position);

            if (aPathTileToAdd.GetType() != typeof(PathTileIntersection))
            {
                if (pathTileIdeifier != null)
                {
                    pathTileIdeifier.GetSetLastPlacedTile = aPathTileToAdd;
                }
                else
                {
                    myLastPlacedPathTile = aPathTileToAdd;
                }
            }
            else
            {
                myPathIntersectionlist.Add((PathTileIntersection)aPathTileToAdd);
            }
        }
        else
        {
            aList.Add(myEndTile.transform.position);
            myPathTiles[x, z] = myEndTile;
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
