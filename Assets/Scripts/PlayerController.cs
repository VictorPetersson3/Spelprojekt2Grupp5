using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Tile myCurrentTile;
    [SerializeField]
    WorldController myWorldController;
    int myDirection;

    public Tile GetCurrectTile { get { return myCurrentTile; } }


    private void Start()
    {
        myDirection = 1;
        myWorldController.GetTileAtPosition(0, 0);
    }
    private void Update()
    {
        SetCurrentTile();
        print(myCurrentTile.GetX + " " + myCurrentTile.GetZ);

        int staticCastedDirection = (int)myCurrentTile.GetSetDirection;
        Move(staticCastedDirection);

        if (transform.position.x >= 5)
        {
            myDirection = 0;
        }
        if (transform.position.z >= 5)
        {
            myDirection = 4;
        }
    }

    public void Move(int aStaticCastedCoordinate)
    {

        switch (aStaticCastedCoordinate)
        {
            case 0:
                transform.Translate(0, 0, 5 * Time.deltaTime);
                break;
            case 1:
                transform.Translate(5 * Time.deltaTime, 0, 0);
                break;
            case 2:
                transform.Translate(-5 * Time.deltaTime, 0, 0);
                break;
            case 3:
                transform.Translate(0, 0, -5 * Time.deltaTime);
                break;
            case 4:
                transform.Translate(0, 0, 0);
                break;
            default:
                break;
        }
    }

    void GetCurrentTileDirection()
    {

    }

    void SetCurrentTile()
    {
        myCurrentTile = myWorldController.GetWorld.GetTileAt(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z));
    }

}
