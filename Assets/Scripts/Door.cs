using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ObstructTileMap
{


    int myX;
    int myZ;

    [Header("Buttons")]
    [SerializeField]
    RailButton[] myRailButtons;
    [SerializeField]
    PlayerController myPlayerController;

    public override void OnValidate()
    {
        myPlayerController = FindObjectOfType<PlayerController>();
        base.OnValidate();
    }


    public override void Start()
    {
        myX = Mathf.FloorToInt(transform.position.x);
        myZ = Mathf.FloorToInt(transform.position.z);
        base.Start();
    }


    public override void Update()
    {
        //if (myPlayerController.GetCurrectTile.GetX == myX && myPlayerController.GetCurrectTile.GetZ == myZ)
        //{
        //    CheckButtons();
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckButtons();
        }
    }
    protected void CheckButtons()
    {
       
        bool buttonIsPressed = false;

        if (myRailButtons.Length > 1)
        {
            for (int i = 0; i < myRailButtons.Length; i++)
            {

                Debug.Log(myRailButtons[i].GetMySwitch);
                if (!myRailButtons[i].GetMySwitch)
                {
                    buttonIsPressed = false;
                    break;
                }
                buttonIsPressed = true;

            }
            if (buttonIsPressed)
            {
                Debug.Log("Open door with buttons");
                OpenDoor();
            }
        }
        else
        {
            if (myRailButtons[0].GetMySwitch)
            {
                OpenDoor();
                Debug.Log("Open door with one button");

            }
        }
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
        if (myRailButtons != null)
        {
            for (int i = 0; i < myRailButtons.Length; i++)
            {
                if (myRailButtons[i] != null)
                {
                    Gizmos.DrawLine(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(Mathf.FloorToInt(myRailButtons[i].transform.position.x), 0, Mathf.FloorToInt(myRailButtons[i].transform.position.z)));

                }
            }

        }

    }
}
