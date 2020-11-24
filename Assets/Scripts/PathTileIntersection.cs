using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTileIntersection : PathTile
{
    PathManager pathManager;
    PlayerController myPlayerController;
    List<Vector3>[] myPathTiles;
    PathTile myLastPlacedTile;

    public PathTile GetSetLastPlacedTile { get { return myLastPlacedTile; } set { myLastPlacedTile = value; } }
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
        pathManager = FindObjectOfType<PathManager>();
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
        PassThroughPlayer();
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < 4; i++)
            {
                Debug.Log(myPathTiles[i].Count);
            }
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
        if (Vector3.Distance(transform.position, myPlayerController.transform.position) < 1)
        {
            if (myOutDirection == Directions.right)
            {
                if (GetInputDirection() == (int)Directions.down || GetInputDirection() == (int)Directions.left)
                {
                    myPlayerController.myMovementList = myPathTiles[(int)myOutDirection];
                    myPlayerController.SetPlayerStep = 1;
                }
                if (GetInputDirection() == (int)Directions.up || GetInputDirection() == (int)Directions.left)
                {
                    myPlayerController.myMovementList = myPathTiles[(int)myOutDirection];
                    myPlayerController.SetPlayerStep = 1;
                }
            }
            else if (myOutDirection == Directions.left)
            {
                if (GetInputDirection() == (int)Directions.right || GetInputDirection() == (int)Directions.down)
                {
                    myPlayerController.myMovementList = myPathTiles[(int)myOutDirection];
                    myPlayerController.SetPlayerStep = 1;
                }
                if (GetInputDirection() == (int)Directions.up || GetInputDirection() == (int)Directions.right)
                {
                    myPlayerController.myMovementList = myPathTiles[(int)myOutDirection];
                    myPlayerController.SetPlayerStep = 1;
                }
            }
            else if (myOutDirection == Directions.up)
            {
                if (GetInputDirection() == (int)Directions.right || GetInputDirection() == (int)Directions.down)
                {
                    myPlayerController.myMovementList = myPathTiles[(int)myOutDirection];
                    myPlayerController.SetPlayerStep = 1;
                }
                if (GetInputDirection() == (int)Directions.left || GetInputDirection() == (int)Directions.down)
                {
                    myPlayerController.myMovementList = myPathTiles[(int)myOutDirection];
                    myPlayerController.SetPlayerStep = 1;
                }
            }
            else // Down
            {
                if (GetInputDirection() == (int)Directions.up || GetInputDirection() == (int)Directions.right)
                {
                    myPlayerController.myMovementList = myPathTiles[(int)myOutDirection];
                    myPlayerController.SetPlayerStep = 1;

                }
                if (GetInputDirection() == (int)Directions.left || GetInputDirection() == (int)Directions.up)
                {
                    myPlayerController.myMovementList = myPathTiles[(int)myOutDirection];
                    myPlayerController.SetPlayerStep = 1;
                }
            }     
        }     
    }
    
    public void CheckIfPlacedNextToMe()
    {
        int x = Mathf.FloorToInt(transform.position.x);
        int z = Mathf.FloorToInt(transform.position.z);
        if (x - 1 >= 0)
        {
            if (pathManager.GetPathTileMap[x - 1, z] == pathManager.GetLastPlacedTile)
            {
                AddExitingListToIntersection(0 , pathManager.GetPathFromStart);
            }
        }
        if (x + 1 < WorldController.Instance.GetWorldWidth)
        {
            if (pathManager.GetPathTileMap[x + 1, z] == pathManager.GetLastPlacedTile)
            {
                AddExitingListToIntersection(2, pathManager.GetPathFromStart);
            }
        }
        if (z - 1 >= 0)
        {
            if (pathManager.GetPathTileMap[x, z - 1] == pathManager.GetLastPlacedTile)
            {
                AddExitingListToIntersection(3, pathManager.GetPathFromStart);
            }
        }
        if (z + 1 < WorldController.Instance.GetWorldDepth)
        {
            if (pathManager.GetPathTileMap[x, z + 1] == pathManager.GetLastPlacedTile)
            {
                AddExitingListToIntersection(1, pathManager.GetPathFromStart);
            }
        }
    }

    public void AddExitingListToIntersection(int aIndex, List<Vector3> aPathTileList)
    {
        myPathTiles[aIndex] = aPathTileList;
        
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
