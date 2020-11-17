using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ObstructTileMap
{


    int myX;
    int myZ;
    
    
    public override void Start()
    {
        myX = Mathf.FloorToInt(transform.position.x);
        myZ = Mathf.FloorToInt(transform.position.z);
        base.Start();
    }
    public void OpenDoor()
    {
        WorldController.Instance.GetWorld.SetTileState(myX, myZ, Tile.TileState.empty);

    }
    protected override void OnDrawGizmos()
    {
        Color c = Color.blue;
        c.a = 0.8f;
        Gizmos.color = c;

        Gizmos.DrawCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.65f, 0.2f, 0.65f));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.7f, 0.2f, 0.7f));
        
    
    }
}
