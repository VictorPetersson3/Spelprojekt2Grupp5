using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Tile myCurrentTile;
    public Tile GetCurrectTile { get { return myCurrentTile; } }

    public List<PathTile> PlayerMoveList
    {
        get
        {
            return myMovementList;
        }
        set
        {
            step = 1;
            myMovementList = value;
        }
    }

    [SerializeField]
    PathManager myPathManager;
    List<PathTile> myMovementList;
    [SerializeField]
    ParticleSystem myDeathEffect;
    [SerializeField]
    int step = 1;
    [Range(1, 12)]
    [SerializeField]
    float myMovementSpeed = 12;

    int indexForNextPortalDistance = 0;
    bool myDontIncreaseIndexFirstTime = true;
    [SerializeField]
    bool myMovementStart = false;
    GameManager myGameManger;
    public int SetPlayerStep
    {
        set
        {
            step = value;
        }
    }

    public void ToggleStart()
    {
        myMovementStart = !myMovementStart;
    }

    void Start()
    {
        myGameManger = GameManager.globalInstance;
        myMovementList = new List<PathTile>();
        myMovementStart = false;
    }

    void Update()
    {
        
        if (myMovementStart)
        {
            if (myMovementList[step].IsEndTile)
            {
                myGameManger.SetFinishedLevel();
                Debug.Log("You win");
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, myMovementList[step].transform.position, myMovementSpeed * Time.deltaTime);
                Vector3 distanceToNextPos = myMovementList[step].transform.position - transform.position;



                if (distanceToNextPos.magnitude < 0.05f)
                {
                    if (step <= myMovementList.Count)
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
            
            //for (int i = 0; i < myPathManager.GetPortals.Count; i++)
            //{
            //    float distance = Vector3.Distance(myPathManager.GetPortals[i].GetPos(), transform.position);
            //    if (distance < 0.1f)
            //    {
            //        transform.position = myPathManager.GetPortals[i].GetExit() + myPathManager.GetPortals[i].transform.position;

            //        myMovementList = myPathManager.GetPortals[i].GetMovementList();
            //        step = 1;

            //    }

            //}
        }
    }
}
