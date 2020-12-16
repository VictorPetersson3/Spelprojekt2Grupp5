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
    [SerializeField]
    float myDistanceToKill = 0.65f;
    int myStep = 0;

    [SerializeField]
    LineRenderer myLineRender;

    [SerializeField]
    PlayerController myPlayerController;
    [SerializeField]
    Sc_EndGameOver myGameOver;
    [SerializeField]
    PathManager myPathManager;
    GameManager myGameManager;



    Vector3 myOriginalPosition;
    Vector3 myOriginalDistance;
    Quaternion myOrignalRotation;



    private void Start()
    {
        //myOriginalDistance = transform.position - myPositionsToWalkTo[myStep].position;
        //myOrignalRotation = Quaternion.LookRotation(myPositionsToWalkTo[myStep].position - transform.position);
        Vector3 yPosition = gameObject.transform.position;
        yPosition.y = myPositionsToWalkTo[0].position.y;

        myPathManager = FindObjectOfType<PathManager>();
        myPlayerController = FindObjectOfType<PlayerController>();
        myGameOver = FindObjectOfType<Sc_EndGameOver>();
        gameObject.transform.position = myPositionsToWalkTo[0].position;
        myGameManager = GameManager.globalInstance;
        myOriginalPosition = yPosition;
        gameObject.transform.position = yPosition;

        Invoke("AddTransformsToLineRender", 0.25f);
    }

    void AddTransformsToLineRender()
    {
        LineRenderer temp =  Instantiate(myLineRender, Vector3.zero, Quaternion.identity);
        temp.positionCount = myPositionsToWalkTo.Length + 1;
        for (int i = 0; i < myPositionsToWalkTo.Length; i++)
        {
            temp.SetPosition(i, new Vector3( myPositionsToWalkTo[i].position.x, 0.01f ,myPositionsToWalkTo[i].position.z));
        }
        temp.SetPosition(temp.positionCount - 1, new Vector3(myPositionsToWalkTo[0].position.x, 0.01f, myPositionsToWalkTo[0].position.z));
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
        if (distanceToPlayer.magnitude < myDistanceToKill && myPlayerController.GetWalking() == true) 
        {
            AudioManager.ourInstance.StopWalkingEffect();
            AudioManager.ourInstance.PlayEffect(AudioManager.EEffects.LOSS);
            myPlayerController.GetAnimator().SetBool("isWalking", false);
            myPlayerController.GetAnimator().SetBool("isOffRoad", true);
            myPlayerController.SetStopWalking();
            myPlayerController.GetParticleSystem().transform.position = transform.position;
            myPlayerController.GetParticleSystem().Play();
            myGameManager.LoseGame();

            // Kill player
        }
    }

    public void ResetEnemy()
    {
        gameObject.transform.position = myOriginalPosition;
        myStep = 0;
    }


}
