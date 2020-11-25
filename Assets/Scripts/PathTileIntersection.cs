using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTileIntersection : PathTile
{

    PlayerController myPlayerController;
    List<Vector3>[] myPathTiles;
    PathTile myLastPlacedTile;

    public PathTile GetSetLastPlacedTile { get { return myLastPlacedTile; } set { Debug.Log("Update last placed tile"); myLastPlacedTile = value; } }
    [SerializeField]
    Directions myOutDirection;
    enum Directions
    {
        left,
        up,
        right,
        down
    }

    public override void Start()
    {
        myLastPlacedTile = this;

        myPlayerController = FindObjectOfType<PlayerController>();
        // 0 = left, 1 = up, 2 = right, 3 = down
        myPathTiles = new List<Vector3>[4];
        for (int i = 0; i < 4; i++)
        {
            myPathTiles[i] = new List<Vector3>();
        }


        base.Start();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CheckIfPlacedNextToMe();
        }

        PassThroughPlayer();
        if (Input.GetMouseButton(0))
        {

            CheckIfPlacedNextToMe();
        }
    }
    public List<Vector3> ChooseListToAddTileTo
    {
        get
        {
            return myPathTiles[(int)myOutDirection];
        }
        set
        {
            myPathTiles[(int)myOutDirection] = value;
        }
    }
    void PassThroughPlayer()
    {

        if (Vector3.Distance(transform.position, myPlayerController.transform.position) < 1.1f)
        {
            if (myOutDirection == Directions.right)
            {
                if (GetInputDirection() != (int)Directions.right)
                {
                    myPlayerController.PlayerMoveList = myPathTiles[(int)myOutDirection];
                   
                    Debug.Log("Set player list to right");
                }
            }
            else if (myOutDirection == Directions.left)
            {
                if (GetInputDirection() != (int)Directions.left)
                {
                    myPlayerController.PlayerMoveList = myPathTiles[(int)myOutDirection];
                   
                    Debug.Log("Set player list to left");
                }
            }
            else if (myOutDirection == Directions.up)
            {
                if (GetInputDirection() != (int)Directions.up)
                {         
                    myPlayerController.PlayerMoveList = myPathTiles[(int)myOutDirection];
                 
                    Debug.Log("Set player list to up");
                }
            }
            else // Down
            {
                if (GetInputDirection() != (int)Directions.down)
                {
                    Debug.Log("Set player list to down\nList To Copy from: " + myPathTiles[(int)myOutDirection].Count);
                    myPlayerController.PlayerMoveList = myPathTiles[(int)myOutDirection];
                 
                }
            }
        }
    }

    void CheckIfPlacedNextToMe()
    {
        int x = Mathf.FloorToInt(transform.position.x);
        int z = Mathf.FloorToInt(transform.position.z);

        if (x - 1 >= 0)
        {
            if (myPathManager.GetPathTileMap[x - 1, z] == myPathManager.GetLastPlacedTile && myPathTiles[0].Count == 0)
            {
                AddExitingListToIntersection(0, myPathManager.GetPathFromStart);
            }
        }
        if (x + 1 < WorldController.Instance.GetWorldWidth)
        {
            if (myPathManager.GetPathTileMap[x + 1, z] == myPathManager.GetLastPlacedTile && myPathTiles[2].Count == 0)
            {
                AddExitingListToIntersection(2, myPathManager.GetPathFromStart);
            }
        }
        if (z - 1 >= 0)
        {
            if (myPathManager.GetPathTileMap[x, z - 1] == myPathManager.GetLastPlacedTile && myPathTiles[3].Count == 0)
            {
                AddExitingListToIntersection(3, myPathManager.GetPathFromStart);
            }
        }
        if (z + 1 < WorldController.Instance.GetWorldDepth)
        {
            if (myPathManager.GetPathTileMap[x, z + 1] == myPathManager.GetLastPlacedTile && myPathTiles[1].Count == 0)
            {
                AddExitingListToIntersection(1, myPathManager.GetPathFromStart);
            }
        }
    }

    public void AddExitingListToIntersection(int aIndex, List<Vector3> aPathTileList)
    {
        myPathTiles[aIndex].Clear();
        for (int i = aPathTileList.Count - 1; i > 0; i--)
        {
            Debug.Log("Number of tiles added in exsiting list: " + i);
            myPathTiles[aIndex].Add(aPathTileList[i]);
        }
        myPathTiles[aIndex][0] = myPathTiles[aIndex][1];
    }
    int GetInputDirection()
    {

        if (myPlayerController.transform.position.x < transform.position.x + 0.5f && myPlayerController.transform.position.x > transform.position.x - 0.5f)
        {
            if (myPlayerController.transform.position.y < transform.position.y)
            {

                return 3;
            }
            else
            {
                return 1;
            }
        }
        if (myPlayerController.transform.position.y < transform.position.y + 0.5f && myPlayerController.transform.position.y > transform.position.y - 0.5f)
        {
            if (myPlayerController.transform.position.x < transform.position.x)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }
        return 0;

    }



}
