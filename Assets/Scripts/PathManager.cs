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
    PathTile[,] myPathTiles;

    public List<PathTileIntersection> PathTileIntersectionList { get { return myPathIntersectionlist; } set { myPathIntersectionlist = value; } }
    public PathTile[,] GetPathTileMap { get { return myPathTiles; } }
    public PathTile GetLastPlacedTile { get { return myLastPlacedPathTile; } set { myLastPlacedPathTile = value; } }
    public List<Vector3> GetPathFromStart { get { return myPathList; } }

    public PathTileIntersection.Directions GetDirections { get { return myCurrectPathList; } set { myCurrectPathList = value; } }
    List<Vector3> myPathList;
    PathTileIntersection.Directions myCurrectPathList;

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

        myPlayerController.PlayerMoveList(myPathList, null);
        myStartPathTile = Instantiate(myPathTilePrefab, new Vector3(Mathf.FloorToInt(myPlayerController.transform.position.x), 0, Mathf.FloorToInt(myPlayerController.transform.position.z)), Quaternion.identity);
        myStartPathTile.GetPathTilePosition = new Vector3(Mathf.FloorToInt(myPlayerController.transform.position.x), 0, Mathf.FloorToInt(myPlayerController.transform.position.z));

        myPathTiles = new PathTile[WorldController.Instance.GetWorldWidth, WorldController.Instance.GetWorldDepth];

        myLastPlacedPathTile = myStartPathTile;

        AddItemToMap(myStartPathTile, myPathList, null);

        myPathTiles[(int)myEndTile.GetPathTilePosition.x, (int)myEndTile.GetPathTilePosition.z] = myEndTile;
    }

    public void AddItemToMap(PathTile aPathTileToAdd, List<Vector3> aList, PathTileIntersection pathTileIdentifier)
    {
        int x = Mathf.FloorToInt(aPathTileToAdd.GetPathTilePosition.x);
        int z = Mathf.FloorToInt(aPathTileToAdd.GetPathTilePosition.z);

        if (myPathTiles[x, z] != myEndTile)
        {
            myPathTiles[x, z] = aPathTileToAdd;
            aList.Add(aPathTileToAdd.transform.position);

            if (aPathTileToAdd.GetType() != typeof(PathTileIntersection))
            {
                if (pathTileIdentifier != null)
                {
                    myLastPlacedPathTile = aPathTileToAdd;
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
    public List<Vector3> GetMyLastPlacedTiles()
    {

        List<Vector3> somePathTiles = new List<Vector3>();
        somePathTiles.Add(myLastPlacedPathTile.transform.position);
        for (int j = 0; j < myPathIntersectionlist.Count; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                if (myPathIntersectionlist[j].GetPathTileLists[i].Count != 0)
                {
                    somePathTiles.Add(myPathIntersectionlist[j].GetPathTileLists[i][myPathIntersectionlist[j].GetPathTileLists[i].Count - 1]);
                }
                else
                {
                    somePathTiles.Add(myPathIntersectionlist[j].transform.position);
                }
            }
        }
        return somePathTiles;
    }
    public void CheckIfPlacedNextToIntersection(PathTileIntersection aIntersection, List<Vector3> aList)
    {
        int x = Mathf.FloorToInt(aIntersection.transform.position.x);
        int z = Mathf.FloorToInt(aIntersection.transform.position.z);

        List<Vector3> someLastPlacedTiles = new List<Vector3>();
        someLastPlacedTiles = GetMyLastPlacedTiles();



        for (int i = 0; i < someLastPlacedTiles.Count; i++)
        {
            Debug.Log(myPathTiles[x, z - 1].transform.position);
            if (x - 1 >= 0)
            {
                if (myPathTiles[x - 1, z] != null)
                {
                    if (myPathTiles[x - 1, z].transform.position == someLastPlacedTiles[i] && aIntersection.GetPathTileLists[0].Count == 0)
                    {
                        // Fix myPathManager.GetPathFromStart to be dynamic
                        aIntersection.AddExitingListToIntersection(aList, PathTileIntersection.Directions.left);
                    }
                }
            }
            if (x + 1 < WorldController.Instance.GetWorldWidth)
            {
                Debug.Log("Made it through world bounds");
                if (myPathTiles[x + 1, z] != null)
                {
                    Debug.Log("Made it through null check");
                    if (myPathTiles[x + 1, z].transform.position == someLastPlacedTiles[i] && aIntersection.GetPathTileLists[2].Count == 0)
                    {
                        Debug.Log("add list");
                        aIntersection.AddExitingListToIntersection(aList, PathTileIntersection.Directions.right);
                    }
                }
            }
            if (z - 1 >= 0)
            {
                Debug.Log("Made it through world bounds");
                if (myPathTiles[x, z - 1] != null)
                {
                    Debug.Log("Made it through null check");
                    if (myPathTiles[x, z - 1].transform.position == someLastPlacedTiles[i] && aIntersection.GetPathTileLists[3].Count == 0)
                    {
                        Debug.Log("add list");
                        aIntersection.AddExitingListToIntersection(aList, PathTileIntersection.Directions.down);
                    }
                }
            }
            if (z + 1 < WorldController.Instance.GetWorldDepth)
            {
                Debug.Log("Made it through world bounds");
                if (myPathTiles[x, z + 1] != null)
                {
                    Debug.Log("Made it through null check");
                    if (myPathTiles[x, z + 1].transform.position == someLastPlacedTiles[i] && aIntersection.GetPathTileLists[1].Count == 0)
                    {
                        Debug.Log("add list");
                        aIntersection.AddExitingListToIntersection(aList, PathTileIntersection.Directions.up);
                    }
                }
            }
        }
    }
    public bool CheckPlacement(Vector3 aPosition)
    {
        int x = (int)aPosition.x;
        int z = (int)aPosition.z;
        List<Vector3> someLastPlacedTiles = new List<Vector3>();
        someLastPlacedTiles = GetMyLastPlacedTiles();



        for (int i = 0; i < someLastPlacedTiles.Count; i++)
        {
            if (x - 1 >= 0)
            {

                if (myPathTiles[x - 1, z] != null)
                {
                    if (myPathTiles[x - 1, z].transform.position == someLastPlacedTiles[i])
                    {
                        return true;
                    }
                }
            }
            if (x + 1 < WorldController.Instance.GetWorldWidth)
            {
                if (myPathTiles[x + 1, z] != null)
                {
                    if (myPathTiles[x + 1, z].transform.position == someLastPlacedTiles[i])
                    {
                        return true;
                    }
                }
            }
            if (z - 1 >= 0)
            {
                if (myPathTiles[x, z - 1] != null)
                {
                    if (myPathTiles[x, z - 1].transform.position == someLastPlacedTiles[i])
                    {
                        return true;
                    }
                }
            }
            if (z + 1 < WorldController.Instance.GetWorldDepth)
            {
                if (myPathTiles[x, z + 1] != null)
                {
                    if (myPathTiles[x, z + 1].transform.position == someLastPlacedTiles[i])
                    {
                        return true;
                    }
                }
            }
        }
        return false;

    }

}
