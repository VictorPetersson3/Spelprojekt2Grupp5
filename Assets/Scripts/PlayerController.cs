using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Tile myCurrentTile;
    [SerializeField]
    WorldController myWorldController;

    private void Start()
    {
    }
    private void Update()
    {
        SetCurrentTile();
        print(myCurrentTile.GetX);
        print(myCurrentTile.GetZ);
    }

    public void Move(int aStaticCastedCoordinate)
    {
        
        switch (aStaticCastedCoordinate)
        {
            case 0:
                transform.Translate(0, 0, 1);
                break;
            case 1:
                transform.Translate(1, 0, 0);
                break;
            case 2:
                transform.Translate(-1, 0, 0);
                break;
            case 3:
                transform.Translate(0, 0, -1);
                break;
            default:
                break;
        }
    }

    void SetCurrentTile()
    {
        myCurrentTile = myWorldController.GetWorld.GetTileAt(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z));
    }
}
