using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTile : MonoBehaviour
{
    bool isEndTile = false;
    PathManager myPathManager;
    Vector3 myPosition;
    public PathManager SetPathManager { set { myPathManager = value; } }
    public Vector3 GetPathTilePosition { get { return myPosition; } set { myPosition = value; } }
    public bool IsEndTile { get { return isEndTile; } }
    public Neighbor GetNeighbor { get { return myNeigbor; } }


    public GameObject myTurnRoad;

    public GameObject myStraightRoad;
    PathTile myPathTileNeighbors;

    Neighbor myNeigbor = Neighbor.none;
    public enum Neighbor
    {
        none,
        left,
        right,
        up,
        down
    }


    public virtual void Start()
    {
        myPosition = new Vector3(Mathf.FloorToInt(transform.position.x), 0, Mathf.FloorToInt(transform.position.z));
        transform.position = myPosition;
        if (myPathManager != null)
        {
            CheckNeighbors();
        }
    }
    public void CheckNeighbors()
    {
        int x = Mathf.FloorToInt(transform.position.x);
        int z = Mathf.FloorToInt(transform.position.z);

        if (x - 1 >= 0)
        {
            if (myPathManager.GetPathTileMap[x - 1, z] != null)
            {
                if (myPathManager.GetPathTileMap[x - 1, z] == myPathManager.GetLastPlacedTile)
                {
                    myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                    myNeigbor = Neighbor.left;
                    CheckOldNeighborLeft();
                    myPathManager.GetLastPlacedTile = this;
                }
            }
        }
        if (x + 1 < WorldController.Instance.GetWorldWidth)
        {
            if (myPathManager.GetPathTileMap[x + 1, z] != null)
            {
                if (myPathManager.GetPathTileMap[x + 1, z] == myPathManager.GetLastPlacedTile)
                {
                    myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                    myNeigbor = Neighbor.right;
                    CheckOldNeighborRight();
                    myPathManager.GetLastPlacedTile = this;
                }
            }
        }
        if (z - 1 >= 0)
        {
            Debug.Log("Check down");
            if (myPathManager.GetPathTileMap[x, z - 1] != null)
            {
                Debug.Log("Check down");
                if (myPathManager.GetPathTileMap[x, z - 1] == myPathManager.GetLastPlacedTile)
                {
                    myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                    myNeigbor = Neighbor.down;
                    CheckOldNeighborDown();
                    Debug.Log("Check down");
                    myPathManager.GetLastPlacedTile = this;
                }
            }
        }
        if (z + 1 < WorldController.Instance.GetWorldDepth)
        {
            if (myPathManager.GetPathTileMap[x, z + 1] != null)
            {
                if (myPathManager.GetPathTileMap[x, z + 1] == myPathManager.GetLastPlacedTile)
                {
                    myPathTileNeighbors = myPathManager.GetLastPlacedTile;
                    myNeigbor = Neighbor.up;
                    CheckOldNeighborUp();
                    myPathManager.GetLastPlacedTile = this;
                }
            }
        }
    }
    void CheckOldNeighborDown()
    {
        if (myPathTileNeighbors.GetNeighbor == Neighbor.left )
        {
            // left down
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, 0, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.right)
        {
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, 90, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
            // right down
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.down)
        {
            myPathTileNeighbors.myStraightRoad.SetActive(true);
            myPathTileNeighbors.myTurnRoad.SetActive(false);
            // straight vertical
        }
        myStraightRoad.SetActive(true);
        myTurnRoad.SetActive(false);
    }
    void CheckOldNeighborUp()
    {
        if (myPathTileNeighbors.GetNeighbor == Neighbor.left)
        {
            // left down
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, -90, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.right)
        {
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, -180, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
            // right down
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.up)
        {
            myPathTileNeighbors.myStraightRoad.SetActive(true);
            myPathTileNeighbors.myTurnRoad.SetActive(false);
            // straight vertical
        }
        myStraightRoad.SetActive(true);
        myTurnRoad.SetActive(false);
    }
    void CheckOldNeighborLeft()
    {
        if (myPathTileNeighbors.GetNeighbor == Neighbor.up)
        {
            // left down
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, 90, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.down)
        {
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, 180, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
            // right down
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.left)
        {
            myPathTileNeighbors.myStraightRoad.SetActive(true);
            myPathTileNeighbors.myTurnRoad.SetActive(false);
            // straight vertical
        }
        myStraightRoad.SetActive(true);
        myTurnRoad.SetActive(false);
        myStraightRoad.transform.rotation = Quaternion.Euler(0, 90, 0);
    }
    void CheckOldNeighborRight()
    {
        if (myPathTileNeighbors.GetNeighbor == Neighbor.up)
        {
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, 0, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.down)
        {
            myPathTileNeighbors.myTurnRoad.transform.rotation = Quaternion.Euler(0, -90, 0);
            myPathTileNeighbors.myStraightRoad.SetActive(false);
            myPathTileNeighbors.myTurnRoad.SetActive(true);
          
        }
        if (myPathTileNeighbors.GetNeighbor == Neighbor.right)
        {
            myPathTileNeighbors.myStraightRoad.SetActive(true);
            myPathTileNeighbors.myTurnRoad.SetActive(false);
          
        }
        myStraightRoad.SetActive(true);
        myTurnRoad.SetActive(false);
        myStraightRoad.transform.rotation = Quaternion.Euler(0, 90, 0);
    }

}
