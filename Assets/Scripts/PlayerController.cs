using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Tile myCurrentTile;
    [SerializeField]
    WorldController myWorldController;
    List<Tile> myMovementQueue = new List<Tile>();
    bool myCoroutineIsActive;

    public Tile GetCurrectTile { get { return myCurrentTile; } }


    private void Start()
    {
    }
    private void Update()
    {
        SetCurrentTile();

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!myCoroutineIsActive)
            {
                StartCoroutine(StartMovement());
            }
        }
    }

    public void QueueTile(Tile aTile)
    {
        myMovementQueue.Add(aTile);
    }

    IEnumerator StartMovement()
    {
        myCoroutineIsActive = true;

        Debug.Log("StartMovement activated with a count of " + myMovementQueue.Count.ToString() + " moves.");

        for (int i = 0; i < myMovementQueue.Count; i++)
        {
            yield return StartCoroutine(MoveToPosition(i));
        }

        myCoroutineIsActive = false;

    }

    IEnumerator MoveToPosition(int anIndex) 
    {
        Vector3 position = transform.position;
        Vector3 target = new Vector3(myMovementQueue[anIndex].GetX, position.y, myMovementQueue[anIndex].GetZ);
        float multiplicator = 1 / Vector3.Distance(position, target);

        float percentage = 0.0f;

        while (percentage <= 1.0f)
        {
            //transform.position = Vector3.Lerp(myPos, new Vector3(myMovementQueue[anIndex].GetX, transform.position.y, myMovementQueue[anIndex].GetZ), 1f);

            percentage += (Time.deltaTime * 5.0f) * multiplicator;
            transform.position = Vector3.Lerp(position, target, percentage);
            
            yield return null;
        }
        transform.position = target;

    }
    public void Move()
    {
        for (int i = 0; i < myMovementQueue.Count; i++)
        {

            

            StartCoroutine(MoveToPosition(i));

        }

    }

    void SetCurrentTile()
    {
        myCurrentTile = myWorldController.GetWorld.GetTileAt(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z));
    }

}
