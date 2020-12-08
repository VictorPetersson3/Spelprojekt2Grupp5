using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ObstructTileMap
{

    int myX;
    int myZ;

    [Header("Buttons")]
    [SerializeField]
    RailButton myRailButton;
    [SerializeField]
    PlayerController myPlayerController;

    bool isOpen;

    public override void OnValidate()
    {
        myPlayerController = FindObjectOfType<PlayerController>();
        base.OnValidate();

    }


    public override void Start()
    {
        myX = Mathf.FloorToInt(transform.position.x);
        myZ = Mathf.FloorToInt(transform.position.z);
        //base.Start();
        isOpen = false;
    }


    public override void Update()
    {
        //if (myPlayerController.GetCurrectTile.GetX == myX && myPlayerController.GetCurrectTile.GetZ == myZ)
        //{
        //    CheckButtons();
        //}

        print(isOpen);
        if (myPlayerController != null)
        {
            float distance = Vector3.Distance(myPlayerController.transform.position, transform.position);
            if (distance < 0.9f && !isOpen)
            {
                print("hi");

                myPlayerController.SetPlayerStep = myPlayerController.PlayerMoveList.Count;
            }

            if (myRailButton.GetMySwitch)
            {
                Debug.Log("Door opened");
                OpenDoor();
            }
        }
        
    }
    protected void CheckButtons()
    {
        if (myRailButton.GetMySwitch)
        {
            Debug.Log("Door opened");
            OpenDoor();
        }
    }
    public void OpenDoor()
    {
        isOpen = true;
    }
    protected override void OnDrawGizmos()
    {
        Color c = Color.blue;
        c.a = 0.8f;
        Gizmos.color = c;

        Gizmos.DrawCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.65f, 0.2f, 0.65f));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.7f, 0.2f, 0.7f));


        if (myRailButton != null)
        {
            if (myRailButton.GetMySwitch)
            {
                print("hi");
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawLine(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(Mathf.FloorToInt(myRailButton.transform.position.x), 0, Mathf.FloorToInt(myRailButton.transform.position.z)));

        }

    }
}
