using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ObstructTileMap
{

    int myX;
    int myZ;

    [Header("Buttons")]
    [SerializeField]
    List<RailButton> myRailButtons = new List<RailButton>();
    [SerializeField]
    PlayerController myPlayerController;
    [SerializeField]
    Animator myAnimator;

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

        //print(isOpen);
        if (myPlayerController != null)
        {
            float distance = Vector3.Distance(myPlayerController.transform.position, transform.position);
            if (distance < 0.9f && !isOpen)
            {
                print("hi");

                myPlayerController.SetPlayerStep = myPlayerController.PlayerMoveList.Count;
            }
            CheckIfDoorOpened();
        }
    }

    private void CheckIfDoorOpened()
    {
        if (myRailButtons.Count == 1)
        {
            if (myRailButtons[0].GetMySwitch)
            {
                OpenDoor();
            }
        }
        else
        {
            int count = 0;
            foreach (RailButton button in myRailButtons)
            {
                if (button.GetMySwitch)
                {
                    count++;
                    if (count == myRailButtons.Count)
                    {
                        OpenDoor();
                    }
                }
            }
        }
    }
    private void OpenDoor()
    {
        if (isOpen == false)
        {
            isOpen = true;
            myAnimator.SetBool("isOpen", true);
            AudioManager.ourInstance.PlayEffect(AudioManager.EEffects.OPENDOOR);
        }
    }
    protected override void OnDrawGizmos()
    {
        Color c = Color.blue;
        c.a = 0.8f;
        Gizmos.color = c;

        Gizmos.DrawCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.65f, 0.2f, 0.65f));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.7f, 0.2f, 0.7f));

        foreach (RailButton button in myRailButtons)
        {
            if (button != null)
            {
                if (button.GetMySwitch)
                {
                    print("hi");
                    Gizmos.color = Color.green;
                }
                else
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawLine(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(Mathf.FloorToInt(button.transform.position.x), 0, Mathf.FloorToInt(button.transform.position.z)));

            }
        }
        

    }
}
