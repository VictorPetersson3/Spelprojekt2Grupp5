using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTileIntersection : PathTile
{

    PlayerController myPlayerController;
    List<Vector3>[] myPathTiles;
    PathTile myLastPlacedTile;
    int myAmountOfConections = 0;

    PathManager myNewPathManager;
    bool runOnce = true;
    public List<Vector3>[] GetPathTileLists { get { return myPathTiles; } }
    public int GetIntersectionConnections { get { return myAmountOfConections; } }

    [SerializeField]
    Directions myOutDirection;
    public enum Directions
    {
        left,
        up,
        right,
        down,
        none
    }
    bool t = false;

    public override void Start()
    {
        myPathTiles = new List<Vector3>[4];
        for (int i = 0; i < 4; i++)
        {
            myPathTiles[i] = new List<Vector3>();
        }
        myLastPlacedTile = this;
        myNewPathManager = FindObjectOfType<PathManager>();
        myPlayerController = FindObjectOfType<PlayerController>();
        // 0 = left, 1 = up, 2 = right, 3 = down
        myNewPathManager.CheckIfPlacedNextToIntersection(this);
    }
    void Update()
    {
        PassThroughPlayer();

    }
    public void CheckMyNeighbors()
    {
        myNewPathManager.CheckIfPlacedNextToIntersection(this);
    }

    public void AddIntoList(Vector3 aPathTileToAdd, Directions directions)
    {
        if (myPathTiles == null)
        {
            Debug.Log("Create new list");
            myPathTiles = new List<Vector3>[4];
            for (int i = 0; i < 4; i++)
            {
                myPathTiles[i] = new List<Vector3>();
            }
        }

        if (directions != Directions.none)
        {
            if (myPathTiles[(int)directions].Count == 0)
            {

                for (int i = 0; i < myPathTiles[(int)directions].Count; i++)
                {
                    Debug.Log("List to copy: "+ directions + "at index "+ i + " " + myPathTiles[(int)directions][i]);
                }
                myPathTiles[(int)directions].Add(transform.position);
                
            }
            Debug.Log("Add this: " + aPathTileToAdd);
            myPathTiles[(int)directions].Add(aPathTileToAdd);

        }
        else
        {
            Debug.Log("Does not what list to add in to");
        }

    }


    void PassThroughPlayer()
    {
        if (Vector3.Distance(transform.position, myPlayerController.transform.position) < 1.1f && runOnce)
        {
            if (myOutDirection == Directions.right)
            {
                if (GetInputDirection() != (int)Directions.right)
                {
                    myPlayerController.PlayerMoveList(myPathTiles[(int)myOutDirection], this);
                    Debug.Log("Set player list to left in list: " + (int)myOutDirection);

                }

            }
            else if (myOutDirection == Directions.left)
            {
                if (GetInputDirection() != (int)Directions.left)
                {
                    myPlayerController.PlayerMoveList(myPathTiles[(int)myOutDirection], this);
                    Debug.Log("Set player list to left in list: " + (int)myOutDirection);
                }


            }
            else if (myOutDirection == Directions.up)
            {
                if (GetInputDirection() != (int)Directions.up)
                {
                    myPlayerController.PlayerMoveList(myPathTiles[(int)myOutDirection], this);
                    Debug.Log("Set player list to left in list: " + (int)myOutDirection);
                }

            }
            else // Down
            {
                if (GetInputDirection() != (int)Directions.down)
                {
                    Debug.Log("Set player list to left in list: " + (int)myOutDirection);
                    myPlayerController.PlayerMoveList(myPathTiles[(int)myOutDirection], this);

                }

            }
            runOnce = false;
        }
        if (Vector3.Distance(transform.position, myPlayerController.transform.position) > 1.2f)
        {
            runOnce = true;
        }


    }
    public void AddNewPathToIntersection(Directions aDirection)
    {
        myNewPathManager.GetDirections = aDirection;
    }
    public void AddConetions()
    {
        myAmountOfConections++;
    }
    public void AddListToIntersection(List<Vector3> aList, Directions directions)
    {
        if (myNewPathManager.PathTileIntersectionList.Count == 1 && !t)
        {
            if (myNewPathManager.GetPathFromStart.Count != 0)
            {
                AddConetions();
                myPathTiles[(int)directions].Clear();
                for (int i = myNewPathManager.GetPathFromStart.Count - 1; i > 0; i--)
                {
                    myPathTiles[(int)directions].Add(myNewPathManager.GetPathFromStart[i]);
                }
                t = true;
            }
        }
        else
        {
            if (aList.Count != 0)
            {
                AddConetions();
                Debug.Log("Copy list to: " + directions);
                for (int i = aList.Count - 1; i > 0; i--)
                {
                    //Debug.Log("Path list " + i + ". " + aList[i], gameObject);
                    myPathTiles[(int)myNewPathManager.GetDirections].Add(aList[i]);
                }
            }
            else
            {
                AddConetions();
                AddNewPathToIntersection(directions);
            }
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


    private void OnDrawGizmos()
    {
        for (int i = 0; i < myPathTiles.Length; i++)
        {
            for (int j = 0; j < myPathTiles[i].Count; j++)
            {
                Vector3 offset;
                switch (i)
                {
                    case 0:
                        Gizmos.color = Color.red;
                        offset = new Vector3(0, 1, 0);
                        Gizmos.DrawSphere(myPathTiles[i][j] + offset, 0.1f);
                        break;

                    case 1:
                        Gizmos.color = Color.blue;
                        offset = new Vector3(0, 0.75f, 0);
                        Gizmos.DrawSphere(myPathTiles[i][j] + offset, 0.1f);
                        break;
                    case 2:
                        Gizmos.color = Color.yellow;
                        offset = new Vector3(0, 0.5f, 0);
                        Gizmos.DrawSphere(myPathTiles[i][j] + offset, 0.1f);
                        break;
                    case 3:
                        Gizmos.color = Color.cyan;
                        offset = new Vector3(0, 0.25f, 0);
                        Gizmos.DrawSphere(myPathTiles[i][j] + offset, 0.1f);
                        break;
                }

            }
        }
    }

}
