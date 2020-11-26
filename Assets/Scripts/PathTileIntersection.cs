using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTileIntersection : PathTile
{

    PlayerController myPlayerController;
    List<Vector3>[] myPathTiles;
    PathTile myLastPlacedTile;


    PathManager aNewPathManager;

    public List<Vector3>[] GetPathTileLists { get { return myPathTiles; } } 

    [SerializeField]
    Directions myOutDirection;
    public enum Directions
    {
        left,
        up,
        right,
        down
    }

    public override void Start()
    {
        myLastPlacedTile = this;
        aNewPathManager = FindObjectOfType<PathManager>();
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
            if (aNewPathManager.PathTileIntersectionList.Count == 1)
            {
                aNewPathManager.CheckIfPlacedNextToIntersection(this, myPathManager.GetPathFromStart);

            }
            else
            {

            }
        }

        PassThroughPlayer();
        if (Input.GetMouseButton(0))
        {

            if (aNewPathManager.PathTileIntersectionList.Count == 1)
            {
                aNewPathManager.CheckIfPlacedNextToIntersection(this, myPathManager.GetPathFromStart);

            }
            else
            {

            }
        }
    }
    public List<Vector3> ChooseListToAddTileTo
    {
        get
        {
            Debug.Log("Add into list via GET");
            return myPathTiles[(int)myPathManager.GetDirections];
        }
        set
        {
            Debug.Log("Add into list via SET");
            myPathTiles[(int)myPathManager.GetDirections] = value;
        }
    }
   
    public void AddIntoList(Vector3 aPathTileToAdd, Directions directions)
    {
        if (myPathTiles[(int)directions].Count > 1)
        {
            if (aPathTileToAdd != myPathTiles[(int)directions][1])
            {
                myPathTiles[(int)directions].Add(aPathTileToAdd);
            }
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
                    myPlayerController.PlayerMoveList(myPathTiles[(int)myOutDirection], this);
                   
                    Debug.Log("Set player list to right");
                }
            }
            else if (myOutDirection == Directions.left)
            {
                if (GetInputDirection() != (int)Directions.left)
                {
                    myPlayerController.PlayerMoveList(myPathTiles[(int)myOutDirection], this);

                    Debug.Log("Set player list to left");
                }
            }
            else if (myOutDirection == Directions.up)
            {
                if (GetInputDirection() != (int)Directions.up)
                {
                    myPlayerController.PlayerMoveList(myPathTiles[(int)myOutDirection], this);

                    Debug.Log("Set player list to up");
                }
            }
            else // Down
            {
                if (GetInputDirection() != (int)Directions.down)
                {
                    Debug.Log("Set player list to down\nList To Copy from: " + myPathTiles[(int)myOutDirection].Count);
                    myPlayerController.PlayerMoveList(myPathTiles[(int)myOutDirection], this);

                }
            }
        }
    }

    
    public void AddNewPathToIntersection(Directions aDirection)
    {
        myPathManager.GetDirections = aDirection;
    }
    public void AddExitingListToIntersection(List<Vector3> aList, Directions directions)
    {
        myPathTiles[(int)directions].Clear();
        for (int i = aList.Count - 1; i > 0; i--)
        {
            Debug.Log("Number of tiles added in exsiting list: " + i);
            myPathTiles[(int)directions].Add(aList[i]);
        }
      
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
