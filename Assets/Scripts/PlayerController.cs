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

    [SerializeField]
    GameObject myPlayerModel;
    int indexForNextPortalDistance = 0;
    bool myDontIncreaseIndexFirstTime = true;
    [SerializeField]
    bool myMovementStart = false;
    [SerializeField]
    Animator myAnimator;

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
        //Application.targetFrameRate = 60;
        if (myMovementStart)
        {
            myAnimator.SetBool("isWalking", true);
            if (step > myMovementList.Count - 1)
            {
                myDeathEffect.transform.position = transform.position;
                myDeathEffect.Play();
                myPlayerModel.SetActive(false);
                myMovementStart = false;
                myPathManager.ResetPath();
                myGameManger.SetFinishedLevel();
            } 
            else if (myMovementList[step].IsEndTile)
            {
                myPathManager.ResetPath();
                myGameManger.SetFinishedLevel();
                Debug.Log("You win");
                myMovementStart = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, myMovementList[step].transform.position, myMovementSpeed * Time.deltaTime);
                Vector3 distanceToNextPos = myMovementList[step].transform.position - transform.position;
                Quaternion lookAtRotation = Quaternion.LookRotation(myMovementList[step].transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime / 0.05f);

                if (distanceToNextPos.magnitude < 0.05f)
                {
                    if (step <= myMovementList.Count)
                    {
                        step++;

                    }
                    else
                    {
                        //myDeathEffect.transform.position = transform.position;
                        //gameObject.SetActive(false);
                        //myDeathEffect.Play();
                        myAnimator.SetBool("isWalking", false);
                        myAnimator.SetBool("isOffRoad", true);

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
