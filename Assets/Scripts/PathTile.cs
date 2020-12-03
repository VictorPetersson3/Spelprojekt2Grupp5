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
    public Neighbor GetNeighbor { get { return myNeigbor; } set { myNeigbor = value; } }


    public GameObject myTurnRoad;
    public GameObject myStraightRoad;

    float mySpeed = 3f;
    float myCurrentSpeed = 0;
    bool myTemp = false;
    [SerializeField]
    ParticleSystem myPlacementEffect;
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

        myPlacementEffect.transform.position = new Vector3(myPlacementEffect.transform.position.x, 0.1f, myPlacementEffect.transform.position.z);
        //myPlacementEffect.Play();
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
    }
    private void Update()
    {
        MoveObjectToPlaceDown(transform);
    }
    public void CheckNeighborsFromPortal()
    {
        myTemp = false;
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        int x = Mathf.FloorToInt(transform.position.x);
        int z = Mathf.FloorToInt(transform.position.z);
        for (int i = 0; i < myPathManager.GetPortals.Count; i++)
        {
            if (x - 1 >= 0)
            {
                if (myPathManager.GetPathTileMap[x - 1, z] != null)
                {
                    if (myPathManager.GetPathTileMap[x - 1, z] == myPathManager.GetPortals[i].GetSetLastPathTile)
                    {
                        myPathTileNeighbors = myPathManager.GetPortals[i].GetSetLastPathTile;
                        myNeigbor = Neighbor.left;
                        CheckOldNeighborLeft();
                        myPathManager.GetPortals[i].GetSetLastPathTile = this;
                    }
                }
            }
            if (x + 1 < WorldController.Instance.GetWorldWidth)
            {
                if (myPathManager.GetPathTileMap[x + 1, z] != null)
                {
                    if (myPathManager.GetPathTileMap[x + 1, z] == myPathManager.GetPortals[i].GetSetLastPathTile)
                    {
                        myPathTileNeighbors = myPathManager.GetPortals[i].GetSetLastPathTile;
                        myNeigbor = Neighbor.right;
                        CheckOldNeighborRight();
                        myPathManager.GetPortals[i].GetSetLastPathTile = this;
                    }
                }
            }
            if (z - 1 >= 0)
            {
                Debug.Log("Check down");
                if (myPathManager.GetPathTileMap[x, z - 1] != null)
                {
                    Debug.Log("Check down");
                    if (myPathManager.GetPathTileMap[x, z - 1] == myPathManager.GetPortals[i].GetSetLastPathTile)
                    {
                        myPathTileNeighbors = myPathManager.GetPortals[i].GetSetLastPathTile;
                        myNeigbor = Neighbor.down;
                        CheckOldNeighborDown();
                        Debug.Log("Check down");
                        myPathManager.GetPortals[i].GetSetLastPathTile = this;
                    }
                }
            }
            if (z + 1 < WorldController.Instance.GetWorldDepth)
            {
                if (myPathManager.GetPathTileMap[x, z + 1] != null)
                {
                    if (myPathManager.GetPathTileMap[x, z + 1] == myPathManager.GetPortals[i].GetSetLastPathTile)
                    {
                        myPathTileNeighbors = myPathManager.GetPortals[i].GetSetLastPathTile;
                        myNeigbor = Neighbor.up;
                        CheckOldNeighborUp();
                        myPathManager.GetPortals[i].GetSetLastPathTile = this;
                    }
                }
            }
        }
        myPlacementEffect.Play();
    }
    public void CheckNeighbors()
    {
        myTemp = false;
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
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
        myPlacementEffect.Play();
    }
    void CheckOldNeighborDown()
    {
        if (myPathTileNeighbors.GetNeighbor == Neighbor.left)
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
    void MoveObjectToPlaceDown(Transform aTransform)
    {
        myCurrentSpeed = mySpeed * mySpeed;
        if (aTransform.position.y + (myCurrentSpeed * Time.deltaTime) > 0)
        {
            aTransform.position = Vector3.Lerp(aTransform.position, new Vector3(aTransform.position.x, 0, aTransform.position.z), myCurrentSpeed * Time.deltaTime); ;
        }

    }
    public void ResetMe()
    {
        myTurnRoad.transform.rotation = Quaternion.Euler(0, 0, 0);
        myStraightRoad.transform.rotation = Quaternion.Euler(0, 0, 0);
        myPathTileNeighbors.GetNeighbor = Neighbor.none;
        myPathTileNeighbors = null;
        myStraightRoad.SetActive(false);
        myTurnRoad.SetActive(false);
        myNeigbor = Neighbor.none;


    }

}
