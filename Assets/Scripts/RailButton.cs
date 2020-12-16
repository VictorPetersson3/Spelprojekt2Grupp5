using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailButton : ObstructTileMap
{


    int myX;
    int myZ;
    [SerializeField]
    PlayerController myPlayerController;
    public bool mySwitch = false;

    [SerializeField]
    KeyCode debugButton;

    public bool GetMySwitch
    {
        get
        {
            return mySwitch;
        }
    }

    public override void Start()
    {
        myX = Mathf.FloorToInt(transform.position.x);
        myZ = Mathf.FloorToInt(transform.position.z);
        //base.Start();


    }
    public override void Update()
    {
        if (myPlayerController != null)
        {
            float distance = Vector3.Distance(myPlayerController.transform.position, transform.position);
            if (distance < 0.05f)
            {
                mySwitch = true;
                AudioManager.ourInstance.PlayEffect(AudioManager.EEffects.PRESSUREPLATE);
            }
        }
    }
    protected override void OnDrawGizmos()
    {

        if (!mySwitch)
        {
            Gizmos.DrawCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.65f, 0.2f, 0.65f));
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.7f, 0.2f, 0.7f));

        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.65f, 0.2f, 0.65f));
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(new Vector3(Mathf.FloorToInt(transform.position.x), 0.1f, Mathf.FloorToInt(transform.position.z)), new Vector3(0.7f, 0.2f, 0.7f));

        }


    }

}
