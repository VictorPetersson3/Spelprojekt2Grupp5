using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Tile myCurrentTile;
    public Tile GetCurrectTile { get { return myCurrentTile; } }

    [SerializeField]
    PathManager myPathMananger;
    public List<PathTile> myMovementList;
    [SerializeField]
    ParticleSystem myDeathEffect;
    int step = 1;
    [Range(1, 8)]
    [SerializeField]
    float myMovementSpeed = 8;
    void Start()
    {
        myMovementList = new List<PathTile>();
    }

    void Update()
    {
        print(transform.position + Vector3.right + "myPos");
        print(myPathMananger.GetPortalTiles()[0].GetPos() + "PortalTile");
        if (Input.GetKey(KeyCode.Space))
        {
            if (myMovementList[step].IsEndTile)
            {
                Debug.Log("You win");

            }
            if (myPathMananger.GetPortalTiles()[0].GetPos() == transform.position + Vector3.left)
            {
                print("Telelport");
                transform.position = myPathMananger.GetPortalTiles()[0].GetExit() + myPathMananger.GetPortalTiles()[0].transform.position;
                myMovementList = myPathMananger.GetPortalTiles()[0].GetMovementList();
            }
            else
            {

                transform.position = Vector3.MoveTowards(transform.position, myMovementList[step].GetPathTilePosition, myMovementSpeed * Time.deltaTime);
                Vector3 distanceToNextPos = myMovementList[step].GetPathTilePosition - transform.position; 
            
                if (distanceToNextPos.magnitude < 0.05f)
                {
                    if (step < myMovementList.Count - 1)
                    {
                        step++;

                    }
                    else
                    {
                        myDeathEffect.transform.position = transform.position;
                        gameObject.SetActive(false);
                        myDeathEffect.Play();
                    }
                }
            }

        }
    }
}
