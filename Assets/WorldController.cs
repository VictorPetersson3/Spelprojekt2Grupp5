using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance { get; protected set; }
    [Header("World Size Settings")]
    [SerializeField]
    [Range(0, 13)]
    int myWidth = 10;
    [Range(0, 13)]
    [SerializeField]
    int myDepth = 10;

    [Header("Grid color Settings")]
    [SerializeField]
    Color myObstructedTilesColor;
    [SerializeField]
    Color myEmptyTilesColor;
    [Range(0.1f,1)]
    [SerializeField]
    float myAlpha = 0.5f;

    World myWorld;
    public World GetWorld { get { return myWorld; } }
    public int GetWorldDepth { get { return myDepth; } }
    public int GetWorldWidth { get { return myWidth; } }
    void Start()
    {
        myWorld = new World(myWidth, myWidth);
        if (Instance != null)
            Debug.LogError("There should never be two world controllers.");
        Instance = this;
    }
    public Tile GetTileAtPosition(float aX, float aZ)
    {
        
        int x = Mathf.FloorToInt(aX);
        int z = Mathf.FloorToInt(aZ);
        return this.myWorld.GetTileAt(x, z);
    }
    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3((float)(myWidth / 2.0f) - 0.5f, 0, (float)(myDepth / 2.0f) - 0.5f) , new Vector3(myWidth, 0.1f, myDepth));

        if (myWorld != null)
        {
            for (int i = 0; i < myWidth; i++)
            {
                for (int j = 0; j < myDepth; j++)
                {
                    if (myWorld.GetTileAt(i, j).GetSetTileState == Tile.TileState.obstructed)
                    {


                        myObstructedTilesColor.a = myAlpha;
                        Gizmos.color = myObstructedTilesColor;
                        Gizmos.DrawCube(new Vector3(i, 0, j), new Vector3(0.95f,0, 0.95f));

                        Gizmos.color = Color.red;
                        Gizmos.DrawWireCube(new Vector3(i, 0, j), new Vector3(1, 0, 1));


                    }
                    else
                    {
                        
                        myEmptyTilesColor.a = myAlpha;
                        Gizmos.color = myEmptyTilesColor;
                        Gizmos.DrawCube(new Vector3(i, 0, j), new Vector3(0.95f, 0, 0.95f));
                    
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireCube(new Vector3(i, 0, j), new Vector3(1, 0, 1));
                    }

                }
            }

        }


    }
    private void Update()
    {

    }

}
