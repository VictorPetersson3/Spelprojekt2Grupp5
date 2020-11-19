using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    List<Tile> myNeighbors = new List<Tile>();
    EMyRoadTypes myRoadType;
    int myX, myZ;
    public enum EMyRoadTypes
    {
        Straight,
        Turn,
        Threeway,
        Intersection,
        None
    }

    private void Start()
    {
        myX = Mathf.FloorToInt(transform.position.x);
        myZ = Mathf.FloorToInt(transform.position.z);
    }
    private void Update()
    {
        if (myX != Mathf.FloorToInt(transform.position.x) || myZ != Mathf.FloorToInt(transform.position.z))
        {
            myX = Mathf.FloorToInt(transform.position.x);
            myZ = Mathf.FloorToInt(transform.position.z);
        }
    }

    public int GetSetX { get { return myX; } set { myX = value; } }
    public int GetSetZ { get { return myZ; } set { myZ = value; } }

    public List<Tile> GetSetNeighbors { get { return myNeighbors; } set { myNeighbors = value; } }
    public EMyRoadTypes GetSetRoadType { get { return myRoadType; } set { myRoadType = value; } }


    


}
