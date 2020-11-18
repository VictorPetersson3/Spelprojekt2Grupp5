using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Tile myCurrentTile;
    [SerializeField]
    WorldController myWorldController;
    List<Tile> myMovementQueue = new List<Tile>();

    public Tile GetCurrectTile { get { return myCurrentTile; } }


    private void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            myMovementQueue.Add(myWorldController.GetWorld.GetTileAt(i, 0));
        }
    }
    private void Update()
    {
        SetCurrentTile();
        Move();

    }

    public void Move()
    {
        for (int i = 0; i < myMovementQueue.Count; i++)
        {
            while(transform.position != new Vector3(myMovementQueue[i+1].GetX, 0 ,myMovementQueue[i+1].GetZ)) 
            {
                Vector3.Lerp(new Vector3(myCurrentTile.GetX, 0 , myCurrentTile.GetZ), new Vector3(myMovementQueue[i].GetX, 0, myMovementQueue[i].GetZ), 5 * Time.deltaTime);
            }
        }

    }

    void SetCurrentTile()
    {
        myCurrentTile = myWorldController.GetWorld.GetTileAt(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z));
    }

}
