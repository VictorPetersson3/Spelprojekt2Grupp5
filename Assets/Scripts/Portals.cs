using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : ObstructTileMap
{
    [SerializeField]
    Portals mySecondPortal;
    [SerializeField]
    PlayerController myPlayerController;

    [Header("Enter/Exit Settings")]
    [SerializeField]
    Vector3 myEntryAndExitDirection;

    List<PathTile> myContinuingPath;

    public Portals SetSecondPortal { set { mySecondPortal = value; } }

    public override void OnValidate()
    {
        if (myEntryAndExitDirection == Vector3.zero)
        {
            myEntryAndExitDirection = new Vector3(1, 0, 0);
        }
        if (mySecondPortal != null)
        {
            if (gameObject.GetInstanceID() == mySecondPortal.gameObject.GetInstanceID())
            {
                mySecondPortal = null;
            }
            else
            {
               
                mySecondPortal.SetSecondPortal = this;
            }
        }

        
        myPlayerController = FindObjectOfType<PlayerController>();
        base.OnValidate();
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

    public List<PathTile> GetMovementList()
    {
        return myContinuingPath;
    }

    public void AddVectorToMovementList(PathTile aTilePos)
    {
        myContinuingPath.Add(aTilePos);
    }
    public override void Start()
    {
        base.Start();
        myContinuingPath = new List<PathTile>();
    }
    public override void Update()
    {

        base.Update();
    }

    public Vector3 GetExit()
    {
        return myEntryAndExitDirection;
    }
    void TeleportPlayer()
    {
        Vector3 flooredPosition = new Vector3(Mathf.FloorToInt(transform.position.x), 0, Mathf.FloorToInt(transform.position.z));
        Vector3 playerTilePos = new Vector3(myPlayerController.GetCurrectTile.GetX, 0, myPlayerController.GetCurrectTile.GetZ);
       
        if (playerTilePos == flooredPosition)
        {
            myPlayerController.transform.position = mySecondPortal.transform.position + myEntryAndExitDirection;

        }
    }
    protected override void OnDrawGizmos()
    {
        Color c = Color.cyan;
        c.a = 0.8f;
        Gizmos.color = c;

        Gizmos.DrawCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.65f, 0.2f, 0.65f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.7f, 0.2f, 0.7f));

        if (mySecondPortal != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(Mathf.FloorToInt(mySecondPortal.transform.position.x), 0.1f, Mathf.FloorToInt(mySecondPortal.transform.position.z)));
        }

        Color d = Color.yellow;
        d.a = 0.8f;
        Gizmos.color = d;
        myEntryAndExitDirection = new Vector3(Mathf.RoundToInt(myEntryAndExitDirection.x), 0, Mathf.RoundToInt(myEntryAndExitDirection.z));
        Gizmos.DrawCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)) + myEntryAndExitDirection, new Vector3(0.4f, 0.2f, 0.4f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)) + myEntryAndExitDirection, new Vector3(0.45f, 0.2f, 0.45f));


    }

}
