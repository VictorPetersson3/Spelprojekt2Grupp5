using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Tile myCurrentTile;
    [SerializeField]
    WorldController myWorldController;
    [SerializeField] 
    BuildManager myBuildManager;

    List<Tile> myMovementQueue = new List<Tile>();
    bool myCoroutineIsActive;

    public Tile GetCurrectTile { get { return myCurrentTile; } }
    [SerializeField]
    Vector3 myStartingPos;
 

    private void Start()
    {
        myStartingPos.y = 1;
        transform.position = myStartingPos;
        SetCurrentTile();
        PlaceTileAtPlayerStartPosition();
    }
    private void Update()
    {
        SetCurrentTile();

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!myCoroutineIsActive)
            {
                StartCoroutine(StartMovement());
            }
        }


        
    }

    private void PlaceTileAtPlayerStartPosition()
    {
        if (WorldController.Instance.GetTileAtPosition(Mathf.FloorToInt(myStartingPos.x), Mathf.FloorToInt(myStartingPos.z)).GetSetTileState == Tile.TileState.empty)
        {
            //Spawnar en tile
            myBuildManager.SpawnFromPool("Cube", Quaternion.identity).transform.position = myStartingPos + Vector3.down;

            //Sätter tilen till obstructed
            WorldController.Instance.GetWorld.SetTileState(Mathf.FloorToInt(myStartingPos.x), Mathf.FloorToInt(myStartingPos.z), Tile.TileState.road);
            QueueTile(WorldController.Instance.GetWorld.GetTileAt(Mathf.FloorToInt(myStartingPos.x), Mathf.FloorToInt(myStartingPos.z)));
        }
    }

    public void UpdateMovementPath()
    {
        List<Tile> temp = new List<Tile>();
        for (int i = 0; i < myMovementQueue.Count; i++)
        {
            myMovementQueue[i].SetRoad(myWorldController.GetRoadAtPosition(myMovementQueue[i].GetX, myMovementQueue[i].GetZ));

            myMovementQueue[i].GetSetRoad.GetSetNeighbors = GetNeighbors(myMovementQueue[i]);

            switch (myMovementQueue[i].GetSetRoad.GetSetNeighbors.Count)
            {
                case 0:
                    Debug.Log("Case 0 Straight");
                    myMovementQueue[i].GetSetRoad.GetSetRoadType = Road.EMyRoadTypes.Straight;
                    break;
                case 1:
                    Debug.Log("Case 1 Straight");
                    myMovementQueue[i].GetSetRoad.GetSetRoadType = Road.EMyRoadTypes.Straight;
                    break;
                case 2:
                    Debug.Log("Case 2 Straight");
                    myMovementQueue[i].GetSetRoad.GetSetRoadType = Road.EMyRoadTypes.Straight;
                    break;
                case 3:
                    Debug.Log("Case 3 Threeway");
                    myMovementQueue[i].GetSetRoad.GetSetRoadType = Road.EMyRoadTypes.Threeway;
                    break;
                case 4:
                    Debug.Log("Case 4 Intersection");
                    myMovementQueue[i].GetSetRoad.GetSetRoadType = Road.EMyRoadTypes.Intersection;
                    break;
                default:
                    Debug.Log("Error in UpdateMovementPath PlayerController");
                    break;
            }
            temp.Add(myMovementQueue[i]);
        }
    }

    List<Tile> GetNeighbors(Tile center)
    {
        List<Tile> neighbors = new List<Tile>();

        if (center.GetX >= 0)
        {
            Tile t = WorldController.Instance.GetTileAtPosition(center.GetX - 1,center.GetZ);
            //Debug.Log("Found "+ t.Type + " To the left");
            if (t.GetSetTileState == Tile.TileState.road)
            {
                neighbors.Add(t);
            }
        }
        if (center.GetX < WorldController.Instance.GetWorldWidth)
        {
            Tile t = WorldController.Instance.GetTileAtPosition(center.GetX + 1, center.GetZ);
            //Debug.Log("Found " + t.Type + " To the right");
            if (t.GetSetTileState == Tile.TileState.road)
            {
                neighbors.Add(t);
            }
        }
        if (center.GetZ < WorldController.Instance.GetWorldWidth)
        {
            Tile t = WorldController.Instance.GetTileAtPosition(center.GetX, center.GetZ + 1);
            //Debug.Log("Found " + t.Type + " To the right");
            if (t.GetSetTileState == Tile.TileState.road)
            {
                neighbors.Add(t);
            }
        }
        if (center.GetZ >= 0)
        {
            Tile t = WorldController.Instance.GetTileAtPosition(center.GetX , center.GetZ - 1);
            //Debug.Log("Found "+ t.Type + " To the left");
            if (t.GetSetTileState == Tile.TileState.road)
            {
                neighbors.Add(t);
            }
        }
        return neighbors;
    }

    public void QueueTile(Tile aTile)
    {
        myMovementQueue.Add(aTile);
    }

    IEnumerator StartMovement()
    {
        myCoroutineIsActive = true;

        Debug.Log("StartMovement activated with a count of " + myMovementQueue.Count.ToString() + " moves.");

        for (int i = 0; i < myMovementQueue.Count; i++)
        {
            yield return StartCoroutine(MoveToPosition(i));
        }

        myCoroutineIsActive = false;

    }

    IEnumerator MoveToPosition(int anIndex) 
    {
        Vector3 position = transform.position;
        Vector3 target = new Vector3(myMovementQueue[anIndex].GetX, position.y, myMovementQueue[anIndex].GetZ);
        float multiplicator = 1 / Vector3.Distance(position, target);

        float percentage = 0.0f;

        while (percentage <= 1.0f)
        {
            //transform.position = Vector3.Lerp(myPos, new Vector3(myMovementQueue[anIndex].GetX, transform.position.y, myMovementQueue[anIndex].GetZ), 1f);

            percentage += (Time.deltaTime * 5.0f) * multiplicator;
            transform.position = Vector3.Lerp(position, target, percentage);
            
            yield return null;
        }
        transform.position = target;

    }
    public void Move()
    {
        for (int i = 0; i < myMovementQueue.Count; i++)
        {

            

            StartCoroutine(MoveToPosition(i));

        }

    }

    void SetCurrentTile()
    {
        myCurrentTile = myWorldController.GetWorld.GetTileAt(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z));
    }

}
