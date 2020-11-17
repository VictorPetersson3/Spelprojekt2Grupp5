using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileManager : MonoBehaviour
{
    World myWorld;

    [SerializeField]
    WorldController myWorldController;

    [SerializeField] 
    Tile.EMyDirections myStartingDirection = (Tile.EMyDirections) myStartingDirectionOptions.North;

    [SerializeField]
    public int startTileX, startTileZ;
    public enum myStartingDirectionOptions
    {
        North,
        East,
        West,
        South
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetStartingPositionAndDirection();
        myWorld.SetTileDirectionBasedOnStartingTile();
    }

    void SetStartingPositionAndDirection()
    {
        myWorld = myWorldController.GetWorld;
        if (myWorld != null)
        {
            myWorld.SetStartingTileAt(startTileX, startTileZ, myStartingDirection);
        }
    }
}
