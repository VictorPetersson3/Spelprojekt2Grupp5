using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : MonoBehaviour
{
    [SerializeField]
    bool isEndTile = false;
    public PathManager myPathManager;
    Vector3 myPosition;
    PathTileIntersection myIntersection = null;



    public Vector3 GetPathTilePosition { get { return myPosition; } set { myPosition = value; } }
    public bool IsEndTile { get { return isEndTile; } }
    public PathTileIntersection GetSetMyIntersection { get { return myIntersection; } set { myIntersection = value; } }

    public virtual void Start()
    {
        myPathManager = FindObjectOfType<PathManager>();
        myPosition = new Vector3(Mathf.FloorToInt(transform.position.x), 0, Mathf.FloorToInt(transform.position.z));
        transform.position = myPosition;
        CheckNeighbors();

    }
    void CheckLeft(int aX, int aZ)
    {
        if (aX - 1 >= 0)
        {
            for (int i = 0; i < myPathManager.PathTileIntersectionList.Count; i++)
            {
                if (myPathManager.GetPathTileMap[aX - 1, aZ] == myPathManager.PathTileIntersectionList[i])
                {
                    myPathManager.GetDirections = PathTileIntersection.Directions.right;
                    myPathManager.PathTileIntersectionList[i].AddNewPathToIntersection(myPathManager.GetDirections);
                    myIntersection = myPathManager.PathTileIntersectionList[i];
                }

            }
        }
    }
    void CheckRight(int aX, int aZ)
    {
        if (aX + 1 < WorldController.Instance.GetWorldWidth)
        {
            for (int i = 0; i < myPathManager.PathTileIntersectionList.Count; i++)
            {
                if (myPathManager.GetPathTileMap[aX + 1, aZ] == myPathManager.PathTileIntersectionList[i])
                {
                    myPathManager.GetDirections = PathTileIntersection.Directions.left;
                    myPathManager.PathTileIntersectionList[i].AddNewPathToIntersection(myPathManager.GetDirections);
                    myIntersection = myPathManager.PathTileIntersectionList[i];
                }


            }
        }
    }
    void CheckUp(int aX, int aZ)
    {
        if (aZ + 1 < WorldController.Instance.GetWorldDepth)
        {
            for (int i = 0; i < myPathManager.PathTileIntersectionList.Count; i++)
            {
                if (myPathManager.GetPathTileMap[aX, aZ + 1] == myPathManager.PathTileIntersectionList[i])
                {
                    Debug.Log(myPathManager.PathTileIntersectionList[0]);
                    myPathManager.GetDirections = PathTileIntersection.Directions.down;
                    myPathManager.PathTileIntersectionList[i].AddNewPathToIntersection(myPathManager.GetDirections);
                    myIntersection = myPathManager.PathTileIntersectionList[i];
                }

            }
        }
    }
    void CheckDown(int aX, int aZ)
    {

        if (aZ - 1 >= 0)
        {
            for (int i = 0; i < myPathManager.PathTileIntersectionList.Count; i++)
            {
                if (myPathManager.GetPathTileMap[aX, aZ - 1] == myPathManager.PathTileIntersectionList[i])
                {

                    myPathManager.GetDirections = PathTileIntersection.Directions.up;
                    myPathManager.PathTileIntersectionList[i].AddNewPathToIntersection(myPathManager.GetDirections);
                    myIntersection = myPathManager.PathTileIntersectionList[i];
                }

            }
        }
    }

    void CheckNeighbors()
    {
        int x = Mathf.FloorToInt(transform.position.x);
        int z = Mathf.FloorToInt(transform.position.z);

        List<Vector3> someTiles = myPathManager.GetMyLastPlacedTiles();
        if (myPathManager.PathTileIntersectionList != null && myIntersection == null)
        {
            if (myPathManager.PathTileIntersectionList.Count != 0)
            {
                CheckLeft(x,z);
                CheckRight(x, z);
                CheckDown(x, z);
                CheckUp(x, z);
            }
        }
    }


}
