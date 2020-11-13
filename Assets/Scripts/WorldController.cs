using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance { get; protected set; }
    [Header("World Size Settings")]
    [SerializeField]
    int myWidth = 10;
    [SerializeField]
    int myDepth = 10;
    
    World myWorld;
    public World GetWorld { get { return myWorld; } }

    void Start()
    {
        myWorld = new World(myWidth, myWidth); 
        if (Instance != null)
        {
            Debug.LogError("There should never be two world controllers.");
        }        
        Instance = this;
        //for (int i = 0; i < myWidth; i++)
        //{
        //    for (int j = 0; j < myDepth; j++)
        //    {
        //        Debug.Log("Tile: " + myWorld.GetTileAt(i, j).GetX + ", " + myWorld.GetTileAt(i, j).GetZ + ", " + myWorld.GetTileAt(i,j).GetSetTileState );

        //    }
        //}
        
    }    
    public Tile GetTileAtPosition(float aX, float aZ)
    {
        int x = Mathf.FloorToInt(aX);
        int z = Mathf.FloorToInt(aZ);
        return myWorld.GetTileAt(x, z);
    }

}
