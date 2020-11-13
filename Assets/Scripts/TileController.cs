using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField]
    TileManager myTileManager;
    public enum eMyDirections
    {
        North,
        East,
        West,
        South
    }

    [SerializeField]
    Tile myTile;

    int myXPos;
    int myZPos;

    Vector3 myPosition;


    eMyDirections myTileDirection;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //public Vector3 GetDirection()
    //{
        
    //}

    

    void SetDirection(eMyDirections aNextDirection)
    {
        myTileDirection = aNextDirection;
    }

}
