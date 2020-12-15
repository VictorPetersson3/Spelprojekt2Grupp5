using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Way Points")]
    [SerializeField]
    Transform[] myPositionsToWalkTo;

    [Header("Enemy Points")]
    [Range(0.1f, 10)]
    [SerializeField]
    float mySpeed = 1;

    [Range(0,5)]
    [SerializeField]
    float myTimeToWait;
    float myTimerToWait;
    float myDistanceToKill = 0.5f;
    int myStep = 0;

    [SerializeField]
    PlayerController myPlayerController;
    [SerializeField]
    Sc_EndGameOver myGameOver;
    [SerializeField]
    PathManager myPathManager;
    GameManager myGameManager;

    Vector3 myOriginalPosition;

    private void Start()
    {
        myPathManager = FindObjectOfType<PathManager>();
        myPlayerController = FindObjectOfType<PlayerController>();
        myGameOver = FindObjectOfType<Sc_EndGameOver>();
        myGameManager = GameManager.globalInstance;
        myOriginalPosition = gameObject.transform.position;
    }


    void Update()
    {
        Movement();
        CheckPlayer();
    }
    void Movement()
    {      
        Vector3 dist = transform.position - myPositionsToWalkTo[myStep].position;
        Quaternion lookAtRotation = Quaternion.LookRotation(myPositionsToWalkTo[myStep].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime / 0.05f);
       
        if (dist.magnitude < .01f) 
        {
            if (myTimerToWait >= myTimeToWait)
            {
                if (myStep + 1 >= myPositionsToWalkTo.Length)
                {
                    myStep = 0;
                }
                else
                {
                    ++myStep;

                }
                myTimerToWait -= myTimerToWait;
            }
            else
            {
                myTimerToWait += Time.deltaTime;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(myPositionsToWalkTo[myStep].position.x, 0.5f, myPositionsToWalkTo[myStep].position.z), mySpeed * Time.deltaTime);

        }
    }
    void CheckPlayer()
    {
        Vector3 distanceToPlayer = myPlayerController.transform.position - transform.position;
        if (distanceToPlayer.magnitude < myDistanceToKill) 
        {
            myGameManager.LoseGame();
            myPathManager.ResetPath();

            // Kill player
        }
    }

    public void ResetEnemy()
    {
        gameObject.transform.position = myOriginalPosition;
    }


}
