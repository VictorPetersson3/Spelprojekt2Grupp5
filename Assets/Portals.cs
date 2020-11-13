using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : ObstructTileMap
{
    [SerializeField]
    Portals mySecondPortal;

    Tile myTile;
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        
        base.Update();
    }
    void TeleportPlayer()
    {

        Vector3 flooredPosition = new Vector3(Mathf.FloorToInt(transform.position.x), 0, Mathf.FloorToInt(transform.position.z));
        myTile = GetWorldController.GetTileAtPosition(flooredPosition.x, flooredPosition.z);

        // If Player Tile = myTile
        // Teleport
    
    }
    protected override void OnDrawGizmos()
    {
        Color c = Color.cyan;
        c.a = 0.8f;
        Gizmos.color = c;
       
        Gizmos.DrawCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.65f, 0.2f, 0.65f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.7f, 0.2f, 0.7f));
    }

}
